using Avalonia;
using AvaloniaTestsLinux.Models.Utils;
using System;
using System.Linq;

namespace AvaloniaTestsLinux.Models;

public class Test
{
    internal Guid Id { get; }
    internal string Name { get; private set; }
    internal string Description { get; private set; }
    internal Entity ComplateState { get; private set; }
    internal byte[] Script { get; private set; }
    internal DateTime CreatedAt { get; }
    internal DateTime? ModifiedAt { get; private set; }

    internal Test(Guid id, string name, string description, Entity complate, byte[] script, DateTime createdAt, DateTime? modifiedAt = null )
    {
        Id = id;
        Name = name;
        Description = description;
        ComplateState = complate;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        Script = script;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
    }
    internal static Test[] GetCollection(Guid idGroup)
    {
        var sql = $@"SELECT tests.test_id,
                     	    tests.name,
                     	    tests.description,
                     	    tests.e_complate_state,
                     	    tests.check_script,
                     	    tests.created_at,
                     	    tests.modified_at
                     FROM tests INNER JOIN
                     	    group_test ON group_test.group_id = @groupId AND
                     					  group_test.delete_state_code = 0 AND 
                     					  tests.delete_state_code = 0";
        var rows = sql.SQLQueryWithParametrsAsIEnumerable(new System.Collections.Generic.Dictionary<string, dynamic>()
        {
            ["@groupId"] = idGroup
        }).Select(x => new Test(
                x["test_id"].Convert<Guid>(),
                x["name"].Convert<string>(),
                x["description"].Convert<string>(),
                Entity.Get(Entity.eName.complate_state, x["e_complate_state"].Convert<int>()),
                x["check_script"].Convert<byte[]>(),
                x["created_at"].Convert<DateTime>(),
                x["modified_at"].Convert<DateTime?>()
            ));

        return rows.ToArray();
    }

    internal static Test Create(Guid groupId, string name, string description, Entity complate, byte[] script)
    {
        var sql = $@"INSERT INTO tests (test_id, name, description, e_complate_state, check_script)
                     VALUES (@id, @name, @description, @state, @script)";
        var id = Guid.NewGuid();
        sql.SQLNoneQueryWithParametrs(new System.Collections.Generic.Dictionary<string, dynamic>()
        {
            ["@id"] = id,
            ["@name"] = name,
            ["@description"] = description,
            ["@e_complate_state"] = complate.Code,
            ["@script"] = script
        });
        var testNew = new Test(id, name, description, complate, script, DateTime.Now);

        sql = $@"INSERT INTO group_test (group_id, test_id, created_at)
                 VALUES (@groupId, @testId, @createdAt)";
        sql.SQLNoneQueryWithParametrs(new System.Collections.Generic.Dictionary<string, dynamic>()
        {
            ["@groupId"] = groupId,
            ["@testId"] = id,
            ["@createdAt"] = DateTime.Now
        });

        return testNew;
    }
}