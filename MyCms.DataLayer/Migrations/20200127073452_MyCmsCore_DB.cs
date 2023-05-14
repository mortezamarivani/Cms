using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCms.DataLayer.Migrations
{
    public partial class MyCmsCore_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    AboutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    HomeDesc = table.Column<string>(maxLength: 500, nullable: true),
                    Mobile = table.Column<string>(maxLength: 100, nullable: true),
                    TelPhon = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Address = table.Column<string>(maxLength: 400, nullable: true),
                    AboutHeader = table.Column<string>(maxLength: 100, nullable: false),
                    AboutTitle = table.Column<string>(maxLength: 100, nullable: false),
                    AboutDescription = table.Column<string>(maxLength: 500, nullable: false),
                    InstagramID = table.Column<string>(maxLength: 50, nullable: true),
                    LinkdinAddress = table.Column<string>(maxLength: 100, nullable: true),
                    TwitterAddress = table.Column<string>(maxLength: 100, nullable: true),
                    FacebookAddress = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<int>(nullable: false),
                    CreatorUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.AboutID);
                });

            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    GalleryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GalleryDesc = table.Column<string>(maxLength: 200, nullable: true),
                    GalleryName = table.Column<string>(maxLength: 200, nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<int>(nullable: false),
                    CreatorUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gallery", x => x.GalleryID);
                });

            migrationBuilder.CreateTable(
                name: "PageGroups",
                columns: table => new
                {
                    GroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupTitle = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageGroups", x => x.GroupID);
                });

            migrationBuilder.CreateTable(
                name: "ReciveInfo",
                columns: table => new
                {
                    ReciveInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SenderName = table.Column<string>(maxLength: 100, nullable: true),
                    SenderEmail = table.Column<string>(maxLength: 100, nullable: false),
                    ReciveMessage = table.Column<string>(maxLength: 500, nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReciveInfo", x => x.ReciveInfoID);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SkillsTitle = table.Column<string>(maxLength: 400, nullable: false),
                    SkillsDescription = table.Column<string>(maxLength: 500, nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    BootstarpClassName = table.Column<string>(maxLength: 50, nullable: false),
                    Progress = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillsID);
                });

            migrationBuilder.CreateTable(
                name: "UserX",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Family = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserX", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupID = table.Column<int>(nullable: false),
                    PageTitle = table.Column<string>(maxLength: 400, nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 500, nullable: false),
                    PageText = table.Column<string>(nullable: false),
                    PageVisit = table.Column<int>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    PageTags = table.Column<string>(nullable: true),
                    ShowInSlider = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageID);
                    table.ForeignKey(
                        name: "FK_Pages_PageGroups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "PageGroups",
                        principalColumn: "GroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pages_GroupID",
                table: "Pages",
                column: "GroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.DropTable(
                name: "Gallery");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "ReciveInfo");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "UserX");

            migrationBuilder.DropTable(
                name: "PageGroups");
        }
    }
}
