using E3Starter.Services;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Migrations;

[Migration(12092023105100)]
public class SchemaAlterations : Migration
{
    public override void Up()
    {
        //Create.Table("Tasks")
        //    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
        //    .WithColumn("TaskName").AsString(150).NotNullable()
        //    .WithColumn("CreatedAt").AsDateTime().Nullable()
        //    .WithColumn("DeactivatedAt").AsDateTime().Nullable()
        //    .WithColumn("CompletedAt").AsDateTime().Nullable()
        //    .WithColumn("DeletedAt").AsDateTime().Nullable()
        //    .WithColumn("Priority").AsString(15).NotNullable();

        Execute.Sql(@"
            UPDATE TASKS SET PriorityId = 1 WHERE PriorityId is null
        ");

        Alter.Table("Tasks")
            .AlterColumn("PriorityId").AsInt32().NotNullable();
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
