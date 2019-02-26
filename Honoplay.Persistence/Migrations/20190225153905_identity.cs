using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Honoplay.Persistence.Migrations
{
    public partial class identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "AdminUsers",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_AdminUsers_TenantId_Username",
                table: "AdminUsers",
                newName: "IX_AdminUsers_TenantId_UserName");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "AdminUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AdminUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "AdminUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "AdminUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AdminUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "AdminUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "AdminUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "AdminUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "AdminUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "AdminUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "AdminUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "AdminUsers");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AdminUsers",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_AdminUsers_TenantId_UserName",
                table: "AdminUsers",
                newName: "IX_AdminUsers_TenantId_Username");
        }
    }
}
