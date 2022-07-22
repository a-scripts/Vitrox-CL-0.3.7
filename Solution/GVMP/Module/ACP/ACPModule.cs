using GTANetworkAPI;
using System;
using System.IO;
using System.Reflection;

namespace GVMP
{
    public class ACPModule : GVMP.Module.Module<ACPModule>
    {
        protected override bool OnLoad()
        {

            return true;
        }

        internal static void unSpectate(Client client) => throw new NotImplementedException();
    }
}