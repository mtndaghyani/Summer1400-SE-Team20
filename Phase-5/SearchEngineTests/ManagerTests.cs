using System;
using System.Collections.Generic;
using System.IO;
using SearchEngine;
using SearchEngine.Interfaces;
using Xunit;

namespace SearchEngineTests
{
    public class ManagerTests
    {
        private const string DatasetPath = "src/test/testDocs";
        private readonly IManager _manager;

        public ManagerTests()
        {
            _manager = new Manager();
        }

        [Fact]
        void TestFinished_WHEN_dollarSign_EXPECT_true()
        {
            Assert.True(_manager.Finished("$"));
        }
        
        [Fact]
        public void TestFinished_WHEN_empty_EXPECT_false(){
            Assert.False(_manager.Finished(""));
        }
        
        [Fact]
        public void TestFinished_WHEN_end_EXPECT_false(){
            Assert.False(_manager.Finished("end"));
        }
        
        [Fact]
        public void TestPrintElements(){
            var writer = new StringWriter();
            Console.SetOut(writer);
            List<int> testList = new List<int>(new int[]{2, 5, 7});
            _manager.PrintElements(testList);
            writer.Flush();
            
            Assert.Equal("element2\nelement5\nelement7\n",
                writer.GetStringBuilder().ToString().Replace("\r", ""));
        }
    }
}