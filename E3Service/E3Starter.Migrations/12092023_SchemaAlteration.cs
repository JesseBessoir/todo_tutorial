using E3Starter.Services;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Migrations;

[Migration(12092023)]
public class SchemaAlteration : Migration
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

        Create.Table("Priority")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
            .WithColumn("Name").AsString(25).NotNullable()
            .WithColumn("Sequence").AsInt32().NotNullable();

        Alter.Table("Tasks")
            .AddColumn("PriorityId").AsInt32().ForeignKey("Priority", "Id").Nullable();

        Delete.Column("Priority").FromTable("Tasks");

        Insert.IntoTable("Priority")
            .Row(new { Name = "low", Sequence = 1 })
            .Row(new { Name = "medium", Sequence = 2 })
            .Row(new { Name = "high", Sequence = 3 });
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
