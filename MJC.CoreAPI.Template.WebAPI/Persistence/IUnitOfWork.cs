using MJC.CoreAPI.Template.WebAPI.Core.Repositories;

namespace MJC.CoreAPI.Template.WebAPI.Persistence
{
    public interface IUnitOfWork
    {
        IDummyApiRepository Dummies { get; }

        bool Complete();
    }
}
