using System.Collections.Generic;
using System.Linq;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;

namespace DotNetBay.Test.Storage
{
    public class EFMainRepositoryFactory : IRepositoryFactory
    {
        private List<EFMainRepository> repos = new List<EFMainRepository>();

        public void Dispose()
        {
            foreach (var repo in this.repos)
            {
                repo.Database.Delete();
            }
        }

        public IMainRepository CreateMainRepository()
        {
            var repo = new EFMainRepository();

            if (!this.repos.Any())
            {
                repo.Database.Delete();
            }

            this.repos.Add(repo);

            return repo;
        }
    }
}
