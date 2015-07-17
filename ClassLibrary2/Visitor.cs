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

    public class NameVisitor : IVisitor
    {
        private Person _found;
        private Person _target;

        /// <summary>
        /// cTor
        /// </summary>
        /// <param name="person">The desired person</param>
        public NameVisitor(Person person)
        {
            _target = person;
        }

        /// <summary>
        /// Compare the username of the current node to the target node
        /// </summary>
        /// <param name="entity">Current Node</param>
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
