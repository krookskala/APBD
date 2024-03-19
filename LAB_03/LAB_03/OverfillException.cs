namespace DefaultNamespace;
using System;
public class OverfillException : Exception
{
    public OverfillException() : base("Cargo Mass Exceeds Container's Maximum Payload.")
    {
    }
}