using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace XlsxHelper
{
    public static class ExcelReader
    {
        public static List<(string className, string[] columnNames, string[] columnTypes, List<List<object>> dataRows)> ReadExcel(string filePath)
        {
            using (FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (ExcelPackage package = new ExcelPackage(fs))
            {
                List<(string className, string[] columnNames, string[] columnTypes, List<List<object>> dataRows)> sheets = new();

                foreach(var sheet in package.Workbook.Worksheets) 
                {
                    ExcelWorksheet worksheet = sheet; // 获取第一个工作表
                    int rowCount = worksheet.Dimension.End.Row;
                    int colCount = worksheet.Dimension.End.Column;

                    string className = worksheet.Name; // 使用工作表名称作为类名
                    string[] columnNames = new string[colCount];
                    string[] columnTypes = new string[colCount];
                    List<List<object>> dataRows = new List<List<object>>();

                    // 提取列名（第一行）
                    for (int col = 1; col <= colCount; col++)
                    {
                        columnNames[col - 1] = worksheet.Cells[1, col].Text;
                        StringBuilder sb = new(columnNames[col - 1]);
                        sb[0] = char.ToUpper(sb[0]);
                        columnNames[col - 1] = sb.ToString();
                    }

                    // 提取类型信息（第二行）
                    for (int col = 1; col <= colCount; col++)
                    {
                        columnTypes[col - 1] = worksheet.Cells[2, col].Text;
                    }

                    // 读取数据行
                    for (int row = 3; row <= rowCount; row++)
                    {
                        List<object> rowData = new List<object>();
                        for (int col = 1; col <= colCount; col++)
                        {
                            // 处理空值
                            if (worksheet.Cells[row, col].Value == null)
                            {
                                rowData.Add("null");
                            }
                            else
                            {
                                Debug.Log($"row:{row}, col:{col}, value:{worksheet.Cells[row, col].Value}");
                                rowData.Add(worksheet.Cells[row, col].Value);
                            }
                        }
                        dataRows.Add(rowData);
                    }

                    sheets.Add((className, columnNames, columnTypes, dataRows));
                }


                return sheets;
            }
        }
    }
}