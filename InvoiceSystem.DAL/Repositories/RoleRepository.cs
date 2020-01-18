using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.DAL.Repositories
{
    public class RoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public void Dispose()
        {
            _roleManager.Dispose();
        }

        public List<IdentityRole> GetAll()
        {
            return _roleManager.Roles.ToList();
        }

        public IdentityRole GetById(string id)
        {
            return _roleManager.FindById(id);
        }

        public void InsertEntity(IdentityRole entity)
        {
            _roleManager.Create(entity);
        }

        public void UpdateEntity(IdentityRole entity)
        {
            _roleManager.Update(entity);
        }
        public void DeleteEnity(IdentityRole identityRole)
        {
            _roleManager.Delete(identityRole);
        }
    }
}
