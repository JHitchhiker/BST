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
    
    public class DataHandler : IDisposable
    {
        private Tree tree = new Tree();
      
        /// <summary>
        /// Insert a new user into the tree
        /// </summary>
        /// <param name="username">New UserName</param>
        /// <returns></returns>
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

        /// <summary>
        /// Insert a chronological post for an existing user
        /// </summary>
        /// <param name="user">Username</param>
        /// <param name="postText">Post text</param>
        public void InsertPost(string user, string postText)
        {
            Person p = Find(user);
            p.Posts.Add(new Post(postText));
        }

        /// <summary>
        /// Follow a post from another user
        /// </summary>
        /// <param name="user">Current user</param>
        /// <param name="followed">User to follow</param>
        public void Follow(string user, string followed)
        {
            Person p = Find(user);
            p.Following.Add(followed);
        }

        /// <summary>
        /// Chronologically list all the CURRENT users posts
        /// </summary>
        /// <param name="userName">Desired Username</param>
        /// <returns></returns>
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
        /// including all followed posts
        /// </summary>
        /// <param name="userName">Current username</param>
        /// <returns>a formatted chronological list of wall postings, most recent to eldest</string></returns>
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

        /// <summary>
        /// See if a user exists
        /// </summary>
        /// <param name="username">Desired username</param>
        /// <returns>The desired person or null if not found</returns>
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

        public void Dispose()
        {
            tree = null;
        }
    }
}