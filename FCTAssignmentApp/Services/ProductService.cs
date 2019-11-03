using FCTAssignmentApp.Data;
using FCTAssignmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCTAssignmentApp.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();

        Product GetProduct(int id);
        Purchase AddPurchase(Purchase data);

        void UpdatePurchase(Purchase data);

        IEnumerable<Purchase> GetAllPurchases();

    }
    public class ProductService : IProductService
    {
        private CustomerContext _context;

        public ProductService(CustomerContext context)
        {
            _context = context;
        }

        public Purchase AddPurchase(Purchase data)
        {
            _context.Purchases.Add(data);
            _context.SaveChanges();

            return data;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public Product GetProduct(int id)
        {
            return _context.Products.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            return _context.Purchases;
        }

        public void UpdatePurchase(Purchase data)
        {
            
            var purchase = _context.Purchases.Find(data.Id);

            if (purchase == null)
                throw new Exception("Purchase not found");

            purchase.OrderStatus = data.OrderStatus;

            _context.Purchases.Update(purchase);
            _context.SaveChanges();
        }
    }
}
