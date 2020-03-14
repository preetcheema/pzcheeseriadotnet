using Microsoft.AspNetCore.Http;
using PZCheeseria.BusinessLogic.Cheeses.Commands;

namespace PZCheeseria.Api.Models
{
    public class AddCheeseApiModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerKilo { get; set; }
        public string Colour { get; set; }
        public IFormFile Image { get; set; }

        public AddCheeseCommand ToAddCheeseCommand()
        {
            return new AddCheeseCommand
            {
                Name = Name,
                Description = Description,
                Price = PricePerKilo,
                Colour = Colour,
                Image = Image.FileName
            };
        }
    }
}