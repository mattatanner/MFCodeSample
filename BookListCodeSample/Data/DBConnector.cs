using System.Collections.ObjectModel;
using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;

namespace BookListCodeSample.Data;

public class DBConnector
{
    public string connStr = string.Empty;

    public DBConnector()
    {
        connStr =
            "Data Source=Data/booklist.db";
    }
    public async Task<List<T>> LoadData<T>(string sql)
    {
        using (IDbConnection conn = new SqliteConnection(connStr))
        {
            var data = await conn.QueryAsync<T>(sql);
            return data.ToList();
        }
    }
    public async Task<ObservableCollection<T>> LoadData2<T>(string sql)
    {
        using (IDbConnection conn = new SqliteConnection(connStr))
        {
            var data = await conn.QueryAsync<T>(sql);

            return new ObservableCollection<T>(data);
        }
    }

    public async Task<T> LoadRecord<T>(string sql)
    {
        using (IDbConnection conn = new SqliteConnection(connStr))
        {
            var data = await conn.QueryFirstOrDefaultAsync<T>(sql);

            if (data == null) data = (T)Activator.CreateInstance(typeof(T));
            return data;
        }
    }
    public async Task<T> ExecuteScalar<T>(string sql)
    {
        using (IDbConnection conn = new SqliteConnection(connStr))
        {
            var data = await conn.ExecuteScalarAsync<T>(sql);
            if (data == null)
                if (typeof(T) == typeof(string))
                    return (dynamic)"";
                else
                    data = (T)Activator.CreateInstance(typeof(T))!;

            return data;
        }
    }

    public async Task SaveData<T>(string sql, T parameters)
    {
        using (IDbConnection conn = new SqliteConnection(connStr))
        {
            await conn.ExecuteAsync(sql, parameters);
        }
    }

    public async Task<int> InsertData<T>(string sql, T parameters)
    {
        using (IDbConnection conn = new SqliteConnection(connStr))
        {
            var newID = await conn.QuerySingleAsync<int>(sql + "; SELECT @@IDENTITY", parameters);
            return newID;
        }
    }

    public async Task<DataTable> SqlTable(string sql)
    {
        var table = new DataTable();

        using (var conn = new SqliteConnection(connStr))
        {
            await conn.OpenAsync();
            using (var cmd = new SqliteCommand(sql, conn))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                table.Load(reader);
            }
        }

        return table;
    }

    public async Task Execute(string sql)
    {
        using (var conn = new SqliteConnection(connStr))
        {
            try
            {
                await conn.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }

    public string Quote(string val)
    {
        return "'" + val.Replace("'", ",").Replace("`", "'") + "'";
    }
    public string CleanStr(string val)
    {
        return val.Replace("'", ",").Replace("`", "'");
    }
    public string SqlDate(DateTime? _dateT)
    {
        if (_dateT is null)
            return "null";
        else
        {
            return Quote(((DateTime)_dateT).ToString("MM-dd-yyyy"));
        }
    }
    public string SqlDateTime(DateTime? _dateT)
    {
        if (_dateT is null)
            return "null";
        else
        {
            return Quote(((DateTime)_dateT).ToString("MM-dd-yyyy HH:mm"));
        }
    }

}
