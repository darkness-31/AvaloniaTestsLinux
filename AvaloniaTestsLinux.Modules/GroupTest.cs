using AvaloniaTestsLinux.Models.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;

namespace AvaloniaTestsLinux.Models;

public class GroupTest
{
    public Guid Id { get; }
    public string Name { get; }
    public Entity State { get; private set; }
    public uint ComplatePercent { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }
    public ObservableCollection<Test> Tests { get; }

    public delegate void GroupTestHandler(GroupTest group);

    public static GroupTestHandler? GroupTestCreateEvent;
    
    public GroupTest(Guid id, string name, uint complatePercent, Entity state, DateTime createdAt, DateTime? modifiedAt)
    {
        Id = id;
        Name = name;
        ComplatePercent = complatePercent;
        State = state;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        Tests = new ObservableCollection<Test>(Test.GetCollection(id));
    } 

    public static GroupTest[] GetCollection()
    {
        var sql = $@"SELECT group_id,
                            name,
                            e_complate_state,
                            complate_percent,
                            created_at,
                            modified_at
                     FROM groups
                     WHERE delete_state_code = 0";
        var rows = sql.SQLQueryWithParametrsAsIEnumerable(new System.Collections.Generic.Dictionary<string, dynamic>())
                      .Select(x => new GroupTest(
                            x["group_id"].Convert<Guid>(),
                            x["name"].Convert<string>(),
                            x["complate_percent"].Convert<uint>(),
                            Entity.Get(Handbook.Entity.NameEnum.complate_state, x["e_complate_state"].Convert<int>()),
                            x["created_at"].Convert<DateTime>(),
                            x["modified_at"].Convert<DateTime?>()
                          ));
        return rows.ToArray();
    }

    public static GroupTest Create(string name)
    {
        var id = Guid.NewGuid();
        var sql = $@"INSERT INTO groups (group_id, name)
                     VALUES (@id, @name)";
        sql.SQLNoneQueryWithParametrs(new Dictionary<string, dynamic>()
        {
            ["@id"] = id,
            ["@name"] = name
        });
        
        var group = new GroupTest(id, name, 0, Entity.Get(Handbook.Entity.NameEnum.complate_state, 0), DateTime.Now, null);
        GroupTestCreateEvent?.Invoke(group);
        return group;
    }
}