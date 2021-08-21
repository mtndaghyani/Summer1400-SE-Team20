using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SearchEngine.Classes.IO.Database.Models;
using SearchEngine.Interfaces.Core;
using SearchEngine.Interfaces.Indexers;

namespace SearchEngine.Classes.Core
{
    public class SearchEngineCore : ISearchEngineCore
    {
        private const string SearchWordRegex = "([,.;'\"?!@#$%^&:*]*)([+-]?\\w+)([,.;'\"?!@#$%^&:*]*)";
        private readonly IInvertedIndex<string, Document> _invertedIndex;
        private readonly IWordProcessor _wordProcessor;
        private static readonly Regex SearchRegex = new Regex(SearchWordRegex, RegexOptions.Compiled);

        public SearchEngineCore(IWordProcessor wordProcessor, IInvertedIndex<string, Document> invertedIndex)
        {
            _wordProcessor = wordProcessor;
            _invertedIndex = invertedIndex;
        }

        private static MatchCollection GetMatches(string statement)
        {
            return SearchRegex.Matches(statement);
        }

        private static string GetRegexSecondGroupMatched(Match match)
        {
            var groups = match.Groups;
            var word = groups[2].Value;
            return word;
        }

        public HashSet<Document> Search(string statement)
        {
            var fields = MakeSearchFields(statement);
            return AdvancedSearch(fields);
        }

        private SearchFields MakeSearchFields(string statement)
        {
            var fields = new SearchFields();
            var matches = GetMatches(statement);
            foreach (Match match in matches)
            {
                var word = GetRegexSecondGroupMatched(match);
                if (word.StartsWith("+"))
                {
                    fields.AddPlusWord(_wordProcessor.ProcessWord(word.Substring(1)));
                }
                else if (word.StartsWith("-"))
                {
                    fields.AddMinusWord(_wordProcessor.ProcessWord(word.Substring(1)));
                }
                else
                {
                    fields.AddSimpleWord(_wordProcessor.ProcessWord(word));
                }
            }

            return fields;
        }


        private HashSet<Document> AdvancedSearch(SearchFields fields)
        {
            var result = new HashSet<Document>();
            HandlePlusWords(fields, result);
            HandleSimpleWords(fields, result);
            HandleMinusWords(fields, result);

            return result;
        }

        private void HandleMinusWords(ISearchFields fields, HashSet<Document> result)
        {
            foreach (string s in fields.GetMinusWords())
            {
                if (_invertedIndex.ContainsKey(s))
                    result.ExceptWith(_invertedIndex.Get(s));
            }
        }

        private void HandlePlusWords(ISearchFields fields, HashSet<Document> result)
        {
            if (!result.Any() && !fields.GetPlusWords().Any())
            {
                foreach (string s in fields.GetSimpleWords())
                    result.UnionWith(_invertedIndex.Get(s));
                return;
            }

            foreach (string s in fields.GetPlusWords())
            {
                if (_invertedIndex.ContainsKey(s))
                    result.UnionWith(_invertedIndex.Get(s));
            }
        }

        private void HandleSimpleWords(ISearchFields fields, HashSet<Document> result)
        {
            foreach (string s in fields.GetSimpleWords())
            {
                if (!_invertedIndex.ContainsKey(s))
                {
                    result.Clear();
                    return;
                }

                result.IntersectWith(_invertedIndex.Get(s));
            }
        }
    }
}