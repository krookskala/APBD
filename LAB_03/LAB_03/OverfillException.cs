namespace LAB_03
{
    public class OverfillException : Exception
    {
        public OverfillException(string message) : base("Cargo Mass Exceeds Container's Maximum Payload.")
        {
        }
    }
}