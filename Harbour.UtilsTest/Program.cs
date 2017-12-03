using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harbour.Utils;
using System.Linq;
namespace Harbour.UtilsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> userList = new List<User>();
            userList.IsValuable();
            userList.TryForEach((e, i) => { i = 1; });
        }
    }
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }
    }

}
