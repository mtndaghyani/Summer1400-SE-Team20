using System.Collections.Generic;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class SearchFields : ISearchFields
    {
        private List<string> _simpleWords;
        private List<string> _minusWords;
        private List<string> _plusWords;

        public SearchFields()
        {
            this._simpleWords = new List<string>();
            this._minusWords = new List<string>();
            this._plusWords = new List<string>();
        }

        public List<string> GetSimpleWords()
        {
            return _simpleWords;
        }

        public List<string> GetMinusWords()
        {
            return _minusWords;
        }

        public List<string> GetPlusWords()
        {
            return _plusWords;
        }

        public void AddSimpleWord(string word)
        {
            _simpleWords.Add(word);
        }

        public void AddPlusWord(string word)
        {
            _plusWords.Add(word);
        }

        public void AddMinusWord(string word)
        {
            _minusWords.Add(word);
        }
    }
}