namespace Test_01.Models {
    public class PhoneNumber
    {
        public int Id { get; set; }
        public Operator Operator { get; set; } = null!;
        public int OperatorId { get; set; }
        public Client Client { get; set; } = null!;
        public int ClientId { get; set; }
        public string Number { get; set; } = null!;
    }
}