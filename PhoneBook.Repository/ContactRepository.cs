using Microsoft.EntityFrameworkCore;
using PhoneBook.Contracts;
using PhoneBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
    public class ContactRepository : IContactRepository
    {// The context
        private readonly PhoneBookDbContext _context;

        // The table
        private readonly DbSet<ContactModel> _table;


        // Initializes a new instance of the <see cref="UserRepository" /> class.
        // <param name="context">The context.</param>
        public ContactRepository(PhoneBookDbContext context)
        {
            _context = context;
            _table = _context.Set<ContactModel>();
        }
        public async Task<ContactModel> AddAsync(ContactModel item)
        {
            var entity = await _table.AddAsync(item);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task AddAsync(List<ContactModel> items)
        {
            await Task.Run(() => _table.AddRange(items));
        }

        public async Task DeleteAsync(ContactModel item)
        {
            _table.Attach(item);
            await Task.Run(() => {
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            });
        }

        public async Task<List<ContactModel>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<List<ContactModel>> GetAllAsync(Expression<Func<ContactModel, bool>> expression)
        {
            var orders = _table.Where(expression);
            return await orders.ToListAsync();
        }

        public async Task<ContactModel> GetByIdAsync(long id)
        {
            return await _table.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ContactModel item)
        {
            _table.Attach(item);
            await Task.Run(() => _context.Entry(item).State = EntityState.Modified);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(List<ContactModel> items)
        {
            await Task.Run(() =>
            {
                foreach (var item in items)
                {
                    _table.Attach(item);
                    _context.Entry(item).State = EntityState.Modified;
                }
                _context.SaveChanges();
            });
        }
    }
}
