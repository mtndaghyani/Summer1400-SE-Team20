using System.Collections.Generic;
using System.Linq;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class Indexer : IIndexer
    {
        private const string Separator = " ";
        private IReader _reader;
        private IWordProcessor _wordProcessor;
        private Dictionary<string, HashSet<int>> _invertedIndex;

        public Indexer(IReader reader, IWordProcessor wordProcessor)
        {
            _reader = reader;
            _wordProcessor = wordProcessor;
            _invertedIndex = new Dictionary<string, HashSet<int>>();
            SetUpInvertedIndex();
        }

        public List<List<string>> GetDocumentsTokens()
        {
            var contents = _reader.Read();

            return contents.Select(TokenizeContent).ToList();
        }

        private List<string> TokenizeContent(string content)
        {
            var words = content.Trim().Split(Separator);
            return new List<string>(words.Select(Stem)).ToList();
        }

        public Dictionary<string, HashSet<int>> GetInvertedIndex()
        {
            return _invertedIndex;
        }

        private void SetUpInvertedIndex()
        {
            var tokens = GetDocumentsTokens();
            var documentCounter = 1;
            foreach (var tokenList in tokens)
            {
                foreach (var token in tokenList)
                {
                    if (_invertedIndex.ContainsKey(token))
                        _invertedIndex[token].Add(documentCounter);
                    else
                        _invertedIndex.Add(token, new HashSet<int>() {documentCounter});
                }

                documentCounter += 1;
            }
        }

        public string Stem(string word)
        {
            return _wordProcessor.ProcessWord(word);
        }
    }
}