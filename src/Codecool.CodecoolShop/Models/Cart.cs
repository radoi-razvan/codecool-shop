using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    public class Cart
    {
        public Dictionary<Product, int> Products { get; set; } = new Dictionary<Product, int>();
        public bool Add(Product product)
        {
            if (Products.Count >= 1)
            {
                foreach (Product key in Products.Keys)
                {
                    if (key.Id == product.Id)
                    {
                        Products[key] += 1;
                        break;
                    }
                    else
                    {
                        Products[product] = 1;
                        break;
                    }
                }
            }
            else
            {
                Products[product] = 1;
            }


            return true;
        }
        public void Edit(Product product, bool increase)
        {
            if (Products.ContainsKey(product))
            {
                Products[product] = increase ? Products[product] + 1 : Products[product] - 1;
            }
        }
        public void Remove(Product product)
        {
            if (Products.ContainsKey(product))
                Products.Remove(product);
        }

        public void Clear()
        {
            Products.Clear();
        }
    }
}

