using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ImportDataToVideoClubTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"         
                INSERT INTO Pelatis(Onoma, Tilefono, IsActive)
                VALUES
                ('Perkins', '246801',1),
                ('Kantakouzinos','246801',1),
                ('Palaiologos','987654',1);
                
                INSERT INTO Tainia(Titlos, Xronia, IsActive)
                VALUES
                ('Rear Window', 1954,1),
                ('Psycho',1960,1),
                ('Ben-Hur',1959,1);

                INSERT INTO Kasetes(IDTainias, Posotita, IsActive, Timi, Tipos) 
                VALUES
			    ((select id from Tainia where Titlos='Rear Window' and Xronia=1954), 3, 1, 2, 'VHS'),
			    ((select id from Tainia where Titlos='Rear Window' and Xronia=1954), 1, 1, 3, 'DVD'),
			    ((select id from Tainia where Titlos='Psycho' and Xronia=1960), 2, 1, 2, 'VHS');

                INSERT INTO Sintelestis(Onoma, IsActive)
                VALUES
                ('Alfred Hitchcock',1),
                ('Grace Kelly',1),
                ('Anthony Perkins',1);

                INSERT INTO Enoikiasi(IDKasetas, IDPelati, Apo, Eos) 
                VALUES
			    ((select id from Kasetes where Tipos='VHS' and Posotita=3 and Timi=2),(select id from Pelatis where Onoma='Perkins' and Tilefono='246801'), '20060710', '20060910'),
			    ((select id from Kasetes where Tipos='DVD' and Posotita=1 and Timi=3),(select id from Pelatis where Onoma='Kantakouzinos' and Tilefono='246801'), '20060920', '20061120'),
			    ((select id from Kasetes where Tipos='VHS' and Posotita=2 and Timi=2),(select id from Pelatis where Onoma='Palaiologos' and Tilefono='987654'), '20060910', NULL);
                    
                INSERT INTO Tn_sn(IDSintelesti, IDTainias, Rolos) 
                VALUES
			    ((select id from Sintelestis where Onoma='Alfred Hitchcock'),(select id from Tainia where Titlos='Rear Window' and Xronia=1954), 'Director'),			
			    ((select id from Sintelestis where Onoma='Grace Kelly'),(select id from Tainia where Titlos='Rear Window' and Xronia=1954), 'Actor'),	
			    ((select id from Sintelestis where Onoma='Alfred Hitchcock'),(select id from Tainia where Titlos='Psycho' and Xronia=1960), 'Director'),			
			    ((select id from Sintelestis where Onoma='Anthony Perkins'),(select id from Tainia where Titlos='Psycho' and Xronia=1960), 'Actor');	
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
