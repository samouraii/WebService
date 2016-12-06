using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using test.user;
namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            user.Service1Client t = new Service1Client();

            user.User y = new User();
            y.username = "samouraii";
            y.mdp = "toto";
            Console.Write( t.addUser(y));
            Console.ReadLine();

        }
    }
}
