namespace PZCheeseria.Domain.Entities
{
    /// <summary>
    /// Object to describe cheese
    ///
    ///  I am serving images as static resource.Assumption is that we are storing only one image per cheese.
    /// In real life scenario we can have multiple images for one product. In that case or if we have lots and lots of products, we can store
    /// images in separate table in database (with data or just metadata in case we want to use external service to store images)
    /// We could also store 
    /// </summary>
    public class Cheese
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerKilo { get; set; }
        public string ImageName { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public CheeseColour Colour { get; set; }

    }

    public class CheeseColour
    {
        public int Id { get; set; }
        public string Colour { get; set; }
    }
}