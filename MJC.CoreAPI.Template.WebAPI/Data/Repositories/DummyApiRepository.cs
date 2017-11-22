using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using MJC.CoreAPI.Template.WebAPI.Data.Entities;
using MJC.CoreAPI.Template.WebAPI.Data;
using MJC.CoreAPI.Template.WebAPI.Data.Repositories;

namespace MJC.CoreAPI.Template.WebAPI.Data.Repositories
{
    public class DummyApiRepository : IDummyApiRepository
    {
        private DummyApiContext _ctx;

        public DummyApiRepository(DummyApiContext ctx)
        {
            _ctx = ctx;
        }

        public void Add<T>(T entity) where T : class
        {
            _ctx.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _ctx.Remove(entity);
        }

        public bool Save()
        {
            try
            {
                if (_ctx.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Dummy> GetDummies()
        {
            return _ctx.Dummies.ToList();
        }

        public Dummy GetDummy(int dummyId)
        {
            return _ctx.Dummies.Where(p => p.Id == dummyId).FirstOrDefault();
        }

        public bool DummyExists(string dummyName)
        {
            return _ctx.Dummies.Any(o => o.Name == dummyName);
        }
    }
}
