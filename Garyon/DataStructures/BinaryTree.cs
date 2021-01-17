using System.Collections.Generic;

namespace Garyon.DataStructures
{
    /// <summary>Represents a binary tree.</summary>
    /// <typeparam name="T">The type of the elements the binary tree nodes store.</typeparam>
    public class BinaryTree<T> : BinaryTree<T, BinaryTree<T>, BinaryTreeNode<T>>
    {
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTree{T}"/> class with no root node.</summary>
        public BinaryTree()
            : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTree{T}"/> class with a root value.</summary>
        /// <param name="rootValue">The value of the root node.</param>
        public BinaryTree(T rootValue)
            : base(rootValue) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTree{T}"/> class with a root node.</summary>
        /// <param name="root">The root node.</param>
        public BinaryTree(BinaryTreeNode<T> root)
            : base(root) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTree{T}"/> class from a different tree.</summary>
        /// <param name="tree">The tree to create this tree from. Both trees remain independent.</param>
        public BinaryTree(BinaryTree<T> tree)
            : base(tree) { }

        #region Abstract Constructors
        /// <inheritdoc/>
        protected override BinaryTreeNode<T> InitializeNewNode(T value = default) => new BinaryTreeNode<T>(value);
        /// <inheritdoc/>
        protected override BinaryTreeNode<T> InitializeNewNode(BinaryTree<T> baseTree, T value = default) => new BinaryTreeNode<T>(baseTree, value);
        /// <inheritdoc/>
        protected override BinaryTreeNode<T> InitializeNewNode(BinaryTreeNode<T> parentNode, T value = default) => new BinaryTreeNode<T>(parentNode, value);
        #endregion
    }

