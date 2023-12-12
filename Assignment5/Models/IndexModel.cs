using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment5.Models
{
    public class IndexModel : PageModel
    {
        public const string SessionKeyUser = "_User";
        public const string SessionKeyType = "_Type";
        public Cart ShoppingCart;

        public IndexModel()
        {
            ShoppingCart = new Cart();
        }

    }
}
