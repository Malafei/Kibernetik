﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.SS.UserModel;
using NPOI.Util;
using NPOI.XSSF.UserModel;

namespace Kibernetik.Helper
{
    public class Helper<T> where T : class, new()
    {
        /// <summary>
        /// Экспорт Excel с шаблоном
        /// </summary>
        /// <param name = "list"> Данные </ param>
        /// <param name = "path"> путь к шаблону </ param>
        /// <param name = "index"> вставить из нескольких строк </ param>
        /// <returns></returns>
        public static byte[] TemplateOutput(List<T> list, string path, int index)
        {
            if (list.Count() == 0)
            {
                return new byte[] { };
            }
            using (MemoryStream ms = new MemoryStream())
            {
                var entityProperties = list.FirstOrDefault().GetType().GetProperties();
                string extension = Path.GetExtension(path);
                FileStream fs = File.OpenRead(path);
                IWorkbook workbook = null;
                if (extension.Equals(".xls"))
                {
                    workbook = new HSSFWorkbook(fs);
                }
                else
                {
                    workbook = new XSSFWorkbook(fs);
                }
                fs.Close();
                ISheet sheet = workbook.GetSheetAt(0);
                IRow rows = null;
                int i = 0;
                foreach (var dbitem in list)
                {
                    rows = sheet.CreateRow(i++ + index);
                    for (int j = 0; j < entityProperties.Length; j++)
                    {
                        var prop = entityProperties[j];
                        var value = prop.GetValue(dbitem)?.ToString();
                        rows.CreateCell(j).SetCellValue(value ?? "");
                    }
                }
                workbook.Write(ms);
                return ms.GetBuffer();
            }
        }
    }


    //public class DataTableRenderToExcelHelper
    //{
    //    HSSFWorkbook hssfworkbook;
    //    public DataTable ImportExcelFile(string filePath)
    //    {

    //        try
    //        {
    //            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
    //            {
    //                hssfworkbook = new HSSFWorkbook(file);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            throw e;
    //        }


    //        NPOI.SS.UserModel.Sheet sheet = hssfworkbook.GetSheetAt(0);
    //        System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

    //        DataTable dt = new DataTable();
    //        for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
    //        {
    //            dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
    //        }

    //        while (rows.MoveNext())
    //        {
    //            HSSFRow row = (HSSFRow)rows.Current;
    //            DataRow dr = dt.NewRow();

    //            for (int i = 0; i < row.LastCellNum; i++)
    //            {
    //                NPOI.SS.UserModel.Cell cell = row.GetCell(i);


    //                if (cell == null)
    //                {
    //                    dr[i] = null;
    //                }
    //                else
    //                {
    //                    dr[i] = cell.ToString();
    //                }
    //            }
    //            dt.Rows.Add(dr);
    //        }
    //        return dt;
    //    }


        //public static Stream RenderDataTableToExcel(DataTable SourceTable)
        //{
        //    HSSFWorkbook workbook = new HSSFWorkbook();
        //    MemoryStream ms = new MemoryStream();
        //    HSSFSheet sheet = workbook.CreateSheet();
        //    HSSFRow headerRow = sheet.CreateRow(0);

        //    // handling header.
        //    foreach (DataColumn column in SourceTable.Columns)
        //        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

        //    // handling value.
        //    int rowIndex = 1;

        //    foreach (DataRow row in SourceTable.Rows)
        //    {
        //        HSSFRow dataRow = sheet.CreateRow(rowIndex);

        //        foreach (DataColumn column in SourceTable.Columns)
        //        {
        //            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
        //        }

        //        rowIndex++;
        //    }

        //    workbook.Write(ms);
        //    ms.Flush();
        //    ms.Position = 0;

        //    sheet = null;
        //    headerRow = null;
        //    workbook = null;

        //    return ms;
        //}

        //public static void RenderDataTableToExcel(DataTable SourceTable, string FileName)
        //{
        //    MemoryStream ms = RenderDataTableToExcel(SourceTable) as MemoryStream;
        //    FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write);
        //    byte[] data = ms.ToArray();

