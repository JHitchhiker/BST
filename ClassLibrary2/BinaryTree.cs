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

        
        private Node findParent(Node key, ref Node parent)
        {
            PersonVisitor visitor = new PersonVisitor(key.Value);
            Node rootNode = root;
            parent = null;
            int comparison;

            while (!visitor.Done)
            {
                rootNode.Accept(visitor);
                comparison = rootNode.Value.CompareTo(key.Value);
                if (comparison == 0)
                {
                    return rootNode;
                }

                if (comparison < 0)
                {
                    parent = rootNode;
                    rootNode = rootNode.LeftNode;
                }
                else
                {
                    parent = rootNode;
                    rootNode = rootNode.RightNode;
                }
            }
            
            return null;
        }

        /// <summary>
        /// Find the left most node on the right branch.
        /// </summary>
        /// <param name="startNode">Name key to use for searching</param>
        /// <param name="parent">Returns the parent node if search successful</param>
        /// <returns>Returns a reference to the node if successful, else null</returns>
        public Node findSuccessor(Node startNode, ref Node parent)
        {
            parent = startNode;
            // Look for the left-most node on the right side
            startNode = startNode.RightNode;
            while (startNode.LeftNode != null)
            {
                parent = startNode;
                startNode = startNode.LeftNode;
            }
            return startNode;
        }

        private void removeNode(Node p)
        {
            if (p != null)
            {
                removeNode(p.LeftNode);
                removeNode(p.RightNode);
                p = null;
            }
        }
    }
    #endregion
}
