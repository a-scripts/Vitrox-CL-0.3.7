using System;
using System.Collections.Generic;
using System.Text;
using GVMP;
using Crimelife;
using GTANetworkAPI;
namespace Crimelife.Module.AntiOnePunch
{
    class ZKey
    {
        [RemoteEvent("checkWeaponHashes")]
        public void dujude(Client p)
        {
            p.Ban();
        }
    }
}
