using MJC.CoreAPI.Template.WebAPI.Core.Entities;
using System.Collections.Generic;

namespace MJC.CoreAPI.Template.WebAPI.Core.Repositories
{
    public interface IDummyApiRepository
    {
        IEnumerable<Dummy> GetDummies();
        Dummy GetDummy(int dummyId);
        bool DummyExists(string dummyName);
        void Add(Dummy category);
        void Delete(Dummy category);
    }
}
