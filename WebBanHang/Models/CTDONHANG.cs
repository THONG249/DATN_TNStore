using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Context;

namespace WebBanHang.Models
{
    public class CTDONHANG
    {
        public List<Product> ListProduct { get; set; }
        public List<OrderDetail> ListOderDetail { get; set; }
        public Order Order { get; set; }
        public List<User> User { get; set; }

    }
   
}