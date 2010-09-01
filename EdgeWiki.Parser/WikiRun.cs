
namespace EdgeWiki.Parser
{
	public abstract class WikiRun
	{
		protected WikiRun(StringSegment sourceText)
		{
			m_sourceText = sourceText;
		}

		public StringSegment SourceText
		{
			get { return m_sourceText; }
		}

		readonly StringSegment m_sourceText;
	}
}
