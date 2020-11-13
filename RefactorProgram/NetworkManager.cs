using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security;
using System.Diagnostics;

namespace RefactorProgram
{
    class NetworkManager : INetworkManager
    {
        public string LocalPing()
        {
            // Ping's the local machine.
            Ping pingSender = new Ping();
            IPAddress address = IPAddress.Loopback;
            PingReply reply = pingSender.Send(address);

            if (reply.Status == IPStatus.Success)
            {
                StringBuilder s = new StringBuilder();
                s.AppendLine("Address: " + reply.Address.ToString());
                s.AppendLine("Address: " + reply.Address.ToString());
                s.AppendLine("RoundTrip time: " + reply.RoundtripTime);
                s.AppendLine("Time to live: " + reply.Options.Ttl);
                s.AppendLine("Don't fragment: " + reply.Options.DontFragment);
                s.AppendLine("Buffer size: " + reply.Buffer.Length);
                return s.ToString();
            }
            else
            {
                return reply.Status.ToString();
            }
        }

        public string GetHostnameFromIp(string Ip)
        {
            string hostname = "";
            try
            {
                IPHostEntry ipHostEntry = Dns.GetHostByAddress(Ip);
                hostname = ipHostEntry.HostName;
            }
            catch (FormatException)
            {
                hostname = "Please specify a valid IP address.";
                return hostname;
            }
            catch (SocketException)
            {
                hostname = "Unable to perform lookup - a socket error occured.";
                return hostname;
            }
            catch (SecurityException)
            {
                hostname = "Unable to perform lookup - permission denied.";
                return hostname;
            }
            catch (Exception)
            {
                hostname = "An unspecified error occured.";
                return hostname;
            }

            return hostname;
        }

        public string GetIpFromHostname(string Hostname)
        {
            string ip = "";
            try
            {
                IPHostEntry ipHostEntry = Dns.Resolve(Hostname);
                if (ipHostEntry.AddressList.Length > 0)
                {
                    //ip = ipHostEntry.AddressList[0].Address.ToString();
                    ip = ipHostEntry.AddressList[0].ToString();
                }
                else
                {
                    ip = "No information found.";
                }
            }
            catch (SocketException)
            {
                ip = "Unable to perform lookup - a socket error occured.";
                return ip;
            }
            catch (SecurityException)
            {
                ip = "Unable to perform lookup - permission denied.";
                return ip;
            }
            catch (Exception)
            {
                ip = "An unspecified error occured.";
                return ip;
            }

            return ip;
        }

        public string Traceroute(string ipAddressOrHostName)
        {
            IPAddress ipAddress = Dns.GetHostEntry(ipAddressOrHostName).AddressList[0];
            StringBuilder traceResults = new StringBuilder();


            using (Ping pingSender = new Ping())
            {

                PingOptions pingOptions = new PingOptions();
                Stopwatch stopWatch = new Stopwatch();
                byte[] bytes = new byte[32];

                pingOptions.DontFragment = true;
                pingOptions.Ttl = 1;
                int maxHops = 30;

                traceResults.AppendLine(
                    string.Format(
                        "Tracing route to {0} over a maximum of {1} hops:",
                        ipAddress,
                        maxHops));

                traceResults.AppendLine();

                for (int i = 1; i < maxHops + 1; i++)
                {
                    stopWatch.Reset();
                    stopWatch.Start();

                    PingReply pingReply = pingSender.Send(
                        ipAddress,
                        5000,
                        new byte[32], pingOptions);

                    stopWatch.Stop();

                    traceResults.AppendLine(
                        string.Format("{0}\t{1} ms\t{2}",
                        i,
                        stopWatch.ElapsedMilliseconds,
                        pingReply.Address));



                    if (pingReply.Status == IPStatus.Success)
                    {
                        traceResults.AppendLine();
                        traceResults.AppendLine("Trace complete."); break;
                    }

                    pingOptions.Ttl++;

                }
            }
            return traceResults.ToString();
        }

        public List<string> DisplayDhcpServerAddresses()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            IPAddressCollection addresses;
            List<string> informations = new List<string>();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties adapteradapterProperties = adapter.GetIPProperties();
                addresses = adapteradapterProperties.DhcpServerAddresses;
                StringBuilder s = new StringBuilder();
                if (addresses.Count > 0)
                {
                    s.AppendLine(adapter.Description);
                    foreach (IPAddress address in addresses)
                    {
                        s.AppendLine("  Dhcp Address ............................ : " + address.ToString());
                    }
                    informations.Add(s.ToString());

                }
            }
            return informations;
        }
    }
}
