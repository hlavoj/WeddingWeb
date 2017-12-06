using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TestSvWeb2
{
    public class WeddingDbContext : DbContext
    {
        DbSet<Wedding> Weddings { get; set; }
        DbSet<WeddingAttemp> WeddingAttemps { get; set; }
             
        public WeddingDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=testvojta222.database.windows.net;Database=Wedding;integrated security=false;persist security info=True;User ID=hlavoj;Password=Azure123");
            }
        }
    }

    [Table("Wedding")]
    public class Wedding
    {
        [Key]
        public int Id { get; set; }
        public string Identifikator { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public int InvitationType { get; set; }
        public int QrCodeOpend { get; set; }
        public int PhoneNumberCalled { get; set; }
        public string Questions { get; set; }
        
    }

    [Table("WeddingAttemp")]
    public class WeddingAttemp
    {
        [Key]
        public int Id { get; set; }
        public int IdWedding { get; set; }
        public DateTime OpenTime { get; set; }
        public int? Participation { get; set; }
        public int? Question1 { get; set; }
        public int? Question2 { get; set; }
        public int? Question3 { get; set; }
        public int? Question4 { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }

    }
}
