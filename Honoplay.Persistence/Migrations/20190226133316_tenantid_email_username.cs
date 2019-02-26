using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Honoplay.Persistence.Migrations
{
    public partial class tenantid_email_username : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminUsers_Tenants",
                table: "AdminUsers");

            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_TenantId_Email",
                table: "AdminUsers");

            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_TenantId_UserName",
                table: "AdminUsers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AdminUsers");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedDateTime",
                table: "AdminUsers",
                nullable: true,
                oldClrType: typeof(DateTimeOffset));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AdminUsers",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AdminUsers",
                maxLength: 150,
                nullable: false,
                computedColumnSql: "Email",
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_Email",
                table: "AdminUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdminUsers_Email",
                table: "AdminUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AdminUsers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldComputedColumnSql: "Email");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedDateTime",
                table: "AdminUsers",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AdminUsers",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AdminUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_TenantId_Email",
                table: "AdminUsers",
                columns: new[] { "TenantId", "Email" },
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_TenantId_UserName",
                table: "AdminUsers",
                columns: new[] { "TenantId", "UserName" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AdminUsers_Tenants",
                table: "AdminUsers",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
