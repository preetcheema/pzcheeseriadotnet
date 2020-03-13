using System.ComponentModel.DataAnnotations;

namespace PZCheeseria.Api.Configuration
{
    public class ConnectionStrings
    {
        [Required]
        public string PZCheeseriaConnectionString { get; set; }
       
    }
}