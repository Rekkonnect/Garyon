using System.Collections.Generic;

namespace Garyon.DataStructures
{
    /// <summary>Represents a tree node, which is a component that is contained within a tree.</summary>
    /// <typeparam name="TValue">The type of the value that is stored in the node.</typeparam>
    /// <typeparam name="TTree">The type of the tree that this type is used in.</typeparam>
    /// <typeparam name="TTreeNode">The type of the tree nodes that are used in the <typeparamref name="TTree"/> type.</typeparam>
    public interface ITreeNode<TValue, TTree, TTreeNode> : IEnumerable<TValue>
        where TTree : ITree<TValue, TTree, TTreeNode>
        where TTreeNode : ITreeNode<TValue, TTree, TTreeNode>
    {
        /// <summary>The value of the node.</summary>
        TValue Value { get; set; }

        /// <summary>Gets or sets the base tree that contains this node.</summary>
        TTree BaseTree { get; set; }

        /// <summary>Gets or sets the parent node of this tree node.</summary>
        TTreeNode Parent { get; set; }
        /// <summary>Gets or sets the children nodes of this tree.</summary>
        /// <value>The new list of nodes to set the children to.</value>
        List<TTreeNode> Children { get; set; }

        /// <summary>Gets the count of children of this node; prefer calling this property instead of getting the count directly from the <see cref="Children"/> property.</summary>
        int ChildrenCount { get; }

        #region Properties
        /// <summary>Determines whether this node is the root; that is, its parent is <see langword="null"/>.</summary>
        bool IsRoot { get; }
        /// <summary>Determines whether this node is a leaf; that is, it has no children.</summary>
        bool IsLeaf { get; }

        /// <summary>Gets the height of this tree node's base tree height. It is equal to the sum of this tree node's height and depth. Prefer calling this instead of the <see cref="Height"/> property on <see cref="TTree.Root"/>.</summary>
        int BaseTreeHeight { get; }
        /// <summary>Gets the height of this tree node's subtree.</summary>
        int Height { get; }
        /// <summary>Gets the depth of this tree node.</summary>
        int Depth { get; }
        /// <summary>Gets the breadth of this tree node.</summary>
        int Breadth { get; }
        /// <summary>Gets the descendants of this tree node.</summary>
        int Descendants { get; }
        #endregion

        #region Nodes
        /// <summary>Gets a direct child of this node that has the specified value.</summary>
        /// <param name="value">The value of the direct child node to find.</param>
        /// <returns>The <seealso cref="TTreeNode"/> with the specified value, if found; otherwise <see langword="false"/>.</returns>
        TTreeNode GetChild(TValue value);
        /// <summary>Gets a node from the subtree with this tree node as a root node that has the specified value.</summary>
        /// <param name="value">The value of the tree node to find.</param>
        /// <returns>The <seealso cref="TTreeNode"/> with the specified value, if found; otherwise <see langword="false"/>.</returns>
        TTreeNode GetNode(TValue value);

        /// <summary>Removes a node that has the specified value. This includes any node that is within the subtree with this tree node as the root.</summary>
        /// <param name="value">The value of the node to remove from this node's children list.</param>
        /// <returns><see langword="true"/> if the child was successfully removed from this node's children list, otherwise <see langword="false"/>.</returns>
        bool RemoveNode(TValue value);
        /// <summary>Removes a node that has the specified value. This includes any node that is within the subtree with this tree node as the root.</summary>
        /// <param name="node">The node to remove from this node's children list.</param>
        /// <returns><see langword="true"/> if the child was successfully removed from this node's children list, otherwise <see langword="false"/>.</returns>
        bool RemoveNode(TTreeNode node);
        /// <summary>Removes nodes that have the specified values. This includes any node that is within the subtree with this tree node as the root.</summary>
        /// <param name="values">The values of the child nodes to remove from this node's children list.</param>
        /// <returns>The number of nodes from the list that were removed.</returns>
        void RemoveNodes(IEnumerable<TValue> values);
        /// <summary>Removes nodes that have the specified values. This includes any node that is within the subtree with this tree node as the root.</summary>
        /// <param name="values">The values of the child nodes to remove from this node's children list.</param>
        /// <returns>The number of children that were removed.</returns>
        void RemoveNodes(params TValue[] values);

        /// <summary>Removes a child from this node's children list.</summary>
        /// <param name="childToRemove">The child to remove from this node's children list.</param>
        bool RemoveChild(TTreeNode childToRemove);
        /// <summary>Removes a child from this node's children list.</summary>
        /// <param name="childToRemove">The child to remove from this node's children list.</param>
        bool RemoveChild(TValue childToRemove);

        /// <summary>Makes this node a child of another tree node.</summary>
        /// <param name="other">The other tree node that will become this node's parent.</param>
        void MakeChildOf(TTreeNode other);
        /// <summary>Makes this node a parent of another tree node.</summary>
        /// <param name="other">The other tree node that will become this node's child.</param>
        void MakeParentOf(TTreeNode other);
        #endregion

        /// <summary>Clones this <seealso cref="TTreeNode"/> and all its children recursively.</summary>
        /// <returns>The cloned <seealso cref="TTreeNode"/>. Its base tree is also equal to the original <seealso cref="TTreeNode"/>'s base tree.</returns>
        TTreeNode Clone();

        #region Traversal
        /// <summary>Traverses the subtree with this node as a root using pre-order.</summary>
        /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded values.</returns>
        IEnumerable<TValue> TraversePreOrder();
        /// <summary>Traverses the subtree with this node as a root using post-order.</summary>
        /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded values.</returns>
        IEnumerable<TValue> TraversePostOrder();
        /// <summary>Traverses the subtree with this node as a root using level-order.</summary>
        /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded values.</returns>
        IEnumerable<TValue> TraverseLevelOrder();
        /// <summary>Traverses the subtree with this node as a root using pre-order.</summary>
        /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded nodes.</returns>
        IEnumerable<TTreeNode> TraversePreOrderNodes();
        /// <summary>Traverses the subtree with this node as a root using post-order.</summary>
        /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded nodes.</returns>
        IEnumerable<TTreeNode> TraversePostOrderNodes();
        /// <summary>Traverses the subtree with this node as a root using level-order.</summary>
        /// <returns>An <seealso cref="IEnumerable{T}"/> containing the yielded nodes.</returns>
        IEnumerable<TTreeNode> TraverseLevelOrderNodes();
        #endregion

        /// <summary>Gets the tree view of this tree node as a subtree.</summary>
        /// <param name="childIndent">The length of the indentation for each subsequent child.</param>
        /// <returns>The tree view.</returns>
        string GetTreeView(int childIndent = 4);
    }
}