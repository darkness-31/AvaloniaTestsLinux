using AvaloniaTestsLinux.Models.Utils;

namespace AvaloniaTestsLinux.Models;

public class Test
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Entity ComplateState { get; private set; }
    public byte[] Script { get; private set; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }

    public Test(Guid id, string name, string description, Entity complate, byte[] script, DateTime createdAt, DateTime? modifiedAt = null )
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
    public static Test[] GetCollection(Guid idGroup)
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
     		                              group_test.test_id = tests.test_id AND
                     					  group_test.delete_state_code = 0 AND 
                     					  tests.delete_state_code = 0";
        var rows = sql.SQLQueryWithParametrsAsIEnumerable(new System.Collections.Generic.Dictionary<string, dynamic>()
        {
            ["@groupId"] = idGroup
        }).Select(x => new Test(
                x["test_id"].Convert<Guid>(),
                x["name"].Convert<string>(),
                x["description"].Convert<string>(),
                Entity.Get(Handbook.Entity.NameEnum.complate_state, x["e_complate_state"].Convert<int>()),
                x["check_script"].Convert<byte[]>(),
                x["created_at"].Convert<DateTime>(),
                x["modified_at"].Convert<DateTime?>()
            ));

        return rows.ToArray();
    }

    public static Test Create(Guid groupId, string name, string description, Entity complate, byte[] script)
    {
        var sql = @"INSERT INTO tests (test_id, name, description, e_complate_state, check_script)
                     VALUES (@id, @name, @description, @state, @script)";
        var id = Guid.NewGuid();
        sql.SQLNoneQueryWithParametrs(new System.Collections.Generic.Dictionary<string, dynamic>()
        {
            ["@id"] = id,
            ["@name"] = name,
            ["@description"] = description,
            ["@state"] = complate.Code,
            ["@script"] = script
        });
        var testNew = new Test(id, name, description, complate, script, DateTime.Now);

        sql = @"INSERT INTO group_test (group_id, test_id, created_at)
                 VALUES (@groupId, @testId, @createdAt)";
        sql.SQLNoneQueryWithParametrs(new System.Collections.Generic.Dictionary<string, dynamic>()
        {
            ["@groupId"] = groupId,
            ["@testId"] = id,
            ["@createdAt"] = DateTime.Now
        });

        return testNew;
    }

    public void Complete()
    {
        var sql = @"UPDATE tests
                    SET e_complate_state = 3
                    WHERE test_id = @id";
        sql.SQLNoneQueryWithParametrs(new Dictionary<string, dynamic>()
        {
            ["@id"] = this.Id
        });
        
        sql = @"SELECT group_id
                FROM group_test
				WHERE test_id = @test_id";
        var group_id = (Guid)sql.SQLQueryWithParametrsAsIEnumerable(new Dictionary<string, dynamic>()
        {
            ["@test_id"] = this.Id
        }).First()[0];
        
        sql = @"UPDATE groups 
                SET complate_percent = (SELECT COUNT(*) * 100 / (SELECT COUNT(*) 
						 						                 FROM group_test AS TEMP
						 						                 WHERE temp.group_id = @group_id) AS ""count""
                                        FROM group_test INNER JOIN
                                               tests ON group_test.group_id = @group_id AND
                                                        group_test.test_id = tests.test_id AND
                                                        tests.e_complate_state = 3)
                WHERE group_id = @group_id";
        sql.SQLNoneQueryWithParametrs(new Dictionary<string, dynamic>()
        {
            ["@group_id"] = group_id
        });
        
    }
}