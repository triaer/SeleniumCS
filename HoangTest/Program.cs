using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoangTest
{
    class Program
    {
        public enum MenuItemLink
        {
           Inbox,
           Drafts
        }
        static void Main(string[] args)
        {
            var values = Enum.GetValues(typeof(MenuItemLink));

            foreach (var item in values)
            {
                Console.WriteLine(item);
            }
            
        }
    }
}
