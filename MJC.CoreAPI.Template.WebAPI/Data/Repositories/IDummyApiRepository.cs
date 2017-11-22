using MJC.CoreAPI.Template.WebAPI.Data.Entities;
using System.Collections.Generic;

namespace MJC.CoreAPI.Template.WebAPI.Data.Repositories
{
    public interface IDummyApiRepository
    {
        // Basic DB Operations
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool Save();

        //IQueryable<Country> GetCountires();
        IEnumerable<Dummy> GetDummies();
        Dummy GetDummy(int dummyId);
        bool DummyExists(string dummyName);
    }
}
