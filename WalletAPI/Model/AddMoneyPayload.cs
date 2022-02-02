using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletAPI.Model
{
    public class AddMoneyPayload
    {
        public string CPF { get; set; }

        public double Value { get; set; }
    }
}
