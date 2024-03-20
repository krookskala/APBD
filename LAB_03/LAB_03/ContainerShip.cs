using System.Collections.Generic;

namespace LAB_03
{
    public class ContainerShip
    {
        public int MaxSpeed { get; private set; }
        public int MaxContainerNum { get; private set; }
        public double MaxWeight { get; private set; }
        public List<Container> Containers { get; private set; }

        public ContainerShip(int maxSpeed, int maxContainerNum, double maxWeight)
        {
            MaxSpeed = maxSpeed;
            MaxContainerNum = maxContainerNum;
            MaxWeight = maxWeight;
            Containers = new List<Container>();
        }

        public void AddContainer(Container container)
        {
            Containers.Add(container);
        }
    }
}