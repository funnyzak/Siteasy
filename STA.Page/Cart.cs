using System;
using System.Web;
using System.Data;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Core;

namespace STA.Page
{
    public class Cart : PageBase
    {
        public DataTable prods;
        protected override void PageShow()
        {
            prods = Carts.GetCartProdList(Carts.GetCartProdsListByCookies());
            Carts.WriteCookie(prods);
        }

    }

}
