using System;
using System.Collections.Generic;
using System.IO;
using NSubstitute;
using NSubstitute.Extensions;
using SearchEngine;
using SearchEngine.Classes;
using SearchEngine.Interfaces;
using Xunit;

namespace SearchEngineTests
{
    public class ManagerTests
    {
        private const string DatasetPath = "../../../testDocs";
        private readonly IManager _manager;

        public ManagerTests()
        {
            _manager = new Manager(DatasetPath);
        }

        [Fact]
        void TestFinished_WHEN_dollarSign_EXPECT_true()
        {
            Assert.True(_manager.Finished("$"));
        }

        [Fact]
        public void TestFinished_WHEN_empty_EXPECT_false()
        {
            Assert.False(_manager.Finished(""));
        }

        [Fact]
        public void TestFinished_WHEN_end_EXPECT_false()
        {
            Assert.False(_manager.Finished("end"));
        }

        [Fact]
        public void TestPrintElements()
        {
            var writer = new StringWriter();
            Console.SetOut(writer);
            List<int> testList = new List<int>(new int[] {2, 5, 7});
            IManager.PrintElements(testList);
            writer.Flush();

            Assert.Equal("element2\nelement5\nelement7\n",
                writer.GetStringBuilder().ToString().Replace("\r", ""));
        }

        [Fact]
        public void TestRun_WHEN_firstInputIsDollar_EXPECT_emptyOutput()
        {
            Manager runManager = Substitute.ForPartsOf<Manager>(DatasetPath);
            runManager.Configure().Finished(Arg.Any<string>()).Returns(true);
            var reader = new StringReader("$\n");
            Console.SetIn(reader);
            var writer = new StringWriter();
            Console.SetOut(writer);

            runManager.Run();
            Assert.Equal("Enter something:\n",
                writer.GetStringBuilder().ToString().Replace("\r", ""));
        }

        [Fact]
        public void TestRun_WHEN_secondInputIsDollar_EXPECT_oneTimeOutput()
        {
            Manager runManager = Substitute.ForPartsOf<Manager>(DatasetPath);

            runManager.Finished(Arg.Any<string>()).Returns(false);
            runManager.Finished("$").Returns(true);

            runManager.DoSearch(Arg.Any<string>()).Returns(new HashSet<int>(new int[] {1, 3}));

            var reader = new StringReader("Something\r\n$\r\n");
            Console.SetIn(reader);
            var writer = new StringWriter();
            Console.SetOut(writer);

            runManager.Run();
            Assert.Equal("Enter something:\nelement1\nelement3\nEnter something:\n",
                writer.GetStringBuilder().ToString().Replace("\r", ""));
        }
    }
}