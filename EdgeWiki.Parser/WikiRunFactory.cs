
using System;
using System.Text.RegularExpressions;

namespace EdgeWiki.Parser
{
	public sealed class WikiRunFactory
	{
		public WikiRunFactory(string pattern, Func<StringSegment, Match, WikiRun> parse)
		{
			m_pattern = pattern;
			m_parse = parse;
		}

		public string Pattern
		{
			get { return m_pattern; }
		}

		public WikiRun Parse(StringSegment text, Match match)
		{
			return m_parse(text, match);
		}

		readonly string m_pattern;
		readonly Func<StringSegment, Match, WikiRun> m_parse;
	}
}
