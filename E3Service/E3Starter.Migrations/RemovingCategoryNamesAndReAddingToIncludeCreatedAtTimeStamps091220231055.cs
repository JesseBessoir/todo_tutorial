using E3Starter.Models;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Migrations
{
    [Migration(3)]

    public class RemovingCategoryNamesAndReAddingToIncludeCreatedAtTimeStamps091220231055 : Migration
    {
        public override void Up()
        {
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Fitness';");
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Productivity';");
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Work';");
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Relaxation';");

            // Insert roles.
            Execute.Sql("INSERT INTO Categories (CategoryName, CreatedAt) VALUES ('Relaxation', GetUTCDate());");
            Execute.Sql("INSERT INTO Categories (CategoryName, CreatedAt) VALUES ('Work', GetUTCDate());");
            Execute.Sql("INSERT INTO Categories (CategoryName, CreatedAt) VALUES ('Productivity', GetUTCDate());");
            Execute.Sql("INSERT INTO Categories (CategoryName, CreatedAt) VALUES ('Fitness', GetUTCDate());");

        }

        public override void Down()
        {
            
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Fitness';");
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Productivity';");
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Work';");
            Execute.Sql("DELETE FROM Categories WHERE CategoryName = 'Relaxation';");

            Execute.Sql("INSERT INTO Categories (CategoryName) VALUES ('Relaxation');");
            Execute.Sql("INSERT INTO Categories (CategoryName) VALUES ('Work');");
            Execute.Sql("INSERT INTO Categories (CategoryName) VALUES ('Productivity');");
            Execute.Sql("INSERT INTO Categories (CategoryName) VALUES ('Fitness');");
        }
    }
}
