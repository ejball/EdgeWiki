
namespace EdgeWiki.Parser.Runs
{
	public class SmartQuoteWikiRun : WikiRun
	{
		public SmartQuoteWikiRun(StringSegment sourceText, string smartQuote)
			: base(sourceText)
		{
			m_smartQuote = smartQuote;
		}

		public static WikiRunFactory CreateFactory()
		{
			// left quote follows nothing, space, or open punct, and is not followed by nothing, space, or close punct
			return new WikiRunFactory(@"
				(?<left>
					(?<![^\s\p{Ps}\p{Pi}])
					['""]
					(?=[^\s\p{Pe}\p{Pf}]))
				|
				['""]
				",
				(text, match) =>
				{
					StringSegment sourceText = text.Redirect(match);
					char straightQuoteCharacter = sourceText[0];
					bool isLeftQuote = match.Groups["left"].Success;
					string smartQuote = straightQuoteCharacter == '"' ? (isLeftQuote ? LeftDoubleQuote : RightDoubleQuote) : (isLeftQuote ? LeftSingleQuote : RightSingleQuote);
					return new SmartQuoteWikiRun(sourceText, smartQuote);
				});
		}

		public string SmartQuote
		{
			get { return m_smartQuote; }
		}

		public const string LeftSingleQuote = "\u2018";

		public const string RightSingleQuote = "\u2019";

		public const string LeftDoubleQuote = "\u201c";

		public const string RightDoubleQuote = "\u201d";

		readonly string m_smartQuote;
	}
}
