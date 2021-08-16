using System;
using System.Collections.Generic;
using System.Linq;
using SearchEngine.Classes.IO.Database.Models;
using SearchEngine.Interfaces.Indexers;
using SearchEngine.Interfaces.IO;

namespace SearchEngine.Classes.Indexers
{
    public class Indexer : IIndexer <string, Document>
    {
        private const string Separator = " ";
        private IReader _reader;
        private IWordProcessor _wordProcessor;
        private IInvertedIndex<string, Document> _invertedIndex;

        public Indexer(IReader reader, IWordProcessor wordProcessor, IInvertedIndex<string, Document> invertedIndex)
        {
            _reader = reader;
            _wordProcessor = wordProcessor;
            _invertedIndex = invertedIndex;
            
        }

        public List<List<string>> GetDocumentsTokens()
        {
            var contents = _reader.Read();

            return contents.Select(TokenizeContent).ToList();
        }

        private List<string> TokenizeContent(string content)
        {
            var words = content.Trim().Split(Separator);
            return new List<string>(words.Select(_wordProcessor.ProcessWord)).ToList();
        }

        public IInvertedIndex<string, Document> GetInvertedIndex()
        {
            return _invertedIndex;
        }

        public void SetUpInvertedIndex()
        {
            Console.WriteLine("read start");
            var tokens = GetDocumentsTokens();
            Console.WriteLine("read end");
            Console.WriteLine("index start");

            var documentCounter = 1;
            foreach (var tokenList in tokens)
            {
                Document document = new Document() {DocumentNumber = documentCounter};
                foreach (var token in tokenList)
                {
                    _invertedIndex.Add(token, document);
                }

                documentCounter += 1;
            }
            Console.WriteLine("index end");

        }

        public string Stem(string word)
        {
            return _wordProcessor.ProcessWord(word);
        }
    }
}