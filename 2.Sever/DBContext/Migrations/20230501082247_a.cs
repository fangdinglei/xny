using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBContext.Main.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Device_Groups_Users_CreatorId",
                table: "User_Device_Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_User_SFs_Users_FatherId",
                table: "User_SFs");

            migrationBuilder.DropForeignKey(
                name: "FK_User_SFs_Users_SonId",
                table: "User_SFs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CreatorId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Device_AutoControls");

            migrationBuilder.DropTable(
                name: "Historys");

            migrationBuilder.DropIndex(
                name: "IX_User_SFs_SonId_FatherId",
                table: "User_SFs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Devices",
                table: "User_Devices");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DropColumn(
                name: "PControl",
                table: "User_Devices");

            migrationBuilder.DropColumn(
                name: "PData",
                table: "User_Devices");

            migrationBuilder.DropColumn(
                name: "PStatus",
                table: "User_Devices");

            migrationBuilder.DropColumn(
                name: "DataPoints",
                table: "Device_Types");

            migrationBuilder.RenameColumn(
                name: "SonId",
                table: "User_SFs",
                newName: "User2Id");

            migrationBuilder.RenameColumn(
                name: "FatherId",
                table: "User_SFs",
                newName: "User1Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Authoritys",
                table: "Users",
                type: "longtext",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "MaxSubUser",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxSubUserDepth",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TreeDeep",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserTreeId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "表示用户属于哪一个用户族");

            migrationBuilder.AddColumn<bool>(
                name: "IsFather",
                table: "User_SFs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSelf",
                table: "User_SFs",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserTreeId",
                table: "User_SFs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Op",
                table: "User_Op_Audits",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "User_Op_Audits",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "UserTreeId",
                table: "User_Op_Audits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "User_Devices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Authority",
                table: "User_Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserTreeId",
                table: "User_Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User_Device_Groups",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "User_Device_Groups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserTreeId",
                table: "User_Device_Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserTreeId",
                table: "Internal_Mails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Devices",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "Devices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserTreeId",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "Device_Types",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserTreeId",
                table: "Device_Types",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Devices",
                table: "User_Devices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AccountHistorys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserTreeId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    Time = table.Column<long>(type: "bigint", nullable: false),
                    Success = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Data = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountHistorys", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ColdDataSettings",
                columns: table => new
                {
                    TreeId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ManagerName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ColdDownTime = table.Column<long>(type: "bigint", nullable: false),
                    MinCount = table.Column<long>(type: "bigint", nullable: false),
                    Open = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColdDataSettings", x => x.TreeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Device_AutoControl_Settings_Items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Open = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Order = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TriggerType = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    TimeStart = table.Column<long>(type: "bigint", nullable: false),
                    TimeEnd = table.Column<long>(type: "bigint", nullable: false),
                    OwnerID = table.Column<long>(type: "bigint", nullable: false),
                    Week = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Cmd = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device_AutoControl_Settings_Items", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Device_DataPoint_Colds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<long>(type: "bigint", nullable: false),
                    StreamId = table.Column<long>(type: "bigint", nullable: false),
                    CreatTime = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<long>(type: "bigint", nullable: false),
                    EndTime = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Pars = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    ManagerName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TreeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device_DataPoint_Colds", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Device_Repairs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserTreeId = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<long>(type: "bigint", nullable: false),
                    DiscoveryTime = table.Column<long>(type: "bigint", nullable: false),
                    CompletionTime = table.Column<long>(type: "bigint", nullable: false),
                    Context = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device_Repairs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeviceHistorys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserTreeId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    Time = table.Column<long>(type: "bigint", nullable: false),
                    Success = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Data = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHistorys", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "t_kv",
                columns: table => new
                {
                    Key = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_kv", x => x.Key);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ThingModels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserTreeId = table.Column<long>(type: "bigint", nullable: false),
                    DeviceTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Remark = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Unit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MinValue = table.Column<float>(type: "float", nullable: false),
                    MaxValue = table.Column<float>(type: "float", nullable: false),
                    AlertLowValue = table.Column<float>(type: "float", nullable: false),
                    AlertHighValue = table.Column<float>(type: "float", nullable: false),
                    Abandonted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThingModels", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTreeId",
                table: "Users",
                column: "UserTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_SFs_User2Id",
                table: "User_SFs",
                column: "User2Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_SFs_UserTreeId",
                table: "User_SFs",
                column: "UserTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Op_Audits_AuditorId",
                table: "User_Op_Audits",
                column: "AuditorId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Op_Audits_Op",
                table: "User_Op_Audits",
                column: "Op");

            migrationBuilder.CreateIndex(
                name: "IX_User_Op_Audits_SponsorId",
                table: "User_Op_Audits",
                column: "SponsorId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Op_Audits_Time",
                table: "User_Op_Audits",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_User_Op_Audits_UserTreeId",
                table: "User_Op_Audits",
                column: "UserTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Devices_UserId_DeviceId",
                table: "User_Devices",
                columns: new[] { "UserId", "DeviceId" });

            migrationBuilder.CreateIndex(
                name: "IX_User_Devices_UserTreeId",
                table: "User_Devices",
                column: "UserTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Device_Groups_Name",
                table: "User_Device_Groups",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_User_Device_Groups_UserTreeId",
                table: "User_Device_Groups",
                column: "UserTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Internal_Mails_ReceiverId",
                table: "Internal_Mails",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Internal_Mails_SenderId",
                table: "Internal_Mails",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Internal_Mails_Time",
                table: "Internal_Mails",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_Internal_Mails_UserTreeId",
                table: "Internal_Mails",
                column: "UserTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Name",
                table: "Devices",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserTreeId",
                table: "Devices",
                column: "UserTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Types_UserTreeId",
                table: "Device_Types",
                column: "UserTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_DataPoints_DeviceId_StreamId_Time",
                table: "Device_DataPoints",
                columns: new[] { "DeviceId", "StreamId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountHistorys_CreatorId",
                table: "AccountHistorys",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountHistorys_Time",
                table: "AccountHistorys",
                column: "Time");

            migrationBuilder.CreateIndex(
                name: "IX_AccountHistorys_UserTreeId",
                table: "AccountHistorys",
                column: "UserTreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_DataPoint_Colds_DeviceId_StreamId_StartTime_EndTime",
                table: "Device_DataPoint_Colds",
                columns: new[] { "DeviceId", "StreamId", "StartTime", "EndTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Device_Repairs_CompletionTime",
                table: "Device_Repairs",
                column: "CompletionTime");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Repairs_DeviceId",
                table: "Device_Repairs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Repairs_DiscoveryTime_Id",
                table: "Device_Repairs",
                columns: new[] { "DiscoveryTime", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_ThingModels_DeviceTypeId",
                table: "ThingModels",
                column: "DeviceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Device_Groups_Users_CreatorId",
                table: "User_Device_Groups",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Op_Audits_Users_AuditorId",
                table: "User_Op_Audits",
                column: "AuditorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Op_Audits_Users_SponsorId",
                table: "User_Op_Audits",
                column: "SponsorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_SFs_Users_User1Id",
                table: "User_SFs",
                column: "User1Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_SFs_Users_User2Id",
                table: "User_SFs",
                column: "User2Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CreatorId",
                table: "Users",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Device_Groups_Users_CreatorId",
                table: "User_Device_Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Op_Audits_Users_AuditorId",
                table: "User_Op_Audits");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Op_Audits_Users_SponsorId",
                table: "User_Op_Audits");

            migrationBuilder.DropForeignKey(
                name: "FK_User_SFs_Users_User1Id",
                table: "User_SFs");

            migrationBuilder.DropForeignKey(
                name: "FK_User_SFs_Users_User2Id",
                table: "User_SFs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CreatorId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "AccountHistorys");

            migrationBuilder.DropTable(
                name: "ColdDataSettings");

            migrationBuilder.DropTable(
                name: "Device_AutoControl_Settings_Items");

            migrationBuilder.DropTable(
                name: "Device_DataPoint_Colds");

            migrationBuilder.DropTable(
                name: "Device_Repairs");

            migrationBuilder.DropTable(
                name: "DeviceHistorys");

            migrationBuilder.DropTable(
                name: "t_kv");

            migrationBuilder.DropTable(
                name: "ThingModels");

            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTreeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_User_SFs_User2Id",
                table: "User_SFs");

            migrationBuilder.DropIndex(
                name: "IX_User_SFs_UserTreeId",
                table: "User_SFs");

            migrationBuilder.DropIndex(
                name: "IX_User_Op_Audits_AuditorId",
                table: "User_Op_Audits");

            migrationBuilder.DropIndex(
                name: "IX_User_Op_Audits_Op",
                table: "User_Op_Audits");

            migrationBuilder.DropIndex(
                name: "IX_User_Op_Audits_SponsorId",
                table: "User_Op_Audits");

            migrationBuilder.DropIndex(
                name: "IX_User_Op_Audits_Time",
                table: "User_Op_Audits");

            migrationBuilder.DropIndex(
                name: "IX_User_Op_Audits_UserTreeId",
                table: "User_Op_Audits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User_Devices",
                table: "User_Devices");

            migrationBuilder.DropIndex(
                name: "IX_User_Devices_UserId_DeviceId",
                table: "User_Devices");

            migrationBuilder.DropIndex(
                name: "IX_User_Devices_UserTreeId",
                table: "User_Devices");

            migrationBuilder.DropIndex(
                name: "IX_User_Device_Groups_Name",
                table: "User_Device_Groups");

            migrationBuilder.DropIndex(
                name: "IX_User_Device_Groups_UserTreeId",
                table: "User_Device_Groups");

            migrationBuilder.DropIndex(
                name: "IX_Internal_Mails_ReceiverId",
                table: "Internal_Mails");

            migrationBuilder.DropIndex(
                name: "IX_Internal_Mails_SenderId",
                table: "Internal_Mails");

            migrationBuilder.DropIndex(
                name: "IX_Internal_Mails_Time",
                table: "Internal_Mails");

            migrationBuilder.DropIndex(
                name: "IX_Internal_Mails_UserTreeId",
                table: "Internal_Mails");

            migrationBuilder.DropIndex(
                name: "IX_Devices_Name",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserTreeId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Device_Types_UserTreeId",
                table: "Device_Types");

            migrationBuilder.DropIndex(
                name: "IX_Device_DataPoints_DeviceId_StreamId_Time",
                table: "Device_DataPoints");

            migrationBuilder.DropColumn(
                name: "Authoritys",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MaxSubUser",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MaxSubUserDepth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TreeDeep",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserTreeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsFather",
                table: "User_SFs");

            migrationBuilder.DropColumn(
                name: "IsSelf",
                table: "User_SFs");

            migrationBuilder.DropColumn(
                name: "UserTreeId",
                table: "User_SFs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "User_Op_Audits");

            migrationBuilder.DropColumn(
                name: "UserTreeId",
                table: "User_Op_Audits");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User_Devices");

            migrationBuilder.DropColumn(
                name: "Authority",
                table: "User_Devices");

            migrationBuilder.DropColumn(
                name: "UserTreeId",
                table: "User_Devices");

            migrationBuilder.DropColumn(
                name: "UserTreeId",
                table: "User_Device_Groups");

            migrationBuilder.DropColumn(
                name: "UserTreeId",
                table: "Internal_Mails");

            migrationBuilder.DropColumn(
                name: "UserTreeId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "UserTreeId",
                table: "Device_Types");

            migrationBuilder.RenameColumn(
                name: "User2Id",
                table: "User_SFs",
                newName: "SonId");

            migrationBuilder.RenameColumn(
                name: "User1Id",
                table: "User_SFs",
                newName: "FatherId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "Users",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Op",
                table: "User_Op_Audits",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "PControl",
                table: "User_Devices",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PData",
                table: "User_Devices",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PStatus",
                table: "User_Devices",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "User_Device_Groups",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "User_Device_Groups",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Devices",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "Devices",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                table: "Device_Types",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "DataPoints",
                table: "Device_Types",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_Devices",
                table: "User_Devices",
                columns: new[] { "UserId", "DeviceId" });

            migrationBuilder.CreateTable(
                name: "Device_AutoControls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<long>(type: "bigint", nullable: true),
                    TimedControl = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TimedControlSetting = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device_AutoControls", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Historys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historys", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatorId", "EMail", "LastLogin", "Name", "Pass", "Phone", "Status" },
                values: new object[] { 1L, null, "2432114474@qq.com", 0L, "admin", "123", "15850798245", (byte)0 });

            migrationBuilder.CreateIndex(
                name: "IX_User_SFs_SonId_FatherId",
                table: "User_SFs",
                columns: new[] { "SonId", "FatherId" });

            migrationBuilder.CreateIndex(
                name: "IX_Device_AutoControls_DeviceId",
                table: "Device_AutoControls",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Device_Groups_Users_CreatorId",
                table: "User_Device_Groups",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_SFs_Users_FatherId",
                table: "User_SFs",
                column: "FatherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_SFs_Users_SonId",
                table: "User_SFs",
                column: "SonId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CreatorId",
                table: "Users",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
