using MTS.Data;
using MTS.Data.Infrastructure;
using MTS.Model.Models.Account;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Data.Repository.Autofac
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        EasyContext _context;
        public UserRepository(DbContext context)
            : base(context)
        {
            _context = (EasyContext)context;
        }

    }
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        
    }
}
