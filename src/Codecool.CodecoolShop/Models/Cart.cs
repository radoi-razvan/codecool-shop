using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class Cart
    {
        public Dictionary<Product, int> Products { get; set; } = new Dictionary<Product, int>();
        public void Add(Product product)
        {
            if (Products.Count >= 1)
            {
                bool productFound = false;
                foreach (Product key in Products.Keys)
                {
                    if (key.Id == product.Id)
                    {
                        Products[key] += 1;
                        productFound = true;
                    }
                }
                if (!productFound)
                    Products[product] = 1;
            }
            else
            {
                Products[product] = 1;
            }
        }
        public void Delete(int productId)
        {
            foreach (var key in Products.Keys)
            {
                if (key.Id == productId)
                {
                    if (Products[key] < 1)
                    {
                        Remove(Products[key]);
                    }
                    else
                    {
                        Products[key] -= 1;
                    }
                }
            }
        }
        public void Remove(Product product)
        {
            if (Products.ContainsKey(product))
                Products.Remove(product);
        }

        public void Remove(int productId)
        {
            foreach (var key in Products.Keys)
            {
                if (key.Id == productId)
                {
                    Products.Remove(key);
                }
            }
        }

        public void Clear()
        {
            Products.Clear();
        }
    }
}
