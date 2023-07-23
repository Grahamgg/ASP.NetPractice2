using System;
using NathansCRUDWebsite.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



namespace NathansCRUDWebsite
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();
        public Product GetProduct(int id);
        public void UpdateProduct(Product product);
    }
}
