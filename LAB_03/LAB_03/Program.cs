using System;
using System.Collections.Generic;

namespace LAB_03
{
    class Program
    {
        private static List<ContainerShip> ships = new List<ContainerShip>();
        private static int serialNumberCounter = 1;

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                DisplayMenu();
                string option = Console.ReadLine().ToLower();

                switch (option)
                {
                    case "a":
                        AddContainerShip();
                        break;
                    case "b":
                        AddContainerToShip();
                        break;
                    case "c":
                        ListShips();
                        break;
                    case "d":
                        ListContainersOnShip();
                        break;
                    case "e":
                        RemoveContainerFromShip();
                        break;
                    case "f":
                        UnloadContainerFromShip();
                        break;
                    case "g":
                        TransferContainerBetweenShips();
                        break;
                    case "h":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Option! Please Choose Again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nContainer Management System Menu:");
            Console.WriteLine("|=======================================================================|");
    
            
            Console.BackgroundColor = ConsoleColor.Black; 
            Console.ForegroundColor = ConsoleColor.Cyan; 
            Console.WriteLine("| (a) Add A Container Ship                                              |");
            Console.WriteLine("| (b) Add A Container To A Ship                                         |");
            Console.WriteLine("| (c) List Container Ships                                              |");
            Console.WriteLine("| (d) List Containers On A Ship                                         |");
            Console.WriteLine("| (e) Remove A Container From A Ship                                    |");
            Console.WriteLine("| (f) Unload a Container                                                |");
            Console.WriteLine("| (g) Transfer a Container Between Ships                                |");
            Console.WriteLine("| (h) Exit                                                              |");
            Console.WriteLine("|=======================================================================|");
            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Choose An Option: ");
        }
        
        static void AddContainerShip()
        {
            int maxSpeed = ReadInt("Enter Ship's Max Speed (In Knots): ", true);
            int maxContainerNum = ReadInt("Enter Ship's Max Container Number: ", true);
            double maxWeight = ReadDouble("Enter Ship's Max Weight (In Tons): ", true);

            var ship = new ContainerShip(maxSpeed, maxContainerNum, maxWeight);
            ships.Add(ship);
            Console.WriteLine("Container Ship Added Successfully. Press Any Key To Continue...");
            Console.ReadKey();
        }

        static void AddContainerToShip()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("No Ships Available. Please Add A Ship First. Press Any Key To Continue...");
                Console.ReadKey();
                return;
            }

            int shipIndex = ChooseShip();
            if (shipIndex < 0) return;

            Console.WriteLine("Select Container Type: ");
            Console.WriteLine("1. Liquid Container");
            Console.WriteLine("2. Gas Container");
            Console.WriteLine("3. Refrigerated Container");
            int containerTypeSelection = ReadInt("Choose A Container Type (1-3): ", true);

            string serialNumber = GenerateSerialNumber(containerTypeSelection);
            Container container = null;
            double cargoMass = ReadDouble("Enter The Cargo Mass For The Container: ", false);
            
            switch (containerTypeSelection)
            {
                case 1: 
                    bool isHazardous = ReadBool("Is The Liquid Hazardous? (Y/N): ");
                    container = new LiquidContainer(cargoMass, "",1000, 200, 2000, serialNumber, 1000, isHazardous);
                    Console.WriteLine($"Liquid Container {serialNumber} Created. Hazardous: {(isHazardous ? "Yes" : "No")}");
                    break;

                case 2: 
                    double pressure = ReadDouble("Enter The Pressure (In Atmospheres): ", true);
                    container = new GasContainer(cargoMass, "", 1000, 200, 2000, serialNumber, 1000, pressure); 
                    Console.WriteLine($"Gas Container {serialNumber} Created With Pressure: {pressure} Atmospheres.");
                    break;

                case 3: 
                    Console.WriteLine("Available Products:");
                    foreach (var product in ProductTemperature.ProductTemperatures.Keys)
                    {
                        Console.WriteLine(product);
                    }
                    
                    string productType;
                    while (true)
                    {
                        productType = ReadString("Enter the type of product from the list: ");
                        if (ProductTemperature.IsValidProduct(productType))
                        {
                            productType = ProductTemperature.ProductTemperatures.Keys.FirstOrDefault(p => p.Equals(productType, StringComparison.OrdinalIgnoreCase));
                            break;
                        }
                        Console.WriteLine("Invalid product type. Please select a product from the list.");
                    }
                    
                    var (minTemp, maxTemp) = ProductTemperature.GetTemperatureRange(productType);
                    double initialTemperature;
                    while (true)
                    { 
                        initialTemperature = ReadDouble($"Enter The Temperature For {productType} (Celsius): ", true); 
                        if (initialTemperature >= minTemp && initialTemperature <= maxTemp)
                        {
                            break;
                        }
                        Console.WriteLine($"Invalid Temperature. For {productType}, The Temperature Should Be Between {minTemp}°C And {maxTemp}°C.");                    }

                    try
                    {
                        container = new RefrigeratedContainer(cargoMass, "", 1000, 200, 2000, serialNumber, 1000, productType, initialTemperature); 
                        Console.WriteLine($"Refrigerated Container {serialNumber} Created For {productType}. Temperature: {initialTemperature}°C.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                        return;
                    }
                    break;
                
                default:
                    Console.WriteLine("Invalid Container Type Selected. Press Any Key To Continue...");
                    Console.ReadKey();
                    return;
            }
            
            if (container != null && ShipCanCarryMore(shipIndex, container))
            {
                try
                {
                    container.LoadCargo(cargoMass);
                    ships[shipIndex].AddContainer(container);
                    Console.WriteLine($"Container {serialNumber} Added To The Ship Successfully.");
                }
                catch (OverfillException ex)
                {
                    Console.WriteLine($"Error Adding Container To The Ship: {ex.Message}");
                }
            }

            Console.ReadKey();
        }

