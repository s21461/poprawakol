using Microsoft.EntityFrameworkCore;
using System;

namespace KolokwiumPoprawa.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<File> Files { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Team> Teams { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<File>(p =>
            {
                p.HasKey(e => e.FileID);
                p.Property(e => e.FileName).IsRequired().HasMaxLength(100);
                p.Property(e => e.FileExtension).IsRequired().HasMaxLength(4);
                p.Property(e => e.FileSize).IsRequired();
                p.HasOne(e => e.Team).WithMany(e => e.Files).HasForeignKey(e => e.TeamID).IsRequired();

                p.HasData(
                    new File { FileID = 1, FileName = "File1", FileExtension = "ext1", FileSize = 123, TeamID = 1 },
                    new File { FileID = 2, FileName = "File2", FileExtension = "ext1", FileSize = 334, TeamID = 2 },
                    new File { FileID = 3, FileName = "File3", FileExtension = "ext2", FileSize = 153, TeamID = 1 },
                    new File { FileID = 4, FileName = "File4", FileExtension = "ext2", FileSize = 653, TeamID = 2 },
                    new File { FileID = 5, FileName = "File5", FileExtension = "ext4", FileSize = 113, TeamID = 3 },
                    new File { FileID = 6, FileName = "File6", FileExtension = "ext2", FileSize = 53, TeamID = 4 },
                    new File { FileID = 7, FileName = "File7", FileExtension = "ext2", FileSize = 23, TeamID = 1 },
                    new File { FileID = 8, FileName = "File8", FileExtension = "ext1", FileSize = 1223, TeamID = 1 }
                    );

            });


            modelBuilder.Entity<Team>(p =>
            {
                p.HasKey(e => e.TeamID);
                p.Property(e => e.TeamName).IsRequired().HasMaxLength(50);
                p.Property(e => e.TeamDescription).HasMaxLength(500);
                p.HasOne(e => e.Organization).WithMany(e => e.Teams).HasForeignKey(e => e.OrganizationID).IsRequired();


                p.HasData(
                    new Team { TeamID = 1, TeamName = "Team Name 1", TeamDescription = "Desc 1", OrganizationID = 1 },
                    new Team { TeamID = 2, TeamName = "Team Name 2", TeamDescription = "Desc 2", OrganizationID = 1 },
                    new Team { TeamID = 3, TeamName = "Team Name 3", TeamDescription = "Desc 3", OrganizationID = 2 },
                    new Team { TeamID = 4, TeamName = "Team Name 4", TeamDescription = "Desc 4", OrganizationID = 2 }
                    );

            });


            modelBuilder.Entity<Organization>(p =>
            {
                p.HasKey(e => e.OrganizationID);
                p.Property(e => e.OrganizationName).IsRequired().HasMaxLength(100);
                p.Property(e => e.OrganizationDomain).HasMaxLength(50);

                p.HasData(
                        new Organization { OrganizationID = 1, OrganizationName = "OrganizationName 1", OrganizationDomain = "OrganizationDomain 1" },
                        new Organization { OrganizationID = 2, OrganizationName = "OrganizationName 2", OrganizationDomain = "OrganizationDomain 2" }
                    );
            });

            modelBuilder.Entity<Member>(p =>
            {
                p.HasKey(e => e.MemberID);
                p.Property(e => e.MemberName).IsRequired().HasMaxLength(20);
                p.Property(e => e.MemberSurname).IsRequired().HasMaxLength(50);
                p.Property(e => e.MemberNickName).HasMaxLength(20);
                p.HasOne(e => e.Organization).WithMany(e => e.Members).HasForeignKey(e => e.OrganizationID).IsRequired();

                p.HasData(
                    new Member { MemberID = 1, MemberName = "MemberName 1", MemberSurname = "MemberSurname 1", OrganizationID = 1 },
                    new Member { MemberID = 2, MemberName = "MemberName 2", MemberSurname = "MemberSurname 2", OrganizationID = 1 },
                    new Member { MemberID = 3, MemberName = "MemberName 3", MemberSurname = "MemberSurname 3", OrganizationID = 2 },
                    new Member { MemberID = 4, MemberName = "MemberName 4", MemberSurname = "MemberSurname 4", OrganizationID = 2 }
                    );
            });

            modelBuilder.Entity<Membership>(p =>
            {
                p.HasKey(e => new
                {
                    e.MemberID,
                    e.TeamID
                });
                p.Property(e => e.MembershipDate).IsRequired();
                p.HasOne(e => e.Team).WithMany(e => e.Memberships).HasForeignKey(e => e.TeamID).IsRequired().OnDelete(DeleteBehavior.NoAction);
                p.HasOne(e => e.Member).WithMany(e => e.Memberships).HasForeignKey(e => e.MemberID).IsRequired().OnDelete(DeleteBehavior.NoAction);

                p.HasData(
                    new Membership { MemberID = 1, TeamID = 1, MembershipDate = DateTime.Parse("2012-07-22") },
                    new Membership { MemberID = 2, TeamID = 1, MembershipDate = DateTime.Parse("2011-01-12") },
                    new Membership { MemberID = 3, TeamID = 2, MembershipDate = DateTime.Parse("2014-02-15") },
                    new Membership { MemberID = 4, TeamID = 3, MembershipDate = DateTime.Parse("2005-06-17") },
                    new Membership { MemberID = 3, TeamID = 3, MembershipDate = DateTime.Parse("2018-04-12") },
                    new Membership { MemberID = 2, TeamID = 4, MembershipDate = DateTime.Parse("2022-02-02") }
                    );
            });

        }
    }
}
