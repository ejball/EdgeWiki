
namespace EdgeWiki.Parser.Runs
{
	public class TextWikiRun : WikiRun
	{
		public TextWikiRun(StringSegment sourceText)
			: base(sourceText)
		{
		}

		public string Text
		{
			get { return SourceText.ToString(); }
		}
	}
}
