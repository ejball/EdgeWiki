
using EdgeWiki.Parser.Runs;
using MbUnit.Framework;

namespace EdgeWiki.Parser.Tests.Runs
{
	public class TextWikiRunTests
	{
		[Test]
		public void Basic()
		{
			StringSegment text = new StringSegment("basic");
			TextWikiRun run = new TextWikiRun(text);
			Assert.IsTrue(run.Text == text.ToString());
		}
	}
}
