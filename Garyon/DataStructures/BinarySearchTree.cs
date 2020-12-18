using System;
using System.Collections.Generic;

namespace Garyon.DataStructures
{
    /// <summary>Represents a tree.</summary>
    /// <typeparam name="T">The type of the elements the tree nodes store.</typeparam>
    public class BinarySearchTree<T> : BinarySearchTree<T, BinarySearchTree<T>, BinarySearchTreeNode<T>>
        where T : IComparable<T>
    {
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTree{T}"/> class with no root node.</summary>
        public BinarySearchTree()
            : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTree{T}"/> class with a root value.</summary>
        /// <param name="rootValue">The value of the root node.</param>
        public BinarySearchTree(T rootValue)
            : base(rootValue) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTree{T}"/> class with a root node.</summary>
        /// <param name="root">The root node.</param>
        public BinarySearchTree(BinarySearchTreeNode<T> root)
            : base(root) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTree{T}"/> class from a different tree.</summary>
        /// <param name="tree">The tree to create this tree from. Both trees remain independent.</param>
        public BinarySearchTree(BinarySearchTree<T> tree)
            : base(tree) { }

        #region Abstract Constructors
        protected override BinarySearchTreeNode<T> InitializeNewNode(T value = default) => new BinarySearchTreeNode<T>(value);
        protected override BinarySearchTreeNode<T> InitializeNewNode(BinarySearchTree<T> baseTree, T value = default) => new BinarySearchTreeNode<T>(baseTree, value);
        protected override BinarySearchTreeNode<T> InitializeNewNode(BinarySearchTreeNode<T> parentNode, T value = default) => new BinarySearchTreeNode<T>(parentNode, value);
        #endregion
    }

    /// <summary>Represents a tree.</summary>
    /// <typeparam name="TValue">The type of the elements the tree nodes store.</typeparam>
    /// <typeparam name="TTree">The type of the tree that this type is used in.</typeparam>
    /// <typeparam name="TTreeNode">The type of the tree nodes that are used in the <typeparamref name="TTree"/> type.</typeparam>
    public abstract class BinarySearchTree<TValue, TTree, TTreeNode> : BinaryTree<TValue, TTree, TTreeNode>
        where TValue : IComparable<TValue>
        where TTree : BinarySearchTree<TValue, TTree, TTreeNode>
        where TTreeNode : BinarySearchTreeNode<TValue, TTree, TTreeNode>
    {
        // Something is extremely suspiciously wrong here
        // Investigate and ensure that the entire BinarySearchTree does not reek of uncleaniness

        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTree{TValue, TTree, TTreeNode}"/> class with no root node.</summary>
        protected BinarySearchTree()
            : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTree{TValue, TTree, TTreeNode}"/> class with a root value.</summary>
        /// <param name="rootValue">The value of the root node.</param>
        protected BinarySearchTree(TValue rootValue)
            : base(rootValue) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTree{TValue, TTree, TTreeNode}"/> class with a root node.</summary>
        /// <param name="root">The root node.</param>
        protected BinarySearchTree(TTreeNode root)
            : base(root) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinarySearchTree{TValue, TTree, TTreeNode}"/> class from a different tree.</summary>
        /// <param name="tree">The tree to create this tree from. Both trees remain independent.</param>
        protected BinarySearchTree(TTree tree)
            : base(tree) { }

        public TTreeNode Find(TValue value)
        {
            return Root.Find(value);
        }
    }
}
