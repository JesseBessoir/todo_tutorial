using E3Starter.Models;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Migrations
{
    [Migration(2)]

    public class AddingCreatedAndDeactivatedAtColumnsToTasksAndCat091220231018 : Migration
    {
        public override void Up()
        {
            Delete.Column("Priority").FromTable("Tasks");

            Alter.Table("Categories")
                .AddColumn("CreatedAt").AsDateTime().Nullable()
                .AddColumn("DeactivatedAt").AsDateTime().Nullable();

            Alter.Table("TaskCategories")
                .AddColumn("CreatedAt").AsDateTime().Nullable()
                .AddColumn("DeactivatedAt").AsDateTime().Nullable();

            // Insert roles.
            Execute.Sql("INSERT INTO Categories (CategoryName) VALUES ('Relaxation');");
            Execute.Sql("INSERT INTO Categories (CategoryName) VALUES ('Work');");
            Execute.Sql("INSERT INTO Categories (CategoryName) VALUES ('Productivity');");
            Execute.Sql("INSERT INTO Categories (CategoryName) VALUES ('Fitness');");

        }

        public override void Down()
        {
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Fitness';");
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Productivity';");
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Work';");
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Relaxation';");

            Delete.Column("DeactivatedAt").FromTable("TaskCategories");
            Delete.Column("CreatedAt").FromTable("TaskCategories");

            Delete.Column("DeactivatedAt").FromTable("Categories");
            Delete.Column("CreatedAt").FromTable("Categories");

            Alter.Table("Tasks")
                .AddColumn("Priority").AsInt32().NotNullable();

        }
    }
}
