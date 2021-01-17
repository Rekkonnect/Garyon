using Garyon.Extensions;
using Garyon.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garyon.DataStructures
{
    /// <summary>Represents a tree node, which is a component that is contained within a tree.</summary>
    /// <typeparam name="T">The type of the value that is stored in the node.</typeparam>
    public class TreeNode<T> : TreeNode<T, Tree<T>, TreeNode<T>>
    {
        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with no base tree, parent or children nodes.</summary>
        /// <param name="value">The value of the node.</param>
        public TreeNode(T value = default)
            : base(value) { }
        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with a base tree, but no parent or children nodes.</summary>
        /// <param name="baseTree">The base tree that contains this node.</param>
        /// <param name="value">The value of the node.</param>
        public TreeNode(Tree<T> baseTree, T value = default)
            : base(baseTree, value) { }
        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with a parent node, but no children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="value">The value of the node.</param>
        public TreeNode(TreeNode<T> parentNode, T value = default)
            : base(parentNode, value) { }
        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with a parent and children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="childrenNodes">The children nodes.</param>
        /// <param name="value">The value of the node.</param>
        public TreeNode(TreeNode<T> parentNode, List<TreeNode<T>> childrenNodes, T value = default)
            : base(parentNode, childrenNodes, value) { }

        #region Abstract Constructors
        /// <inheritdoc/>
        protected override TreeNode<T> InitializeNewNode(T value = default) => new TreeNode<T>(value);
        /// <inheritdoc/>
        protected override TreeNode<T> InitializeNewNode(Tree<T> baseTree, T value = default) => new TreeNode<T>(baseTree, value);
        /// <inheritdoc/>
        protected override TreeNode<T> InitializeNewNode(TreeNode<T> parentNode, T value = default) => new TreeNode<T>(parentNode, value);
        #endregion
    }

    /// <summary>Represents a tree node, which is a component that is contained within a tree.</summary>
    /// <typeparam name="TValue">The type of the value that is stored in the node.</typeparam>
    /// <typeparam name="TTree">The type of the tree that this type is used in.</typeparam>
    /// <typeparam name="TTreeNode">The type of the tree nodes that are used in the <typeparamref name="TTree"/> type.</typeparam>
    public abstract class TreeNode<TValue, TTree, TTreeNode> : BaseTreeNode<TValue, TTree, TTreeNode>
        where TTree : Tree<TValue, TTree, TTreeNode>
        where TTreeNode : TreeNode<TValue, TTree, TTreeNode>
    {
        /// <summary>The internal children list.</summary>
        protected List<TTreeNode> InternalChildren;

        /// <summary>Gets or sets the base tree that contains this node.</summary>
        public override TTree BaseTree
        {
            set
            {
                InternalBaseTree = value;
                foreach (var child in InternalChildren)
                    child.BaseTree = value;
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

        /// <summary>Gets or sets the children nodes of this tree. The retrieved list is a copy of the internally stored list, therefore it is best to call this property as less as possible.</summary>
        /// <value>The new list of nodes to set the children to. If the list is <see langword="null"/>, a new list is initialized.</value>
        public override List<TTreeNode> Children
        {
            get => new List<TTreeNode>(InternalChildren);
            set
            {
                value ??= new List<TTreeNode>();

                // The children that were requested to be set will now have this node's base tree as a base tree
                if (value.Any() && !Checks.SafeEquals(value.FirstOrDefault()?.BaseTree, InternalBaseTree))
                    foreach (var child in value)
                        child.BaseTree = InternalBaseTree;

                var oldChildren = InternalChildren;
                // If the children have been already migrated to another tree, do not corrupt their state
                if (oldChildren.Any() && !Checks.SafeEquals(oldChildren.FirstOrDefault()?.BaseTree, InternalBaseTree))
                    foreach (var child in oldChildren)
                        child.BaseTree = default;

                InternalChildren = value;
            }
        }

        /// <inheritdoc/>
        public override int ChildrenCount => InternalChildren.Count;

        /// <inheritdoc/>
        public override int Height
        {
            get
            {
                if (IsLeaf)
                    return 1;

                int max = 0;
                foreach (var c in InternalChildren)
                    max = Math.Max(max, c.Height + 1);

                return max;
            }
        }

        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with no base tree, parent or children nodes.</summary>
        /// <param name="value">The value of the node.</param>
        public TreeNode(TValue value = default)
            : base(value)
        {
            InternalChildren = new List<TTreeNode>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with a base tree, but no parent or children nodes.</summary>
        /// <param name="baseTree">The base tree that contains this node.</param>
        /// <param name="value">The value of the node.</param>
        public TreeNode(TTree baseTree, TValue value = default)
            : base(baseTree, value)
        {
            InternalChildren = new List<TTreeNode>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with a parent node, but no children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="value">The value of the node.</param>
        public TreeNode(TTreeNode parentNode, TValue value = default)
            : base(parentNode, value)
        {
            InternalChildren = new List<TTreeNode>();
        }
        /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with a parent and children nodes. The base tree is considered to be that of the parent.</summary>
        /// <param name="parentNode">The parent node.</param>
        /// <param name="childrenNodes">The children nodes.</param>
        /// <param name="value">The value of the node.</param>
        public TreeNode(TTreeNode parentNode, List<TTreeNode> childrenNodes, TValue value = default)
            : base(parentNode, value)
        {
            InternalChildren = childrenNodes;
        }

        #region Children
        /// <summary>Gets a direct child of this node that has the specified value.</summary>
        /// <param name="value">The value of the direct child node to find.</param>
        /// <returns>The <seealso cref="TreeNode{T}"/> with the specified value, if found; otherwise <see langword="false"/>.</returns>
        public override TTreeNode GetChild(TValue value) => GetNode(InternalChildren, value);

        /// <summary>Adds a child node that has the specified value to the end of this node's children list.</summary>
        /// <param name="value">The value of the new child node to add to the end of this node's children list.</param>
        /// <returns>The <seealso cref="TreeNode{T}"/> that was created and added to this node's children list.</returns>
        public TTreeNode AddChild(TValue value)
        {
            var node = InitializeNewNode(value);
            AddChild(node);
            return node;
        }
        /// <summary>Adds a child to the end of this node's children list.</summary>
        /// <param name="newChild">The new child to add to the end of this node's children list.</param>
        public void AddChild(TTreeNode newChild)
        {
            InternalChildren.Add(newChild);
            newChild.InternalBaseTree = InternalBaseTree;
            newChild.InternalParent = this as TTreeNode;
            InternalBaseTree?.AddChild(This, newChild, false);
        }
        /// <summary>Adds child nodes that have the specified values to the end of this node's children list.</summary>
        /// <param name="values">The values of the new child nodes to add to the end of this node's children list.</param>
        /// <returns>The tree nodes that were created and added to this node's children list.</returns>
        public IEnumerable<TTreeNode> AddChildren(IEnumerable<TValue> values)
        {
            var nodes = values.Select(v => InitializeNewNode(v));
            AddChildren(nodes);
            return nodes;
        }
        /// <summary>Adds child nodes that have the specified values to the end of this node's children list.</summary>
        /// <param name="values">The values of the new child nodes to add to the end of this node's children list.</param>
        /// <returns>The tree nodes that were created and added to this node's children list.</returns>
        public IEnumerable<TTreeNode> AddChildren(params TValue[] values) => AddChildren((IEnumerable<TValue>)values);
        /// <summary>Adds children to the end of this node's children list.</summary>
        /// <param name="newChildren">The new children to add to the end of this node's children list.</param>
        public void AddChildren(IEnumerable<TTreeNode> newChildren)
        {
            InternalChildren.AddRange(newChildren);
            foreach (var c in newChildren)
            {
                c.InternalBaseTree = InternalBaseTree;
                c.InternalParent = This;
            }
            InternalBaseTree?.AddChildren(This, newChildren, false);
        }
        /// <summary>Adds children to the end of this node's children list.</summary>
        /// <param name="newChildren">The new children to add to the end of this node's children list.</param>
        public void AddChildren(params TTreeNode[] newChildren)
        {
            AddChildren((IEnumerable<TTreeNode>)newChildren);
        }
        #endregion

        /// <inheritdoc/>
        protected override void RegisterAddedChild(TTreeNode addedChild)
        {
            addedChild.InternalBaseTree = InternalBaseTree;
            addedChild.InternalParent = this as TTreeNode;
            InternalBaseTree?.AddChild(This, addedChild, false);
        }

        /// <inheritdoc/>
        protected override void AddChildrenToClonedInstance(TTreeNode result)
        {
            foreach (var c in InternalChildren)
                result.AddChild(c.Clone(false));
        }

        #region Traversal
        /// <summary>Traverses the subtree with this node as a root using pre-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded nodes.</returns>
        public override IEnumerable<TTreeNode> TraversePreOrderNodes()
        {
            yield return This;
            foreach (var c in InternalChildren)
                foreach (var e in c.TraversePreOrderNodes())
                    yield return e;
        }
        /// <summary>Traverses the subtree with this node as a root using post-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded nodes.</returns>
        public override IEnumerable<TTreeNode> TraversePostOrderNodes()
        {
            foreach (var c in InternalChildren)
                foreach (var e in c.TraversePostOrderNodes())
                    yield return e;
            yield return This;
        }
        /// <summary>Traverses the subtree with this node as a root using level-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded nodes.</returns>
        public override IEnumerable<TTreeNode> TraverseLevelOrderNodes()
        {
            yield return This;

            var currentNodes = new List<TTreeNode> { This };
            var childrenNodes = new List<TTreeNode>();

            while (currentNodes.Any())
            {
                foreach (var n in currentNodes)
                {
                    foreach (var c in n.InternalChildren)
                    {
                        childrenNodes.Add(c);
                        yield return c;
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
            InternalChildren = InternalChildren.RemoveRange(childrenToRemove);
            // If the removed children are already in another tree, do not corrupt their state
            foreach (var c in childrenToRemove)
            {
                if (Checks.SafeEquals(c.BaseTree, InternalBaseTree))
                    c.BaseTree = default;
                if (Checks.SafeEquals(c.InternalParent, this))
                    c.InternalParent = default;
            }

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
            RemoveChildren(InternalChildren.Where(c => values.Contains(c.Value)));
        }
        /// <summary>Removes children from this node's children list.</summary>
        /// <param name="values">The values of the children to remove from this node's children list.</param>
        public void RemoveChildren(params TValue[] values)
        {
            RemoveChildren((IEnumerable<TValue>)values);
        }

        /// <inheritdoc/>
        protected override bool PerformChildRemoval(TTreeNode childToRemove)
        {
            if (!InternalChildren.Remove(childToRemove))
                return false;

            return true;
        }
    }
}
