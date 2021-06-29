using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Data
{
    public class PhoneBookDbContext : DbContext
    {
        //public FrusContext(
        //   DbContextOptions options,
        //   IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        //{
        //}
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
        {
        }
        //private readonly string _connectionString;
        public virtual DbSet<PhoneBookUserModel> PhoneBookUsers { get; set; }
        public virtual DbSet<ContactModel> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            PhoneBookUserModel[] _phoneBookUsersList = new PhoneBookUserModel[] {
                new PhoneBookUserModel { Id = 1, FirstName = "Edward", LastName = "Zeyn", ModifiedDate = DateTime.UtcNow, CreatedDate = DateTime.UtcNow },
                new PhoneBookUserModel { Id = 2, FirstName = "Simon", LastName = "Gi", ModifiedDate = DateTime.UtcNow, CreatedDate = DateTime.UtcNow },
                new PhoneBookUserModel { Id = 3, FirstName = "King", LastName = "Kinh", ModifiedDate = DateTime.UtcNow, CreatedDate = DateTime.UtcNow },
                new PhoneBookUserModel { Id = 4, FirstName = "Frek", LastName = "Lek", ModifiedDate = DateTime.UtcNow, CreatedDate = DateTime.UtcNow }
            };
            ContactModel[] _contactList = new ContactModel[] {
                new ContactModel { Id = 1, PhoneNumber = "0817771662",FirstName = "Sui", LastName = "Lui", ModifiedDate = DateTime.UtcNow, CreatedDate = DateTime.UtcNow, PhoneBookUserId = 1},//PhoneBookUser = _phoneBookUsersList.Where(phu => phu.Id == 1).FirstOrDefault() },
            new ContactModel { Id = 2, PhoneNumber = "0817819662", FirstName = "Jean", LastName = "Jut", ModifiedDate = DateTime.UtcNow, CreatedDate = DateTime.UtcNow, PhoneBookUserId = 2 },//, PhoneBookUser = _phoneBookUsersList.Where(phu => phu.Id == 2).FirstOrDefault() },
                new ContactModel { Id = 3, PhoneNumber = "0819182662",FirstName = "Sam", LastName = "Lam", ModifiedDate = DateTime.UtcNow, CreatedDate = DateTime.UtcNow, PhoneBookUserId = 3 },//, PhoneBookUser = _phoneBookUsersList.Where(phu => phu.Id == 3).FirstOrDefault() },
                new ContactModel { Id = 4, PhoneNumber = "0817718928",FirstName = "Oros", LastName = "Loros", ModifiedDate = DateTime.UtcNow, CreatedDate = DateTime.UtcNow, PhoneBookUserId = 4 }//, PhoneBookUser = _phoneBookUsersList.Where(phu => phu.Id == 4).FirstOrDefault() }
            };

            modelBuilder.Entity<PhoneBookUserModel>().HasData(_phoneBookUsersList);
            modelBuilder.Entity<ContactModel>().HasData(_contactList);

            base.OnModelCreating(modelBuilder);
        }
    }
}
