namespace Data.Dto
{
    public class User
    {
        public string Id { get; set; }
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProviderName { get; set; }
        public string BearerToken { get; set; }
    }
}