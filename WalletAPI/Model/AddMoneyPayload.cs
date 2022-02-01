using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.Model
{
    public class AddMoneyPayload
    {
        public long CPF { get; set; }

        public double value { get; set; }
    }
}
