﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Implementation;
using Datas;
using System.Text.RegularExpressions;

namespace trendLine
{
    class Program
    {
        static void Main(string[] args)
        {
            DataHandler dataHandler = DataHandler.Instance;
            CommandHandler trendFeed = new CommandHandler();
            bool quit = false;

            
            string trendCommand ="";
            while (!quit)
            {
                trendCommand = Console.ReadLine().ToString();
                trendFeed.HandleCommand(trendCommand, dataHandler);
            }
        }

        
    }
}