        //    fs.Write(data, 0, data.Length);
        //    fs.Flush();
        //    fs.Close();

        //    data = null;
        //    ms = null;
        //    fs = null;
        //}

        //public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, string SheetName, int HeaderRowIndex)
        //{
        //    HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
        //    HSSFSheet sheet = workbook.GetSheet(SheetName);

        //    DataTable table = new DataTable();

        //    HSSFRow headerRow = sheet.GetRow(HeaderRowIndex);
        //    int cellCount = headerRow.LastCellNum;

        //    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        //    {
        //        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
        //        table.Columns.Add(column);
        //    }

        //    int rowCount = sheet.LastRowNum;

        //    for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
        //    {
        //        HSSFRow row = sheet.GetRow(i);
        //        DataRow dataRow = table.NewRow();

        //        for (int j = row.FirstCellNum; j < cellCount; j++)
        //            dataRow[j] = row.GetCell(j).ToString();
        //    }

        //    ExcelFileStream.Close();
        //    workbook = null;
        //    sheet = null;
        //    return table;
        //}

        //public static DataTable RenderDataTableFromExcel(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
        //{
        //    HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
        //    HSSFSheet sheet = workbook.GetSheetAt(SheetIndex);

        //    DataTable table = new DataTable();

        //    HSSFRow headerRow = sheet.GetRow(HeaderRowIndex);
        //    int cellCount = headerRow.LastCellNum;

        //    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        //    {
        //        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
        //        table.Columns.Add(column);
        //    }

        //    int rowCount = sheet.LastRowNum;

        //    for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
        //    {
        //        HSSFRow row = sheet.GetRow(i);
        //        DataRow dataRow = table.NewRow();

        //        for (int j = row.FirstCellNum; j < cellCount; j++)
        //        {
        //            if (row.GetCell(j) != null)
        //                dataRow[j] = row.GetCell(j).ToString();
        //        }

        //        table.Rows.Add(dataRow);
        //    }

        //    ExcelFileStream.Close();
        //    workbook = null;
        //    sheet = null;
        //    return table;
        //}

        ///// <summary> Читать Excel
        ///// заголовок первой строки по умолчанию
        ///// </summary>
        ///// <param name = "path"> путь к документу Excel </ param>
        ///// <returns></returns>
        //public static DataTable RenderDataTableFromExcel(string path)
        //{
        //    DataTable dt = new DataTable();

        //    HSSFWorkbook hssfworkbook;
        //    using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
        //    {
        //        hssfworkbook = new HSSFWorkbook(file);
        //    }
        //    HSSFSheet sheet = hssfworkbook.GetSheetAt(0);
        //    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

        //    HSSFRow headerRow = sheet.GetRow(0);
        //    int cellCount = headerRow.LastCellNum;

        //    for (int j = 0; j < cellCount; j++)
        //    {
        //        HSSFCell cell = headerRow.GetCell(j);
        //        dt.Columns.Add(cell.ToString());
        //    }

        //    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
        //    {
        //        HSSFRow row = sheet.GetRow(i);
        //        DataRow dataRow = dt.NewRow();

        //        for (int j = row.FirstCellNum; j < cellCount; j++)
        //        {
        //            if (row.GetCell(j) != null)
        //                dataRow[j] = row.GetCell(j).ToString();
        //        }

        //        dt.Rows.Add(dataRow);
        //    }

        //    //while (rows.MoveNext())
        //    //{
        //    //    HSSFRow row = (HSSFRow)rows.Current;
        //    //    DataRow dr = dt.NewRow();

        //    //    for (int i = 0; i < row.LastCellNum; i++)
        //    //    {
        //    //        HSSFCell cell = row.GetCell(i);


        //    //        if (cell == null)
        //    //        {
        //    //            dr[i] = null;
        //    //        }
        //    //        else
        //    //        {
        //    //            dr[i] = cell.ToString();
        //    //        }
        //    //    }
        //    //    dt.Rows.Add(dr);
        //    //}

        //    return dt;
        //}
    //}
}
