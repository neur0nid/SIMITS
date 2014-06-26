using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simits2
{
    public class AccessCoordinate
    {
        public int Access { get; set; }
        public int Frame { get; set; }
        public int SuccessTx { get; set; }

        public AccessCoordinate(int access, int frame, int successTx)
        {
            this.Access = access;
            this.Frame = frame;
            this.SuccessTx = successTx;
        }
    }
}
