namespace DefaultNamespace;
using System;
using System.Collections.Generic;
public class ContainerShip
{
    public string Name { get; private set; }
    public double MaximumSpeed { get; private set; }
    public int MaximumContainerNumber { get; private set; }
    public double MaximumWeight { get; private set; }
    public List<Container> Containers { get; private set; }

    public ContainerShip(string name, double maximumSpeed, int maximumContainerNumber, double maximumWeight)
    {
        Name = name;
        MaximumSpeed = maximumSpeed;
        MaximumContainerNumber = maximumContainerNumber;
        MaximumWeight = maximumWeight;
        Containers = new List<Container>();
    }

    public void LoadContainer(Container container)
    {
        if (Containers.Count >= MaximumContainerNumber)
            throw new InvalidOperationException("Ship Is At Maximum Capacity.");
        if (container.CargoMass + CalculateTotalWeight() > MaximumWeight)
            throw new InvalidOperationException("Adding This Container Exceeds The Maximum Weight Limit.");
        Containers.Add(container);
    }

    public void UnloadContainer(Container container)
    {
        Containers.Remove(container);
    }

    private double CalculateTotalWeight()
    {
        double totalWeight = 0;
        foreach (var container in Containers)
        {
            totalWeight += container.CargoMass + container.TareWeight;
        }
        return totalWeight;
    }
}