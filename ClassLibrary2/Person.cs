using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas
{
    public class Person : Entity, IComparable<Person>
    {
        public string Username { get; set; }
        public List<Post> Posts { get; set; }
        public List<string> Following { get; set; }

        public Person()
        {
            Posts = new List<Post>();
            Following = new List<string>();
        }
        public int CompareTo(Person other)  
        {
            return String.Compare(other.Username,Username);
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Post
    {
        public DateTime PostTime { get; set; }
        public string PostText { get; set; }

        public Post()
        {
        }

        public Post (string postText)
        {
            PostTime = DateTime.Now;
            PostText = postText;
        }
    }
}
