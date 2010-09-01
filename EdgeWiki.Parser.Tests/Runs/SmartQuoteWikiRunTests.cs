
using System.Collections.Generic;
using System.Linq;
using EdgeWiki.Parser.Runs;
using MbUnit.Framework;

namespace EdgeWiki.Parser.Tests.Runs
{
	public class SmartQuoteWikiRunTests
	{
		[SetUp]
		public void SetUp()
		{
			m_parser = new WikiRunParser(new[] { SmartQuoteWikiRun.CreateFactory() });
		}

		[Test]
		public void Contraction()
		{
			StringSegment text = new StringSegment("we're");
			List<WikiRun> runs = m_parser.Parse(text).ToList();
			int runIndex = 0;
			Assert.AreEqual("we", ((TextWikiRun) runs[runIndex++]).Text);
			Assert.AreEqual(SmartQuoteWikiRun.RightSingleQuote, ((SmartQuoteWikiRun) runs[runIndex++]).SmartQuote);
			Assert.AreEqual("re", ((TextWikiRun) runs[runIndex++]).Text);
			Assert.AreEqual(runIndex, runs.Count);
		}

		[Test]
		public void Complex()
		{
			StringSegment text = new StringSegment(@"He read, ""She said, 'Let 'em eat cake!'""");
			List<WikiRun> runs = m_parser.Parse(text).ToList();
			int runIndex = 0;
			Assert.AreEqual("He read, ", ((TextWikiRun) runs[runIndex++]).Text);
			Assert.AreEqual(SmartQuoteWikiRun.LeftDoubleQuote, ((SmartQuoteWikiRun) runs[runIndex++]).SmartQuote);
			Assert.AreEqual("She said, ", ((TextWikiRun) runs[runIndex++]).Text);
			Assert.AreEqual(SmartQuoteWikiRun.LeftSingleQuote, ((SmartQuoteWikiRun) runs[runIndex++]).SmartQuote);
			Assert.AreEqual("Let ", ((TextWikiRun) runs[runIndex++]).Text);
			Assert.AreEqual(SmartQuoteWikiRun.LeftSingleQuote, ((SmartQuoteWikiRun) runs[runIndex++]).SmartQuote); // would be RightSingleQuote in a perfect world
			Assert.AreEqual("em eat cake!", ((TextWikiRun) runs[runIndex++]).Text);
			Assert.AreEqual(SmartQuoteWikiRun.RightSingleQuote, ((SmartQuoteWikiRun) runs[runIndex++]).SmartQuote);
			Assert.AreEqual(SmartQuoteWikiRun.RightDoubleQuote, ((SmartQuoteWikiRun) runs[runIndex++]).SmartQuote);
			Assert.AreEqual(runIndex, runs.Count);
		}

		WikiRunParser m_parser;
	}
}
