namespace CustomersApi.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Cpf { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
