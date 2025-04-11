using UnityEditor;
using System;
using UnityEngine;
using XlsxHelper;
using System.IO;

public class ExcelToClassGenerator : EditorWindow
{
    private string excelPath = "";

    [MenuItem("Tools/Generate Classes from Excel")]
    public static void ShowWindow()
    {
        GetWindow<ExcelToClassGenerator>("Excel to Class Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Excel to Class Generator", EditorStyles.boldLabel);
        excelPath = EditorGUILayout.TextField("Excel Path", excelPath);

        if (GUILayout.Button("Browse"))
        {
            excelPath = EditorUtility.OpenFilePanel("Select Excel File", "", "xlsx");
        }

        if (GUILayout.Button("Generate Classes"))
        {
            if (string.IsNullOrEmpty(excelPath))
            {
                EditorUtility.DisplayDialog("Error", "Please select an Excel file.", "OK");
                return;
            }

            GenerateClassesFromExcel(excelPath);
            Close();
            EditorUtility.DisplayDialog("Success", "Classes generated successfully!", "OK");
        }

        if (GUILayout.Button("Generate All Classes"))
        {
            string excelFolder = Application.dataPath + "/Excel/";
            if (!Directory.Exists(excelFolder))
            {
                EditorUtility.DisplayDialog("Error", $"The folder '{excelFolder}' does not exist.", "OK");
                return;
            }

            string[] excelFiles = Directory.GetFiles(excelFolder, "*.xlsx", SearchOption.AllDirectories);
            if (excelFiles.Length == 0)
            {
                EditorUtility.DisplayDialog("Error", "No Excel files found in the specified folder.", "OK");
                return;
            }

            int filesProcessed = 0;
            foreach (string filePath in excelFiles)
            {
                try
                {
                    var (className, columnNames, columnTypes, dataRows) = ExcelReader.ReadExcel(filePath);
                    ScriptGenerator.GenerateClassCode(className, columnNames, columnTypes, dataRows);
                    filesProcessed++;
                }
                catch (Exception ex)
                {
                    EditorUtility.DisplayDialog("Error", $"Failed to process file {filePath}: {ex.Message}", "OK");
                }
            }

            Close();
            AssetDatabase.Refresh(); // Ë¢ÐÂUnity×ÊÔ´
            EditorUtility.DisplayDialog("Success", $"Successfully generated classes for {filesProcessed} files.", "OK");
        }
    }

    private void GenerateClassesFromExcel(string filePath)
    {
        try
        {
            var (className, columnNames, columnTypes, dataRows) = ExcelReader.ReadExcel(filePath);
            ScriptGenerator.GenerateClassCode(className, columnNames, columnTypes, dataRows);
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    [MenuItem("Tools/Out table data(for test)")]
    public static void OutTableData()
    {
        try
        {
            var (className, columnNames, columnTypes, dataRows) = ExcelReader.ReadExcel(Application.dataPath+"/Excel/Unit.xlsx");
            //Debug.Log(className);
            //foreach (var column in columnNames) 
            //{
            //    Debug.Log(column);
            //}
            //foreach (var column in columnTypes)
            //{
            //    Debug.Log($"{column}");
            //}
            //foreach(var column in dataRows)
            //{
            //    foreach(var data in  column)
            //    {
            //        Debug.Log(data);
            //    }
            //}
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }
}