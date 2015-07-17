using System.Drawing;
using Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Implementation
{
    
    public class DataHandler
    {

        private static DataHandler instance;

        private DataHandler() { }

        public static DataHandler Instance
        {
            get 
                {
                    if (instance == null)
                    {
                        instance = new DataHandler();
                    }
                    return instance;
                }
        }
        private static Tree tree = new Tree();
      

        public bool InsertPerson(string username)
        {
            try
            {
                tree.Insert(new Person { Username = username });
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void InsertPost(string user, string postText)
        {
            Person p = Find(user);
            p.Posts.Add(new Post(postText));
        }

        public void Follow(string user, string followed)
        {
            Person p = Find(user);
            p.Following.Add(followed);
        }

        public List<Post> Read(string userName)
        {
            List<Post> posts = new List<Post>();
            Person user = Find(userName);
            if (user != null)
            {
                posts.AddRange(user.Posts);
            }
            else
            {
                return null;
            }
            return posts.OrderBy(p => p.PostTime).ToList();
        }   
        /// <summary>
        /// Fetch all posts relevant to the current user
        /// </summary>
        /// <param name="userName">Current username</param>
        /// <param name="includeAll">If true, then read function, else wall function</param>
        /// <returns>a formatted list of wall postings</string></returns>
        public List<Post> BuildWall(string userName)
        {
            
            List<Post> returnPosts = new List<Post>();
            Person user = Find(userName);
            if (user != null)
            {
                returnPosts.AddRange(user.Posts.Select(s => new Post { PostText = user.Username + " - " + s.PostText, PostTime = s.PostTime } ).ToList());
            }
            else
            {
                return null;
            }
            if (user.Following.Count != 0)
            {
                foreach (string following in user.Following)
                {
                    Person followed = Find(following);
                    returnPosts.AddRange(followed.Posts.Select(s => new Post { PostText = followed.Username + " - " + s.PostText, PostTime = s.PostTime } ).ToList());
                }
            }
            return returnPosts.OrderByDescending(p => p.PostTime).ToList();
        }

        public Person Find(string username)
        {
            Node node = tree.Root;
            Person person = new Person { Username = username };

            NameVisitor visitor = new NameVisitor(person);
            try
            {
                while (!visitor.Done)
                {
                    node.Accept(visitor);
                    int compare = node.Value.CompareTo(person);
                    if (compare < 0)
                    {
                        node = node.LeftNode;
                    }
                    else if (compare > 0)
                    {
                        node = node.RightNode;
                    }
                }
                person = node.Value;
            }
            catch
            {
                return null;
            }
            return person;
        }   
    }
}