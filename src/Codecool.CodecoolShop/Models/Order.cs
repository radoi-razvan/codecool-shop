using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Models
{
    [Serializable]
    public class Order
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string OrderStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderProduct OrderProduct { get; set; }
    }
}