    /// <summary>Represents a binary tree.</summary>
    /// <typeparam name="TValue">The type of the elements the binary tree nodes store.</typeparam>
    /// <typeparam name="TTree">The type of the binary tree that this type is used in.</typeparam>
    /// <typeparam name="TTreeNode">The type of the binary tree nodes that are used in the <typeparamref name="TTree"/> type.</typeparam>
    public abstract class BinaryTree<TValue, TTree, TTreeNode> : BaseTree<TValue, TTree, TTreeNode>
        where TTree : BinaryTree<TValue, TTree, TTreeNode>
        where TTreeNode : BinaryTreeNode<TValue, TTree, TTreeNode>
    {
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTree{TValue, TTree, TTreeNode}"/> class with no root node.</summary>
        protected BinaryTree()
            : base() { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTree{TValue, TTree, TTreeNode}"/> class with a root value.</summary>
        /// <param name="rootValue">The value of the root node.</param>
        protected BinaryTree(TValue rootValue)
            : base(rootValue) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTree{TValue, TTree, TTreeNode}"/> class with a root node.</summary>
        /// <param name="root">The root node.</param>
        protected BinaryTree(TTreeNode root)
            : base(root) { }
        /// <summary>Initializes a new instance of the <seealso cref="BinaryTree{TValue, TTree, TTreeNode}"/> class from a different tree.</summary>
        /// <param name="tree">The tree to create this tree from. Both trees remain independent.</param>
        protected BinaryTree(TTree tree)
            : base(tree) { }

        #region Children
        /// <summary>Sets the children of a node within this tree.</summary>
        /// <param name="value">The value of the parent node whose children to set. The node must be within this tree.</param>
        /// <param name="leftChild">The left child to set to the node.</param>
        /// <param name="rightChild">The right child to set to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.LeftChild"/> and <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.RightChild"/> properties by setting the property to <paramref name="leftChild"/>. Only set this to <see langword="false"/> if the new children are already set in the node before calling this function.</param>
        public void SetChildren(TValue value, TTreeNode leftChild, TTreeNode rightChild, bool propagate = true)
        {
            SetChildren(GetNode(value), leftChild, rightChild, propagate);
        }
        /// <summary>Sets the children of a node within this tree.</summary>
        /// <param name="node">The parent node whose children to set. The node must be within this tree.</param>
        /// <param name="leftChild">The left child to set to the node.</param>
        /// <param name="rightChild">The right child to set to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.LeftChild"/> and <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.RightChild"/> properties by setting the property to <paramref name="leftChild"/> and <paramref name="rightChild"/> respectively. Only set this to <see langword="false"/> if the new children are already set in the node before calling this function.</param>
        public void SetChildren(TTreeNode node, TTreeNode leftChild, TTreeNode rightChild, bool propagate = true)
        {
            if (!PrepareChildrenAdjustmentOperation(node))
                return;

            CachedCount = null;

            if (propagate)
            {
                node.LeftChild = leftChild;
                node.RightChild = rightChild;
            }
        }

        /// <summary>Adds a child to a node within this tree.</summary>
        /// <param name="node">The parent node whose children to set. The node must be within this tree.</param>
        /// <param name="value">The value of the new child to add to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.AddChild(TTreeNode)"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void AddChild(TTreeNode node, TValue value, bool propagate = true)
        {
            AddChild(node, InitializeNewNode(value), propagate);
        }
        /// <summary>Adds a child to a node within this tree.</summary>
        /// <param name="node">The parent node whose children to set. The node must be within this tree.</param>
        /// <param name="newChild">The new child to add to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.AddChild(TTreeNode)"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void AddChild(TTreeNode node, TTreeNode newChild, bool propagate = true)
        {
            if (!PrepareChildrenAdjustmentOperation(node))
                return;

            if (propagate)
                node.AddChild(newChild);
        }
        /// <summary>Adds children to a node within this tree.</summary>
        /// <param name="node">The parent node whose children to add. The node must be within this tree.</param>
        /// <param name="leftChild">The left child to add to the node.</param>
        /// <param name="rightChild">The right child to add to the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.AddChildren(TTreeNode, TTreeNode)"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void AddChildren(TTreeNode node, TTreeNode leftChild, TTreeNode rightChild, bool propagate = true)
        {
            if (!PrepareChildrenAdjustmentOperation(node))
                return;

            if (propagate)
                node.AddChildren(leftChild, rightChild);
        }
        #endregion

        /// <summary>Removes this tree's root direct children.</summary>
        /// <param name="childrenToRemove">The children to remove from the tree root node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.RemoveChildren(IEnumerable{TTreeNode})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveRootChildren(IEnumerable<TValue> childrenToRemove, bool propagate = true)
        {
            RemoveChildren(Root, childrenToRemove, propagate);
        }

        /// <summary>Removes this tree's root direct children.</summary>
        /// <param name="childrenToRemove">The children to remove from the tree root node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.RemoveChildren(IEnumerable{TTreeNode})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveRootChildren(IEnumerable<TTreeNode> childrenToRemove, bool propagate = true)
        {
            RemoveChildren(Root, childrenToRemove, propagate);
        }

        /// <summary>Removes children from a node within this tree.</summary>
        /// <param name="value">The parent node whose children to add. The node must be within this tree.</param>
        /// <param name="childrenToRemove">The children to remove from the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.RemoveChildren(IEnumerable{TTreeNode})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveChildren(TValue value, IEnumerable<TTreeNode> childrenToRemove, bool propagate = true)
        {
            RemoveChildren(GetNode(value), childrenToRemove, propagate);
        }

        /// <summary>Removes children from a node within this tree.</summary>
        /// <param name="value">The parent node whose children to add. The node must be within this tree.</param>
        /// <param name="childrenToRemove">The children to remove from the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.RemoveChildren(IEnumerable{TValue})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveChildren(TValue value, IEnumerable<TValue> childrenToRemove, bool propagate = true)
        {
            RemoveChildren(GetNode(value), childrenToRemove, propagate);
        }

        /// <summary>Removes children from a node within this tree.</summary>
        /// <param name="node">The parent node whose children to add. The node must be within this tree.</param>
        /// <param name="childrenToRemove">The children to remove from the node.</param>
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.RemoveChildren(IEnumerable{TValue})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
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
        /// <param name="propagate">Determines whether this function should propagate to the <seealso cref="BinaryTreeNode{TValue, TTree, TTreeNode}.RemoveChildren(IEnumerable{TTreeNode})"/> function. Only set this to <see langword="false"/> if the new child was already added before calling this function.</param>
        public void RemoveChildren(TTreeNode node, IEnumerable<TTreeNode> childrenToRemove, bool propagate = true)
        {
            if (!PrepareChildrenAdjustmentOperation(node))
                return;

            if (propagate)
                node.RemoveChildren(childrenToRemove);
        }

        /// <inheritdoc/>
        public override void ClearChildren(TTreeNode node, bool propagate = true)
        {
            SetChildren(node, null, null, propagate);
        }

        /// <summary>Traverses the tree from the root using pre-order. The elements are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded values.</returns>
        public IEnumerable<TValue> TraverseInOrder() => InternalRoot.TraverseInOrder();

        /// <summary>Traverses the tree from the root using pre-order. The nodes are returned with <see langword="yield return"/>.</summary>
        /// <returns>The yielded nodes.</returns>
        public IEnumerable<TTreeNode> TraverseInOrderNodes() => InternalRoot.TraverseInOrderNodes();
    }
}
