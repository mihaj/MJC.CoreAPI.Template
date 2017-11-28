using MJC.CoreAPI.Template.WebAPI.Core.Repositories;
using MJC.CoreAPI.Template.WebAPI.Persistence.Repositories;

namespace MJC.CoreAPI.Template.WebAPI.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DummyApiContext _context;
        public IDummyApiRepository Dummies { get; private set; }


        public UnitOfWork(DummyApiContext context)
        {
            _context = context;
            Dummies = new DummyApiRepository(_context);
        }

        public bool Complete()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
