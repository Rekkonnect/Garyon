using System.Collections.Generic;
using System.Linq;

namespace Garyon.DataStructures
{
    /// <summary>Represents a tree.</summary>
    /// <typeparam name="T">The type of the elements the tree nodes store.</typeparam>
    public class Tree<T> : Tree<T, Tree<T>, TreeNode<T>>
    {
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class with no root node.</summary>
        public Tree()
            : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class with a root value.</summary>
        /// <param name="rootValue">The value of the root node.</param>
        public Tree(T rootValue)
            : base(rootValue) { }
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class with a root node.</summary>
        /// <param name="root">The root node.</param>
        public Tree(TreeNode<T> root)
            : base(root) { }
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class from a different tree.</summary>
        /// <param name="tree">The tree to create this tree from. Both trees remain independent.</param>
        public Tree(Tree<T> tree)
            : base(tree) { }

        #region Abstract Constructors
        protected override TreeNode<T> InitializeNewNode(T value = default) => new TreeNode<T>(value);
        protected override TreeNode<T> InitializeNewNode(Tree<T> baseTree, T value = default) => new TreeNode<T>(baseTree, value);
        protected override TreeNode<T> InitializeNewNode(TreeNode<T> parentNode, T value = default) => new TreeNode<T>(parentNode, value);
        #endregion
    }

