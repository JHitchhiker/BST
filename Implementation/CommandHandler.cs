using Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Implementation
{
    public class CommandHandler
    {
        /// <summary>
        /// Translates console text into Feed command
        /// </summary>
        /// <param name="trendCommand">console text</param>
        /// <param name="trendFeed">the current feed</param>
        public void HandleCommand(string trendCommand, DataHandler trendFeed)
        {
            if (trendCommand.ToLower().Trim() == "clear")
            {
                Console.Clear();
            }
            else if (trendCommand.IndexOf(" ") == -1)
            {
                List<Post> posts = trendFeed.Read(trendCommand);
                if (posts != null)
                {
                    foreach (Post p in posts)
                    {
                        Console.WriteLine(p.ToTimeLapseString());
                    }
                }
            }
            else if (trendCommand.Contains("->"))
            {
                string[] newTrend = Regex.Split(trendCommand, " -> ");
                try
                {

                    trendFeed.InsertPerson(newTrend[0]);
                    trendFeed.InsertPost(newTrend[0], newTrend[1]);
                }
                catch
                {
                    trendFeed.InsertPost(newTrend[0], newTrend[1]);
                }
            }
            else if (trendCommand.Contains("wall"))
            {
                List<Post> posts = trendFeed.BuildWall(trendCommand.Substring(0, trendCommand.IndexOf(' ')));
                foreach (Post p in posts)
                {
                    Console.WriteLine(p.ToTimeLapseString());
                }

            }
            else if (trendCommand.Contains("follows"))
            {
                trendFeed.Follow(trendCommand.Substring(0, trendCommand.IndexOf(' ')), trendCommand.Substring(trendCommand.LastIndexOf(' ') + 1));
            }
            
        }

        public void Instructions()
        {
            Console.WriteLine("*** Welcome to TrendLine Messaging board ***");
            Console.WriteLine("Keep up to date with what your friends are doing.");
            Console.WriteLine("No logging in or personal info needed, just connect and enjoy!");
            Console.WriteLine("\r\nHow to use the notice board");
            Console.WriteLine("<---->");
            Console.WriteLine("Posting :- Your first post will create your profile.");
            Console.WriteLine("[username] -> [post text]");
            Console.WriteLine(">----<");
            Console.WriteLine("Reading your own posts");
            Console.WriteLine("[username]");
            Console.WriteLine(">----<");
            Console.WriteLine("Following a friend");
            Console.WriteLine("[username] follows [username]");
            Console.WriteLine(">----<");
            Console.WriteLine("Checking your wall");
            Console.WriteLine("[username] wall");
            Console.WriteLine(">----<");
            Console.WriteLine("Clear the screen when it gets out of hand");
            Console.WriteLine("clear");
            Console.WriteLine("");
            Console.WriteLine("Press any key to get started:");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
