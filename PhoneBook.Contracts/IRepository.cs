using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Contracts
{
    // Interface IRepository
    // <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        // Adds the asynchronous.
        // <param name="item">The item.</param>
        // <returns>Task&lt;T&gt;.</returns>
        Task<T> AddAsync(T item);

        // Adds the asynchronous.
        // <param name="items">The items.</param>
        // <returns>Task.</returns>
        Task AddAsync(List<T> items);

        // Updates the asynchronous.
        // <param name="item">The item.</param>
        // <returns>Task.</returns>
        Task UpdateAsync(T item);

        // Updates the asynchronous.
        // <param name="items">The items.</param>
        // <returns>Task.</returns>
        Task UpdateAsync(List<T> items);
        // Deletes the asynchronous.
        // <returns>Task.</returns>ms);
        Task DeleteAsync(T item);
        // Saves the asynchronous.
        // <returns>Task.</returns>
        Task SaveAsync();

        // Gets all asynchronous.
        // <returns>Task&lt;List&lt;T&gt;&gt;.</returns>
        Task<List<T>> GetAllAsync();

        // Gets the by identifier asynchronous.
        // <param name="id">The identifier.</param>
        // <returns>Task&lt;T&gt;.</returns>
        Task<T> GetByIdAsync(long id);

        // Gets all asynchronous.
        // <param name="expression">The expression.</param>
        // <returns>Task&lt;List&lt;T&gt;&gt;.</returns>
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
    }
}
