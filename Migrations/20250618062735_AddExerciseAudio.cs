using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PianoFlow.Migrations
{
    // Миграция для добавления поля AudioPath в таблицу Exercises
    public partial class AddExerciseAudio : Migration
    {
        // Применение миграции (добавление столбца)
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioPath",
                table: "Exercises",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        // Откат миграции (удаление столбца)
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioPath",
                table: "Exercises");
        }
    }
}
