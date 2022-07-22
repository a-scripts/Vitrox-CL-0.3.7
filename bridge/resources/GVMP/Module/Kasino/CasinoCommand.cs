﻿using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GVMP
{
    class CasinoCommand
    {
        public string Name
        {
            get;
            set;
        }

        public int Permission
        {
            get;
            set;
        }

        public Action<DbPlayer, string[]> Callback
        {
            get;
            set;
        }

        public Action<Client, DbPlayer, string[]> Callback2
        {
            get;
            set;
        }

        public int Args
        {
            get;
            set;
        }


        public CasinoCommand(Action<DbPlayer, string[]> Callback, string Name, int Permission, int Args)
        {
            this.Name = Name;
            this.Permission = Permission;
            this.Callback = Callback;
            this.Args = Args;
        }

        public CasinoCommand(Action<Client, DbPlayer, string[]> Callback, string Name, int Permission, int Args)
        {
            this.Name = Name;
            this.Permission = Permission;
            this.Callback2 = Callback;
            this.Args = Args;
        }

    }
}
