using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoiceSystem.DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InvoiceSystem.BLL.Services
{
    public class RoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }
        public IdentityRole GetRoleById(string id)
        {
            return _roleManager.FindById(id);
        }
        public void CreateRole(IdentityRole identityRole)
        {
            _roleManager.Create(identityRole);
        }
        public void UpdateRole(IdentityRole identityRole)
        {
            _roleManager.Update(identityRole);
        }
        public void DeleteRole(IdentityRole identityRole)
        {
            _roleManager.Delete(identityRole);
        }
    }
}
