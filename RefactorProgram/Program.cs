using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RefactorProgram
{
    class Program
    {
        static void Main()
        {
            NetworkManager nm = new NetworkManager();
            List<string> options = new List<string>()
            {
                "1: Get host address",
                "2: Make a local ping",
                "3: Get hostname from IP",
                "4: Perform a traceroute",
                "5: Display the DHCP servers",
                "6: Get a host by name"

            };
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select an option...");
                Console.WriteLine();
                Console.WriteLine();
                foreach (string option in options)
                {
                    Console.WriteLine(option);
                }
                try
                {
                    Menu(int.Parse(Console.ReadLine()), nm);
                }
                catch (Exception)
                {
                }
            }
        }

        static void Menu(int input, NetworkManager nm)
        {
            switch (input)
            {
                case 1:
                    Console.WriteLine("Enter a host address - (en.wikipedie.org)");
                    IPAddress[] array = Dns.GetHostAddresses(Console.ReadLine());
                    foreach (IPAddress ip in array)
                    {
                        Console.WriteLine(ip.ToString());
                    }
                    GoBack();
                    break;
                case 2:
                    nm.LocalPing();
                    GoBack();
                    break;
                case 3:
                    Console.WriteLine("Enter IP"); try
                    {
                        string t = nm.GetHostnameFromIp(Console.ReadLine());
                        Console.WriteLine(t);
                        string adr = nm.GetIpFromHostname(t);
                        Console.WriteLine("Weee " + adr);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Something went wrong");
                    }
                    GoBack();
                    break;
                case 4:
                    Console.WriteLine("Enter an IP address or hostname");
                    try
                    {
                        string a = nm.Traceroute("8.8.8.8");
                        Console.WriteLine("route*** " + a);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Something went wrong");
                    }
                    GoBack();
                    break;
                case 5:
                    List<string> informations = nm.DisplayDhcpServerAddresses();
                    if (informations.Count > 0)
                    {
                        foreach (string information in informations)
                        {
                            Console.WriteLine(information);
                        }
                        Console.WriteLine();
                    }
                    GoBack();
                    break;
                case 6:
                    Console.WriteLine("Enter a hostname");
                    nm.GetHostByName(Console.ReadLine());
                    GoBack();
                    break;
                default:
                    break;
            }
        }

        static void GoBack()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to return ...");
            Console.ReadKey();
        }











    }
}
