using MPinger.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MPinger
{
    public class MPing
    {
        public static void PingHost(PingStationModel device)
        {
            bool pingable = false;
            Ping pinger = null;

            if (device.IPAddress != "")
            {

                try
                {
                    pinger = new Ping();
                    PingReply reply = pinger.Send(device.IPAddress.Trim());
                    pingable = reply.Status == IPStatus.Success;
                    ResultPing.PingResultStations.Add(new PingStationModel
                    {
                        Id = device.Id,
                        Name = device.Name,
                        IPAddress = device.IPAddress,
                        Pingable = pingable,
                        Description = reply.Status.ToString()
                    });

                }
                catch (PingException)
                {
                    // Discard PingExceptions and return false;
                }
                finally
                {
                    if (pinger != null)
                    {
                        pinger.Dispose();
                    }
                }
            }
        }
    }

}
