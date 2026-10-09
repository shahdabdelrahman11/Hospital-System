using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
       insert into Doctors (Name, Specialization, Image) values ('Keely Willgoose', 'Cardiology', 'img4.jpg');
insert into Doctors (Name, Specialization, Image) values ('Galvan Redmile', 'Cardiology', 'img2.png');
insert into Doctors (Name, Specialization, Image) values ('Janella Erb', 'Pediatrics', 'img3.jpg');
insert into Doctors (Name, Specialization, Image) values ('Candra Shafier', 'Pediatrics', 'img3.jpg');
insert into Doctors (Name, Specialization, Image) values ('Antonio Strathman', 'Dermatology', 'img3.jpg');
insert into Doctors (Name, Specialization, Image) values ('Jasun Slegg', 'Orthopedics', 'img4.jpg');
insert into Doctors (Name, Specialization, Image) values ('Pennie Huebner', 'Dermatology', 'img2.png');
insert into Doctors (Name, Specialization, Image) values ('Carola Pegram', 'Orthopedics', 'img4.jpg');
insert into Doctors (Name, Specialization, Image) values ('Becka Leither', 'Dermatology', 'img1.png');
insert into Doctors (Name, Specialization, Image) values ('Rowe Tallow', 'Pediatrics', 'img4.jpg');
insert into Doctors (Name, Specialization, Image) values ('Moe Bedlington', 'Neurology', 'img1.png');
insert into Doctors (Name, Specialization, Image) values ('Baron Hedderly', 'Pediatrics', 'img4.jpg');

    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Doctors");
        }
    }
}
