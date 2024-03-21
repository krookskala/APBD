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
            double totalWeight = Containers.Sum(c => c.CargoMass + c.TareWeight) + container.CargoMass + container.TareWeight;
            if (totalWeight > MaxWeight)
            {
                throw new OverfillException("Adding This Container Would Exceed The Ship's Maximum Weight Capacity.");
            }
            Containers.Add(container);
        }

        
        public void ReplaceContainer(string serialNumber, Container newContainer)
        {
            int index = Containers.FindIndex(c => c.SerialNumber == serialNumber);
            if (index == -1)
            {
                throw new InvalidOperationException("Container With The Given Serial Number Does Not Exist.");
            }
            Containers[index] = newContainer;
        }
        
        public void TransferContainer(string serialNumber, ContainerShip targetShip)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container == null)
            {
                throw new InvalidOperationException("Container With The Given Serial Number Does Not Exist.");
            }

            double totalWeightOnTargetShip = targetShip.Containers.Sum(c => c.CargoMass + c.TareWeight);
            double newContainerWeight = container.CargoMass + container.TareWeight;

            if (totalWeightOnTargetShip + newContainerWeight > targetShip.MaxWeight)
            {
                throw new InvalidOperationException("Transferring This Container Would Exceed The Target Ship's Maximum Weight Capacity.");
            }

            Containers.Remove(container);
            targetShip.AddContainer(container);
            Console.WriteLine($"Container {container.SerialNumber} Transferred Successfully.");
        }
    }
}