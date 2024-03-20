namespace LAB_03
{
    public class ContainerShip
    {
        public List<Container> Containers { get; private set; } = new List<Container>();
        public int MaxSpeed { get; private set; }
        public int MaxContainerNum { get; private set; }
        public double MaxWeight { get; private set; }

        public ContainerShip(int maxSpeed, int maxContainerNum, double maxWeight)
        {
            MaxSpeed = maxSpeed;
            MaxContainerNum = maxContainerNum;
            MaxWeight = maxWeight;
        }

        public void AddContainer(Container container)
        {
            if (Containers.Count >= MaxContainerNum || GetTotalWeight() + container.CargoMass + container.TareWeight > MaxWeight)
            {
                throw new Exception("Cannot Add Container: Exceeds Ship Capacity Or Weight Limit.");
            }
            Containers.Add(container);
        }

        public void RemoveContainer(string serialNumber)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container != null)
            {
                Containers.Remove(container);
            }
        }

        private double GetTotalWeight()
        {
            return Containers.Sum(c => c.CargoMass + c.TareWeight);
        }

        public void ListAllContainers()
        {
            Console.WriteLine("Listing all containers on the ship:");
            foreach (var container in Containers)
            {
                Console.WriteLine($"Type: {container.GetContainerType()}, Serial Number: {container.SerialNumber}, Cargo Mass: {container.CargoMass} kg");
            }
        }
    }
}