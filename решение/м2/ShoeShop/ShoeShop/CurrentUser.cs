using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShop
{
    public static class CurrentUser
    {
        public static int Id { get; set; }

        public static string FullName { get; set; } = "Гость";

        public static string Role { get; set; } = "Гость";

        public static bool IsGuest => Role != "Гость";
    }

}
