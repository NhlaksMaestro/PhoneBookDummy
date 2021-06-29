using Microsoft.EntityFrameworkCore;
using PhoneBook.Contracts;
using PhoneBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhoneBook.Repository
{
    public class PhoneBookUserRepository : IPhoneBookUserRepository
    {
        // The context
        private readonly PhoneBookDbContext _context;

        // The table
        private readonly DbSet<PhoneBookUserModel> _table;


        // Initializes a new instance of the <see cref="UserRepository" /> class.
        // <param name="context">The context.</param>
        public PhoneBookUserRepository(PhoneBookDbContext context)
        {
            _context = context;
            _table = _context.Set<PhoneBookUserModel>();
        }

        // save as an asynchronous operation.
        // <returns>A Task representing the asynchronous operation.</returns>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // add as an asynchronous operation.
        // <param name="item">The item.</param>
        // <returns>A Task&lt;UserRequest&gt; representing the asynchronous operation.</returns>
        public async Task<PhoneBookUserModel> AddAsync(PhoneBookUserModel item)
        {
            var entity = await _table.AddAsync(item);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        // add as an asynchronous operation.
        // <param name="items">The items.</param>
        // <returns>A Task representing the asynchronous operation.</returns>
        public async Task AddAsync(List<PhoneBookUserModel> items)
        {
            await Task.Run(() => _table.AddRange(items));
        }


        // get all as an asynchronous operation.
        // <param name="query">The query.</param>
        // <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<PhoneBookUserModel>> GetAllAsync(Expression<Func<PhoneBookUserModel, bool>> query)
        {
            var orders = _table.Include(user => user.Contacts).Where(query);
            return await orders.ToListAsync();
        }

        // get all as an asynchronous operation.
        // <returns>A Task&lt;List`1&gt; representing the asynchronous operation.</returns>
        public async Task<List<PhoneBookUserModel>> GetAllAsync()
        {
            return await _table.Include(user => user.Contacts).ToListAsync();
        }

        // get by identifier as an asynchronous operation.
        // <param name="id">The identifier.</param>
        // <returns>A Task&lt;UserRequest&gt; representing the asynchronous operation.</returns>
        public async Task<PhoneBookUserModel> GetByIdAsync(long id)
        {
            return await _table.FindAsync(id);
        }


        // update as an asynchronous operation.
        // <param name="item">The item.</param>
        // <returns>A Task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(PhoneBookUserModel item)
        {
            _table.Attach(item);
            await Task.Run(() => _context.Entry(item).State = EntityState.Modified);
            await _context.SaveChangesAsync();
        }


        // update as an asynchronous operation.
        // <param name="item">The item.</param>
        // <returns>A Task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(PhoneBookUserModel item)
        {
            _table.Attach(item);
            await Task.Run(() => {
                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();
            });
        }

        // update as an asynchronous operation.
        // <param name="items">The items.</param>
        // <returns>A Task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(List<PhoneBookUserModel> items)
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