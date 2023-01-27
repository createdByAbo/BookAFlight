using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAFlight.Migrations
{
    public partial class CreateType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fleet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Model = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    NumberOfFirstClassSeats = table.Column<byte>(type: "tinyint", nullable: true),
                    NumberOfBusinessClassSeats = table.Column<byte>(type: "tinyint", nullable: true),
                    NumberOfEconomicClassSeats = table.Column<byte>(type: "tinyint", nullable: true),
                    NumberOfServiceSeats = table.Column<byte>(type: "tinyint", nullable: false),
                    Registry = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fleet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PassangerData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIdIfRegistred = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    SecondName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    SurName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    PeselNumber = table.Column<decimal>(type: "numeric(11,0)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassangerData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationCreatorId = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    PassengerRegistryId = table.Column<int>(type: "int", nullable: false),
                    SeatType = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    RegistredBaggage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AircraftId = table.Column<int>(type: "int", nullable: false),
                    StartCity = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    StartAirport = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    StartDateOnly = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndCity = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    EndAirport = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDateOnly = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeetweenAproche = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    BeetweenAprocheDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FlightCode = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false),
                    FirstClassSeatPrice = table.Column<decimal>(type: "money", nullable: true),
                    BuisnessClassSeatPrice = table.Column<decimal>(type: "money", nullable: true),
                    EconomicClassSeatPrice = table.Column<decimal>(type: "money", nullable: true),
                    RegistredBaggagePrice = table.Column<decimal>(type: "money", nullable: false),
                    NumberOfMaxPersonsWithRegistredBaggage = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "AircraftId",
                        column: x => x.AircraftId,
                        principalTable: "Fleet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    SecondName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Username = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    LastName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PeselNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AircraftId",
                table: "Flights",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "UQ_FlightCode",
                table: "Flights",
                column: "FlightCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Id",
                table: "Flights",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UC_User",
                table: "Users",
                columns: new[] { "Id", "Username", "Email", "PeselNumber", "PhoneNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "PassangerData");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Fleet");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
