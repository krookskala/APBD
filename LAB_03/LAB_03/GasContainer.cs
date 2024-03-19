namespace LAB_03;
using System;

public class GasContainer : Container, HazardNotifier
{
    public double Pressure { get; private set; }

    public GasContainer(double maximumPayload, double tareWeight, double height, double depth, double pressure) : base(
        maximumPayload, tareWeight, height, depth)
    {
        Pressure = pressure;
    }

    public void Notify(string containerNumber)
    {
        Console.WriteLine($"Hazardous Situtation Detected In Gas Container {containerNumber}.");
    }

    public override void EmptyCargo()
    {
        CargoMass *= 0.05;
    }
}   