﻿using System.Collections.Generic;
using System.Linq;
using SearchEngine.Interfaces;

namespace SearchEngine
{
    public class InvertedIndex : IInvertedIndex
    {
        private IReader _reader;
        private IWordProcessor _wordProcessor;
        private Dictionary<string, HashSet<int>> _dictionary;

        public InvertedIndex(IReader reader, IWordProcessor wordProcessor)
        {
            _reader = reader;
            _wordProcessor = wordProcessor;
            _dictionary = new Dictionary<string, HashSet<int>>();
            SetUpDictionary();
        }

        public List<List<string>> GetTokens()
        {
            var contents = _reader.Read();

            return contents.Select(content => new List<string>(content.Trim().Split(" "))
                    .Select(Stem))
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

        public string Stem(string word)
        {
            return _wordProcessor.ProcessWord(word);
        }
    }
}