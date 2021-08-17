using System.Collections.Generic;
using SearchEngine.Classes.IO.Database.Models;
using Xunit;

namespace SearchEngineTests
{
    public class Utils
    {
        public static void AssertEqualDocumentEnumerable(ICollection<int> expected, HashSet<Document> result)
        {
            Assert.Equal(expected.Count, result.Count);
            foreach (Document doc in result)
            {
                if (!expected.Contains(doc.DocumentNumber))
                {
                    Assert.True(false, "Hashset does not contain expected value.");
                }
            }
        }
    }
}