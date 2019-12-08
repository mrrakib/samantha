using MTS.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTS.Model.Models;
using System.Data.Entity;

namespace MTS.Data.Repository.SupplierInfo
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        EasyContext _context;
        public SupplierRepository(DbContext context)
            : base(context)
        {
            _context = (EasyContext)context;
        }
    }

    public interface ISupplierRepository : IRepository<Supplier>
    {
    }
}
