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
        private IInvertedIndex<string, int> _invertedIndex;

        public Indexer(IReader reader, IWordProcessor wordProcessor, IInvertedIndex<string, int> invertedIndex)
        {
            _reader = reader;
            _wordProcessor = wordProcessor;
            _invertedIndex = invertedIndex;
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

        public IInvertedIndex<string, int> GetInvertedIndex()
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
                    _invertedIndex.Add(token, documentCounter);
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