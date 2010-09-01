
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using EdgeWiki.Parser.Runs;

namespace EdgeWiki.Parser
{
	public class WikiRunParser
	{
		public WikiRunParser(IEnumerable<WikiRunFactory> runFactories)
		{
			m_runFactories = runFactories.ToList().AsReadOnly();

			StringBuilder sbPattern = new StringBuilder();
			sbPattern.Append("(");
			for (int runFactoryIndex = 0; runFactoryIndex < m_runFactories.Count; runFactoryIndex++)
			{
				if (runFactoryIndex != 0)
					sbPattern.Append('|');
				sbPattern.Append("(?<z" + runFactoryIndex + ">");
				sbPattern.Append(m_runFactories[runFactoryIndex].Pattern);
				sbPattern.Append(')');
			}
			sbPattern.Append(')');

			m_regex = new Regex(sbPattern.ToString(),
				RegexOptions.Compiled | RegexOptions.CultureInvariant |
				RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);
		}

		public IEnumerable<WikiRun> Parse(StringSegment text)
		{
			WikiRun run;
			while (text.Length != 0 && (run = FindRunInText(text)) != null)
			{
				StringSegment textBefore = run.SourceText.Before().Intersect(text);
				if (textBefore.Length != 0)
					yield return new TextWikiRun(textBefore);

				yield return run;

				text = run.SourceText.After().Intersect(text);
			}

			if (text.Length != 0)
				yield return new TextWikiRun(text);
		}

		private WikiRun FindRunInText(StringSegment text)
		{
			Match match = text.Match(m_regex);
			for (int runFactoryIndex = 0; runFactoryIndex < m_runFactories.Count; runFactoryIndex++)
				if (match.Groups["z" + runFactoryIndex].Success)
					return m_runFactories[runFactoryIndex].Parse(text, match);

			return null;
		}

		readonly ReadOnlyCollection<WikiRunFactory> m_runFactories;
		readonly Regex m_regex;
	}
}
