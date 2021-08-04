using System.Collections.Generic;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class SearchFields : ISearchFields
    {
        public List<string> GetSimpleWords()
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetMinusWords()
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetPlusWords()
        {
            throw new System.NotImplementedException();
        }

        public void AddSimpleWord(string word)
        {
            throw new System.NotImplementedException();
        }

        public void AddPlusWord(string word)
        {
            throw new System.NotImplementedException();
        }

        public void AddMinusWord(string word)
        {
            throw new System.NotImplementedException();
        }
    }
}