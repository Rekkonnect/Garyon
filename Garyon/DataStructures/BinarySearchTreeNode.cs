using Garyon.Exceptions;
using Garyon.Extensions;
using Garyon.Functions;
using Garyon.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.DataStructures
{
    /// <summary>Represents a tree node, which is a component that is contained within a tree.</summary>
    /// <typeparam name="T">The type of the value that is stored in the node.</typeparam>
    public class BinarySearchTreeNode<T> : BinarySearchTreeNode<T, BinarySearchTree<T>, BinarySearchTreeNode<T>>
        where T : IComparable<T>
    {
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTreeNode{T}"/> class with no base tree, parent or children nodes.</summary>
        public BinarySearchTreeNode(T value = default)
            : base(value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTreeNode{T}"/> class with a base tree, but no parent or children nodes.</summary>
        /// <param name="baseTree">The base tree that contains this node.</param>
        public BinarySearchTreeNode(BinarySearchTree<T> baseTree, T value = default)
            : base(baseTree, value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTreeNode{T}"/> class with a parent node, but no children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        public BinarySearchTreeNode(BinarySearchTreeNode<T> parentNode, T value = default)
            : base(parentNode, value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTreeNode{T}"/> class with a parent and children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="child1">The first child node.</param>
        /// <param name="child2">The second child node.</param>
        public BinarySearchTreeNode(BinarySearchTreeNode<T> parentNode, BinarySearchTreeNode<T> child1, BinarySearchTreeNode<T> child2, T value = default)
            : base(parentNode, child1, child2, value) { }

        #region Abstract Constructors
        protected override BinarySearchTreeNode<T> InitializeNewNode(T value = default) => new BinarySearchTreeNode<T>(value);
        protected override BinarySearchTreeNode<T> InitializeNewNode(BinarySearchTree<T> baseTree, T value = default) => new BinarySearchTreeNode<T>(baseTree, value);
        protected override BinarySearchTreeNode<T> InitializeNewNode(BinarySearchTreeNode<T> parentNode, T value = default) => new BinarySearchTreeNode<T>(parentNode, value);
        #endregion
    }

    /// <summary>Represents a tree node, which is a component that is contained within a tree.</summary>
    /// <typeparam name="TValue">The type of the value that is stored in the node.</typeparam>
    /// <typeparam name="TTree">The type of the tree that this type is used in.</typeparam>
    /// <typeparam name="TTreeNode">The type of the tree nodes that are used in the <typeparamref name="TTree"/> type.</typeparam>
    public abstract class BinarySearchTreeNode<TValue, TTree, TTreeNode> : BinaryTreeNode<TValue, TTree, TTreeNode>
        where TValue : IComparable<TValue>
        where TTree : BinarySearchTree<TValue, TTree, TTreeNode>
        where TTreeNode : BinarySearchTreeNode<TValue, TTree, TTreeNode>
    {
        /// <summary>Gets or sets the parent node of this tree node.</summary>
        public override TTreeNode Parent
        {
            set
            {
                InternalParent?.RemoveChild(This);
                InternalParent = value;
                InternalParent?.AddChild(This);
            }
        }

        /// <summary>Gets or sets the children nodes of this tree. The retrieved list is a copy of the internally stored list, therefore it is best to call this property as less as possible.</summary>
        /// <value>The new list of nodes to set the children to. If the list is <see langword="null"/>, a new list is initialized.</value>
        public override List<TTreeNode> Children
        {
            set
            {
                value ??= new List<TTreeNode>(0);

                if (value.Count > 2)
                    ThrowHelper.Throw<InvalidOperationException>("Cannot send the children of a binary tree node to more than 2 nodes.");

                VerifyChildrenPosition(value[0], value[1]);

                // Have the base implementation handle the rest of the operations
                base.Children = value;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTreeNode{T}"/> class with no base tree, parent or children nodes.</summary>
        public BinarySearchTreeNode(TValue value = default)
            : base(value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTreeNode{T}"/> class with a base tree, but no parent or children nodes.</summary>
        /// <param name="baseTree">The base tree that contains this node.</param>
        public BinarySearchTreeNode(TTree baseTree, TValue value = default)
            : base(baseTree, value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTreeNode{T}"/> class with a parent node, but no children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        public BinarySearchTreeNode(TTreeNode parentNode, TValue value = default)
            : base(parentNode, value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTreeNode{T}"/> class with a parent and children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="child1">The first child node.</param>
        /// <param name="child2">The second child node.</param>
        public BinarySearchTreeNode(TTreeNode parentNode, TTreeNode child1, TTreeNode child2, TValue value = default)
            : base(parentNode, value)
        {
            if (child1.Value.GreaterThan(child2.Value))
                Misc.Swap(ref child1, ref child2);

            VerifyChildrenPosition(child1.Value, child2.Value);

            LeftChild = child1;
            RightChild = child2;
        }

        private void VerifyChildrenPosition(TTreeNode left, TTreeNode right) => VerifyChildrenPosition(left.Value, right.Value);
        private void VerifyChildrenPosition(TValue left, TValue right)
        {
            if (left.GreaterThan(right))
                ThrowException(left, Value, right);

            if (left.GreaterThan(Value))
                ThrowException(left, Value, right);

            if (right.LessThan(Value))
                ThrowException(left, Value, right);

            static void ThrowException(TValue left, TValue parent, TValue right) => ThrowHelper.Throw<ArgumentException>(GetExceptionMessage(left, parent, right));
            static string GetExceptionMessage(TValue left, TValue parent, TValue right) => $"The nodes should have the order Left < Parent < Right. Given values were:\nLeft: {left}\n Parent: {parent}\nRight: {right}";
        }

        #region Children
        /// <summary>Gets the leftmost direct child of this node that has the specified value.</summary>
        /// <param name="value">The value of the direct child node to find.</param>
        /// <returns>The <seealso cref="BinarySearchTreeNode{T}"/> with the specified value, if found; otherwise <see langword="null"/>.</returns>
        public override TTreeNode GetChild(TValue value)
        {
            if (Matches(LeftChild, value))
                return LeftChild;
            if (Matches(RightChild, value))
                return RightChild;

            return null;

            static bool Matches(TTreeNode node, TValue value)
            {
                if (node != null)
                    if (node.Value.Equals(value))
                        return true;
                return false;
            }
        }

        /// <summary>Adds a child to the first available spot from the two children spots.</summary>
        /// <param name="newChild">The new child to add.</param>
        public override void AddChild(TTreeNode newChild)
        {
            if (newChild.Value.LessThan(Value))
            {
                if (LeftChild == null)
                    LeftChild = newChild;
                else
                    return;
            }
            else
            {
                if (RightChild == null)
                    RightChild = newChild;
                else
                    return;
            }

            RegisterAddedChild(newChild);
        }
        /// <summary>Adds children to the available spots from the two children spots. If both are unoccupied, both nodes are added in the provided order, otherwise only the <paramref name="left"/> is added, if there is any unoccupied spot.</summary>
        /// <param name="left">The left child to add.</param>
        /// <param name="right">The right child to add.</param>
        public override void AddChildren(TTreeNode left, TTreeNode right)
        {
            AddChild(left);
            AddChild(right);
        }
        #endregion

        #region Subtree Nodes
        /// <summary>Adds a new node to this subtree.</summary>
        /// <param name="value">The value of the node to add to this subtree.</param>
        /// <returns><see langword="true"/> if the node was successsfully added to this subtree, otherwise <see langword="false"/>.</returns>
        public bool AddNode(TValue value) => AddNode(InitializeNewNode(value));
        /// <summary>Adds a new node to this subtree.</summary>
        /// <param name="node">The node to add to this subtree.</param>
        /// <returns><see langword="true"/> if the node was successsfully added to this subtree, otherwise <see langword="false"/>.</returns>
        public bool AddNode(TTreeNode node)
        {
            var existingNode = Seek(node.Value, out var parent);
            if (existingNode != null)
                return false;

            parent.AddChild(node);
            return true;
        }

        /// <summary>Adds a collection of nodes to this subtree.</summary>
        /// <param name="values">The values of the nodes to add to this subtree.</param>
        public void AddNodes(IEnumerable<TValue> values) => AddNodes(values.Select(v => InitializeNewNode(v)));
        /// <summary>Adds a collection of nodes to this subtree.</summary>
        /// <param name="nodes">The the nodes to add to this subtree.</param>
        public void AddNodes(IEnumerable<TTreeNode> nodes)
        {
            foreach (var n in nodes)
                AddNode(n);
        }

        /// <summary>Adds a collection of nodes to this subtree.</summary>
        /// <param name="values">The values of the nodes to add to this subtree.</param>
        public void AddNodes(params TValue[] values) => AddNodes((IEnumerable<TValue>)values);
        /// <summary>Adds a collection of nodes to this subtree.</summary>
        /// <param name="nodes">The the nodes to add to this subtree.</param>
        public void AddNodes(params TTreeNode[] nodes) => AddNodes((IEnumerable<TTreeNode>)nodes);

        /// <summary>Removes a node with the specified value from this subtree.</summary>
        /// <param name="value">The value of the node to remove from this subtree.</param>
        /// <returns><see langword="true"/> if the node was successfully removed from this subtree, otherwise <see langword="false"/>.</returns>
        public override bool RemoveNode(TValue value)
        {
            return RemoveNode(Find(value));
        }

        /// <summary>Attempts to find a node with the specified value within this subtree.</summary>
        /// <param name="value">The value of the node to be found.</param>
        /// <returns>The node within this subtree that has the specified value, if found, otherwise <see langword="null"/>.</returns>
        public TTreeNode Find(TValue value)
        {
            return Seek(value, out _);
        }

        private TTreeNode Seek(TValue value, out TTreeNode parent)
        {
            parent = Parent;
            var currentNode = This;

            do
            {
                var comparison = value.GetComparisonResult(currentNode.Value);

                if (comparison == ComparisonResult.Equal)
                    return currentNode;

                parent = currentNode;

                if (comparison == ComparisonResult.Less)
                    currentNode = currentNode.LeftChild;
                else
                    currentNode = currentNode.RightChild;
            }
            while (currentNode != null);

            return null;
        }
        #endregion

        protected override void AddChildrenToClonedInstance(TTreeNode result)
        {
            if (LeftChild != null)
                result.AddChild(LeftChild.Clone(false));
            if (RightChild != null)
                result.AddChild(RightChild.Clone(false));
        }
    }
}
