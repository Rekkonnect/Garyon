using System;
using System.Collections;
using System.Collections.Generic;
using Garyon.Exceptions;
using Garyon.Functions;
using Garyon.Objects.Enumerators;

namespace Garyon.DataStructures
{
    /// <summary>Represents a tree.</summary>
    /// <typeparam name="TValue">The type of the elements the tree nodes store.</typeparam>
    /// <typeparam name="TTree">The type of the tree that this type is used in.</typeparam>
    /// <typeparam name="TTreeNode">The type of the tree nodes that are used in the <typeparamref name="TTree"/> type.</typeparam>
    public abstract class BaseTree<TValue, TTree, TTreeNode> : ITree<TValue, TTree, TTreeNode>
        where TTree : BaseTree<TValue, TTree, TTreeNode>
        where TTreeNode : BaseTreeNode<TValue, TTree, TTreeNode>
    {
        /// <summary>
        /// <para>The cached count of elements within this tree.</para>
        /// <para>It is <see langword="null"/> if the element count has never been calculated.</para>
        /// <para>If the value is not <see langword="null"/> and any modification occurs, it will be set to <see langword="null"/>.</para>
        /// </summary>
        protected int? CachedCount;
        /// <summary>The internal field for the root of this tree. If no side-effects are bound to occur, prefer setting this over the <see cref="Root"/> property.</summary>
        protected TTreeNode InternalRoot;

        /// <summary>Gets this tree node instance as a <typeparamref name="TTree"/>.</summary>
        protected TTree This => this as TTree;

        /// <summary>Gets or sets the root node.</summary>
        public TTreeNode Root
        {
            get => InternalRoot;
            set
            {
                if (Checks.SafeEquals(InternalRoot, value))
                    return;

                if (value?.Parent != null)
                    ThrowHelper.Throw<InvalidOperationException>("Cannot set the root of the tree to a node that has a parent.");

                CachedCount = null;

                // Update base trees
                if (InternalRoot != null)
                    InternalRoot.BaseTree = default;
                if (value != null)
                    value.BaseTree = This;
                InternalRoot = value;
            }
        }

        /// <summary>Gets the number of elements that this tree has. The count is only calculated upon the first request after any adjustments that were made to the tree.</summary>
        public int Count
        {
            get
            {
                if (CachedCount == null)
                    CachedCount = (InternalRoot?.Descendants + 1) ?? 0;
                return CachedCount.Value;
            }
        }

        /// <summary>Gets the height of this tree. It is equal to the height of the root node.</summary>
        public int Height => Root.Height;
        /// <summary>Gets the breadth of this tree. It is equal to the breadth of the root node.</summary>
        public int Breadth => Root.Breadth;

        bool ICollection<TValue>.IsReadOnly => false;

        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class with no root node.</summary>
        public BaseTree() { }
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class with a root value.</summary>
        /// <param name="rootValue">The value of the root node.</param>
        public BaseTree(TValue rootValue)
        {
            Root = InitializeNewNode(rootValue);
        }
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class with a root node.</summary>
        /// <param name="root">The root node.</param>
        public BaseTree(TTreeNode root)
        {
            Root = root;
        }
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class from a different tree.</summary>
        /// <param name="tree">The tree to create this tree from. Both trees remain independent.</param>
        public BaseTree(TTree tree)
        {
            InternalRoot = tree.InternalRoot.Clone();
            InternalRoot.BaseTree = This;
        }

        #region Abstract Node Constructors
        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with no base tree or parent node.</summary>
        /// <param name="value">The value of the node.</param>
        protected abstract TTreeNode InitializeNewNode(TValue value = default);
        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with a base tree, but no parent node.</summary>
        /// <param name="baseTree">The base tree that contains this node.</param>
        /// <param name="value">The value of the node.</param>
        protected abstract TTreeNode InitializeNewNode(TTree baseTree, TValue value = default);
        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with a parent node. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="value">The value of the node.</param>
        protected abstract TTreeNode InitializeNewNode(TTreeNode parentNode, TValue value = default);
        #endregion

        /// <summary>Determines whether this tree contains a node with the specified value.</summary>
        /// <param name="value">The value to determine whether it is contained within the tree.</param>
        /// <returns>A value determining whether the requested value was found.</returns>
        public virtual bool Contains(TValue value) => GetNode(value) != null;

        /// <summary>Gets a child node within this tree that has the specified value.</summary>
        /// <param name="value">The value of the child tree node to find.</param>
        /// <returns>The <seealso cref="TreeNode{T}"/> with the specified value, if found; otherwise <see langword="false"/>.</returns>
        public virtual TTreeNode GetNode(TValue value) => Root?.GetNode(value);

        /// <summary>Removes a child within this tree.</summary>
        /// <param name="value">The value of the child to remove from the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BaseTreeNode{TValue, TTree, TTreeNode}.RemoveChild(TTreeNode)"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        /// <returns><see langword="true"/> if the node was successfully removed, otherwise <see langword="false"/>.</returns>
        public bool RemoveNode(TValue value, bool propagate)
        {
            return RemoveNode(GetNode(value), propagate);
        }
        /// <summary>Removes a child within this tree.</summary>
        /// <param name="value">The value of the child to remove from the node.</param>
        public bool RemoveNode(TValue value) => RemoveNode(value, true);
        /// <summary>Removes a child within this tree.</summary>
        /// <param name="childToRemove">The child to remove from the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BaseTreeNode{TValue, TTree, TTreeNode}.RemoveChild(TTreeNode)"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        /// <returns><see langword="true"/> if the node was successfully removed, otherwise <see langword="false"/>.</returns>
        public bool RemoveNode(TTreeNode childToRemove, bool propagate)
        {
            return RemoveChild(childToRemove.Parent, childToRemove, propagate);
        }
        /// <summary>Removes a child within this tree.</summary>
        /// <param name="childToRemove">The child to remove from the node.</param>
        public bool RemoveNode(TTreeNode childToRemove) => RemoveNode(childToRemove, true);
        /// <summary>Removes nodes within this tree that contain the specified values.</summary>
        /// <param name="values">The values of the children to remove.</param>
        public void RemoveNodes(IEnumerable<TValue> values) => RemoveNodes(InternalRoot, values);
        /// <summary>Removes nodes within a subtree with root as the specified node that contain the specified values.</summary>
        /// <param name="node">The node which is the root of the subtree in which to remove the nodes at.</param>
        /// <param name="values">The values of the children to remove.</param>
        public void RemoveNodes(TTreeNode node, IEnumerable<TValue> values)
        {
            node.RemoveNodes(values);
        }

        /// <summary>Removes a child from a node within this tree.</summary>
        /// <param name="node">The parent node whose children to set. The node must be within this tree.</param>
        /// <param name="childToRemove">The child to remove from the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BaseTreeNode{TValue, TTree, TTreeNode}.RemoveChild(TTreeNode)"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        /// <returns><see langword="true"/> if the child was sucessfully removed from the node, otherwise <see langword="false"/>.</returns>
        public bool RemoveChild(TTreeNode node, TTreeNode childToRemove, bool propagate)
        {
            if (Checks.SafeEquals(childToRemove, InternalRoot))
            {
                Clear();
                return true;
            }

            if (!PrepareChildrenAdjustmentOperation(node))
                return false;

            if (propagate)
                node.RemoveChild(childToRemove);

            return true;
        }

        /// <summary>Removes a node from the tree that has the value that is equal to the provided value.</summary>
        /// <param name="value">The value of the node to find and remove.</param>
        /// <returns><see langword="true"/> if the node with the requested value was found and removed, otherwise <see langword="false"/>.</returns>
        public bool Remove(TValue value)
        {
            return RemoveNode(value);
        }

        /// <summary>Clears the children of a node with the specified value, that is within this tree.</summary>
        /// <param name="value">The value of the parent node whose children to clear. The node must be within this tree.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BaseTreeNode{TValue, TTree, TTreeNode}.Children"/> property. Only set this to <see langword="false"/> if the new children are already set in the node before calling this function.</param>
        public void ClearChildren(TValue value, bool propagate)
        {
            ClearChildren(GetNode(value), propagate);
        }
        /// <summary>Clears the children of a node with the specified value, that is within this tree.</summary>
        /// <param name="value">The value of the parent node whose children to clear. The node must be within this tree.</param>
        public void ClearChildren(TValue value) => ClearChildren(value, true);
        /// <summary>Clears the children of a node within this tree.</summary>
        /// <param name="node">The parent node whose children to clear. The node must be within this tree.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BaseTreeNode{TValue, TTree, TTreeNode}.Children"/> property. Only set this to <see langword="false"/> if the new children are already set in the node before calling this function.</param>
        public abstract void ClearChildren(TTreeNode node, bool propagate);
        /// <summary>Clears the children of a node within this tree.</summary>
        /// <param name="node">The parent node whose children to clear. The node must be within this tree.</param>
        public void ClearChildren(TTreeNode node) => ClearChildren(node, true);

        /// <summary>Clears all this tree's nodes, including its root.</summary>
        public void Clear() => Root = default;

        /// <summary>Performs some operations to prepare for any children adjustment operation (add, remove, set, clear).</summary>
        /// <param name="node">The node on which the children adjustment operation is to be performed.</param>
        /// <returns><see langword="true"/> if the operation can be performed, otherwise <see langword="false"/>. This requires <paramref name="node"/> to not be <see langword="null"/> and belong to this tree.</returns>
        protected bool PrepareChildrenAdjustmentOperation(TTreeNode node)
        {
            if (node == null)
                return false;
            if (!Checks.SafeEquals(node.BaseTree, this))
                return false;
            CachedCount = null;
            return true;
        }

        /// <summary>Traverses the tree from the root using pre-order. The elements are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded values.</returns>
        public IEnumerable<TValue> TraversePreOrder() => InternalRoot.TraversePreOrder();
        /// <summary>Traverses the tree from the root using post-order. The elements are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded values.</returns>
        public IEnumerable<TValue> TraversePostOrder() => InternalRoot.TraversePostOrder();
        /// <summary>Traverses the tree from the root using level-order. The elements are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded values.</returns>
        public IEnumerable<TValue> TraverseLevelOrder() => InternalRoot.TraverseLevelOrder();

        /// <summary>Traverses the tree from the root using pre-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded nodes.</returns>
        public IEnumerable<TTreeNode> TraversePreOrderNodes() => InternalRoot.TraversePreOrderNodes();
        /// <summary>Traverses the tree from the root using post-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded nodes.</returns>
        public IEnumerable<TTreeNode> TraversePostOrderNodes() => InternalRoot.TraversePostOrderNodes();
        /// <summary>Traverses the tree from the root using level-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded nodes.</returns>
        public IEnumerable<TTreeNode> TraverseLevelOrderNodes() => InternalRoot.TraverseLevelOrderNodes();

        /// <summary>Gets the tree view of this tree node as a subtree.</summary>
        /// <param name="childIndent">The length of the indentation for each subsequent child.</param>
        /// <returns>The tree view.</returns>
        public string GetTreeView(int childIndent = 4) => Root.GetTreeView(childIndent);

        /// <summary>Gets an enumerator that traverses the tree using pre-order traversal.</summary>
        /// <returns>The <seealso cref="IEnumerator{T}"/> that traverses the tree using pre-order traversal.</returns>
        public IEnumerator<TValue> GetEnumerator() => TraversePreOrder().GetEnumerator();
        /// <summary>Gets an enumerator that traverses the tree using pre-order traversal.</summary>
        /// <returns>The <seealso cref="IEnumerator"/> that traverses the tree using pre-order traversal.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #region Hidden Interface Implementations
        void ICollection<TValue>.Add(TValue item) => ThrowHelper.Throw<NotSupportedException>("Cannot add an item to the tree without specifying its parent.");
        void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex)
        {
            int currentIndex = arrayIndex;
            foreach (var n in this)
            {
                array[currentIndex] = n;
                currentIndex++;
            }
        }
        #endregion

        public bool Equals(TTree other)
        {
            if (other is null)
                return false;

            if (CachedCount != null && other.CachedCount != null && CachedCount != other.CachedCount)
                return false;

            var parallellyEnumerable = new ParallellyEnumerable<TValue, TValue>(this, other);

            foreach (var (thisValue, otherValue) in parallellyEnumerable)
            {
                // If any value is null, the respective enumeration is over
                if (thisValue == null ^ otherValue == null)
                    return false;

                // If both enumerations end at the same time, it's only necessary to at least check the one
                if (thisValue == null)
                    return true;

                if (!Checks.SafeEquals(thisValue, otherValue))
                    return false;
            }

            // Yay
            return true;
        }

        public override bool Equals(object? obj) => Equals(obj as TTree);
        public override int GetHashCode() => Root.GetHashCode() | CachedCount.GetHashCode();
    }
}