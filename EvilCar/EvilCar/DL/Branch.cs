using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilCar.DL
{
    class Branch
    {
        protected string branchName;
        protected Guid[] branchManager = new Guid[5];

        public Branch(string name)
        {
            branchName = name;
        }
    }
}
