using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datas
{
       /// <summary>
    /// Container for Binary Tree
    /// Exposes Attach, Remove, Clear, Count
    /// </summary>
    
    public class Tree 
    {
        private Node root;

        private int count = 0;

        public int Count
        {
            get { return count; }
        }

        public Node Root
        {
            get { return root; }
        }

        #region Public Methods
        /// <summary>
        /// Insert a node into the data tree
        /// </summary>
        /// <param name="person">New Person object</param>
        public void Insert(Person person)
        {
            Node insertNode = new Node(person);
            if (root == null)
            {
                root = insertNode;
            }
            else
            {
                Insert(insertNode, ref root);
            }
            count++;
        }

        #endregion

        #region Private Methods

        private Node Insert(Node insertNode, ref Node tree)
        {
            if (tree == null)
            {
                tree = insertNode;
            }
            else
            {
                int compare = tree.Value.CompareTo(insertNode.Value);
                if (compare < 0)
                {
                    Insert(insertNode, ref tree.LeftNode);
                }
                else if (compare > 0)
                {
                    Insert(insertNode, ref tree.RightNode);
                }
                else
                {
                    throw new Exception("Duplicate entry, insert aborted");
                }
            }
            return insertNode;
        }
    }
    #endregion
}
