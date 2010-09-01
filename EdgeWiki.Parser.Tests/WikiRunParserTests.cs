
using System.Collections.Generic;
using System.Linq;
using EdgeWiki.Parser.Runs;
using MbUnit.Framework;

namespace EdgeWiki.Parser.Tests
{
	public class WikiRunParserTests
	{
		[Test]
		public void NoRunFactories()
		{
			WikiRunParser parser = new WikiRunParser(Enumerable.Empty<WikiRunFactory>());
			StringSegment text = new StringSegment("we're");
			List<WikiRun> runs = parser.Parse(text).ToList();
			Assert.AreEqual(1, runs.Count);
			Assert.AreEqual("we're", ((TextWikiRun) runs[0]).Text);
		}
	}
}
