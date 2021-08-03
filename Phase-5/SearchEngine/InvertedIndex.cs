using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class InvertedIndex : IInvertedIndex
    {
        private IReader _reader;
        private Dictionary<string, HashSet<int>> _dictionary;

        public InvertedIndex(IReader reader)
        {
            _reader = reader;
            _dictionary = new Dictionary<string, HashSet<int>>();
            SetUpDictionary();
        }

        public List<List<string>> GetTokens()
        {
            var contents = _reader.Read();

            return contents.Select(content => new List<string>(content.Split(" "))
                    .Select(word => word.ToLower()))
                .Select(a => a.ToList()).ToList();
        }

        public Dictionary<string, HashSet<int>> GetDictionary()
        {
            return _dictionary;
        }

        private void SetUpDictionary()
        {
            var tokens = GetTokens();
            var counter = 1;
            foreach (var token in tokens.SelectMany(tokenList => tokenList))
            {
                if (_dictionary.ContainsKey(token))
                    _dictionary[token].Add(counter);
                else
                    _dictionary.Add(token, new HashSet<int>() {counter});
                counter += 1;
            }
        }
    }
}