using Microsoft.EntityFrameworkCore;
using speezs.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speezs.Repositories.Base
{
	public class GenericRepository<T> where T : class
	{
		private SpeezsDbContext _context;

		public GenericRepository()
		{
			_context ??= new SpeezsDbContext();
		}

		public List<T> GetAll()
		{
			return _context.Set<T>().ToList();
		}
		public async Task<List<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}
		public async Task<List<T>> GetAllAsync(params string[] includeProperties)
		{
			IQueryable<T> query = _context.Set<T>();

			// Eagerly load the related entities specified in includeProperties
			foreach (var includeProperty in includeProperties)
			{
				query = query.Include(includeProperty);
			}

			return await query.ToListAsync();
		}

		public void Create(T entity)
		{
			_context.Add(entity);
			_context.SaveChanges();
		}

		public async Task<int> CreateAsync(T entity)
		{
			_context.Add(entity);
			return await _context.SaveChangesAsync();
		}

		public void Update(T entity)
		{
			var tracker = _context.Attach(entity);
			tracker.State = EntityState.Modified;
			_context.SaveChanges();
		}

		public async Task<int> UpdateAsync(T entity)
		{
			var tracker = _context.Attach(entity);
			tracker.State = EntityState.Modified;

			return await _context.SaveChangesAsync();
		}

		public bool Remove(T entity)
		{
			_context.Remove(entity);
			_context.SaveChanges();
			return true;
		}

		public async Task<bool> RemoveAsync(T entity)
		{
			_context.Remove(entity);
			await _context.SaveChangesAsync();
			return true;
		}

		public T GetById(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}
		public async Task<T> GetByIdAsync(int id, params string[] includes)
		{
			IQueryable<T> query = _context.Set<T>();

			// Dynamically include related entities if provided
			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
		}

		public T GetById(string code)
		{
			return _context.Set<T>().Find(code);
		}

		public async Task<T> GetByIdAsync(string code)
		{
			return await _context.Set<T>().FindAsync(code);
		}

		public T GetById(Guid code)
		{
			return _context.Set<T>().Find(code);
		}

		public async Task<T> GetByIdAsync(Guid code)
		{
			return await _context.Set<T>().FindAsync(code);
		}

	}
}
