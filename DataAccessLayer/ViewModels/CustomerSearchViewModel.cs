namespace DataAccessLayer.ViewModels
{
    public class CustomerSearchViewModel
    {
        public int CustomerId { get; set; }

        public string Givenname { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string Streetaddress { get; set; } = null!;

        public string City { get; set; } = null!;

        public string? NationalId { get; set; }
        public List<CustomerSearchViewModel> Customers { get; set; }
    }
}
