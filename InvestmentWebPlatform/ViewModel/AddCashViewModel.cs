using System.ComponentModel.DataAnnotations;

namespace InvestmentWebPlatform.ViewModel
{
    public class AddCashViewModel
    {
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Banco de Origem")]
        public string OriginBank { get; set; }

        [Required]
        [Display(Name = "Agencia de origem")]
        public string OriginBranch { get; set; }

        [Required]
        [Display(Name = "CPF do remetente")]
        public string OriginCPFOwner { get; set; }

        public string TargetCPF { get; set; }
        [Required]
        [Display(Name = "Valor")]
        public double Value { get; set; }
    }
}