    /// <summary>Represents a tree.</summary>
    /// <typeparam name="TValue">The type of the elements the tree nodes store.</typeparam>
    /// <typeparam name="TTree">The type of the tree that this type is used in.</typeparam>
    /// <typeparam name="TTreeNode">The type of the tree nodes that are used in the <typeparamref name="TTree"/> type.</typeparam>
    public abstract class Tree<TValue, TTree, TTreeNode> : BaseTree<TValue, TTree, TTreeNode>
        where TTree : Tree<TValue, TTree, TTreeNode>
        where TTreeNode : TreeNode<TValue, TTree, TTreeNode>
    {
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class with no root node.</summary>
        public Tree()
            : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class with a root value.</summary>
        /// <param name="rootValue">The value of the root node.</param>
        public Tree(TValue rootValue)
            : base(rootValue) { }
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class with a root node.</summary>
        /// <param name="root">The root node.</param>
        public Tree(TTreeNode root)
            : base(root) { }
        /// <summary>Initializes a new instance of the <seealso cref="Tree{T}"/> class from a different tree.</summary>
        /// <param name="tree">The tree to create this tree from. Both trees remain independent.</param>
        public Tree(TTree tree)
            : base(tree) { }

        #region Children
        /// <summary>Sets the children of a node within this tree.</summary>
        /// <param name="value">The value of the parent node whose children to set. The node must be within this tree.</param>
        /// <param name="newValues">The values of the new children to set to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.Children"/> property by setting the property to <paramref name="newValues"/>. Only set this to <see langword="false"/> if the new children are already set in the node before calling this function.</param>
        public void SetChildren(TValue value, IEnumerable<TValue> newValues, bool propagate = true)
        {
            SetChildren(value, newValues.Select(v => InitializeNewNode(v)), propagate);
        }
        /// <summary>Sets the children of a node within this tree.</summary>
        /// <param name="node">The parent node whose children to set. The node must be within this tree.</param>
        /// <param name="newValues">The values of the new children to set to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.Children"/> property by setting the property to <paramref name="newValues"/>. Only set this to <see langword="false"/> if the new children are already set in the node before calling this function.</param>
        public void SetChildren(TTreeNode node, IEnumerable<TValue> newValues, bool propagate = true)
        {
            SetChildren(node, newValues.Select(v => InitializeNewNode(v)), propagate);
        }
        /// <summary>Sets the children of a node within this tree.</summary>
        /// <param name="value">The value of the parent node whose children to set. The node must be within this tree.</param>
        /// <param name="newChildren">The new children to set to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.Children"/> property by setting the property to <paramref name="newChildren"/>. Only set this to <see langword="false"/> if the new children are already set in the node before calling this function.</param>
        public void SetChildren(TValue value, IEnumerable<TTreeNode> newChildren, bool propagate = true)
        {
            SetChildren(value, new List<TTreeNode>(newChildren), propagate);
        }
        /// <summary>Sets the children of a node within this tree.</summary>
        /// <param name="value">The value of the parent node whose children to set. The node must be within this tree.</param>
        /// <param name="newChildren">The new children to set to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.Children"/> property by setting the property to <paramref name="newChildren"/>. Only set this to <see langword="false"/> if the new children are already set in the node before calling this function.</param>
        public void SetChildren(TValue value, List<TTreeNode> newChildren, bool propagate = true)
        {
            SetChildren(GetNode(value), newChildren, propagate);
        }
        /// <summary>Sets the children of a node within this tree.</summary>
        /// <param name="node">The parent node whose children to set. The node must be within this tree.</param>
        /// <param name="newChildren">The new children to set to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.Children"/> property by setting the property to <paramref name="newChildren"/>. Only set this to <see langword="false"/> if the new children are already set in the node before calling this function.</param>
        public void SetChildren(TTreeNode node, IEnumerable<TTreeNode> newChildren, bool propagate = true)
        {
            SetChildren(node, new List<TTreeNode>(newChildren), propagate);
        }
        /// <summary>Sets the children of a node within this tree.</summary>
        /// <param name="node">The parent node whose children to set. The node must be within this tree.</param>
        /// <param name="newChildren">The new children to set to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.Children"/> property by setting the property to <paramref name="newChildren"/>. Only set this to <see langword="false"/> if the new children are already set in the node before calling this function.</param>
        public void SetChildren(TTreeNode node, List<TTreeNode> newChildren, bool propagate = true)
        {
            if (!PrepareChildrenAdjustmentOperation(node))
                return;

            CachedCount = null;

            if (propagate)
                node.Children = newChildren;
        }

        /// <summary>Adds a child to a node within this tree.</summary>
        /// <param name="node">The parent node whose children to set. The node must be within this tree.</param>
        /// <param name="value">The value of the new child to add to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.AddChild(TreeNode{T})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void AddChild(TTreeNode node, TValue value, bool propagate = true)
        {
            AddChild(node, InitializeNewNode(value), propagate);
        }
        /// <summary>Adds a child to a node within this tree.</summary>
        /// <param name="node">The parent node whose children to set. The node must be within this tree.</param>
        /// <param name="newChild">The new child to add to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.AddChild(TreeNode{T})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void AddChild(TTreeNode node, TTreeNode newChild, bool propagate = true)
        {
            if (!PrepareChildrenAdjustmentOperation(node))
                return;

            if (propagate)
                node.AddChild(newChild);
        }
        /// <summary>Adds children to a node within this tree.</summary>
        /// <param name="node">The parent node whose children to add. The node must be within this tree.</param>
        /// <param name="newChildren">The new children to add to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.AddChildren(IEnumerable{TreeNode{T}})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void AddChildren(TTreeNode node, IEnumerable<TTreeNode> newChildren, bool propagate = true)
        {
            if (!PrepareChildrenAdjustmentOperation(node))
                return;

            if (propagate)
                node.AddChildren(newChildren);
        }
        #endregion

        /// <summary>Removes this tree's root direct children.</summary>
        /// <param name="childrenToRemove">The children to remove from the tree root node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.RemoveChildren(IEnumerable{TreeNode{T}})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveRootChildren(IEnumerable<TValue> childrenToRemove, bool propagate = true)
        {
            RemoveChildren(Root, childrenToRemove, propagate);
        }

        /// <summary>Removes this tree's root direct children.</summary>
        /// <param name="childrenToRemove">The children to remove from the tree root node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.RemoveChildren(IEnumerable{TreeNode{T}})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveRootChildren(IEnumerable<TTreeNode> childrenToRemove, bool propagate = true)
        {
            RemoveChildren(Root, childrenToRemove, propagate);
        }

        /// <summary>Removes children from a node within this tree.</summary>
        /// <param name="value">The parent node whose children to add. The node must be within this tree.</param>
        /// <param name="childrenToRemove">The children to remove from the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.RemoveChildren(IEnumerable{TreeNode{T}})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveChildren(TValue value, IEnumerable<TTreeNode> childrenToRemove, bool propagate = true)
        {
            RemoveChildren(GetNode(value), childrenToRemove, propagate);
        }

        /// <summary>Removes children from a node within this tree.</summary>
        /// <param name="value">The parent node whose children to add. The node must be within this tree.</param>
        /// <param name="childrenToRemove">The children to remove from the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.RemoveChildren(IEnumerable{TreeNode{T}})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveChildren(TValue value, IEnumerable<TValue> childrenToRemove, bool propagate = true)
        {
            RemoveChildren(GetNode(value), childrenToRemove, propagate);
        }

        /// <summary>Removes children from a node within this tree.</summary>
        /// <param name="node">The parent node whose children to add. The node must be within this tree.</param>
        /// <param name="childrenToRemove">The children to remove from the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.RemoveChildren(IEnumerable{TreeNode{T}})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveChildren(TTreeNode node, IEnumerable<TValue> childrenToRemove, bool propagate = true)
        {
            if (!PrepareChildrenAdjustmentOperation(node))
                return;

            if (propagate)
                node.RemoveChildren(childrenToRemove);
        }

        /// <summary>Removes children from a node within this tree.</summary>
        /// <param name="node">The parent node whose children to add. The node must be within this tree.</param>
        /// <param name="childrenToRemove">The children to remove from the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="TreeNode{T}.RemoveChildren(IEnumerable{TreeNode{T}})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveChildren(TTreeNode node, IEnumerable<TTreeNode> childrenToRemove, bool propagate = true)
        {
            if (!PrepareChildrenAdjustmentOperation(node))
                return;

            if (propagate)
                node.RemoveChildren(childrenToRemove);
        }

        public override void ClearChildren(TTreeNode node, bool propagate = true)
        {
            SetChildren(node, new List<TTreeNode>(), propagate);
        }
    }
}
