using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_7._1
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
