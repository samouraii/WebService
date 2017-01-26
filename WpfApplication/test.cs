using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication
{
    class test
    {
        toto[] toto;

        public test()
        {
            toto =  new toto[5];
            toto[0] = new toto();

        }

        public toto[] Toto
        {
            get
            {
                return toto;
            }

            set
            {
                toto = value;
            }
        }
    }
    class toto {

        test test;

        public test Test
        {
            get
            {
                return test;
            }

            set
            {
                test = value;
            }
        }
    }
}
