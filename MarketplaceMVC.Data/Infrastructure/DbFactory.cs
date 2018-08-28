using MarketplaceMVC.Data.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ApplicationContext Init();
    }

    public class DbFactory : IDbFactory
    {
        private ApplicationContext _db;

        public ApplicationContext Init()
        {
            return _db ?? (_db = new ApplicationContext());
        }

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_db != null)
                {
                    _db.Dispose();
                    _db = null;
                }

                _disposed = true;
            }
        }
    }
}
