using System;
using System.Collections.Generic;
using System.Linq;
using AvaloniaTestsLinux.Models.Utils;

namespace AvaloniaTestsLinux.Models;

public class Entity
{
    public int Code { get; }
    public Handbook.Entity.NameEnum Name { get; }
    public string Meaning { get; }
    public DateTime CreatedAt { get; }

    public Entity(int code, Handbook.Entity.NameEnum name, string meaning, DateTime createdAt)
    {
        Code = code;
        Name = name;
        Meaning = meaning;
        CreatedAt = createdAt;
    }

    public static Entity[] GetCollection(Handbook.Entity.NameEnum complate_state)
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

    public static Entity? Get(Handbook.Entity.NameEnum complate_state, int code)
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