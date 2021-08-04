using System.Collections.Generic;

namespace SearchEngine.Interfaces
{
    public interface ISearchFields
    {
        public List<string> GetSimpleWords();
        public List<string> GetMinusWords();
        public List<string> GetPlusWords();
        public void AddSimpleWord(string word);
        public void AddPlusWord(string word);
        public void AddMinusWord(string word);
    }
}