using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SearchEngine.Database;
using SearchEngine.Interfaces;

namespace SearchEngine.Classes
{
    public class SearchEngineCore : ISearchEngineCore
    {
        private const string SearchWordRegex = "([,.;'\"?!@#$%^&:*]*)([+-]?\\w+)([,.;'\"?!@#$%^&:*]*)";
        private readonly IIndexer<string, Document> _indexer;
        private static readonly Regex SearchRegex = new Regex(SearchWordRegex, RegexOptions.Compiled);

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

        public SearchEngineCore(IIndexer<string, Document> indexer)
        {
            this._indexer = indexer;
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
                    fields.AddPlusWord(_indexer.Stem(word.Substring(1)));
                }
                else if (word.StartsWith("-"))
                {
                    fields.AddMinusWord(_indexer.Stem(word.Substring(1)));
                }
                else
                {
                    fields.AddSimpleWord(_indexer.Stem(word));
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
                if (_indexer.GetInvertedIndex().ContainsKey(s))
                    result.ExceptWith(_indexer.GetInvertedIndex().Get(s));
            }
        }

        private void HandlePlusWords(ISearchFields fields, HashSet<Document> result)
        {
            if (!result.Any() && !fields.GetPlusWords().Any())
            {
                foreach (string s in fields.GetSimpleWords())
                    result.UnionWith(_indexer.GetInvertedIndex().Get(s));
                return;
            }

            foreach (string s in fields.GetPlusWords())
            {
                if (_indexer.GetInvertedIndex().ContainsKey(s))
                    result.UnionWith(_indexer.GetInvertedIndex().Get(s));
            }
        }

        private void HandleSimpleWords(ISearchFields fields, HashSet<Document> result)
        {
            foreach (string s in fields.GetSimpleWords())
            {
                if (!_indexer.GetInvertedIndex().ContainsKey(s))
                {
                    result.Clear();
                    return;
                }

                result.IntersectWith(_indexer.GetInvertedIndex().Get(s));
            }
        }
    }
}