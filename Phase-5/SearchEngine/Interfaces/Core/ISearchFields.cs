using System.Collections.Generic;

namespace SearchEngine.Interfaces.Core
{
    public interface ISearchFields
    {
        List<string> GetSimpleWords();
        List<string> GetMinusWords();
        List<string> GetPlusWords();
        void AddSimpleWord(string word);
        void AddPlusWord(string word);
        void AddMinusWord(string word);
    }
}