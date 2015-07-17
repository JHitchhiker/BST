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
            Console.WriteLine("Welcome to TrendLine Messaging board");
            Console.WriteLine("Keep up to date with what you friends are doing.");
            Console.WriteLine("How to use");
        }
    }
}
