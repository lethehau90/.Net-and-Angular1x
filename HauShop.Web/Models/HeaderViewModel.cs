using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HauShop.Web.Models
{
    public class HeaderViewModel
    {
        public IEnumerable<LogoViewModel> Logos { set; get; }
        public IEnumerable<PageViewModel> Pages { set; get; }

    }
}