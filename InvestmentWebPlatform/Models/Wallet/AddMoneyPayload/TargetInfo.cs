using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestmentWebPlatform.Models.Wallet.AddMoneyPayload
{
    public class TargetInfo : BankDataInfo
    {
        public string Account { get; set; }
    }
}
