using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JAP_Task_1_MoviesApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PosterPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Overview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    ActorsId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.ActorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    SoldTickets = table.Column<int>(type: "int", nullable: false),
                    ScreeningDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreeningId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BoughtTickets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalTable: "Screenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "Morgan", "Freeman" },
                    { 51, "Lance", "Reddick" },
                    { 50, "Dominic", "West" },
                    { 49, "Jessie", "Buckley" },
                    { 48, "Stellan", "Skarsgard" },
                    { 47, "Jared", "Harris" },
                    { 46, "Jessie", "Buckley" },
                    { 52, "Sonja", "Sohn" },
                    { 45, "Ron", "Livingston" },
                    { 43, "Scott", "Grimes" },
                    { 42, "Anna", "Gunn" },
                    { 41, "Aaron", "Paul" },
                    { 40, "Bryan", "Cranston" },
                    { 39, "Nikolay", "Drozdov" },
                    { 38, "Sigourney", "Weaver" },
                    { 44, "Damian", "Lewis" },
                    { 37, "David", "Attenborough" },
                    { 53, "Peter", "Drost" },
                    { 55, "Neil", "deGrasse Tyson" },
                    { 70, "Eddie", "Falco" },
                    { 69, "Lorraine", "Bracco" },
                    { 68, "James", "Gandolfini" },
                    { 67, "Kit", "Harington" },
                    { 66, "Peter", "Dinklage" },
                    { 65, "Emilia", "Clarke" },
                    { 54, "Roger", "Horrocks" },
                    { 64, "Jonathan", "Fahn" },
                    { 62, "Carl", "Sagan" },
                    { 61, "Mae", "Whitman" },
                    { 59, "Dee", "Bradley Baker" },
                    { 58, "Neil", "deGrasse Tyson" },
                    { 57, "Piotr", "Michael" },
                    { 56, "Stoney", "Emshwiller" },
                    { 63, "Jaromir", "Hanzlik" },
                    { 36, "Elliot", "Page" },
                    { 60, "Zach", "Tyler" },
                    { 34, "Leonardo", "DiCaprio" },
                    { 15, "Liam", "Neeson" },
                    { 14, "Martin", "Balsam" },
                    { 13, "Lee", "J. Cobb" },
                    { 12, "Henry", "Fonda" },
                    { 11, "Aaron", "Eckhart" }
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { 10, "Heath", "Ledger" },
                    { 16, "Ralph", "Fiennes" },
                    { 9, "Christian", "Bale" },
                    { 6, "James", "Caan" },
                    { 5, "Al", "Pacino" },
                    { 4, "Marlon", "Brando" },
                    { 3, "Tim", "Robbins" },
                    { 2, "Bob", "Gunton" },
                    { 35, "Joseph", "Gordon-Levitt" },
                    { 8, "Robert", "Duvall" },
                    { 17, "Ben", "Kingsley" },
                    { 7, "Robert", "De Niro" },
                    { 19, "Viggo", "Mortensen" },
                    { 33, "Gary", "Sinise" },
                    { 18, "Elijah", "Wood" },
                    { 32, "Robin", "Wright" },
                    { 31, "Tom", "Hanks" },
                    { 29, "Edward", "Norton" },
                    { 28, "Brad", "Pitt" },
                    { 27, "Orlando", "Bloom" },
                    { 30, "Meat", "Loaf" },
                    { 25, "Eli", "Wallach" },
                    { 24, "Clint", "Eastwood" },
                    { 20, "Ian", "McKellen" },
                    { 23, "Samuel", "L. Jackson" },
                    { 22, "Uma", "Thurman" },
                    { 26, "Lee", "Van Cleef" },
                    { 21, "John", "Travolta" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Overview", "PosterPath", "ReleaseDate", "Title", "Type" },
                values: new object[,]
                {
                    { 66, "David Attenborough returns to the world's oceans in this sequel to the acclaimed documentary filming rare and unusual creatures of the deep, as well as documenting the problems our oceans face.", "https://cdn.shopify.com/s/files/1/0747/3829/products/mL1006_1024x1024.jpg?v=1571445246", new DateTime(2017, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blue Planet II", (short)1 },
                    { 67, "Documentary series focusing on the breadth of the diversity of habitats around the world, from the remote Arctic wilderness and mysterious deep oceans to the vast landscapes of Africa and diverse jungles of South America.", "https://www.penguin.co.uk/content/dam/prh/books/111/1115210/9780593079768.jpg.transform/PRHDesktopWide_small/image.jpg", new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Our Planet", (short)1 },
                    { 68, "An exploration of our discovery of the laws of nature and coordinates in space and time.", "https://pisces.bbystatic.com/image2/BestBuy_US/images/products/5945/5945188_sa.jpg;maxHeight=640;maxWidth=550", new DateTime(2014, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cosmos: A Spacetime Odyssey", (short)1 },
                    { 69, "In a war-torn world of elemental magic, a young boy reawakens to undertake a dangerous mystic quest to fulfill his destiny as the Avatar, and bring peace to the world.", "https://images-na.ssl-images-amazon.com/images/I/914eUC4XPML.jpg", new DateTime(2005, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avatar: The Last Airbender", (short)1 },
                    { 71, "Nine noble families fight for control over the lands of Westeros, while an ancient enemy returns after being dormant for millennia.", "https://m.media-amazon.com/images/M/MV5BYTRiNDQwYzAtMzVlZS00NTI5LWJjYjUtMzkwNTUzMWMxZTllXkEyXkFqcGdeQXVyNDIzMzcwNjc@._V1_FMjpg_UX1000_.jpg", new DateTime(2011, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Game of Thrones", (short)1 },
                    { 72, "New Jersey mob boss Tony Soprano deals with personal and professional issues in his home and business life that affect his mental state, leading him to seek professional psychiatric counseling.", "https://m.media-amazon.com/images/M/MV5BZGJjYzhjYTYtMDBjYy00OWU1LTg5OTYtNmYwOTZmZjE3ZDdhXkEyXkFqcGdeQXVyNTAyODkwOQ@@._V1_FMjpg_UX1000_.jpg", new DateTime(1999, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Sopranos", (short)1 },
                    { 65, "The Baltimore drug scene, as seen through the eyes of drug dealers and law enforcement.", "https://tvshows.today/wp-content/uploads/the-wire-season-1-poster.jpg", new DateTime(2008, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Wire", (short)1 },
                    { 74, "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/c617e634647543.56d846b10d56f.jpg", new DateTime(1994, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Schindler's List 2", (short)0 },
                    { 73, "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", "https://upload.wikimedia.org/wikipedia/sh/8/83/Dark_knight_rises_poster.jpg", new DateTime(2008, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Dark Knight 2", (short)0 },
                    { 70, "Astronomer Carl Sagan leads us on an engaging guided tour of the various elements and cosmological theories of the universe.", "https://www.themoviedb.org/t/p/original/4WJ9kNejI8apl3f8hMNwo8V3hGT.jpg", new DateTime(1980, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cosmos", (short)1 },
                    { 64, "In April 1986, an explosion at the Chernobyl nuclear power plant in the Union of Soviet Socialist Republics becomes one of the world's worst man-made catastrophes.", "https://i.redd.it/bv5isr69yr531.png", new DateTime(1986, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chernobyl", (short)1 },
                    { 57, "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.", "https://m.media-amazon.com/images/I/51v5ZpFyaFL._AC_.jpg", new DateTime(1999, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fight Club", (short)0 },
                    { 62, "A high school chemistry teacher diagnosed with inoperable lung cancer turns to manufacturing and selling methamphetamine in order to secure his family's future.", "https://www.themoviedb.org/t/p/w600_and_h900_bestv2/ggFHVNu6YYI5L9pCfOacjizRGt.jpg", new DateTime(2008, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breaking Bad", (short)1 },
                    { 61, "Emmy Award-winning, 11 episodes, five years in the making, the most expensive nature documentary series ever commissioned by the BBC, and the first to be filmed in high definition.", "https://m.media-amazon.com/images/I/91X9p6+58KL._SY445_.jpg", new DateTime(2010, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Planet Earth", (short)1 }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Overview", "PosterPath", "ReleaseDate", "Title", "Type" },
                values: new object[,]
                {
                    { 60, "Wildlife documentary series with David Attenborough, beginning with a look at the remote islands which offer sanctuary to some of the planet's rarest creatures, to the beauty of cities, which are home to humans, and animals..", "https://blackwells.co.uk/jacket/l/9781785943041.jpg", new DateTime(2016, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Planet Earth II", (short)1 },
                    { 59, "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.", "https://m.media-amazon.com/images/I/81p+xe8cbnL._SY445_.jpg", new DateTime(2010, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inception", (short)0 },
                    { 58, "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.", "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/37a5b434647543.56d846b10ca45.jpg", new DateTime(1994, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Forrest Gump", (short)0 },
                    { 56, "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.", "https://images.moviesanywhere.com/198e228b071c60f5ad57e5f62fe60029/ff22cad6-2218-414d-b853-3f95d76905c7.jpg?h=375&resize=fit&w=250", new DateTime(2001, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Fellowship of the Ring", (short)0 },
                    { 55, "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", "https://prod.miramax.digital/media/assets/Pulp-Fiction1.png", new DateTime(1994, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pulp Fiction", (short)0 },
                    { 54, "A bounty hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.", "https://upload.wikimedia.org/wikipedia/en/4/45/Good_the_bad_and_the_ugly_poster.jpg", new DateTime(1996, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Good, the Bad and the Ugly", (short)0 },
                    { 75, "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", "https://img.discogs.com/MsgjJVAxVCXb1w86ffIbaNRr2BY=/fit-in/600x600/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-728620-1512923411-4779.jpeg.jpg", new DateTime(2003, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Return of the King 2", (short)0 },
                    { 53, "Jake, who is paraplegic, replaces his twin on the Na'vi inhabited Pandora for a corporate mission. After the natives accept him as one of their own, he must decide where his loyalties lie.", "https://i.pinimg.com/originals/17/aa/71/17aa718c1ab15b482505b8431cf596fc.jpg", new DateTime(2009, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avatar", (short)0 },
                    { 63, "The story of Easy Company of the U.S. Army 101st Airborne Division and their mission in World War II Europe, from Operation Overlord to V-J Day.", "https://i.dailymail.co.uk/i/pix/2017/02/13/01/3D24EF6B00000578-4215748-image-a-63_1486948627611.jpg", new DateTime(2001, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Band of Brothers", (short)1 },
                    { 76, "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", "https://prod.miramax.digital/media/assets/Pulp-Fiction1.png", new DateTime(1994, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pulp Fiction 2", (short)0 },
                    { 93, "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.", "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/37a5b434647543.56d846b10ca45.jpg", new DateTime(1994, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Forrest Gump 3", (short)0 },
                    { 78, "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.", "https://images.moviesanywhere.com/198e228b071c60f5ad57e5f62fe60029/ff22cad6-2218-414d-b853-3f95d76905c7.jpg?h=375&resize=fit&w=250", new DateTime(2001, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Fellowship of the Ring 2", (short)0 },
                    { 52, "Anakin joins forces with Obi-Wan and sets Palpatine free from th evil clutches of Count Doku. However, he falls prey to Palpatine and the Jedis' mind games and gives into temptation.", "https://play-lh.googleusercontent.com/mMyoXM8bf72KK-Udap4-hAvqqdXgn0AIBXkS8zejT0RXITIh8oK9a-SYIVk89CA0rHJi", new DateTime(2005, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: Revenge of the Sith (Episode III)", (short)0 },
                    { 100, "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/c617e634647543.56d846b10d56f.jpg", new DateTime(1994, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Schindler's List 4", (short)0 },
                    { 99, "A jury holdout attempts to prevent a miscarriage of justice by forcing his colleagues to reconsider the evidence.", "https://upload.wikimedia.org/wikipedia/commons/b/b5/12_Angry_Men_%281957_film_poster%29.jpg", new DateTime(1957, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12 Angry Men 4", (short)0 },
                    { 98, "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", "https://upload.wikimedia.org/wikipedia/sh/8/83/Dark_knight_rises_poster.jpg", new DateTime(2008, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Dark Knight 4", (short)0 },
                    { 97, "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.", "https://shotonwhat.com/images/0071562-med.jpg", new DateTime(1974, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Godfather: Part II 4", (short)0 },
                    { 96, "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.", "https://www.reelviews.net/resources/img/posters/thumbs/godfather_poster.jpg", new DateTime(1972, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Godfather 4", (short)0 },
                    { 95, "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", "https://swank.azureedge.net/swank/prod-film/3560cd8a-9491-4ab9-876c-8a8d6b84a6dd/f8e7c904-669a-4c9f-ac29-d19b64b43e33/one-sheet.jpg?width=335&height=508&mode=crop", new DateTime(1994, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Shawshank Redemption 4", (short)0 },
                    { 94, "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.", "https://m.media-amazon.com/images/I/81p+xe8cbnL._SY445_.jpg", new DateTime(2010, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inception 3", (short)0 },
                    { 92, "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.", "https://m.media-amazon.com/images/I/51v5ZpFyaFL._AC_.jpg", new DateTime(1999, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fight Club 3", (short)0 },
                    { 91, "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.", "https://images.moviesanywhere.com/198e228b071c60f5ad57e5f62fe60029/ff22cad6-2218-414d-b853-3f95d76905c7.jpg?h=375&resize=fit&w=250", new DateTime(2001, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Fellowship of the Ring 3", (short)0 },
                    { 77, "A bounty hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.", "https://upload.wikimedia.org/wikipedia/en/4/45/Good_the_bad_and_the_ugly_poster.jpg", new DateTime(1996, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Good, the Bad and the Ugly 2", (short)0 },
                    { 90, "A bounty hunting scam joins two men in an uneasy alliance against a third in a race to find a fortune in gold buried in a remote cemetery.", "https://upload.wikimedia.org/wikipedia/en/4/45/Good_the_bad_and_the_ugly_poster.jpg", new DateTime(1996, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Good, the Bad and the Ugly 3", (short)0 },
                    { 88, "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", "https://img.discogs.com/MsgjJVAxVCXb1w86ffIbaNRr2BY=/fit-in/600x600/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-728620-1512923411-4779.jpeg.jpg", new DateTime(2003, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Return of the King 3", (short)0 },
                    { 87, "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/c617e634647543.56d846b10d56f.jpg", new DateTime(1994, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Schindler's List 3", (short)0 },
                    { 86, "A jury holdout attempts to prevent a miscarriage of justice by forcing his colleagues to reconsider the evidence.", "https://upload.wikimedia.org/wikipedia/commons/b/b5/12_Angry_Men_%281957_film_poster%29.jpg", new DateTime(1957, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "12 Angry Men 3", (short)0 },
                    { 85, "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", "https://upload.wikimedia.org/wikipedia/sh/8/83/Dark_knight_rises_poster.jpg", new DateTime(2008, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Dark Knight 3", (short)0 },
                    { 84, "The early life and career of Vito Corleone in 1920s New York City is portrayed, while his son, Michael, expands and tightens his grip on the family crime syndicate.", "https://shotonwhat.com/images/0071562-med.jpg", new DateTime(1974, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Godfather: Part II 3", (short)0 },
                    { 83, "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.", "https://www.reelviews.net/resources/img/posters/thumbs/godfather_poster.jpg", new DateTime(1972, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Godfather 3", (short)0 },
                    { 82, "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", "https://swank.azureedge.net/swank/prod-film/3560cd8a-9491-4ab9-876c-8a8d6b84a6dd/f8e7c904-669a-4c9f-ac29-d19b64b43e33/one-sheet.jpg?width=335&height=508&mode=crop", new DateTime(1994, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Shawshank Redemption 3", (short)0 },
                    { 81, "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.", "https://m.media-amazon.com/images/I/81p+xe8cbnL._SY445_.jpg", new DateTime(2010, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inception 2", (short)0 },
                    { 80, "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.", "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/37a5b434647543.56d846b10ca45.jpg", new DateTime(1994, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Forrest Gump 2", (short)0 },
                    { 79, "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.", "https://m.media-amazon.com/images/I/51v5ZpFyaFL._AC_.jpg", new DateTime(1999, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fight Club 2", (short)0 },
                    { 89, "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", "https://prod.miramax.digital/media/assets/Pulp-Fiction1.png", new DateTime(1994, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pulp Fiction 3", (short)0 },
                    { 51, "While pursuing an assassin, Obi Wan uncovers a sinister plot to destroy the Republic. With the fate of the galaxy hanging in the balance, the Jedi must defend the galaxy against the evil Sith.", "https://m.media-amazon.com/images/M/MV5BMDAzM2M0Y2UtZjRmZi00MzVlLTg4MjEtOTE3NzU5ZDVlMTU5XkEyXkFqcGdeQXVyNDUyOTg3Njg@._V1_.jpg", new DateTime(2002, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: Attack of the Clones (Episode II)", (short)0 },
                    { 16, "The world is stunned when a group of time travelers arrive from the year 2051 to deliver an urgent message: Thirty years in the future, mankind is losing a global war against a deadly alien species. The only hope for survival is for soldiers and civilians from the present to be transported to the future and join the fight. Among those recruited is high school teacher and family man Dan Forester. Determined to save the world for his young daughter, Dan teams up with a brilliant scientist and his estranged father in a desperate quest to rewrite the fate of the planet.", "https://image.tmdb.org/t/p/w500/34nDCQZwaEvsy4CFO5hkGRFDCVU.jpg", new DateTime(2021, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Tomorrow War", (short)0 },
                    { 49, "The revival of Emperor Palpatine resurrects the battle between the Resistance and the First Order while the Jedi's legendary conflict with the Sith Lord comes to a head.", "https://lumiere-a.akamaihd.net/v1/images/star-wars-the-rise-of-skywalker-theatrical-poster-1000_ebc74357.jpeg?region=0%2C0%2C891%2C1372", new DateTime(2019, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Rise of Skywalker (Episode IX)", (short)0 },
                    { 22, "Raj is a rich, carefree, happy-go-lucky second generation NRI. Simran is the daughter of Chaudhary Baldev Singh, who in spite of being an NRI is very strict about adherence to Indian values. Simran has left for India to be married to her childhood fiancé. Raj leaves for India with a mission at his hands, to claim his lady love under the noses of her whole family. Thus begins a saga.", "https://image.tmdb.org/t/p/w500/2CAL2433ZeIihfX1Hb2139CX0pW.jpg", new DateTime(1995, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dilwale Dulhania Le Jayenge", (short)0 },
                    { 21, "The film centers on the love relationship among the band's bassist Haruki Nakayama, drummer Akihiko Kaji, and Akihiko's roommate and ex-boyfriend Ugetsu Murata.", "https://image.tmdb.org/t/p/w500/xKCtoYHUyX8zAg68eemnYa2orep.jpg", new DateTime(2021, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Given", (short)0 },
                    { 20, "Lindy is an acid-tongued woman with rage issues who controls her temper by shocking herself with an electrode vest. One day she makes a connection with Justin, who gives her a glimmer of hope for a shock-free future, but when he’s murdered she launches herself on a revenge-fueled rampage in pursuit of his killer.", "https://image.tmdb.org/t/p/w500/gYZAHan5CHPFXORpQMvOjCTug4E.jpg", new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jolt", (short)0 },
                    { 19, "The Loud family travel to Scotland and discover they are descendants of Scottish royalty as they move into their giant ancestral castle.", "https://image.tmdb.org/t/p/w500/mab5wPeGVjbMyYMzyzfdKKnG9cl.jpg", new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Loud House Movie", (short)0 }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Overview", "PosterPath", "ReleaseDate", "Title", "Type" },
                values: new object[,]
                {
                    { 18, "A mysterious former secret service agent must urgently return to France when his estranged son  is falsely accused of arms and drug trafficking by the government, following a blunder by an overzealous bureaucrat and a mafia operation.", "https://image.tmdb.org/t/p/w500/ttpKJ7XQxDZV252KNEHXtykYT41.jpg", new DateTime(2021, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Last Mercenary", (short)0 },
                    { 17, "", "https://image.tmdb.org/t/p/w500/oxNoVgbu2v9ETL93Kri1pw8osYf.jpg", new DateTime(2021, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breathless", (short)0 },
                    { 101, "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.", "https://img.discogs.com/MsgjJVAxVCXb1w86ffIbaNRr2BY=/fit-in/600x600/filters:strip_icc():format(jpeg):mode_rgb():quality(90)/discogs-images/R-728620-1512923411-4779.jpeg.jpg", new DateTime(2003, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lord of the Rings: The Return of the King 4", (short)0 },
                    { 15, "Luca and his best friend Alberto experience an unforgettable summer on the Italian Riviera. But all the fun is threatened by a deeply-held secret: they are sea monsters from another world just below the water’s surface.", "https://image.tmdb.org/t/p/w500/jTswp6KyDYKtvC52GbHagrZbGvD.jpg", new DateTime(2021, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luca", (short)0 },
                    { 14, "A waiter pretends to be an important businessman in order to reach the upper class through his entrepreneurial dreams.", "https://image.tmdb.org/t/p/w500/zvGC5jX5wQmU1GgPc0VGZz7Mtcs.jpg", new DateTime(2021, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "El mesero", (short)0 },
                    { 13, "The Templeton brothers — Tim and his Boss Baby little bro Ted — have become adults and drifted away from each other. But a new boss baby with a cutting-edge approach and a can-do attitude is about to bring them together again … and inspire a new family business.", "https://image.tmdb.org/t/p/w500/kv2Qk9MKFFQo4WQPaYta599HkJP.jpg", new DateTime(2021, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Boss Baby", (short)0 },
                    { 23, "Framed in the 1940s for the double murder of his wife and her lover, upstanding banker Andy Dufresne begins a new life at the Shawshank prison, where he puts his accounting skills to work for an amoral warden. During his long stretch in prison, Dufresne comes to be admired by the other inmates -- including an older prisoner named Red -- for his integrity and unquenchable sense of hope.", "https://image.tmdb.org/t/p/w500/q6y0Go1tsGEsmtFryDOJo3dEmqu.jpg", new DateTime(1994, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Shawshank Redemption", (short)0 },
                    { 12, "Dominic Toretto and his crew battle the most skilled assassin and high-performance driver they've ever encountered: his forsaken brother.", "https://image.tmdb.org/t/p/w500/bOFaAXmWWXC3Rbv4u4uM9ZSzRXP.jpg", new DateTime(2021, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "F9", (short)0 },
                    { 10, "Evan McCauley has skills he never learned and memories of places he has never visited. Self-medicated and on the brink of a mental breakdown, a secret group that call themselves “Infinites” come to his rescue, revealing that his memories are real.", "https://image.tmdb.org/t/p/w500/niw2AKHz6XmwiRMLWaoyAOAti0G.jpg", new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Infinite", (short)0 },
                    { 9, "A bank teller called Guy realizes he is a background character in an open world video game called Free City that will soon go offline.", "https://image.tmdb.org/t/p/w500/hEqw9swA8gFJuNjgWYEypwZfkZg.jpg", new DateTime(2021, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Free Guy", (short)0 },
                    { 8, "A man will become a criminal to save his family.  Director: Shawn Welling  Writer: Derek H. Potts  Stars: Tom Vera, Tom Sizemore, Lee Majors", "https://image.tmdb.org/t/p/w500/7p0O4mKYLIhU2E5Zcq9Z3vOZ4e9.jpg", new DateTime(2021, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Narco Sub", (short)0 },
                    { 7, "When LeBron and his young son Dom are trapped in a digital space by a rogue A.I., LeBron must get them home safe by leading Bugs, Lola Bunny and the whole gang of notoriously undisciplined Looney Tunes to victory over the A.I.'s digitized champions on the court. It's Tunes versus Goons in the highest-stakes challenge of his life.", "https://image.tmdb.org/t/p/w500/5bFK5d3mVTAvBCXi5NPWH0tYjKl.jpg", new DateTime(2021, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Space Jam: A New Legacy", (short)0 },
                    { 6, "Natasha Romanoff, also known as Black Widow, confronts the darker parts of her ledger when a dangerous conspiracy with ties to her past arises. Pursued by a force that will stop at nothing to bring her down, Natasha must deal with her history as a spy and the broken relationships left in her wake long before she became an Avenger.", "https://image.tmdb.org/t/p/w500/qAZ0pzat24kLdO3o8ejmbLxyOac.jpg", new DateTime(2021, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black Widow", (short)0 },
                    { 5, "A rooster and his fowl partner embark on a dangerous trip to the Congo to recover their stolen eggs from a group of Russian goons.", "https://image.tmdb.org/t/p/w500/wrlQnKHLCBheXMNWotyr5cVDqNM.jpg", new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Eggs Run", (short)0 },
                    { 4, "Ryder and the pups are called to Adventure City to stop Mayor Humdinger from turning the bustling metropolis into a state of chaos...", "https://image.tmdb.org/t/p/w500/ic0intvXZSfBlYPIvWXpU1ivUCO.jpg", new DateTime(2021, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "PAW Patrol: The Movie", (short)0 },
                    { 3, "Dr. Lily Houghton enlists the aid of wisecracking skipper Frank Wolff to take her down the Amazon in his dilapidated boat. Together, they search for an ancient tree that holds the power to heal – a discovery that will change the future of medicine.", "https://image.tmdb.org/t/p/w500/9dKCd55IuTT5QRs989m9Qlb7d2B.jpg", new DateTime(2021, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jungle Cruise", (short)0 },
                    { 2, "A devastated husband vows to bring justice to the people responsible for his wife's death while protecting the only family he has left, his daughter.", "https://image.tmdb.org/t/p/w500/cP7odDzzFBD9ycxj2laTeFWGLjD.jpg", new DateTime(2021, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sweet Girl", (short)0 },
                    { 1, "Supervillains Harley Quinn, Bloodsport, Peacemaker and a collection of nutty cons at Belle Reve prison join the super-secret, super-shady Task Force X as they are dropped off at the remote, enemy-infused island of Corto Maltese.", "https://image.tmdb.org/t/p/w500/iXbWpCkIauBMStSTUT9v4GXvdgH.jpg", new DateTime(2021, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Suicide Squad", (short)0 },
                    { 11, "Whilst vacationing in Greece, Beckett, becomes the target of a manhunt after a devastating car accident forces him to run for his life across the country to clear his name but tensions escalate as the authorities close in and political unrest mounts which makes Beckett fall even deeper into a dangerous web of conspiracy.", "https://image.tmdb.org/t/p/w500/fBJducGBcmrcIOQdhm4BUBNDiMu.jpg", new DateTime(2021, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beckett", (short)0 },
                    { 50, "Jedi Knights Qui-Gon Jinn and Obi-Wan Kenobi set out to stop the Trade Federation from invading Naboo. While travelling, they come across a gifted boy, Anakin, and learn that the Sith have returned.", "https://play-lh.googleusercontent.com/sR1pzOxnF50WLR3vUqXYFvY01_tLD4XPn1RDHf0Xh-W04Vek_3iiZ98U7Db2JcmrqS8", new DateTime(1999, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Phantom Menace (Episode I)", (short)0 },
                    { 24, "Spanning the years 1945 to 1955, a chronicle of the fictional Italian-American Corleone crime family. When organized crime family patriarch, Vito Corleone barely survives an attempt on his life, his youngest son, Michael steps in to take care of the would-be killers, launching a campaign of bloody revenge.", "https://image.tmdb.org/t/p/w500/3bhkrj58Vtu7enYsRolD1fZdja1.jpg", new DateTime(1972, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Godfather", (short)0 },
                    { 26, "The final part of the film adaption of the erotic romance novel Gabriel's Inferno written by an anonymous Canadian author under the pen name Sylvain Reynard.", "https://image.tmdb.org/t/p/w500/fYtHxTxlhzD4QWfEbrC1rypysSD.jpg", new DateTime(2020, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gabriel's Inferno Part III", (short)0 },
                    { 48, "Rey seeks to learn the ways of the Jedi under Luke Skywalker, its remaining member, to reinvigorate the Resistance's war against the First Order.", "https://i.pinimg.com/originals/f4/5a/ea/f45aea75f65c0feb5cbe168f17a9a087.jpg", new DateTime(2017, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Last Jedi (Episode VIII)", (short)0 },
                    { 47, "A new order threatens to destroy the New Republic. Finn, Rey and Poe, backed by the Resistance and the Republic, must find a way to stop them and find Luke, the last surviving Jedi.", "https://m.media-amazon.com/images/M/MV5BOTAzODEzNDAzMl5BMl5BanBnXkFtZTgwMDU1MTgzNzE@._V1_.jpg", new DateTime(2015, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Force Awakens (Episode VII)", (short)0 },
                    { 46, "Luke Skywalker attempts to bring his father back to the light side of the Force. At the same time, the rebels hatch a plan to destroy the second Death Star.", "https://m.media-amazon.com/images/I/91LlN7J+Z9L._SL1500_.jpg", new DateTime(1983, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: Return of the Jedi (Episode VI)", (short)0 },
                    { 45, "After Princess Leia, the leader of the Rebel Alliance, is held hostage by Darth Vader, Luke and Han Solo must free her and destroy the powerful weapon created by the Galactic Empire.", "https://kbimages1-a.akamaihd.net/538b1473-6d45-47f4-b16e-32a0a6ba7f9a/1200/1200/False/star-wars-episode-iv-a-new-hope-3.jpg", new DateTime(1997, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: A New Hope (Episode IV)", (short)0 },
                    { 44, "A wanted fugitive mysteriously surrenders himself to the FBI and offers to help them capture deadly criminals. His sole condition is that he will work only with the new profiler, Elizabeth Keen.", "https://static.wikia.nocookie.net/blacklist/images/5/57/Season_7_Poster.jpg", new DateTime(2013, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Blacklist", (short)0 },
                    { 43, "Madison is paralyzed by shocking visions of grisly murders, and her torment worsens as she discovers that these waking dreams are in fact terrifying realities.", "https://image.tmdb.org/t/p/w500/dGv2BWjzwAz6LB8a8JeRIZL8hSz.jpg", new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malignant", (short)0 },
                    { 42, "Cinderella, an orphaned girl with an evil stepmother, has big dreams and with the help of her Fabulous Godmother, she perseveres to make them come true.", "https://image.tmdb.org/t/p/w500/clDFqATL4zcE7LzUwkrVj3zHvk7.jpg", new DateTime(2021, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cinderella", (short)0 },
                    { 41, "An off-duty SAS soldier, Tom Buckingham, must thwart a terror attack on a train running through the Channel Tunnel. As the action escalates on the train, events transpire in the corridors of power that may make the difference as to whether Buckingham and the civilian passengers make it out of the tunnel alive.", "https://image.tmdb.org/t/p/w500/6Y9fl8tD1xtyUrOHV2MkCYTpzgi.jpg", new DateTime(2021, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "SAS: Red Notice", (short)0 },
                    { 40, "The Blind Man has been hiding out for several years in an isolated cabin and has taken in and raised a young girl orphaned from a devastating house fire. Their quiet life together is shattered when a group of criminals kidnap the girl, forcing the Blind Man to leave his safe haven to save her.", "https://image.tmdb.org/t/p/w500/hRMfgGFRAZIlvwVWy8DYJdLTpvN.jpg", new DateTime(2021, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Don't Breathe 2", (short)0 },
                    { 39, "Female adventurer Parker joins a crew of male trophy hunters in a remote wilderness park. Their goal: slaughter genetically recreated dinosaurs for sport using rifles, arrows, and grenades. After their guide is killed by raptors, the team tries to escape the park – but the hunters quickly become the hunted. Even worse, the park’s manager suspects Parker of being a spy and sends a hit squad after her. This battle’s about to become primitive!", "https://image.tmdb.org/t/p/w500/CGJAj5kNWQZypNgUSTTQrFlnG.jpg", new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jurassic Hunt", (short)0 },
                    { 25, "Professor Gabriel Emerson finally learns the truth about Julia Mitchell's identity, but his realization comes a moment too late. Julia is done waiting for the well-respected Dante specialist to remember her and wants nothing more to do with him. Can Gabriel win back her heart before she finds love in another's arms?", "https://image.tmdb.org/t/p/w500/x5o8cLZfEXMoZczTYWLrUo1P7UJ.jpg", new DateTime(2020, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gabriel's Inferno Part II", (short)0 },
                    { 38, "Darth Vader is adamant about turning Luke Skywalker to the dark side. Master Yoda trains Luke to become a Jedi Knight while his friends try to fend off the Imperial fleet.", "https://images.penguinrandomhouse.com/cover/9780345320223", new DateTime(1980, 5, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Star Wars: The Empire Strikes Back (Episode V)", (short)0 },
                    { 36, "After 8-year-old So-won narrowly survives a brutal sexual assault, her family labors to help her heal while coping with their own rage and grief.", "https://image.tmdb.org/t/p/w500/x9yjkm9gIz5qI5fJMUTfBnWiB2o.jpg", new DateTime(2013, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hope", (short)0 },
                    { 35, "A supernatural tale set on death row in a Southern prison, where gentle giant John Coffey possesses the mysterious power to heal people's ailments. When the cell block's head guard, Paul Edgecomb, recognizes Coffey's miraculous gift, he tries desperately to help stave off the condemned man's execution.", "https://image.tmdb.org/t/p/w500/velWPhVMQeQKcxggNEU8YmIo52R.jpg", new DateTime(1999, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Green Mile", (short)0 },
                    { 34, "All unemployed, Ki-taek's family takes peculiar interest in the wealthy and glamorous Parks for their livelihood until they get entangled in an unexpected incident.", "https://image.tmdb.org/t/p/w500/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg", new DateTime(2019, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Parasite", (short)0 },
                    { 33, "Born free in the American West, Black Beauty is a horse rounded up and brought to Birtwick Stables, where she meets spirited teenager Jo Green. The two forge a bond that carries Beauty through the different chapters, challenges and adventures.", "https://image.tmdb.org/t/p/w500/5ZjMNJJabwBEnGVQoR2yoMEL9um.jpg", new DateTime(2020, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black Beauty", (short)0 },
                    { 32, "A young girl, Chihiro, becomes trapped in a strange new world of spirits. When her parents undergo a mysterious transformation, she must call upon the courage she never knew she had to free her family.", "https://image.tmdb.org/t/p/w500/39wmItIWsg5sZMyRUHLkWBcuVCM.jpg", new DateTime(2001, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spirited Away", (short)0 },
                    { 31, "Just as Tessa's life begins to become unglued, nothing is what she thought it would be. Not her friends nor her family. The only person that she should be able to rely on is Hardin, who is furious when he discovers the massive secret that she's been keeping. Before Tessa makes the biggest decision of her life, everything changes because of revelations about her family.", "https://image.tmdb.org/t/p/w500/oOZITZodAja6optBgLh8ZZrgzbb.jpg", new DateTime(2021, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "After We Fell", (short)0 },
                    { 30, "High schoolers Mitsuha and Taki are complete strangers living separate lives. But one night, they suddenly switch places. Mitsuha wakes up in Taki’s body, and he in hers. This bizarre occurrence continues to happen randomly, and the two must adjust their lives around each other.", "https://image.tmdb.org/t/p/w500/q719jXXEzOoYaps6babgKnONONX.jpg", new DateTime(2016, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Your Name.", (short)0 },
                    { 29, "In the continuing saga of the Corleone crime family, a young Vito Corleone grows up in Sicily and in 1910s New York. In the 1950s, Michael Corleone attempts to expand the family business into Las Vegas, Hollywood and Cuba.", "https://image.tmdb.org/t/p/w500/hek3koDUyRQk7FIhPXsa6mT2Zc3.jpg", new DateTime(1974, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Godfather: Part II", (short)0 }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Overview", "PosterPath", "ReleaseDate", "Title", "Type" },
                values: new object[,]
                {
                    { 28, "The true story of how businessman Oskar Schindler saved over a thousand Jewish lives from the Nazis while they worked as slaves in his factory during World War II.", "https://image.tmdb.org/t/p/w500/sF1U4EUQS8YHUYjNl3pMGNIQyr0.jpg", new DateTime(1993, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Schindler's List", (short)0 },
                    { 27, "An intriguing and sinful exploration of seduction, forbidden love, and redemption, Gabriel's Inferno is a captivating and wildly passionate tale of one man's escape from his own personal hell as he tries to earn the impossible--forgiveness and love.", "https://image.tmdb.org/t/p/w500/oyG9TL7FcRP4EZ9Vid6uKzwdndz.jpg", new DateTime(2020, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gabriel's Inferno", (short)0 },
                    { 37, "The defense and the prosecution have rested and the jury is filing into the jury room to decide if a young Spanish-American is guilty or innocent of murdering his father. What begins as an open and shut case soon becomes a mini-drama of each of the jurors' prejudices and preconceptions about the trial, the accused, and each other.", "https://image.tmdb.org/t/p/w500/e02s4wmTAExkKg0yF4dEG98ZRpK.jpg", new DateTime(1957, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "12 Angry Men", (short)0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, new DateTime(2021, 9, 16, 20, 26, 19, 840, DateTimeKind.Local).AddTicks(5446), "Admin", "Admin", new byte[] { 177, 81, 118, 210, 216, 112, 98, 162, 61, 215, 196, 226, 222, 2, 253, 57, 76, 62, 42, 142, 28, 192, 19, 60, 200, 219, 17, 52, 192, 45, 106, 214, 177, 144, 80, 130, 254, 58, 240, 210, 114, 206, 47, 23, 135, 72, 139, 101, 133, 239, 236, 51, 19, 51, 93, 116, 68, 180, 147, 72, 145, 53, 161, 159 }, new byte[] { 242, 162, 224, 204, 218, 47, 157, 26, 106, 118, 46, 11, 126, 180, 215, 244, 245, 77, 62, 131, 246, 70, 201, 169, 134, 141, 124, 33, 83, 78, 235, 236, 162, 129, 55, 118, 19, 241, 63, 233, 109, 179, 221, 58, 180, 167, 234, 155, 201, 140, 50, 210, 78, 21, 90, 192, 12, 111, 34, 78, 48, 142, 211, 106, 59, 154, 34, 200, 75, 150, 150, 215, 237, 169, 180, 203, 134, 175, 82, 14, 253, 130, 91, 233, 207, 31, 96, 64, 253, 73, 223, 25, 133, 247, 21, 76, 239, 156, 104, 235, 128, 202, 116, 147, 41, 213, 152, 9, 11, 121, 148, 114, 38, 21, 72, 240, 118, 159, 48, 176, 41, 148, 102, 107, 78, 252, 159, 13 }, "admin" });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "MovieId", "UserId", "Value" },
                values: new object[,]
                {
                    { 100, 100, 1, 4.0999999999999996 },
                    { 26, 26, 1, 3.8999999999999999 },
                    { 27, 27, 1, 4.7000000000000002 },
                    { 28, 28, 1, 4.5999999999999996 },
                    { 29, 29, 1, 4.5999999999999996 },
                    { 30, 30, 1, 4.5999999999999996 },
                    { 31, 31, 1, 4.5 },
                    { 32, 32, 1, 4.4000000000000004 },
                    { 33, 33, 1, 4.2999999999999998 },
                    { 34, 34, 1, 4.2000000000000002 },
                    { 35, 35, 1, 4.0999999999999996 },
                    { 36, 36, 1, 4.0999999999999996 },
                    { 37, 37, 1, 4.0999999999999996 },
                    { 38, 38, 1, 4.0 },
                    { 39, 39, 1, 3.8999999999999999 },
                    { 40, 40, 1, 4.7000000000000002 },
                    { 41, 41, 1, 4.5999999999999996 },
                    { 42, 42, 1, 4.5999999999999996 },
                    { 43, 43, 1, 4.5999999999999996 },
                    { 44, 44, 1, 4.5 },
                    { 45, 45, 1, 4.4000000000000004 },
                    { 46, 46, 1, 4.2999999999999998 },
                    { 25, 25, 1, 4.0 },
                    { 47, 47, 1, 4.2000000000000002 },
                    { 24, 24, 1, 4.0999999999999996 },
                    { 22, 22, 1, 4.0999999999999996 },
                    { 1, 1, 1, 4.5999999999999996 },
                    { 2, 2, 1, 4.5 },
                    { 3, 3, 1, 4.5 },
                    { 4, 4, 1, 4.5 },
                    { 5, 5, 1, 4.4000000000000004 },
                    { 6, 6, 1, 4.3499999999999996 },
                    { 7, 7, 1, 4.2999999999999998 },
                    { 8, 8, 1, 4.2000000000000002 },
                    { 9, 9, 1, 4.2000000000000002 },
                    { 10, 10, 1, 4.2000000000000002 },
                    { 11, 11, 1, 4.2000000000000002 },
                    { 12, 12, 1, 4.0999999999999996 },
                    { 13, 13, 1, 4.0999999999999996 },
                    { 14, 14, 1, 4.7000000000000002 },
                    { 15, 15, 1, 4.5999999999999996 },
                    { 16, 16, 1, 4.5999999999999996 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "MovieId", "UserId", "Value" },
                values: new object[,]
                {
                    { 17, 17, 1, 4.5999999999999996 },
                    { 18, 18, 1, 4.5 },
                    { 19, 19, 1, 4.4000000000000004 },
                    { 20, 20, 1, 4.2999999999999998 },
                    { 21, 21, 1, 4.2000000000000002 },
                    { 23, 23, 1, 4.0999999999999996 },
                    { 49, 49, 1, 4.0999999999999996 },
                    { 48, 48, 1, 4.0999999999999996 },
                    { 51, 51, 1, 4.0 },
                    { 77, 77, 1, 4.0 },
                    { 78, 78, 1, 3.8999999999999999 },
                    { 79, 79, 1, 4.7000000000000002 },
                    { 80, 80, 1, 4.5999999999999996 },
                    { 81, 81, 1, 4.5999999999999996 },
                    { 82, 82, 1, 4.5999999999999996 },
                    { 83, 83, 1, 4.5 },
                    { 84, 84, 1, 4.4000000000000004 },
                    { 85, 85, 1, 4.2999999999999998 },
                    { 86, 86, 1, 4.2000000000000002 },
                    { 50, 50, 1, 4.0999999999999996 },
                    { 87, 87, 1, 4.0999999999999996 },
                    { 89, 89, 1, 4.0999999999999996 },
                    { 90, 90, 1, 4.0 },
                    { 91, 91, 1, 3.8999999999999999 },
                    { 92, 92, 1, 4.7000000000000002 },
                    { 93, 93, 1, 4.5999999999999996 },
                    { 94, 94, 1, 4.5999999999999996 },
                    { 95, 95, 1, 4.5999999999999996 },
                    { 96, 96, 1, 4.5 },
                    { 97, 97, 1, 4.4000000000000004 },
                    { 98, 98, 1, 4.2999999999999998 },
                    { 88, 88, 1, 4.0999999999999996 },
                    { 75, 75, 1, 4.0999999999999996 },
                    { 76, 76, 1, 4.0999999999999996 },
                    { 73, 73, 1, 4.2000000000000002 },
                    { 52, 52, 1, 3.8999999999999999 },
                    { 53, 53, 1, 4.7000000000000002 },
                    { 54, 54, 1, 4.5999999999999996 },
                    { 55, 55, 1, 4.5999999999999996 },
                    { 56, 56, 1, 4.5999999999999996 },
                    { 57, 57, 1, 4.5 },
                    { 58, 58, 1, 4.4000000000000004 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "MovieId", "UserId", "Value" },
                values: new object[,]
                {
                    { 59, 59, 1, 4.2999999999999998 },
                    { 74, 74, 1, 4.0999999999999996 },
                    { 61, 61, 1, 4.0999999999999996 },
                    { 60, 60, 1, 4.2000000000000002 },
                    { 63, 63, 1, 4.0999999999999996 },
                    { 64, 64, 1, 4.0 },
                    { 65, 65, 1, 3.8999999999999999 },
                    { 66, 66, 1, 4.7000000000000002 },
                    { 67, 67, 1, 4.5999999999999996 },
                    { 68, 68, 1, 4.5999999999999996 },
                    { 69, 69, 1, 4.5999999999999996 },
                    { 70, 70, 1, 4.5 },
                    { 71, 71, 1, 4.4000000000000004 },
                    { 72, 72, 1, 4.2999999999999998 },
                    { 62, 62, 1, 4.0999999999999996 },
                    { 99, 99, 1, 4.2000000000000002 }
                });

            migrationBuilder.InsertData(
                table: "ActorMovie",
                columns: new[] { "ActorsId", "MoviesId" },
                values: new object[,]
                {
                { 1, 1 },
                { 2, 1 },
                { 3, 1 },
                { 4, 2 },
                { 5, 2 },
                { 6, 2 },
                { 5, 3 },
                { 7, 3 },
                { 8, 3 },
                { 9, 4 },
                { 10, 4 },
                { 11, 4 },
                { 12, 5 },
                { 13, 5 },
                { 14, 5 },
                { 15, 6 },
                { 16, 6 },
                { 17, 6 },
                { 18, 7 },
                { 19, 7 },
                { 20, 7 },
                { 21, 8 },
                { 22, 8 },
                { 23, 8 },
                { 24, 9 },
                { 25, 9 },
                { 26, 9 },
                { 18, 10 },
                { 20, 10 },
                { 27, 10 },
                { 28, 11 },
                { 29, 11 },
                { 30, 11 },
                { 31, 12 },
                { 32, 12 },
                { 33, 12 },
                { 34, 13 },
                { 35, 13 },
                { 36, 13 },
                { 37, 14 },
                { 37, 15 },
                { 38, 15 },
                { 39, 15 },
                { 40, 16 },
                { 41, 16 },
                { 42, 16 },
                { 43, 17 },
                { 44, 17 },
                { 45, 17 },
                { 46, 18 },
                { 47, 18 },
                { 48, 18 },
                { 50, 19 },
                { 51, 19 },
                { 52, 19 },
                { 37, 20 },
                { 53, 20 },
                { 54, 20 },
                { 37, 21 },
                { 56, 22 },
                { 57, 22 },
                { 58, 22 },
                { 59, 23 },
                { 60, 23 },
                { 61, 23 },
                { 62, 24 },
                { 63, 24 },
                { 64, 24 },
                { 65, 25 },
                { 66, 25 },
                { 67, 25 },
                { 68, 26 },
                { 69, 26 },
                { 70, 26 },

                { 1, 27 },
                { 2, 27 },
                { 3, 27 },
                { 4, 28 },
                { 5, 28 },
                { 6, 28 },
                { 5, 29 },
                { 7, 29 },
                { 8, 29 },
                { 9, 30 },
                { 10, 30 },
                { 11, 30 },
                { 12, 31 },
                { 13, 31 },
                { 14, 31 },
                { 15, 32 },
                { 16, 32 },
                { 17, 32 },
                { 18, 33 },
                { 19, 34 },
                { 20, 35 },
                { 21, 36 },
                { 22, 36 },
                { 23, 36 },
                { 24, 37 },
                { 25, 37 },
                { 26, 37 },
                { 18, 38 },
                { 20, 39 },
                { 27, 39 },
                { 28, 40 },
                { 29, 40 },
                { 30, 40 },
                { 31, 41 },
                { 32, 41 },
                { 33, 41 },
                { 34, 42 },
                { 35, 42 },
                { 36, 42 },
                { 37, 43 },
                { 37, 44 },
                { 38, 44 },
                { 39, 44 },
                { 40, 45 },
                { 41, 45 },
                { 42, 45 },
                { 43, 46 },
                { 44, 46 },
                { 45, 46 },
                { 46, 47 },
                { 47, 47 },
                { 48, 47 },
                { 50, 48 },
                { 51, 48 },
                { 52, 48 },
                { 37, 49 },
                { 53, 49 },
                { 54, 49 },
                { 37, 50 },
                { 56, 51 },
                { 57, 51 },
                { 58, 51 },
                { 59, 52 },
                { 60, 52 },
                { 61, 52 },
                { 62, 53 },
                { 63, 53 },
                { 64, 53 },
                { 65, 54 },
                { 66, 54 },
                { 67, 54 },
                { 68, 55 },
                { 69, 55 },
                { 70, 55 },

                { 1, 56 },
                { 2, 56 },
                { 3, 56 },
                { 4, 57 },
                { 5, 57 },
                { 6, 57 },
                { 5, 58 },
                { 7, 58 },
                { 8, 58 },
                { 9, 59 },
                { 10, 59 },
                { 11, 59 },
                { 12, 60 },
                { 13, 60 },
                { 14, 60 },
                { 15, 61 },
                { 16, 61 },
                { 17, 61 },
                { 18, 62 },
                { 19, 62 },
                { 20, 62 },
                { 21, 63 },
                { 22, 63 },
                { 23, 63 },
                { 24, 64 },
                { 25, 64 },
                { 26, 64 },
                { 18, 65 },
                { 20, 65 },
                { 27, 65 },
                { 28, 66 },
                { 29, 66 },
                { 30, 66 },
                { 31, 67 },
                { 32, 67 },
                { 33, 67 },
                { 34, 68 },
                { 35, 68 },
                { 36, 68 },
                { 37, 69 },
                { 37, 70 },
                { 38, 71 },
                { 39, 71 },
                { 40, 72 },
                { 41, 72 },
                { 42, 72 },
                { 43, 73 },
                { 44, 73 },
                { 45, 74 },
                { 46, 75 },
                { 47, 75 },
                { 48, 75 },
                { 50, 76 },
                { 51, 76 },
                { 52, 76 },
                { 37, 77 },
                { 53, 77 },
                { 54, 77 },
                { 37, 78 },
                { 56, 79 },
                { 57, 80 },
                { 58, 81 },
                { 59, 82 },
                { 60, 82 },
                { 61, 82 },
                { 62, 83 },
                { 63, 83 },
                { 64, 84 },
                { 65, 85 },
                { 66, 85 },
                { 67, 86 },
                { 68, 87 },
                { 69, 87 },
                { 70, 88 },

                { 1, 89 },
                { 2, 89 },
                { 3, 89 },
                { 4, 90 },
                { 5, 91 },
                { 6, 92 },
                { 5, 92 },
                { 7, 93 },
                { 8, 94 },
                { 9, 94 },
                { 10, 94 },
                { 11, 95 },
                { 12, 95 },
                { 13, 95 },
                { 14, 95 },
                { 15, 96 },
                { 16, 96 },
                { 17, 96 },
                { 18, 97 },
                { 19, 97 },
                { 20, 97 },
                { 21, 98 },
                { 22, 98 },
                { 23, 89 },
                { 24, 99 },
                { 25, 99 },
                { 26, 99 },
                { 18, 100 },
                { 20, 100 },
                { 27, 100 },
                { 13, 101 },
                { 20, 101 }
            });

            migrationBuilder.InsertData(
                table: "Screenings",
                columns: new[] { "Id", "MovieId", "Name", "ScreeningDate", "SoldTickets" },
                values: new object[,]
                {
                    { 104, 101, "Screening 104", new DateTime(2015, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 22 },
                    { 106, 101, "Screening 106", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 105, 101, "Screening 105", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 103, 100, "Screening 103", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 101, 100, "Screening 101", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 28, 28, "Screening 28", new DateTime(2021, 9, 21, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1263), 4 },
                    { 29, 29, "Screening 29", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1267), 3 },
                    { 30, 30, "Screening 30", new DateTime(2015, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 31, 31, "Screening 31", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1272), 2 },
                    { 32, 32, "Screening 32", new DateTime(2021, 10, 15, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1275), 2 },
                    { 33, 33, "Screening 33", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1279), 2 },
                    { 34, 34, "Screening 34", new DateTime(2021, 10, 7, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1283), 4 },
                    { 35, 35, "Screening 35", new DateTime(2010, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 36, 36, "Screening 36", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 37, 37, "Screening 37", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1289), 7 },
                    { 38, 38, "Screening 38", new DateTime(2021, 10, 6, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1293), 6 },
                    { 39, 39, "Screening 39", new DateTime(2021, 9, 26, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1297), 5 },
                    { 40, 40, "Screening 40", new DateTime(2021, 9, 21, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1301), 4 },
                    { 41, 41, "Screening 41", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1305), 3 },
                    { 42, 42, "Screening 42", new DateTime(2015, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 43, 43, "Screening 43", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1310), 2 },
                    { 44, 44, "Screening 44", new DateTime(2021, 10, 15, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1314), 2 },
                    { 45, 45, "Screening 45", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1317), 2 },
                    { 46, 46, "Screening 46", new DateTime(2021, 10, 7, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1321), 4 },
                    { 47, 47, "Screening 47", new DateTime(2010, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 48, 48, "Screening 48", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 }
                });

            migrationBuilder.InsertData(
                table: "Screenings",
                columns: new[] { "Id", "MovieId", "Name", "ScreeningDate", "SoldTickets" },
                values: new object[,]
                {
                    { 27, 27, "Screening 27", new DateTime(2021, 9, 26, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1259), 5 },
                    { 26, 26, "Screening 26", new DateTime(2021, 10, 6, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1256), 6 },
                    { 25, 25, "Screening 25", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1252), 7 },
                    { 24, 24, "Screening 24", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 2, 2, "Screening 2", new DateTime(2021, 10, 6, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1163), 6 },
                    { 3, 3, "Screening 3", new DateTime(2021, 9, 26, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1180), 5 },
                    { 4, 4, "Screening 4", new DateTime(2021, 9, 21, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1185), 4 },
                    { 5, 5, "Screening 5", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1189), 3 },
                    { 6, 6, "Screening 6", new DateTime(2015, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, 7, "Screening 7", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1195), 2 },
                    { 8, 8, "Screening 8", new DateTime(2021, 10, 15, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1199), 2 },
                    { 9, 9, "Screening 9", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1203), 2 },
                    { 10, 10, "Screening 10", new DateTime(2021, 10, 7, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1207), 4 },
                    { 11, 11, "Screening 11", new DateTime(2010, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 49, 49, "Screening 49", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1327), 7 },
                    { 12, 12, "Screening 12", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 14, 14, "Screening 14", new DateTime(2021, 10, 6, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1217), 6 },
                    { 15, 15, "Screening 15", new DateTime(2021, 9, 26, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1221), 5 },
                    { 16, 16, "Screening 16", new DateTime(2021, 9, 21, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1225), 4 },
                    { 17, 17, "Screening 17", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1229), 3 },
                    { 18, 18, "Screening 18", new DateTime(2015, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 19, 19, "Screening 19", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1234), 2 },
                    { 20, 20, "Screening 20", new DateTime(2021, 10, 15, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1238), 2 },
                    { 21, 21, "Screening 21", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1242), 2 },
                    { 22, 22, "Screening 22", new DateTime(2021, 10, 7, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1246), 4 },
                    { 23, 23, "Screening 23", new DateTime(2010, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 13, 13, "Screening 13", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1213), 7 },
                    { 50, 50, "Screening 50", new DateTime(2021, 10, 6, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1331), 6 },
                    { 51, 51, "Screening 51", new DateTime(2021, 9, 26, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1334), 5 },
                    { 52, 52, "Screening 52", new DateTime(2021, 9, 21, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1338), 4 },
                    { 79, 79, "Screening 79", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1423), 2 },
                    { 80, 80, "Screening 80", new DateTime(2021, 10, 15, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1427), 2 },
                    { 81, 81, "Screening 81", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1431), 2 },
                    { 82, 82, "Screening 82", new DateTime(2021, 10, 7, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1435), 4 },
                    { 83, 83, "Screening 83", new DateTime(2010, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 84, 84, "Screening 84", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 85, 85, "Screening 85", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1441), 7 },
                    { 86, 86, "Screening 86", new DateTime(2021, 10, 6, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1445), 6 },
                    { 87, 87, "Screening 87", new DateTime(2021, 9, 26, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1448), 5 },
                    { 88, 88, "Screening 88", new DateTime(2021, 9, 21, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1452), 4 },
                    { 78, 78, "Screening 78", new DateTime(2015, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 89, 89, "Screening 89", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1456), 3 }
                });

            migrationBuilder.InsertData(
                table: "Screenings",
                columns: new[] { "Id", "MovieId", "Name", "ScreeningDate", "SoldTickets" },
                values: new object[,]
                {
                    { 91, 91, "Screening 91", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1461), 2 },
                    { 92, 92, "Screening 92", new DateTime(2021, 10, 15, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1465), 2 },
                    { 93, 93, "Screening 93", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1468), 2 },
                    { 94, 94, "Screening 94", new DateTime(2021, 10, 7, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1472), 4 },
                    { 95, 95, "Screening 95", new DateTime(2010, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 96, 96, "Screening 96", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 97, 97, "Screening 97", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1478), 2 },
                    { 98, 98, "Screening 98", new DateTime(2021, 10, 7, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1481), 4 },
                    { 99, 99, "Screening 99", new DateTime(2010, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 100, 100, "Screening 100", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 90, 90, "Screening 90", new DateTime(2015, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 102, 100, "Screening 102", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 77, 77, "Screening 77", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1419), 3 },
                    { 75, 75, "Screening 75", new DateTime(2021, 9, 26, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1411), 5 },
                    { 53, 53, "Screening 53", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1342), 3 },
                    { 54, 54, "Screening 54", new DateTime(2015, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 55, 55, "Screening 55", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1347), 2 },
                    { 56, 56, "Screening 56", new DateTime(2021, 10, 15, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1352), 2 },
                    { 57, 57, "Screening 57", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1355), 2 },
                    { 58, 58, "Screening 58", new DateTime(2021, 10, 7, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1359), 4 },
                    { 59, 59, "Screening 59", new DateTime(2010, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 60, 60, "Screening 60", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 61, 61, "Screening 61", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1365), 7 },
                    { 62, 62, "Screening 62", new DateTime(2021, 10, 6, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1369), 6 },
                    { 76, 76, "Screening 76", new DateTime(2021, 9, 21, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1415), 4 },
                    { 63, 63, "Screening 63", new DateTime(2021, 9, 26, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1372), 5 },
                    { 65, 65, "Screening 65", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1381), 3 },
                    { 66, 66, "Screening 66", new DateTime(2015, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 67, 67, "Screening 67", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1385), 2 },
                    { 68, 68, "Screening 68", new DateTime(2021, 10, 15, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1389), 2 },
                    { 69, 69, "Screening 69", new DateTime(2021, 9, 17, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1393), 2 },
                    { 70, 70, "Screening 70", new DateTime(2021, 10, 7, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1397), 4 },
                    { 71, 71, "Screening 71", new DateTime(2010, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 72, 72, "Screening 72", new DateTime(2012, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21 },
                    { 73, 73, "Screening 73", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1402), 7 },
                    { 74, 74, "Screening 74", new DateTime(2021, 10, 6, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1407), 6 },
                    { 64, 64, "Screening 64", new DateTime(2021, 9, 21, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(1377), 4 },
                    { 1, 1, "Screening 1", new DateTime(2021, 10, 16, 20, 26, 19, 844, DateTimeKind.Local).AddTicks(749), 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MoviesId",
                table: "ActorMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_MovieId",
                table: "Screenings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ScreeningId",
                table: "Tickets",
                column: "ScreeningId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");

            string getTop10MoviesWithMostRatings = @"CREATE PROCEDURE getTop10MoviesWithMostRatings
                                                     AS
                                                     BEGIN
	                                                    SELECT TOP(10) m.Id as MovieId, m.Title as MovieName, COUNT(r.Value) AS NumberOfRatings, AVG(r.Value) AS AverageRating
                                                        FROM Movies m, Ratings r
                                                        WHERE m.Id = r.MovieId AND TYPE = 0
                                                        GROUP BY m.Id, m.Title
                                                        ORDER BY AVG(r.Value) DESC
                                                     END;";
            string getTop10MoviesWithMostScreenings = @"CREATE PROCEDURE getTop10MoviesWithMostScreenings
                                                        @start_date Date,
                                                        @end_date Date
                                                        AS
                                                        BEGIN
	                                                        SELECT TOP(10) m.Id as MovieId, m.Title as MovieName, COUNT(s.Id) AS NumberOfScreenings
                                                            FROM Movies m, Screenings s
                                                            WHERE m.Id = s.MovieId AND m.Type = 0
                                                            AND s.ScreeningDate BETWEEN @start_date AND @end_date
                                                            GROUP BY m.Id, m.Title, s.Name
                                                            ORDER BY COUNT(s.Id) DESC
                                                        END;";

            string getMoviesWithMostSoldTicketsNoRating = @"CREATE PROCEDURE getMoviesWithMostSoldTicketsNoRating
                                                            AS
                                                            BEGIN
	                                                            SELECT m.Id as MovieId, m.Title as MovieName, s.Name as ScreeningName, s.SoldTickets as SoldTickets
                                                                FROM Movies m, Screenings s
                                                                WHERE (SELECT COUNT(*) 
	                                                                FROM Ratings r 
	                                                                WHERE r.MovieId = m.Id) = 0 
                                                                AND m.Id = s.MovieId
                                                                ORDER BY s.SoldTickets DESC
                                                            END;";

            migrationBuilder.Sql(getTop10MoviesWithMostRatings);
            migrationBuilder.Sql(getTop10MoviesWithMostScreenings);
            migrationBuilder.Sql(getMoviesWithMostSoldTicketsNoRating);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
