using UnityEngine;
using UnityEditor;
using System.IO;
using OfficeOpenXml;

public class ReadUnits : MonoBehaviour
{
    [MenuItem("Excel/Read Excel")]
    static void LoadExcel()
    {
        string path = Application.dataPath + "/Excel/Unit.xlsx";
        FileStream fs = new(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        ExcelPackage excel = new(fs);
        var workSheets = excel.Workbook.Worksheets;
        var workSheet = workSheets[1];

        int colCount = workSheet.Dimension.End.Column;
        int rowCount = workSheet.Dimension.End.Row;

        for(int row = 1; row <= rowCount; row++)
        {
            for(int col = 1; col <= colCount; col++) 
            {
                string text = workSheet.Cells[row, col].Text;
                Debug.LogFormat("表格坐标:({0},{1})，表格内容:{2}", row, col, text);
            }
        }
        Debug.Log("complete");
        return;
    }
}