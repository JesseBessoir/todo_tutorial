using E3Starter.Services;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Migrations;

[Migration(1)]
public class AddingTasksAndCatTables : Migration
{
    public override void Up()
    {

        Create.Table("Tasks")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("TaskName").AsString(150).NotNullable()
            .WithColumn("CreatedAt").AsDateTime().Nullable()
            .WithColumn("DeactivatedAt").AsDateTime().Nullable()
            .WithColumn("CompletedAt").AsDateTime().Nullable()
            .WithColumn("DeletedAt").AsDateTime().Nullable()
            .WithColumn("Priority").AsInt32().NotNullable();

        Create.Table("Categories")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("CategoryName").AsString(150).NotNullable();

        Create.Table("TaskCategories")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("TaskId").AsInt32().ForeignKey("Tasks", "Id").NotNullable()
            .WithColumn("CatergoryId").AsInt32().ForeignKey("Categories", "Id").NotNullable();

    }

    public override void Down()
    {

        Delete.Table("TaskCategories");
        Delete.Table("Categories");
        Delete.Table("Tasks");

    }
}
