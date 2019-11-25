using CPUWS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPUWS.WebsUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}