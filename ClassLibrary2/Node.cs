using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas
{
    public class Node : Entity
    {
        public Person Value { get; set; }
        public Node RightNode;
        public Node LeftNode;

        public Node(Person person)
        {
            RightNode = null;
            LeftNode = null;
            Value = person;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this.Value);
        }
    }
}
    