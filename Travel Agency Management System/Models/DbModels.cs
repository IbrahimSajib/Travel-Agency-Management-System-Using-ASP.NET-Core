using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Travel_Agency_Management_System.Models
{
    public class Spot
    {
        public Spot()
        {
            this.BookingEntries = new List<BookingEntry>();
        }
        public int SpotId { get; set; }
        [Required,DisplayName("Spot Name")]
        public string SpotName { get; set; } = default!;
        //nev
        public virtual ICollection<BookingEntry> BookingEntries { get; set; }
    }
    public class Client
    {
        public Client()
        {
            this.BookingEntries = new List<BookingEntry>();
        }
        public int ClientId { get; set; }
        [Required, StringLength(50), Display(Name = "Client Name")]
        public string ClientName { get; set; } = default!;
        [Required, Column(TypeName = "date"), DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string? Image { get; set; } = default!;
        [DisplayName("IsMarried")]
        public bool IsMarried { get; set; }
        public virtual ICollection<BookingEntry> BookingEntries { get; set; }
    }
    public class BookingEntry
    {
        public int BookingEntryId { get; set; }
        [ForeignKey("Spot")]
        public int SpotId { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        //nev
        public virtual Spot Spot { get; set; } = default!;
        public virtual Client Client { get; set; } = default!;
    }
    public class TravelDbContext : DbContext
    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Spot> Spots { get; set; }
        public DbSet<BookingEntry> BookingEntries { get; set; }
    }
}
