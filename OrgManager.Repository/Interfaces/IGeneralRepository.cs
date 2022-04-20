using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrgManager.Repository.Interfaces
{
    public interface IGeneralRepository
    {
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        void Delte<T>(T entity) where T: class;
        void DelteRange<T>(T[] entityArray) where T: class;
        Task<bool> SaveChangesAsync();
    }
}