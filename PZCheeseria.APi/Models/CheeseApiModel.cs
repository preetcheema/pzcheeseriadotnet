using PZCheeseria.Api.ExtensionMethods;
using PZCheeseria.BusinessLogic.Cheeses.Queries;

namespace PZCheeseria.Api.Models
{
    public class CheeseApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PriceApiModel Price { get; set; }
        public ImageApiModel Image { get; set; }
        public string Colour { get; set; }

        public static CheeseApiModel ConvertFrom(CheeseModel model, string prependUrl)
        {
            if (model == null) return null;

            var result = new CheeseApiModel
            {
                Id=model.Id,
                Name = model.Name,
                Description = model.Description,
                Colour = model.Colour,
                Price = new PriceApiModel
                {
                    RawValue = model.PricePerKilo,
                    FormattedValue = model.PricePerKilo.FormatAsAustralianDollar()   //This could be added into 
                },
                Image = new ImageApiModel
                {
                    ImageName = model.ImageName,
                    ImageUrl = $"{prependUrl}{model.ImageName}"
                }
            };
            return result;
        }
    }

    public class PriceApiModel
    {
        public decimal RawValue { get; set; }
        public string FormattedValue { get; set; }
    }

    public class ImageApiModel
    {
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
    }
}