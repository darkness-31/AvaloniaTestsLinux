using System;
using System.Collections.Generic;
using System.Linq;
using AvaloniaTestsLinux.Models.Utils;

namespace AvaloniaTestsLinux.Models;

internal class Entity
{
    internal int Code { get; }
    internal eName Name { get; }
    internal string Meaning { get; }
    internal DateTime CreatedAt { get; }

    internal enum eName
    {
        complate_state
    }
    
    internal Entity(int code, eName name, string meaning, DateTime createdAt)
    {
        Code = code;
        Name = name;
        Meaning = meaning;
        CreatedAt = createdAt;
    }

    internal static Entity[] GetCollection(eName complate_state)
    {
        var sql = $@"SELECT code,
                            meaning,
                            created_at
                     FROM entity
                     WHERE name = @name";
        var rows = sql.SQLQueryWithParametrsAsIEnumerable(new Dictionary<string, dynamic>()
        {
            ["@name"] = complate_state.ToString()
        });

        return rows.Select(x => new Entity(x["code"].Convert<int>(), complate_state, x["meaning"].Convert<string>(), x["created_at"].Convert<DateTime>())).ToArray();
    }

    internal static Entity? Get(eName complate_state, int code)
    {
        var sql = $@"SELECT meaning,
                            created_at
                     FROM entity
                     WHERE name = @name AND
                           code = @code";
        var row = sql.SQLQueryWithParametrsAsIEnumerable(new Dictionary<string, dynamic>()
        {
            ["@name"] = complate_state.ToString(),
            ["@code"] = code
        }).FirstOrDefault();

        if (row == null) return null;
        return new Entity(code, complate_state, row["meaning"].Convert<string>()!, row["created_at"].Convert<DateTime>());
    }
}