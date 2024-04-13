using AvaloniaTestsLinux.Models.Utils;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;

namespace AvaloniaTestsLinux.Models;

internal class GroupTest
{
    internal Guid Id { get; }
    internal string Name { get; }
    internal Entity State { get; private set; }
    internal uint ComplatePercent { get; private set; }
    internal DateTime CreatedAt { get; }
    internal DateTime? ModifiedAt { get; private set; }
    internal ObservableCollection<Test> Tests { get; } = new ObservableCollection<Test>();

    internal GroupTest(Guid id, string name, uint complatePercent, Entity state, DateTime createdAt, DateTime? modifiedAt)
    {
        Id = id;
        Name = name;
        ComplatePercent = complatePercent;
        State = state;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        Tests = new ObservableCollection<Test>(Test.GetCollection(id));
    } 

    internal static GroupTest[] GetCollection()
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
                            Entity.Get(Entity.eName.complate_state, x["e_complate_state"].Convert<int>()),
                            x["created_at"].Convert<DateTime>(),
                            x["modified_at"].Convert<DateTime?>()
                          ));
        return rows.ToArray();
    }
}