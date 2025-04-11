using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace XlsxHelper
{
    public static class ScriptGenerator
    {
        public static void GenerateClassCode(string className, string[] columnNames, string[] columnTypes, List<List<object>> dataRows)
        {
            StringBuilder code = new StringBuilder();

            //���������ռ�
            code.AppendLine("using System.Collections.Generic;");

            // �������ͷ��
            code.AppendLine($"public class {className}");
            code.AppendLine("{");

            // �����ֶ�
            for (int i = 0; i < columnNames.Length; i++)
            {
                string fieldName = columnNames[i];
                string fieldType = GetCSharpType(columnTypes[i]); // ��Excel����ӳ��ΪC#����
                code.AppendLine($"\tpublic {fieldType} {fieldName};");
            }

            // ���ɾ�̬�б�
            code.AppendLine($"\tpublic static List<{className}> Configs = new List<{className}>();");
            code.AppendLine();

            // ���ɹ��캯��
            code.AppendLine($"\tpublic {className}() {{ }}");

            // ���ɴ������Ĺ��캯��
            code.AppendLine($"\tpublic {className}({string.Join(", ", GenerateConstructorParams(columnNames, columnTypes))})");
            code.AppendLine("\t{");
            for (int i = 0; i < columnNames.Length; i++)
            {
                code.AppendLine($"\t\tthis.{columnNames[i]} = {columnNames[i]};");
            }
            code.AppendLine("\t}");

            // ����Clone����
            code.AppendLine($"\tpublic virtual {className} Clone()");
            code.AppendLine("\t{");
            code.AppendLine($"\t\tvar config = new {className}();");
            code.AppendLine($"\t\tthis.MergeFrom(config);");
            code.AppendLine("\t\treturn config;");
            code.AppendLine("\t}");

            // ����MergeFrom����
            code.AppendLine($"\tpublic virtual {className} MergeFrom({className} source)");
            code.AppendLine("\t{");
            for (int i = 0; i < columnNames.Length; i++)
            {
                code.AppendLine($"\t\tthis.{columnNames[i]} = source.{columnNames[i]};");
            }
            code.AppendLine("\t\treturn this;");
            code.AppendLine("\t}");

            // ���ɾ�̬���캯���Գ�ʼ��Configs
            code.AppendLine($"\tstatic {className}()");
            code.AppendLine("\t{");
            code.AppendLine($"\t\tConfigs = new List<{className}>");
            code.AppendLine("\t\t{");
            foreach (var rowData in dataRows)
            {
                code.AppendLine($"\t\t\tnew {className}");
                code.AppendLine("\t\t\t{");
                for (int i = 0; i < columnNames.Length; i++)
                {
                    var value = rowData[i];
                    // �������ֺͲ������͵Ŀ�ֵ
                    if (columnTypes[i].ToLower() == "int" || columnTypes[i].ToLower() == "double" || columnTypes[i] == "float")
                    {
                        if (value == null)
                        {
                            value = 0; // Ĭ��ֵ
                        }
                    }
                    else if (columnTypes[i].ToLower() == "bool")
                    {
                        if (value == null)
                        {
                            value = false; // Ĭ��ֵ
                        }
                    }
                    if (columnTypes[i].ToLower() == "string")
                        code.AppendLine($"\t\t\t\t{columnNames[i]} = \"{value}\",");
                    else if(columnTypes[i].ToLower() == "double" || columnTypes[i] == "float")
                        code.AppendLine($"\t\t\t\t{columnNames[i]} = {value}f,");
                    else
                        code.AppendLine($"\t\t\t\t{columnNames[i]} = {value},");
                }
                code.AppendLine("\t\t\t},");
            }
            code.AppendLine("\t\t};");
            code.AppendLine("\t}");

            //�������û�ȡ�ӿ�
            code.AppendLine($"\tprotected static Dictionary<int, {className}> TempDictById;");
            code.AppendLine($"\tpublic static {className} GetConfigById(int id)");
            code.AppendLine("\t{");
            code.AppendLine("\t\tif (TempDictById == null)");
            code.AppendLine("\t\t{");
            code.AppendLine("\t\t\tTempDictById = new(Configs.Count);");
            code.AppendLine("\t\t\tfor(var i = 0; i < Configs.Count; i++)");
            code.AppendLine("\t\t\t{");
            code.AppendLine("\t\t\t\tvar c = Configs[i];");
            code.AppendLine("\t\t\t\tTempDictById.Add(c.Id, c);");
            code.AppendLine("\t\t\t}");
            code.AppendLine("\t\t}");
            code.AppendLine("#if UNITY_EDITOR");
            code.AppendLine("\t\tif (TempDictById.Count != Configs.Count)");
            code.AppendLine("\t\t\tUnityEngine.Debug.LogError($\"������ݲ�һ��(ConfigsUnmatched): {TempDictById.Count}!={Configs.Count}\");");
            code.AppendLine("#endif");
            code.AppendLine("\t\treturn TempDictById.GetValueOrDefault(id);");
            code.AppendLine("\t}");

            code.AppendLine("}");

            // �����ɵĴ��뱣�浽�ļ�
            string path = Application.dataPath + "/Scripts/TableDatas/" + className + ".cs";
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, code.ToString());

            // ˢ��Unity��Դ
            AssetDatabase.Refresh();
        }

        private static string GetCSharpType(string excelType)
        {
            // ��Excel����ӳ��ΪC#����
            switch (excelType.ToLower())
            {
                case "int":
                    return "int";
                case "double":
                    return "double";
                case "string":
                    return "string";
                case "bool":
                    return "bool";
                case "float":
                    return "float";
                // ������չ��������ӳ��
                default:
                    return "string"; // Ĭ������
            }
        }

        private static string[] GenerateConstructorParams(string[] columnNames, string[] columnTypes)
        {
            string[] paramsList = new string[columnNames.Length];
            for (int i = 0; i < columnNames.Length; i++)
            {
                paramsList[i] = $"{GetCSharpType(columnTypes[i])} {columnNames[i]}";
            }
            return paramsList;
        }
    }
}