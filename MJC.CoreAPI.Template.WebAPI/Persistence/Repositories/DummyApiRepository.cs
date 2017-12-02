using MJC.CoreAPI.Template.WebAPI.Core.Entities;
using MJC.CoreAPI.Template.WebAPI.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MJC.CoreAPI.Template.WebAPI.Persistence.Repositories
{
    public class DummyApiRepository : IDummyApiRepository
    {
        private DummyApiContext _ctx;

        public DummyApiRepository(DummyApiContext ctx)
        {
            _ctx = ctx;
        }

        public void Add(Dummy dummy)
        {
            _ctx.Add(dummy);
        }

        public void Delete(Dummy dummy)
        {
            _ctx.Remove(dummy);
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

        public bool DummyExists(int dummyId)
        {
            return _ctx.Dummies.Any(o => o.Id == dummyId);
        }
    }
}
