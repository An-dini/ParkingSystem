using System;
using System.Collections.Generic;

class Program
{
    class Parking
    {
        public int Slot { get; set; }
        public string RegNumber { get; set; }
        public string Type { get; set; }
        public string Colour { get; set; }

        public Parking(int slot, string regNumber, string colour, string type)
        {
            Slot = slot;
            RegNumber = regNumber.ToUpper();
            Colour = CapitalizeInput(colour);
            Type = CapitalizeInput(type);
        }

        public override string ToString()
        {
            return $"{Slot}\t{RegNumber}\t{Type}\t{Colour}";
        }

        private string CapitalizeInput(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
        }
    }

    static void Main(string[] args)
    {
        List<Parking> slotParking = new List<Parking>();
        int parkingCapacity = 0;
        string cmd;

        do
        {
            cmd = Console.ReadLine().ToLower();
            if (cmd.StartsWith("create_parking_lot"))
            {
                string[] parts = cmd.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[1], out parkingCapacity) && parkingCapacity > 0)
                {
                    slotParking = new List<Parking>();
                    Console.WriteLine($"Created a parking lot with {parkingCapacity} slots");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Invalid format. Try: create_parking_lot <capacity>\n");
                }
            }
            else if (cmd.StartsWith("park"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot. \n");
                }

                else
                {
                    string[] parts = cmd.Split(' ');

                    if (parts.Length == 4)
                    {
                        if (slotParking.Count < parkingCapacity)
                        {
                            for (int i = 1; i <= slotParking.Count + 1; i++)
                            {
                                var findNum = slotParking.Find(p => p.Slot == i);

                                if (findNum == null)
                                {
                                    slotParking.Add(new Parking(i, parts[1], parts[2], parts[3]));
                                    Console.WriteLine($"Allocated slot number: {i}");
                                    Console.WriteLine();
                                    break;

                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, parking lot is full\n");
                        }
                    }

                    else
                    {
                        Console.WriteLine("Invalid format. Try: park <registration number> <colour> <vehicle type>\n");
                    }
                }
            }
            else if (cmd.StartsWith("leave"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot. \n");
                }
                else
                {
                    string[] parts = cmd.Split(' ');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int num))
                    {
                        var parkingLeave = slotParking.Find(p => p.Slot == num);
                        if (parkingLeave != null)
                        {
                            slotParking.Remove(parkingLeave);
                            Console.WriteLine($"Slot number {num} is free");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("No Found");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. Try: leave <slot number>\n");
                    }
                }
            }
            else if (cmd.StartsWith("status"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot. \n");
                }
                else
                {
                    Console.WriteLine("Slot\tNo.\t\tType\tColour");
                    if (slotParking.Count == 0)
                    {
                        Console.WriteLine("\tno one has parked yet");
                    }
                    else
                    {
                        foreach (var vehicle in slotParking.OrderBy(p => p.Slot))
                        {
                            Console.WriteLine(vehicle);
                        }
                        Console.WriteLine();
                    }
                }
            }
            else if (cmd.StartsWith("type_of_vehicles"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot. \n");
                }
                else
                {
                    string[] parts = cmd.Split(' ');
                    if (parts.Length == 2)
                    {
                        List<string> findResult = new List<string>();
                        var result = slotParking.FindAll(p => p.Type.ToLower() == parts[1].ToLower());
                        if (result.Count != 0)
                        {
                            foreach (var vehicle in result)
                            {
                                findResult.Add(vehicle.RegNumber);
                            }
                            Console.WriteLine(findResult.Count);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Not Found \n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. Try: type_of_vehicles <type>\n");
                    }
                }
            }
            else if (cmd.StartsWith("registration_numbers_for_vehicles_with_colour"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot. \n");
                }
                else
                {
                    string[] parts = cmd.Split(' ');
                    if (parts.Length == 2)
                    {
                        List<string> findResult = new List<string>();
                        var result = slotParking.FindAll(p => p.Colour.ToLower() == parts[1].ToLower());
                        if (result.Count != 0)
                        {
                            foreach (var vehicle in result)
                            {
                                findResult.Add(vehicle.RegNumber);
                            }
                            Console.WriteLine(string.Join(", ", findResult));
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Not Found \n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. Try: registration_numbers_for_vehicles_with_colour <colour>\n");
                    }
                }
            }
            else if (cmd.StartsWith("slot_numbers_for_vehicles_with_colour"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot. \n");
                }
                else
                {
                    string[] parts = cmd.Split(' ');
                    if (parts.Length == 2)
                    {
                        List<string> findResult = new List<string>();
                        var result = slotParking.FindAll(p => p.Colour.ToLower() == parts[1].ToLower());
                        if (result.Count != 0)
                        {
                            foreach (var vehicle in result)
                            {
                                findResult.Add(vehicle.Slot.ToString());
                            }
                            Console.WriteLine(string.Join(", ", findResult));
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Not Found \n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. Try: slot_numbers_for_vehicles_with_colour <colour> \n");
                    }
                }
            }
            else if (cmd.StartsWith("slot_number_for_registration_number"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot. \n");
                }
                else
                {
                    string[] parts = cmd.Split(' ');
                    if (parts.Length == 2)
                    {
                        var result = slotParking.FindAll(p => p.RegNumber.ToLower() == parts[1].ToLower());
                        if (result.Count != 0)
                        {
                            foreach (var vehicle in result)
                            {
                                Console.WriteLine(vehicle.Slot);
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Not Found \n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid format. Try: slot_number_for_registration_number <Registration number> \n");
                    }
                }
            }
            else if (cmd.StartsWith("registration_numbers_for_vehicles_with_ood_plate"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot. \n");
                }
                else
                {
                    List<string> findResult = new List<string>();
                    var result = slotParking.FindAll(p =>
                    {
                        int nReg = int.Parse(p.RegNumber.Split('-')[1]);
                        return nReg % 2 != 0;
                    });

                    if (result.Count != 0)
                    {
                        foreach (var vehicle in result)
                        {
                            findResult.Add(vehicle.RegNumber);
                        }
                        Console.WriteLine(string.Join(", ", findResult));
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Not Found \n");
                    }
                }
            }
            else if (cmd.StartsWith("registration_numbers_for_vehicles_with_event_plate"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot. \n");
                }
                else
                {
                    List<string> findResult = new List<string>();
                    var result = slotParking.FindAll(p =>
                    {
                        int nReg = int.Parse(p.RegNumber.Split('-')[1]);
                        return nReg % 2 == 0;
                    });

                    if (result.Count != 0)
                    {
                        foreach (var vehicle in result)
                        {
                            findResult.Add(vehicle.RegNumber);
                        }
                        Console.WriteLine(string.Join(", ", findResult));
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Not Found \n");
                    }
                }
            }
        } while (cmd != "exit");
    }
}