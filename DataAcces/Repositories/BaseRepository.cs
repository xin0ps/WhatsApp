using DataAcces.Context;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity, new()
    {
        private readonly WPDBContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepo()
        {
            _context = new WPDBContext();
            _dbSet = _context?.Set<T>();

        }

        public string Add(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Succesfully added!";

        }

        public string AddRange(ICollection<T> entities)
        {

            try
            {
                _dbSet.AddRange(entities);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Succesfully added!";
        }

        public ICollection<T>? GetAll()
        {
            return _dbSet.ToList<T>();
        }

        public T? GetById(int id)
        {
            return _dbSet.FirstOrDefault(p => p.Id == id);
        }

        public string Remove(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Succesfully deleted!";
        }

        public string Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Succesfully updated!";
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
