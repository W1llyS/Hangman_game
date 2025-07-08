using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using HangManV01.Models;

namespace HangManV01.Services
{
    public interface IExcelService
    {
        WordModel GetRandomWord();
        void AddWord(string word, string hint);
        void DeleteWord(string word);
        List<string> GetAllWords();
    }

    public class ExcelService : IExcelService
    {
        private readonly string _excelFilePath;

        public ExcelService()
        {
            _excelFilePath = Path.Combine(Environment.CurrentDirectory, "Data - Excel", "Words.xlsx");
        }

        public WordModel GetRandomWord()
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(_excelFilePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            Random random = new Random();
            int row = random.Next(1, rowCount + 1);

            var wordModel = new WordModel
            {
                Word = xlRange.Cells[row, 1].Value2.ToString(),
                Hint = xlRange.Cells[row, 2].Value2.ToString()
            };

            // Create display word with underscores
            wordModel.DisplayWord = string.Join(" ", new string('_', wordModel.Word.Length).ToCharArray());

            // Clean up Excel objects
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close(false);
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return wordModel;
        }

        public void AddWord(string word, string hint)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkbook = null;
            Excel.Worksheet excelWorksheet = null;

            try
            {
                if (File.Exists(_excelFilePath))
                {
                    excelWorkbook = excelApp.Workbooks.Open(_excelFilePath);
                }
                else
                {
                    excelWorkbook = excelApp.Workbooks.Add();
                    excelWorkbook.SaveAs(_excelFilePath);
                }

                excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];
                int rowIndex = 1;

                while (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2 != null)
                {
                    rowIndex++;
                }

                excelWorksheet.Cells[rowIndex, 1] = word;
                excelWorksheet.Cells[rowIndex, 2] = hint;
                excelWorkbook.Save();
            }
            finally
            {
                if (excelWorkbook != null) excelWorkbook.Close();
                excelApp.Quit();
                Marshal.ReleaseComObject(excelWorksheet);
                Marshal.ReleaseComObject(excelWorkbook);
                Marshal.ReleaseComObject(excelApp);
            }
        }

        public void DeleteWord(string word)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkbook = null;
            Excel.Worksheet excelWorksheet = null;

            try
            {
                if (File.Exists(_excelFilePath))
                {
                    excelWorkbook = excelApp.Workbooks.Open(_excelFilePath);
                    excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                    int rowIndex = 1;
                    bool wordFound = false;

                    while (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2 != null)
                    {
                        if (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2.ToString().Trim() == word.Trim())
                        {
                            wordFound = true;
                            break;
                        }
                        rowIndex++;
                    }

                    if (wordFound)
                    {
                        Excel.Range rowRange = (Excel.Range)excelWorksheet.Rows[rowIndex];
                        rowRange.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        excelWorkbook.Save();
                    }
                }
            }
            finally
            {
                if (excelWorkbook != null) excelWorkbook.Close(false);
                excelApp.Quit();
                Marshal.ReleaseComObject(excelWorksheet);
                Marshal.ReleaseComObject(excelWorkbook);
                Marshal.ReleaseComObject(excelApp);
            }
        }

        public List<string> GetAllWords()
        {
            var words = new List<string>();
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkbook = null;
            Excel.Worksheet excelWorksheet = null;

            try
            {
                if (File.Exists(_excelFilePath))
                {
                    excelWorkbook = excelApp.Workbooks.Open(_excelFilePath);
                    excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                    int rowIndex = 1;
                    while (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2 != null)
                    {
                        string word = ((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2.ToString();
                        string hint = ((Excel.Range)excelWorksheet.Cells[rowIndex, 2]).Value2.ToString();
                        words.Add(word + " + " + hint);
                        rowIndex++;
                    }
                }
            }
            finally
            {
                if (excelWorkbook != null) excelWorkbook.Close(false);
                excelApp.Quit();
                Marshal.ReleaseComObject(excelWorksheet);
                Marshal.ReleaseComObject(excelWorkbook);
                Marshal.ReleaseComObject(excelApp);
            }

            return words;
        }
    }
}