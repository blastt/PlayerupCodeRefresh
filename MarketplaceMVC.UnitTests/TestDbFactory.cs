using MarketplaceMVC.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.UnitTests
{
    public interface ITestDbFactory : IDisposable
    {
        TestContext Init();
    }

    public class TestDbFactory : ITestDbFactory
    {
        private TestContext _db;

        public TestContext Init()
        {
            return _db ?? (_db = new TestContext());
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
