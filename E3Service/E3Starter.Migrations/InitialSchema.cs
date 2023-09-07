using E3Starter.Services;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3Starter.Migrations;

[Migration(0)]
public class InitialSchema : Migration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Username").AsString(50).NotNullable()
            .WithColumn("Email").AsString(100).NotNullable()
            .WithColumn("HashedPassword").AsString(500).NotNullable()
            .WithColumn("PasswordSalt").AsString(500).NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable()
            .WithColumn("CreatedByUserId").AsInt32().ForeignKey("Users", "Id").Nullable()
            .WithColumn("DeactivatedAt").AsDateTime().Nullable()
            .WithColumn("DeactivatedByUserId").AsInt32().ForeignKey("Users", "Id").Nullable();

        Create.Table("Roles")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(50).NotNullable();

        Create.Table("UserRoles")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt32().ForeignKey("Users", "Id").NotNullable()
            .WithColumn("RoleId").AsInt32().ForeignKey("Roles", "Id").NotNullable();

        /// SEED DATA ///
        // Insert roles.
        Execute.Sql("INSERT INTO Roles (Name) VALUES ('Admin');");

        // Insert initial admin user with hashed password and salt.
        var cryptoService = new CryptoService();
        var salt = cryptoService.GenerateSalt();
        var hash = cryptoService.HashPassword("Password1", salt);
        Execute.Sql($"INSERT INTO Users (Username, Email, HashedPassword, PasswordSalt, CreatedAt) VALUES ('admin', 'admin@example.com', '{hash}', '{salt}', GETDATE());");

        // Use SQL to retrieve IDs and insert the Admin user role.
        Execute.Sql("INSERT INTO UserRoles (UserId, RoleId) VALUES ((SELECT Id FROM Users WHERE Username = 'admin'), (SELECT Id FROM Roles WHERE Name = 'Admin'));");
    }

    public override void Down()
    {
        throw new NotImplementedException();
    }
}
