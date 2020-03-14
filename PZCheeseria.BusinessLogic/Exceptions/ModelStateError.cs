namespace PZCheeseria.BusinessLogic.Exceptions
{
    public class ModelStateError
    {
        public string PropertyName { get; set; }

        public string Error { get; set; }
    }
}