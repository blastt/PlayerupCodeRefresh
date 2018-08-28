using MarketplaceMVC.Data.Infrastructure;
using MarketplaceMVC.Data.Repositories;
using MarketplaceMVC.Model.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceMVC.Data.Identity
{
    public interface IRoleStore : IRoleStore<Role, int>
    {
    }

    internal class RoleStore : IRoleStore
    {
        private IUnitOfWork _uow;
        private IRoleRepository _roleRepository;

        public RoleStore(IUnitOfWork uow, IRoleRepository roleRepository)
        {
            _uow = uow;
            _roleRepository = roleRepository;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _disposed;
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && _uow != null)
            {
                if (disposing)
                {
                    _uow = null;
                    _roleRepository = null;
                }
                _disposed = true;
            }
        }

        public async Task CreateAsync(Role role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Add(role);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Update(role);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            ThrowIfDisposed();
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Remove(role);
            await _uow.SaveChangesAsync();
        }

        public async Task<Role> FindByIdAsync(int roleId)
        {
            ThrowIfDisposed();
            return await _roleRepository.GetByIdAsync(roleId);
        }

        public async Task<Role> FindByNameAsync(string roleName)
        {
            ThrowIfDisposed();
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            return await _roleRepository.FindByNameAsync(roleName);
        }
    }
}
