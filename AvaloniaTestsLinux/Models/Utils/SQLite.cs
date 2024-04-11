using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;

namespace AvaloniaTestsLinux.Models.Utils;

internal static class SQLite
{
    private static readonly string _connectionString = "Data Source=Assets\\source.test";
    private static readonly SqliteConnection _connection = new SqliteConnection(_connectionString);

    internal static void Open() => _connection.Open();
    internal static void Close() => _connection.Close();

    
    internal static void SQLNoneQuery()
    {
        
    }
}