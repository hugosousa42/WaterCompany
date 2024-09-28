using System.ComponentModel.DataAnnotations;

namespace WaterCompany.Data.Entities
{
    public class BillDetailTemp : IEntity
    {
        public int id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Client Client { get; set; }


        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = false)]
        public double Volume { get; set; }


        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        public decimal Value => CalculateValue();

        private decimal CalculateValue()
        {
            decimal totalValue = 0;
            double remainingVolume = Volume;

            if (remainingVolume > 25)
            {
                totalValue += (decimal)(remainingVolume - 25) * 1.60m;
                remainingVolume = 25;
            }
            if (remainingVolume > 15)
            {
                totalValue += (decimal)(remainingVolume - 15) * 1.20m;
                remainingVolume = 15;
            }
            if (remainingVolume > 5)
            {
                totalValue += (decimal)(remainingVolume - 5) * 0.80m;
                remainingVolume = 5;
            }
            if (remainingVolume > 0)
            {
                totalValue += (decimal)remainingVolume * 0.30m;
            }

            return totalValue;
        }
    }
}
