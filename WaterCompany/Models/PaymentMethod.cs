using System.ComponentModel.DataAnnotations;

namespace WaterCompany.Models
{
    public enum PaymentMethod
    {
        [Display(Name = "Not Paid")]
        NotPaid,

        [Display(Name = "Cash Payment")]
        Cash,

        [Display(Name = "Debit Card")]
        DebitCard,

        [Display(Name = "Bank Transfer")]
        BankTransfer,

        Other
    }
}
