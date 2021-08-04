using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface IInvertedIndex
    {
        
        List<List<string>> GetTokens();
        Dictionary<string, HashSet<int>> GetDictionary();

        public string Stem(string word);
    }
}