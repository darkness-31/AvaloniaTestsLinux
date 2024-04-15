using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Sqlite;

namespace AvaloniaTestsLinux.Models.Utils;

internal static class SQLite
{
    private static readonly string _connectionString = "Data Source=Assets\\source.test";
    private static readonly SqliteConnection _connection = new SqliteConnection(_connectionString);

    internal static void Open() => _connection.Open();
    internal static void Close() => _connection.Close();

    public static IEnumerable<DataRow> SQLQueryWithParametrsAsIEnumerable(this string sql, Dictionary<string, dynamic> parametrs)
    {
        return sql.SQLQueryWithParametrsAsDataTable(parametrs).Rows.Cast<DataRow>();
    }

    public static DataTable SQLQueryWithParametrsAsDataTable(this string sqlQuery, Dictionary<string, dynamic> parametrs)
    {
        var dt = new DataTable();
        var command = new SqliteCommand(sqlQuery, _connection);
        foreach (KeyValuePair<string, dynamic> item in parametrs)
        {
            command.Parameters.AddWithValue(item.Key, item.Value);
        }
        
        var sqlReader = command.ExecuteReader();

        if (sqlReader.CanGetColumnSchema())
        {
            dt.Columns.AddRange(sqlReader.GetColumnSchema()
                                                .Select(x => new DataColumn(x.ColumnName, x.DataType))
                                                .ToArray());
        }
        else
        {
            dt.Columns.AddRange(Enumerable.Range(0, sqlReader.FieldCount)
                                                 .Select(x => new DataColumn(sqlReader.GetName(x), sqlReader.GetFieldType(x)))
                                                 .ToArray());
        }
        
        if (!sqlReader.HasRows)
            return dt;
        while (sqlReader.Read())
        {
            var row = dt.NewRow();
            foreach (DataColumn column in dt.Columns)
            {
                if (int.TryParse(sqlReader.GetValue(column.Ordinal).ToString(), out int result) && column.DataType != Type.GetType(string.Empty))
                {
                    row[column.ColumnName] = TypeDescriptor.GetConverter(column.DataType)
                                                           .ConvertFromString(sqlReader.GetValue(column.Ordinal).ToString());
                }
                else if (typeof(byte[]) == column.DataType)
                {
                    row[column.ColumnName] = (byte[])sqlReader.GetValue(column.Ordinal);
                }
                else
                {
                    row[column.ColumnName] = TypeDescriptor.GetConverter(column.DataType)
                                                           .ConvertFrom(sqlReader.GetValue(column.Ordinal));
                }
            }

            dt.Rows.Add(row);
        }

        return dt;
    }
    
    public static T[] SQLQueryWithParametrsAsArray<T>(this string sqlQuery, Dictionary<string, dynamic> parametrs)
        where T : new()
    {
        var resultList = new Stack<T>();
        var command = new SqliteCommand(sqlQuery, _connection);
        foreach (KeyValuePair<string, dynamic> item in parametrs)
        {
            command.Parameters.AddWithValue(item.Key, item.Value);
        }

        var sqlReader = command.ExecuteReader();
        if (sqlReader.HasRows)
            return resultList.ToArray();
        
        while (sqlReader.Read())
        {
            T item = new T();
            var classFields = item.GetType()
                                          .GetFields(BindingFlags.Instance | 
                                                              BindingFlags.NonPublic | 
                                                              BindingFlags.Public);
            
            foreach (var field in classFields)
            {
                int indexColumn = sqlReader.GetOrdinal(field.Name.Replace(">k__BackingField", "")
                                                                 .Substring(1, field.Name.Length - 17));
                if (indexColumn == -1) continue;
                var cal = sqlReader.GetValue(indexColumn);
                try
                {
                    if (int.TryParse(cal.ToString(), out int result) && field.FieldType != Type.GetType(string.Empty))
                    {
                        field.SetValue(item, TypeDescriptor.GetConverter(field.FieldType)
                                                                   .ConvertFromString(cal.ToString()!));
                    }
                    else
                    {
                        field.SetValue(item, TypeDescriptor.GetConverter(field.FieldType)
                                                                   .ConvertFrom(cal));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Возникло исключение при попытке записи значение в поле со следующим текстом: \n" + ex.Message);
                }
            }
            resultList.Push(item);
        }
        return resultList.ToArray();
    }

    
    internal static void SQLNoneQuery(this string sql)
    {
        new SqliteCommand(sql, _connection).ExecuteNonQuery();
    }
    
    internal static void SQLNoneQueryWithParametrs(this string sql, Dictionary<string, dynamic> parametrs)
    {
        var command = new SqliteCommand(sql, _connection);
        foreach (KeyValuePair<string, dynamic> item in parametrs)
            command.Parameters.AddWithValue(item.Key, item.Value);
        command.ExecuteNonQuery();
    }

    internal static T? Convert<T>(this object? obj)
    {
        if (obj == null || obj == DBNull.Value)
            return default;
        
        if (int.TryParse(obj.ToString(), out int resultNumeric))
        {
            return (T?)TypeDescriptor.GetConverter(typeof(T))
                          .ConvertFromString(obj.ToString());
        }
        else if (DateTime.TryParse(obj.ToString(), out DateTime resultDateTime) || typeof(T) == typeof(Guid))
        {
            return (T?)TypeDescriptor.GetConverter(typeof(T))
                          .ConvertFrom(obj);
        }
        
        return (T?)obj;
    }
}