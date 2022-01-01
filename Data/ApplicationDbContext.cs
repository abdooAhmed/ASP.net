using e_c_Project.Models;
using e_c_Project.Models.Films;
using e_c_Project.Models.Series;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_c_Project.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        DbSet<serie> series { get; set; }
        public DbSet<film> films { set; get; }
        public DbSet<filmSeriesType> filmSeriesTypes { set; get; }
        public DbSet<filmtype> filmtypes { set; get; }
        public DbSet<serieType> serieTypes { set; get; }
        
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorToFilm> AuthorToFilms { get; set; }
        public DbSet<EpisodeServer> SeriesEpisodeServers { get; set; }
        public DbSet<Episode> SeriesEpisodes { get; set; }
        
        public DbSet<AuthorToSeries> AuthorToSeries { get; set; }
        public DbSet<FilmServer> filmServers { get; set; }
        public DbSet<Gender> genders;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<filmtype>().HasKey(ft => new { ft.filmId, ft.typeId });
            modelBuilder.Entity<filmtype>().HasOne(ft => ft.film).WithMany(f => f.filmtypes).HasForeignKey(ba => ba.filmId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<filmtype>().HasOne(ft => ft.type).WithMany(f => f.filmtypes).HasForeignKey(ba => ba.typeId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<serieType>().HasKey(ft => new { ft.serieId, ft.typeId });
            modelBuilder.Entity<serieType>().HasOne(ft => ft.serie).WithMany(f => f.serieTypes).HasForeignKey(ba => ba.serieId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<serieType>().HasOne(ft => ft.type).WithMany(f => f.serieTypes).HasForeignKey(ba => ba.typeId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuthorToFilm>().HasKey(ft => new { ft.filmId, ft.authorId });
            modelBuilder.Entity<AuthorToFilm>().HasOne(ft => ft.film).WithMany(f => f.AuthorToFilms).HasForeignKey(ba => ba.filmId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AuthorToFilm>().HasOne(ft => ft.Author).WithMany(f => f.AuthorToFilms).HasForeignKey(ba => ba.authorId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AuthorToSeries>().HasKey(ft => new { ft.serieId, ft.authorId });
            modelBuilder.Entity<AuthorToSeries>().HasOne(ft => ft.serie).WithMany(f => f.AuthorToSeries).HasForeignKey(ba => ba.serieId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<AuthorToSeries>().HasOne(ft => ft.Author).WithMany(f => f.AuthorToSeries).HasForeignKey(ba => ba.authorId).OnDelete(DeleteBehavior.Cascade);

           


            modelBuilder.Entity<Author>().Ignore(x => x.Image);
           

            modelBuilder.Entity<filmSeriesType>().HasData(
               new filmSeriesType { typeID = 1, type = "aomance" },
               new filmSeriesType { typeID = 2, type = "action" },
               new filmSeriesType { typeID = 3, type = "animation" },
               new filmSeriesType { typeID = 4, type = "drama" },
               new filmSeriesType { typeID = 5, type = "comedy" },
               new filmSeriesType { typeID = 6, type = "history" },
               new filmSeriesType { typeID = 7, type = "war" },
               new filmSeriesType { typeID = 8, type = "CV" },
               new filmSeriesType { typeID = 9, type = "mystery" },
               new filmSeriesType { typeID = 10, type = "murder" },
               new filmSeriesType { typeID = 11, type = "fiction" },
               new filmSeriesType { typeID = 12, type = "scary" },
               new filmSeriesType { typeID = 14, type = "sport" },
               new filmSeriesType { typeID = 15, type = "adventure" },
               new filmSeriesType { typeID = 16, type = "musical " },
               new filmSeriesType { typeID = 17, type = "documentary " }
           );

            modelBuilder.Entity<ApplicationUser>();
            modelBuilder.Entity<Gender>().HasData(
                new Gender { GenderId = 1, gender = "male" },
                new Gender { GenderId = 2, gender = "female" }
            );
        }
    }
}
