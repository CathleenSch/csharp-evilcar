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
        protected Guid branchID;

        public string Name { get => branchName; set => branchName = value; }
        public Guid BranchId { get => branchID; set => branchID = value; }

    }
}
