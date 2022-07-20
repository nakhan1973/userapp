namespace userDemo1.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PaymentMethod { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public OpStatus Status { get; set; }
    }
}