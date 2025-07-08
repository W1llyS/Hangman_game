using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows;
using System.Windows.Controls;
using System.Text;

namespace HangManV01.Views
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
            LoadWordsIntoTextBox();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Word.Text))
            {
                MessageBox.Show("Please enter a word in the 'Word' field.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Hint.Text))
            {
                MessageBox.Show("Please enter a hint in the 'Hint' field.");
                return;
            }

            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel is not installed correctly!");
                return;
            }

            string workbookPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data - Excel", "Words.xlsx");
            Excel.Workbook excelWorkbook = null;
            Excel.Worksheet excelWorksheet = null;

            try
            {
                if (System.IO.File.Exists(workbookPath))
                {
                    excelWorkbook = excelApp.Workbooks.Open(workbookPath);
                }
                else
                {
                    excelWorkbook = excelApp.Workbooks.Add();
                    excelWorkbook.SaveAs(workbookPath);
                }

                excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];
                int rowIndex = 1;

                while (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2 != null)
                {
                    rowIndex++;
                }

                excelWorksheet.Cells[rowIndex, 1] = Word.Text;
                excelWorksheet.Cells[rowIndex, 2] = Hint.Text;

                excelWorkbook.Save();

                LoadWordsIntoTextBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel is not installed correctly!");
                return;
            }

            string workbookPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data - Excel", "Words.xlsx");
            Excel.Workbook excelWorkbook = null;
            Excel.Worksheet excelWorksheet = null;

            try
            {
                if (System.IO.File.Exists(workbookPath))
                {
                    excelWorkbook = excelApp.Workbooks.Open(workbookPath);
                    excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                    int rowIndex = 1;
                    bool wordFound = false;

                    while (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2 != null)
                    {
                        if (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2.ToString().Trim() == Delete.Text.Trim())
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
                    else
                    {
                        MessageBox.Show("Word not found in Excel.");
                    }
                }
                else
                {
                    MessageBox.Show("Excel file does not exist!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (excelWorkbook != null) excelWorkbook.Close(false, Type.Missing, Type.Missing);
                LoadWordsIntoTextBox();
                excelApp.Quit();
                Marshal.ReleaseComObject(excelWorksheet);
                Marshal.ReleaseComObject(excelWorkbook);
                Marshal.ReleaseComObject(excelApp);
            }
        }

        private void LoadWordsIntoTextBox()
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel is not installed correctly!");
                return;
            }

            string workbookPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data - Excel", "Words.xlsx");
            Excel.Workbook excelWorkbook = null;
            Excel.Worksheet excelWorksheet = null;

            try
            {
                if (System.IO.File.Exists(workbookPath))
                {
                    excelWorkbook = excelApp.Workbooks.Open(workbookPath);
                    excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                    int rowIndex = 1;
                    StringBuilder sb = new StringBuilder();

                    while (((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2 != null)
                    {
                        string word = ((Excel.Range)excelWorksheet.Cells[rowIndex, 1]).Value2.ToString();
                        string hint = ((Excel.Range)excelWorksheet.Cells[rowIndex, 2]).Value2.ToString();

                        sb.AppendLine(word + " + " + hint);
                        rowIndex++;
                    }

                    TextBoxWordsPreview.Text = sb.ToString();
                }
                else
                {
                    MessageBox.Show("Excel file does not exist!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                if (excelWorkbook != null) excelWorkbook.Close(false, Type.Missing, Type.Missing);
                excelApp.Quit();
                Marshal.ReleaseComObject(excelWorksheet);
                Marshal.ReleaseComObject(excelWorkbook);
                Marshal.ReleaseComObject(excelApp);
            }
        }

        private void Delete_GotFocus(object sender, RoutedEventArgs e)
        {
            Delete.Text = string.Empty;
        }

        private void Word_GotFocus(object sender, RoutedEventArgs e)
        {
            Word.Text = string.Empty;
        }

        private void Hint_GotFocus(object sender, RoutedEventArgs e)
        {
            Hint.Text = string.Empty;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            if (this.Owner != null && this.Owner is Menu)
            {
                this.Owner.Show();
            }
        }
    }
}