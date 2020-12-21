using Garyon.Exceptions;
using Garyon.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.DataStructures
{
    /// <summary>Represents a binary tree node, which is a component that is contained within a binary tree.</summary>
    /// <typeparam name="T">The type of the value that is stored in the node.</typeparam>
    public class BinaryTreeNode<T> : BinaryTreeNode<T, BinaryTree<T>, BinaryTreeNode<T>>
    {
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTreeNode{T}"/> class with no base tree, parent or children nodes.</summary>
        public BinaryTreeNode(T value = default)
            : base(value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTreeNode{T}"/> class with a base tree, but no parent or children nodes.</summary>
        /// <param name="baseTree">The base tree that contains this node.</param>
        public BinaryTreeNode(BinaryTree<T> baseTree, T value = default)
            : base(baseTree, value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTreeNode{T}"/> class with a parent node, but no children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        public BinaryTreeNode(BinaryTreeNode<T> parentNode, T value = default)
            : base(parentNode, value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTreeNode{T}"/> class with a parent and children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="leftChild">The left child node.</param>
        /// <param name="rightChild">The right child node.</param>
        public BinaryTreeNode(BinaryTreeNode<T> parentNode, BinaryTreeNode<T> leftChild, BinaryTreeNode<T> rightChild, T value = default)
            : base(parentNode, leftChild, rightChild, value) { }

        #region Abstract Constructors
        protected override BinaryTreeNode<T> InitializeNewNode(T value = default) => new BinaryTreeNode<T>(value);
        protected override BinaryTreeNode<T> InitializeNewNode(BinaryTree<T> baseTree, T value = default) => new BinaryTreeNode<T>(baseTree, value);
        protected override BinaryTreeNode<T> InitializeNewNode(BinaryTreeNode<T> parentNode, T value = default) => new BinaryTreeNode<T>(parentNode, value);
        #endregion
    }

    /// <summary>Represents a binary tree node, which is a component that is contained within a binary tree.</summary>
    /// <typeparam name="TValue">The type of the value that is stored in the node.</typeparam>
    /// <typeparam name="TTree">The type of the binary tree that this type is used in.</typeparam>
    /// <typeparam name="TTreeNode">The type of the binary tree nodes that are used in the <typeparamref name="TTree"/> type.</typeparam>
    public abstract class BinaryTreeNode<TValue, TTree, TTreeNode> : BaseTreeNode<TValue, TTree, TTreeNode>
        where TTree : BinaryTree<TValue, TTree, TTreeNode> // BinaryTree
        where TTreeNode : BinaryTreeNode<TValue, TTree, TTreeNode>
    {
        /// <summary>The left child of this node. This internal field is exposed to avoid using the property setters.</summary>
        protected TTreeNode InternalLeftChild;
        /// <summary>The right child of this node. This internal field is exposed to avoid using the property setters.</summary>
        protected TTreeNode InternalRightChild;

        /// <summary>The left child of this node.</summary>
        public virtual TTreeNode LeftChild
        {
            get => InternalLeftChild;
            set => InternalLeftChild = value;
        }
        /// <summary>The right child of this node.</summary>
        public virtual TTreeNode RightChild
        {
            get => InternalRightChild;
            set => InternalRightChild = value;
        }

        /// <summary>Gets or sets the base tree that contains this node.</summary>
        public override TTree BaseTree
        {
            set
            {
                InternalBaseTree = value;
                if (LeftChild != null)
                    LeftChild.BaseTree = value;
                if (RightChild != null)
                    RightChild.BaseTree = value;
            }
        }

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

        /// <summary>Gets or sets the children nodes of this tree. The retrieved list is a new list created based on the left and right children of this node, therefore it is best to call this property as less as possible.</summary>
        /// <value>The new list of nodes to set the children to. If the list is <see langword="null"/>, a new list is initialized.</value>
        public override List<TTreeNode> Children
        {
            get
            {
                var result = new List<TTreeNode>(2);
                if (LeftChild != null)
                    result.Add(LeftChild);
                if (RightChild != null)
                    result.Add(RightChild);
                return result;
            }
            set
            {
                value ??= new List<TTreeNode>(0);

                if (value.Count > 2)
                    ThrowHelper.Throw<InvalidOperationException>("Cannot send the children of a binary tree node to more than 2 nodes.");

                // The children that were requested to be set will now have this node's base tree as a base tree
                if (value.Any() && !Checks.SafeEquals(value.FirstOrDefault()?.BaseTree, InternalBaseTree))
                    foreach (var child in value)
                        child.BaseTree = InternalBaseTree;

                var oldChildren = Children;
                // If the children have been already migrated to another tree, do not corrupt their state
                if (oldChildren.Any() && !Checks.SafeEquals(oldChildren.FirstOrDefault()?.BaseTree, InternalBaseTree))
                    foreach (var child in oldChildren)
                        child.BaseTree = default;

                LeftChild = value[0];
                RightChild = value[1];
            }
        }

        public sealed override int ChildrenCount => Convert.ToInt32(LeftChild != null) + Convert.ToInt32(RightChild != null);

        public sealed override int Height
        {
            get
            {
                if (IsLeaf)
                    return 1;

                return Math.Max((LeftChild?.Height + 1) ?? 0, (RightChild?.Height + 1) ?? 0);
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="BinaryTreeNode{T}"/> class with no base tree, parent or children nodes.</summary>
        /// <param name="value">The value of the node.</param>
        public BinaryTreeNode(TValue value = default)
            : base(value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTreeNode{T}"/> class with a base tree, but no parent or children nodes.</summary>
        /// <param name="baseTree">The base tree that contains this node.</param>
        /// <param name="value">The value of the node.</param>
        public BinaryTreeNode(TTree baseTree, TValue value = default)
            : base(baseTree, value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTreeNode{T}"/> class with a parent node, but no children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="value">The value of the node.</param>
        public BinaryTreeNode(TTreeNode parentNode, TValue value = default)
            : base(parentNode, value) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTreeNode{T}"/> class with a parent and children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="leftChild">The left child node.</param>
        /// <param name="rightChild">The right child node.</param>
        /// <param name="value">The value of the node.</param>
        public BinaryTreeNode(TTreeNode parentNode, TTreeNode leftChild, TTreeNode rightChild, TValue value = default)
            : base(parentNode, value)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
        }

        #region Children
        /// <summary>Gets the leftmost direct child of this node that has the specified value.</summary>
        /// <param name="value">The value of the direct child node to find.</param>
        /// <returns>The <seealso cref="BinaryTreeNode{T}"/> with the specified value, if found; otherwise <see langword="null"/>.</returns>
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

        /// <summary>Adds a child node that has the specified value to the end of this node's children list.</summary>
        /// <param name="value">The value of the new child node to add to the end of this node's children list.</param>
        /// <returns>The <seealso cref="BinaryTreeNode{T}"/> that was created and added to this node's children list.</returns>
        public TTreeNode AddChild(TValue value)
        {
            var node = InitializeNewNode(value);
            AddChild(node);
            return node;
        }
        /// <summary>Adds a child to the first available spot from the two children spots.</summary>
        /// <param name="newChild">The new child to add.</param>
        public virtual void AddChild(TTreeNode newChild)
        {
            if (LeftChild == null)
                LeftChild = newChild;
            else if (RightChild == null)
                RightChild = newChild;
            else
                return;

            RegisterAddedChild(newChild);
        }
        /// <summary>Adds children to the available spots from the two children spots. If both are unoccupied, both values are added in the provided order, otherwise only the <paramref name="left"/> is added, if there is any unoccupied spot.</summary>
        /// <param name="left">The value of the left child to add.</param>
        /// <param name="right">The value of the right child to add.</param>
        public void AddChildren(TValue left, TValue right)
        {
            AddChildren(InitializeNewNode(left), InitializeNewNode(right));
        }
        /// <summary>Adds children to the available spots from the two children spots. If both are unoccupied, both nodes are added in the provided order, otherwise only the <paramref name="left"/> is added, if there is any unoccupied spot.</summary>
        /// <param name="left">The left child to add.</param>
        /// <param name="right">The right child to add.</param>
        public virtual void AddChildren(TTreeNode left, TTreeNode right)
        {
            AddChild(left);
            AddChild(right);
        }
        #endregion

        protected sealed override void RegisterAddedChild(TTreeNode addedChild)
        {
            addedChild.InternalBaseTree = InternalBaseTree;
            addedChild.InternalParent = this as TTreeNode;
            InternalBaseTree?.AddChild(This, addedChild, false);
        }
        protected sealed override bool PerformChildRemoval(TTreeNode childToRemove)
        {
            // BST searching rules are more expensive than just checking both children if they are equal to the value; no need to override further
            if (LeftChild == childToRemove)
                LeftChild = null;
            else if (RightChild == childToRemove)
                RightChild = null;
            else
                return false;

            return true;
        }

        protected override void AddChildrenToClonedInstance(TTreeNode result)
        {
            if (LeftChild != null)
                result.AddChild(LeftChild.Clone(false));
            if (RightChild != null)
                result.AddChild(RightChild.Clone(false));
        }

        #region Traversal
        /// <summary>Traverses the subtree with this node as a root using pre-order. The elements are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded values.</returns>
        public IEnumerable<TValue> TraverseInOrder()
        {
            foreach (var n in TraverseInOrderNodes())
                yield return n.Value;
        }

        // Local static functions not created to avoid abusing recursive yield returns
        /// <summary>Traverses the subtree with this node as a root using in-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded nodes.</returns>
        public IEnumerable<TTreeNode> TraverseInOrderNodes()
        {
            if (LeftChild != null)
                foreach (var e in LeftChild.TraverseInOrderNodes())
                    yield return e;
            yield return This;
            if (RightChild != null)
                foreach (var e in RightChild.TraverseInOrderNodes())
                    yield return e;
        }
        /// <summary>Traverses the subtree with this node as a root using pre-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded nodes.</returns>
        public override IEnumerable<TTreeNode> TraversePreOrderNodes()
        {
            yield return This;
            if (LeftChild != null)
                foreach (var e in LeftChild.TraversePreOrderNodes())
                    yield return e;
            if (RightChild != null)
                foreach (var e in RightChild.TraversePreOrderNodes())
                    yield return e;
        }
        /// <summary>Traverses the subtree with this node as a root using post-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded nodes.</returns>
        public override IEnumerable<TTreeNode> TraversePostOrderNodes()
        {
            if (LeftChild != null)
                foreach (var e in LeftChild.TraversePostOrderNodes())
                    yield return e;
            if (RightChild != null)
                foreach (var e in RightChild.TraversePostOrderNodes())
                    yield return e;
            yield return This;
        }
        /// <summary>Traverses the subtree with this node as a root using level-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded nodes.</returns>
        public override IEnumerable<TTreeNode> TraverseLevelOrderNodes()
        {
            yield return This;

            var currentNodes = new List<TTreeNode> { This };
            var childrenNodes = new List<TTreeNode>();

            while (currentNodes.Any())
            {
                foreach (var n in currentNodes)
                {
                    if (LeftChild != null)
                    {
                        childrenNodes.Add(LeftChild);
                        yield return LeftChild;
                    }
                    if (RightChild != null)
                    {
                        childrenNodes.Add(RightChild);
                        yield return RightChild;
                    }
                }

                currentNodes = childrenNodes;
                childrenNodes = new List<TTreeNode>();
            }
        }
        #endregion

        /// <summary>Removes children from this node's children list.</summary>
        /// <param name="childrenToRemove">The children to remove from this node's children list.</param>
        public void RemoveChildren(IEnumerable<TTreeNode> childrenToRemove)
        {
            if (LeftChild != null)
                if (childrenToRemove.Contains(LeftChild))
                    RemoveChild(LeftChild);
            if (RightChild != null)
                if (childrenToRemove.Contains(RightChild))
                    RemoveChild(RightChild);

            InternalBaseTree?.RemoveChildren(This, childrenToRemove, false);
        }
        /// <summary>Removes children from this node's children list.</summary>
        /// <param name="childrenToRemove">The children to remove from this node's children list.</param>
        public void RemoveChildren(params TTreeNode[] childrenToRemove)
        {
            RemoveChildren((IEnumerable<TTreeNode>)childrenToRemove);
        }
        /// <summary>Removes children from this node's children list.</summary>
        /// <param name="values">The values of the children to remove from this node's children list.</param>
        public void RemoveChildren(IEnumerable<TValue> values)
        {
            if (LeftChild != null)
                if (values.Contains(LeftChild.Value))
                    RemoveChild(LeftChild);
            if (RightChild != null)
                if (values.Contains(RightChild.Value))
                    RemoveChild(RightChild);

            InternalBaseTree?.RemoveChildren(This, values, false);
        }
        /// <summary>Removes children from this node's children list.</summary>
        /// <param name="values">The values of the children to remove from this node's children list.</param>
        public void RemoveChildren(params TValue[] values)
        {
            RemoveChildren((IEnumerable<TValue>)values);
        }
    }
}
