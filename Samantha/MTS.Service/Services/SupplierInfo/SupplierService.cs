using PagedList;
using MTS.Data.Infrastructure;
using MTS.Data.Repository.SupplierInfo;
using MTS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Service.Services.SupplierInfo
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAllSuppliers();
        Supplier GetSupplierDetails(int supplierId);

        bool AddSupplier(Supplier supplier);
        bool CheckIfExist(string supplierName);

        //Check if any duplicate name in different record
        bool CheckIfExistForUpdate(string name, int id);
        bool UpdateSupplier(Supplier supplier);
        void SaveSupplier();

        bool DeleteSupplier(int supplierId);

        IPagedList<Supplier> GetSupplierPaged(Page page);
    }
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
        }

        public bool AddSupplier(Supplier supplier)
        {
            _supplierRepository.Add(supplier);
            try
            {
                SaveSupplier();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool CheckIfExist(string supplierName)
        {
            var countSupplier = _supplierRepository.GetMany(s => s.Name == supplierName).Count();
            return countSupplier > 0 ? true : false;
        }

        public bool CheckIfExistForUpdate(string name, int id)
        {
            var countSupplier = _supplierRepository.GetMany(s => s.Name == name && s.Id == id).Count();
            return countSupplier > 0 ? true : false;
        }

        public bool DeleteSupplier(int supplierId)
        {
            var supplier = _supplierRepository.Get(s => s.Id == supplierId);
            _supplierRepository.Delete(supplier);
            try
            {
                SaveSupplier();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.GetAll();
        }

        public Supplier GetSupplierDetails(int supplierId)
        {
            return _supplierRepository.Get(s => s.Id == supplierId);
        }

        public IPagedList<Supplier> GetSupplierPaged(Page page)
        {
            return _supplierRepository.GetPage(page, a => true, order => order.Name);
        }

        public void SaveSupplier()
        {
            _unitOfWork.Commit();
        }

        public bool UpdateSupplier(Supplier supplier)
        {
            _supplierRepository.Update(supplier);
            try
            {
                SaveSupplier();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
