using System;
using System.Collections.Generic;

namespace Garyon.DataStructures;

/// <summary>Represents a tree.</summary>
/// <typeparam name="TValue">The type of the elements the tree nodes store.</typeparam>
public interface ITree<TValue> : ICollection<TValue>
{
    #region Properties
    /// <summary>Gets the height of this tree. It is equal to the height of the root node.</summary>
    int Height { get; }
    /// <summary>Gets the breadth of this tree. It is equal to the breadth of the root node.</summary>
    int Breadth { get; }
    #endregion

    #region Nodes
    /// <summary>Removes a child within this tree.</summary>
    /// <param name="value">The value of the child to remove from the node.</param>
    /// <returns><see langword="true"/> if the node was successfully removed, otherwise <see langword="false"/>.</returns>
    bool RemoveNode(TValue value);
    /// <summary>Removes nodes within this tree that contain the specified values.</summary>
    /// <param name="values">The values of the children to remove.</param>
    void RemoveNodes(IEnumerable<TValue> values);

    /// <summary>Clears the children of a node with the specified value, that is within this tree.</summary>
    /// <param name="value">The value of the parent node whose children to clear. The node must be within this tree.</param>
    void ClearChildren(TValue value);
    #endregion

    #region Traversal
    /// <summary>Traverses the tree from the root using pre-order.</summary>
    /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded values.</returns>
    IEnumerable<TValue> TraversePreOrder();
    /// <summary>Traverses the tree from the root using post-order.</summary>
    /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded values.</returns>
    IEnumerable<TValue> TraversePostOrder();
    /// <summary>Traverses the tree from the root using level-order.</summary>
    /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded values.</returns>
    IEnumerable<TValue> TraverseLevelOrder();
    #endregion

    /// <summary>Gets the tree view of this tree node as a subtree.</summary>
    /// <param name="childIndent">The length of the indentation for each subsequent child.</param>
    /// <returns>The tree view.</returns>
    string GetTreeView(int childIndent);
}

/// <summary>Represents a tree.</summary>
/// <typeparam name="TValue">The type of the elements the tree nodes store.</typeparam>
/// <typeparam name="TTree">The type of the tree that this type is used in.</typeparam>
/// <typeparam name="TTreeNode">The type of the tree nodes that are used in the <typeparamref name="TTree"/> type.</typeparam>
public interface ITree<TValue, TTree, TTreeNode> : ITree<TValue>
    where TTree : ITree<TValue, TTree, TTreeNode>
    where TTreeNode : ITreeNode<TValue, TTree, TTreeNode>
{
    /// <summary>Gets or sets the root node.</summary>
    TTreeNode Root { get; set; }

    #region Nodes
    /// <summary>Gets a child node within this tree that has the specified value.</summary>
    /// <param name="value">The value of the child tree node to find.</param>
    /// <returns>The <seealso cref="TreeNode{T}"/> with the specified value, if found; otherwise <see langword="false"/>.</returns>
    TTreeNode GetNode(TValue value);

    /// <summary>Removes a child within this tree.</summary>
    /// <param name="childToRemove">The child to remove from the node.</param>
    /// <returns><see langword="true"/> if the node was successfully removed, otherwise <see langword="false"/>.</returns>
    bool RemoveNode(TTreeNode childToRemove);
    /// <summary>Removes nodes within a subtree with root as the specified node that contain the specified values.</summary>
    /// <param name="node">The node which is the root of the subtree in which to remove the nodes at.</param>
    /// <param name="values">The values of the children to remove.</param>
    void RemoveNodes(TTreeNode node, IEnumerable<TValue> values);

    /// <summary>Clears the children of a node within this tree.</summary>
    /// <param name="node">The parent node whose children to clear. The node must be within this tree.</param>
    void ClearChildren(TTreeNode node);
    #endregion

    #region Traversal
    /// <summary>Traverses the tree from the root using pre-order.</summary>
    /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded nodes.</returns>
    IEnumerable<TTreeNode> TraversePreOrderNodes();
    /// <summary>Traverses the tree from the root using post-order.</summary>
    /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded nodes.</returns>
    IEnumerable<TTreeNode> TraversePostOrderNodes();
    /// <summary>Traverses the tree from the root using level-order.</summary>
    /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded nodes.</returns>
    IEnumerable<TTreeNode> TraverseLevelOrderNodes();
    #endregion
}