        static void ListShips()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("No Container Ships Have Been Added Yet. Press Any Key To Continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nList of Container Ships:");
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1}. Ship Max Speed: {ships[i].MaxSpeed} Knots, Max Container Number: {ships[i].MaxContainerNum}, Max Weight: {ships[i].MaxWeight} Tons. Press Any Key To Continue...");
            }
            Console.ReadKey();
        }

        static void ListContainersOnShip()
        {
            int shipIndex = ChooseShip();
            if (shipIndex == -1) return;

            var containers = ships[shipIndex].Containers;
            if (containers.Count == 0)
            {
                Console.WriteLine("The selected ship does not have any containers. Press Any Key To Continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nContainers on Ship {shipIndex + 1}:");
            foreach (var container in containers)
            {
                Console.WriteLine($"Serial Number: {container.SerialNumber}, Type: {container.GetType().Name}, Cargo Mass: {container.CargoMass}kg, Max Payload: {container.MaxPayload}kg");
                if (container is IHazardNotification hazardContainer)
                {
                    hazardContainer.NotifyHazard();
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void RemoveContainerFromShip()
        {
            try
            {
                int shipIndex = ChooseShip();
                if (shipIndex == -1)
                {
                    Console.WriteLine("Returning To Main Menu...");
                    return;
                }

                var containers = ships[shipIndex].Containers;
                if (containers.Count == 0)
                {
                    Console.WriteLine("The Selected Ship Does Not Have Any Containers. Press Any Key To Continue...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Select A Container To Remove:");
                for (int i = 0; i < containers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Serial Number: {containers[i].SerialNumber}");
                }

                int selection = ReadInt("Enter The Number Of The Container To Remove: ") - 1;
                if (selection >= 0 && selection < containers.Count)
                {
                    containers.RemoveAt(selection);
                    Console.WriteLine("Container Removed Successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid Selection. Please Try Again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Unexpected Error Occurred: {ex.Message}. Please Try Again.");
            }
            finally
            {
                Console.WriteLine("Press Any Key To Continue...");
                Console.ReadKey();
            }
        }
        
            static int ReadInt(string prompt, bool positiveOnly = false)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && (!positiveOnly || value > 0))
                {
                    return value;
                }

                Console.WriteLine("Invalid Input! Please Enter A Valid Integer Number. Press Any Key To Continue...");
                Console.ReadKey();
            }
        }

        static double ReadDouble(string prompt, bool allowNegative)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value) && (allowNegative || value >= 0))
                {
                    return value;
                }

                Console.WriteLine("Invalid Input! Please Enter A Valid Number. Press Any Key To Continue...");
                Console.ReadKey();
            }
        }
        
        static string ReadString(string prompt)
        {
            string input = string.Empty;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }
                Console.WriteLine("Input Cannot Be Empty. Please Try Again.");
            }
        }
        
        static bool ReadBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (Y/N): ");
                string input = Console.ReadLine().ToLower();
                if (input == "y" || input == "yes") return true;
                if (input == "n" || input == "no") return false;
                Console.WriteLine("Invalid Input! Please Enter 'Y' Or 'N'.");
            }
        }

        static string GenerateSerialNumber(int containerType)
        {
            string typeIdentifier = containerType switch
            {
                1 => "L",
                2 => "G",
                3 => "R",
                _ => "U"
            };
            return $"KON-{typeIdentifier}-{serialNumberCounter++}";
        }

        static int ChooseShip()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("No Ships Available. Please Add A Ship First.");
                Console.ReadKey();
                return -1;
            }

            Console.WriteLine("Select A Ship:");
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Ship With Max Speed: {ships[i].MaxSpeed}, Max Containers: {ships[i].MaxContainerNum}, Max Weight: {ships[i].MaxWeight}");
            }

            int shipIndex = ReadInt("Enter The Number Of The Ship: ", false) - 1;
            if (shipIndex >= 0 && shipIndex < ships.Count)
            {
                return shipIndex;
            }
            else
            {
                Console.WriteLine("Invalid Ship Selection. Please Try Again.");
                Console.ReadKey();
                return -1;
            }
        }
        
        static Container CreateContainerByType(int containerTypeSelection, string serialNumber)
        {
            double cargoMass = ReadDouble("Enter Cargo Mass (In Kilograms): ", true);
            int height = ReadInt("Enter Height (In Centimeters): ", true);
            double tareWeight = ReadDouble("Enter Tare Weight (In Kilograms): ", true);
            int depth = ReadInt("Enter Depth (In Centimeters): ", true); 
            double maxPayload = ReadDouble("Enter Maximum Payload (In Kilograms): ", true);

            switch (containerTypeSelection)
            {
                case 1:
                    bool isHazardous = ReadBool("Is It Hazardous? (Y/N): ");
                    return new LiquidContainer(cargoMass,  "", height, tareWeight, depth, serialNumber, maxPayload, isHazardous);
                case 2:
                    double pressure = ReadDouble("Enter Pressure (In Atmospheres): ", true);
                    return new GasContainer(cargoMass, "", height, tareWeight, depth, serialNumber, maxPayload, pressure);
                case 3:
                    string productType = ReadString("Enter The Type Of Product: ");
                    double temperature = ReadDouble("Enter The Temperature (In Celsius): ", true);
                    return new RefrigeratedContainer(cargoMass, "", height, tareWeight, depth, serialNumber, maxPayload, productType, temperature);
                default:
                    Console.WriteLine("Invalid Container Type Selected.");
                    return null;
            }
        }
        
        static void UnloadContainerFromShip()
        {
            int shipIndex = ChooseShip();
            if (shipIndex == -1) return;

            Console.WriteLine("Available Containers:");
            foreach (var container in ships[shipIndex].Containers)
            {
                Console.WriteLine($"Serial Number: {container.SerialNumber}");
            }

            Container containerToUnload = null;
            while (containerToUnload == null)
            {
                string serialNumber = ReadString("Enter The Serial Number Of The Container To Unload: ");
                containerToUnload = ships[shipIndex].Containers.FirstOrDefault(c => c.SerialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));

                if (containerToUnload == null)
                {
                    Console.WriteLine("Invalid Serial Number. Please Try Again.");
                }
            }

            double mass = ReadDouble("Enter The Mass Of Cargo To Unload (kg): ", true);
            containerToUnload.UnloadCargo(mass);
            Console.WriteLine($"Cargo Unloaded Successfully From Container {containerToUnload.SerialNumber}.");
            Console.ReadKey();
        }
        
        static void TransferContainerBetweenShips()
        {
            Console.WriteLine("Select The Source Ship:");
            int sourceShipIndex = ChooseShip();
            if (sourceShipIndex == -1) return;

            Console.WriteLine("Select The Target Ship:");
            int targetShipIndex = ChooseShip();
            if (targetShipIndex == -1 || targetShipIndex == sourceShipIndex)
            {
                Console.WriteLine("Invalid Target Ship Selection. Operation Aborted.");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("Available Containers:");
            foreach (var container in ships[sourceShipIndex].Containers)
            {
                Console.WriteLine($"Serial Number: {container.SerialNumber}");
            }
            
            string serialNumber = ReadString("Enter The Serial Number Of The Container To Transfer: ");

            try
            {
                ships[sourceShipIndex].TransferContainer(serialNumber, ships[targetShipIndex]);
                Console.WriteLine($"Container {serialNumber} transferred successfully from Ship {sourceShipIndex + 1} to Ship {targetShipIndex + 1}.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.ReadKey();
        }
        static bool ShipCanCarryMore(int shipIndex, Container container) {
            double totalWeight = ships[shipIndex].Containers.Sum(c => c.CargoMass + c.TareWeight) + container.CargoMass + container.TareWeight;
            return totalWeight <= ships[shipIndex].MaxWeight * 1000; // Assuming MaxWeight is in tons and cargo is in kg
        }
    }
}