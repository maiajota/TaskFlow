using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskFlow.Migrations
{
    public partial class AdicaoUsuarioAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        migrationBuilder.Sql("INSERT INTO Usuario(Email, Nome, Sobrenome, Senha, DataCriacao) " + 
                                "VALUES('admin@admin.com', 'admin', 'admin', 'admin123', CURRENT_TIMESTAMP)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Urgente",
                table: "Tarefa",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
