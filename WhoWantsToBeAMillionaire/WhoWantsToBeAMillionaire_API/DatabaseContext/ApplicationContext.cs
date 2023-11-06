
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WhoWantsToBeAMillionaire_API.Models;

namespace WhoWantsToBeAMillionaire_API.DatabaseContext
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("project"));
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Scores> Scores { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Roles>().HasData(
                new Roles { role_id = 1, role_name = "Adminstrator" },
                new Roles { role_id = 2, role_name = "Player" }
            );
            modelBuilder.Entity<Users>()
                .HasOne(a => a.Role)
                .WithOne(a => a.User)
                .HasForeignKey<Roles>(c => c.role_id);

            modelBuilder.Entity<Users>().HasData(
                new Users { user_id = 1, username = "vu", name = "Dang Nguyen Khanh Vu", password = "vu1234!", role_id = 1 },
                new Users { user_id = 2, username = "quanla", name = "Le Anh Quan", password = "quan1234!", role_id = 2 },
                new Users { user_id = 3, username = "thaihh", name = "Huynh Hieu Thai", password = "thai1234!", role_id = 2 },
                new Users { user_id = 4, username = "quanpa", name = "Pham Anh Quan", password = "quanfpt1234!", role_id = 2 }
            );

            modelBuilder.Entity<Scores>().HasData(
                new Scores { score_id = 1, score = 10, user_id = 1 },
                new Scores { score_id = 2, score = 11, user_id = 2 },
                new Scores { score_id = 3, score = 30, user_id = 3 }
            );

            modelBuilder.Entity<Questions>().HasData(
                new Questions { question_id = 1, question_content = "Manchester is ?" },
                new Questions { question_id = 2, question_content = "Những thông tin này nói về ai ? BMW, showroom, Nhật Bản, Du học sinh, giấy phép lái xe B2." }
            );

            //modelBuilder.Entity<Answers>().HasKey(vf => new { vf.answer_id, vf.question_id });
            modelBuilder.Entity<Answers>().HasData(
                new Answers { answer_id = "A", answer_content = "Red", isCorrect = true, question_id = 1 },
                new Answers { answer_id = "B", answer_content = "Not Blue", isCorrect = false, question_id = 1 },
                new Answers { answer_id = "C", answer_content = "A", isCorrect = false, question_id = 1 },
                new Answers { answer_id = "D", answer_content = "C", isCorrect = false, question_id = 1 },

                new Answers { answer_id = "A", answer_content = "Lê Quân", isCorrect = false, question_id = 2 },
                new Answers { answer_id = "B", answer_content = "Lê Anh Quân", isCorrect = false, question_id = 2 },
                new Answers { answer_id = "C", answer_content = "Lê Quang", isCorrect = true, question_id = 2 },
                new Answers { answer_id = "D", answer_content = "Lê Quâng", isCorrect = false, question_id = 2 }
            );

        }
    }
}
