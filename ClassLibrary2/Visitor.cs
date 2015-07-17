using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas
{
    public interface IVisitor
    {
        bool Done { get; }
        void Visit(Entity entity);
    }

    public class PersonVisitor : IVisitor
    {
        private Person _found;
        private Person _target;

        public PersonVisitor(Person person)
        {
            _target = person;
        }

        public void Visit(Entity entity)
        {
            Node node = entity as Node;
            if (node.Value.Equals(_target))
            {
                _found = node.Value;
            }
        }

        public bool Done
        {
            get { return (_found != null); }
        }
    }

    public class NameVisitor : IVisitor
    {
        private Person _found;
        private Person _target;

        public NameVisitor(Person person)
        {
            _target = person;
        }

        public void Visit(Entity entity)
        {
            Person person = entity as Person;
            if (String.Compare(_target.Username, person.Username)==0)
            {
                _found = person;
            }
        }

        public bool Done
        {
            get { return (_found != null); }
        }
    }

    public abstract class Entity
    {
        public abstract void Accept(IVisitor visitor);
    }

}
