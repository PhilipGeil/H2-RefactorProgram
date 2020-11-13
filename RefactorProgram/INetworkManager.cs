using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorProgram
{
    interface INetworkManager
    {
        /// <summary>
        /// Makes a ping to localhost
        /// </summary>
        /// <returns></returns>
        string LocalPing();
        /// <summary>
        /// Retrieves the hostname if possible from the given IP address
        /// </summary>
        /// <param name="Ip"></param>
        /// <returns></returns>
        string GetHostnameFromIp(string Ip);
        /// <summary>
        /// Retrieves the IP address from the gives hostname
        /// </summary>
        /// <param name="Hostname"></param>
        /// <returns></returns>
        string GetIpFromHostname(string Hostname);
        /// <summary>
        /// Performs a trace route using the given hostname or IP address
        /// </summary>
        /// <param name="ipAddressOrHostName"></param>
        /// <returns></returns>
        string Traceroute(string ipAddressOrHostName);
        /// <summary>
        /// Displays the DHCP server addresses
        /// </summary>
        /// <returns></returns>
        List<string> DisplayDhcpServerAddresses();
        /// <summary>
        /// Gets the host by name
        /// </summary>
        /// <returns></returns>
        string GetHostByName(string name);
    }
}
