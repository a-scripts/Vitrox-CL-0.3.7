﻿using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMP
{
    public static class Logger
    {
        public static void Print(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[VC] ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(msg);
        }
    }
}
