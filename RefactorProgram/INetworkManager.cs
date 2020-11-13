using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorProgram
{
    interface INetworkManager
    {
        string LocalPing();
        string GetHostnameFromIp(string Ip);
        string GetIpFromHostname(string Hostname);
        string Traceroute(string ipAddressOrHostName);
        List<string> DisplayDhcpServerAddresses();
    }
}
