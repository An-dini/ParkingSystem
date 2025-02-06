using System;
using System.Collections.Generic;

class Program
{
    class Parking
    {
        public int Slot {get; set;}
        public string RegNumber {get; set;}
        public string Type {get; set;}
        public string Colour {get; set;}

        public Parking(int slot, string regNumber, string colour, string type)
        {
            Slot = slot;
            RegNumber = regNumber;
            Colour = colour;
            Type = type;
        }

        public override string ToString()
        {
            return$"{Slot}\t{RegNumber}\t{Type}\t{Colour}" ;
        }
    }
    static void Main(string[] args)
    {
        List<Parking> slotParking = new List<Parking>();
        int parkingCapacity = 0;
        string cmd;

        do
        {
            cmd = Console.ReadLine();
            if (cmd.StartsWith("create_parking_lot"))
            {
                string[] parts = cmd.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[1], out parkingCapacity) && parkingCapacity > 0)
                {
                    slotParking = new List<Parking>();
                    Console.WriteLine($"Created a parking lot with {parkingCapacity} slots");
                }
                else 
                {
                    Console.WriteLine("Invalid format. Try: create_parking_lot <capacity>");
                }
            }
            else if(cmd.StartsWith("park"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot.");
                }
                
                string[] parts = cmd.Split(' ');

                if (parts.Length == 4)
                {
                    if (slotParking.Count < parkingCapacity)
                    {
                        for (int i = 1; i <= slotParking.Count+1; i++)
                        {
                            var findNum = slotParking.Find(p => p.Slot == i);

                            if (findNum == null)
                            {
                                slotParking.Add( new Parking(i, parts[1], parts[2], parts[3]));
                                Console.WriteLine($"Allocated slot number: {i}");   
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
                        Console.WriteLine("Sorry, parking lot is full");
                    }
                }

                else 
                {
                    Console.WriteLine("Invalid format. Try: park <registration number> <colour> <vechile type>");
                }
            }
            else if(cmd.StartsWith("leave"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot.");
                }
                
                string[] parts = cmd.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[1], out int num))
                {
                    var parkingLeave = slotParking.Find(p => p.Slot == num);
                    if (parkingLeave != null)
                    {
                        slotParking.Remove(parkingLeave);
                        Console.WriteLine($"Slot number {num} is free");
                        
                    }
                    else
                    {
                        Console.WriteLine("No Found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format. Try: leave <slot number>");
                }
            }
            else if(cmd.StartsWith("status"))
            {
                Console.WriteLine("Slot\tNo.\t\tType\tColour");
                if (slotParking.Count == 0)
                {
                    Console.WriteLine("Parking is free");
                }
                else
                {
                    foreach (var vechile in slotParking.OrderBy(p => p.Slot))
                    {
                        Console.WriteLine(vechile);
                    }
                }
            }
            else if (cmd.StartsWith("type_of_vehicles"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot.");
                }

                string[] parts = cmd.Split(' ');
                if (parts.Length == 2)
                {
                    List<string> findResult = new List<string>();
                    var result = slotParking.FindAll(p => p.Type.ToLower() == parts[1].ToLower());
                    if (result.Count != 0)
                    {
                        foreach (var vechile in result)
                        {
                            findResult.Add(vechile.RegNumber);
                        }
                            Console.WriteLine(findResult.Count);
                    }
                    else
                    {
                    Console.WriteLine("Not found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format. Try: type_of_vehicles <type>");
                }
            }
            else if (cmd.StartsWith("registration_numbers_for_vehicles_with_colour"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot.");
                }

                string[] parts = cmd.Split(' ');
                if (parts.Length == 2)
                {
                    List<string> findResult = new List<string>();
                    var result = slotParking.FindAll(p => p.Colour.ToLower() == parts[1].ToLower());
                    if (result.Count != 0)
                    {
                        foreach (var vechile in result)
                        {
                            findResult.Add(vechile.RegNumber);
                        }
                            Console.WriteLine(string.Join(", ", findResult));
                    }
                    else
                    {
                    Console.WriteLine("Not Found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format. Try: registration_numbers_for_vehicles_with_colour <colour>");
                }
            }
            else if (cmd.StartsWith("slot_numbers_for_vehicles_with_colour"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot.");
                }

                string[] parts = cmd.Split(' ');
                if (parts.Length == 2)
                {
                    List<string> findResult = new List<string>();
                    var result = slotParking.FindAll(p => p.Colour.ToLower() == parts[1].ToLower());
                    if (result.Count != 0)
                    {
                        foreach (var vechile in result)
                        {
                            findResult.Add(vechile.Slot.ToString());
                        }
                            Console.WriteLine(string.Join(", ", findResult));
                    }
                    else
                    {
                    Console.WriteLine("Not Found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format. Try: slot_numbers_for_vehicles_with_colour <colour>");
                }
            }
            else if (cmd.StartsWith("slot_number_for_registration_number"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot.");
                }

                string[] parts = cmd.Split(' ');
                if (parts.Length == 2)
                {
                    var result = slotParking.FindAll(p => p.RegNumber.ToLower() == parts[1].ToLower());
                    if (result.Count != 0)
                    {
                        foreach (var vechile in result)
                        {
                            Console.WriteLine(vechile.Slot);
                        }
                    }
                    else
                    {
                    Console.WriteLine("Not Found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format. Try: slot_number_for_registration_number <Registration number>");
                }
            }
            else if (cmd.StartsWith("registration_numbers_for_vehicles_with_ood_plate"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot.");
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
                        foreach (var vechile in result)
                        {
                            findResult.Add(vechile.RegNumber);
                        }
                            Console.WriteLine(string.Join(", ", findResult));
                    }
                    else
                    {
                    Console.WriteLine("Not Found");
                    }
                }
            }
            else if (cmd.StartsWith("registration_numbers_for_vehicles_with_event_plate"))
            {
                if (parkingCapacity == 0)
                {
                    Console.WriteLine("Please create parking lot.");
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
                        foreach (var vechile in result)
                        {
                            findResult.Add(vechile.RegNumber);
                        }
                            Console.WriteLine(string.Join(", ", findResult));
                    }
                    else
                    {
                    Console.WriteLine("Not Found");
                    }
                }
            }
        } while (cmd != "exit");
    }
}