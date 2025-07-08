using System.Collections.Generic;
using HangManV01.Models;

namespace HangManV01.Interfaces
{
    
    public interface IExcelService
    {
        WordModel GetRandomWord();
        void AddWord(string word, string hint);
        void DeleteWord(string word);
        List<string> GetAllWords();
    }
}