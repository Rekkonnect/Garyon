using Garyon.Extensions;
using Garyon.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garyon.DataStructures;

/// <summary>Represents a tree node, which is a component that is contained within a tree.</summary>
/// <typeparam name="TValue">The type of the value that is stored in the node.</typeparam>
/// <typeparam name="TTree">The type of the tree that this type is used in.</typeparam>
/// <typeparam name="TTreeNode">The type of the tree nodes that are used in the <typeparamref name="TTree"/> type.</typeparam>
public abstract class BaseTreeNode<TValue, TTree, TTreeNode> : ITreeNode<TValue, TTree, TTreeNode>
    where TTree : BaseTree<TValue, TTree, TTreeNode>
    where TTreeNode : BaseTreeNode<TValue, TTree, TTreeNode>
{
    /// <summary>The internal base tree. Prefer using this instead of calling the property for internal usage, unless the <see cref="BaseTree"/> setter is required.</summary>
    protected TTree InternalBaseTree;
    /// <summary>The internal parent. Prefer using this instead of calling the property for internal usage, unless the <see cref="Parent"/> setter is required.</summary>
    protected TTreeNode InternalParent;

    /// <summary>Gets this tree node instance as a <typeparamref name="TTreeNode"/>.</summary>
    protected TTreeNode This => this as TTreeNode;

    /// <summary>The value of the node.</summary>
    public TValue Value { get; set; }

    /// <summary>Gets or sets the base tree that contains this node.</summary>
    public virtual TTree BaseTree
    {
        get => InternalBaseTree;
        set { }
    }

    /// <summary>Gets or sets the parent node of this tree node.</summary>
    public virtual TTreeNode Parent
    {
        get => InternalParent;
        set { }
    }

    /// <inheritdoc/>
    public abstract List<TTreeNode> Children { get; set; }

    /// <inheritdoc/>
    public abstract int ChildrenCount { get; }

    /// <summary>Determines whether this node is the root; that is, its parent is <see langword="null"/>.</summary>
    public bool IsRoot => Parent == null;
    /// <summary>Determines whether this node is a leaf; that is, it has no children.</summary>
    public bool IsLeaf => ChildrenCount == 0;

    /// <summary>Gets the height of this tree node's base tree height. It is equal to the sum of this tree node's height and depth.</summary>
    public int BaseTreeHeight => Height + Depth;

    /// <summary>Gets the height of this tree node's subtree.</summary>
    public abstract int Height { get; }

    /// <summary>Gets the depth of this tree node.</summary>
    public int Depth
    {
        get
        {
            // Quickly return to caller
            if (IsRoot)
                return 0;

            int depth = 0;

            var current = this;
            while (!current.IsRoot)
            {
                current = current.Parent;
                depth++;
            }

            return depth;
        }
    }
    /// <summary>Gets the breadth of this tree node.</summary>
    public int Breadth
    {
        get
        {
            int count = 0;
            foreach (var n in TraversePreOrderNodes())
                if (n.IsLeaf)
                    count++;
            return count;
        }
    }

    /// <summary>Gets the descendants of this tree node.</summary>
    public int Descendants => TraversePreOrderNodes().Count() - 1;

    /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with no base tree or parent node.</summary>
    /// <param name="value">The value of the node.</param>
    protected BaseTreeNode(TValue value = default)
    {
        Value = value;
    }
    /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with a base tree, but no parent node.</summary>
    /// <param name="baseTree">The base tree that contains this node.</param>
    /// <param name="value">The value of the node.</param>
    protected BaseTreeNode(TTree baseTree, TValue value = default)
        : this(value)
    {
        BaseTree = baseTree;
    }
    /// <summary>Initializes a new instance of the <seealso cref="TreeNode{T}"/> class with a parent node. The base tree is considered to be that of the parent.</summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="value">The value of the node.</param>
    protected BaseTreeNode(TTreeNode parentNode, TValue value = default)
        : this(value)
    {
        Parent = parentNode;
        InternalBaseTree = parentNode.BaseTree;
    }

    #region Abstract Constructors
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

    /// <summary>Gets a direct child of this node that has the specified value.</summary>
    /// <param name="value">The value of the direct child node to find.</param>
    /// <returns>The <seealso cref="TreeNode{T}"/> with the specified value, if found; otherwise <see langword="null"/>.</returns>
    public abstract TTreeNode GetChild(TValue value);

    /// <summary>Gets a node from the subtree with this tree node as a root node that has the specified value.</summary>
    /// <param name="value">The value of the tree node to find.</param>
    /// <returns>The <seealso cref="TreeNode{T}"/> with the specified value, if found; otherwise <see langword="null"/>.</returns>
    public virtual TTreeNode GetNode(TValue value) => GetNode(TraversePreOrderNodes(), value);

    /// <summary>Gets the first node that has the specified value from the specified collection.</summary>
    /// <param name="collection">The collection that may contain the requested value.</param>
    /// <param name="value">The value to find in the collection's nodes.</param>
    /// <returns>The first node that was found in the collection, or <see langword="null"/>, if it was not found.</returns>
    protected static TTreeNode GetNode(IEnumerable<TTreeNode> collection, TValue value)
    {
        return collection.FirstOrDefault(node => Checks.SafeEquals(node.Value, value));
    }

    /// <summary>Removes a node that has the specified value. This includes any node that is within the subtree with this tree node as the root.</summary>
    /// <param name="value">The value of the node to remove from this node's children list.</param>
    /// <returns><see langword="true"/> if the child was successfully removed from this node's children list, otherwise <see langword="false"/>.</returns>
    public virtual bool RemoveNode(TValue value)
    {
        return RemoveNode(GetNode(value));
    }
    /// <summary>Removes a node that has the specified value. This includes any node that is within the subtree with this tree node as the root.</summary>
    /// <param name="node">The node to remove from this node's children list.</param>
    /// <returns><see langword="true"/> if the child was successfully removed from this node's children list, otherwise <see langword="false"/>.</returns>
    public virtual bool RemoveNode(TTreeNode? node)
    {
        if (node == null)
            return false;

        if (!Checks.SafeEquals(node.BaseTree, InternalBaseTree))
            return false;

        if (node.IsRoot)
        {
            // TODO: What happens if the node belongs in no tree?
            node.BaseTree?.Clear();
            return true;
        }

        return node.Parent.RemoveChild(node);
    }

    /// <summary>Removes nodes that have the specified values. This includes any node that is within the subtree with this tree node as the root.</summary>
    /// <param name="values">The values of the child nodes to remove from this node's children list.</param>
    /// <returns>The number of nodes from the list that were removed.</returns>
    public virtual void RemoveNodes(IEnumerable<TValue> values)
    {
        var nodes = TraversePreOrderNodes().Where(n => values.Contains(n.Value)).ToList();

        foreach (var n in nodes)
        {
            if (n.IsRoot)
            {
                InternalBaseTree.Root = null;
                break; // If one of these nodes is the root, the entire tree is then removed
            }

            InternalBaseTree.RemoveNode(n);
        }
    }

    /// <summary>Removes nodes that have the specified values. This includes any node that is within the subtree with this tree node as the root.</summary>
    /// <param name="values">The values of the child nodes to remove from this node's children list.</param>
    /// <returns>The number of children that were removed.</returns>
    public void RemoveNodes(params TValue[] values) => RemoveNodes((IEnumerable<TValue>)values);

    /// <summary>Removes a child from this node's children list.</summary>
    /// <param name="childToRemove">The child to remove from this node's children list.</param>
    public bool RemoveChild(TTreeNode childToRemove)
    {
        // Ensure the child is contained in the children list
        if (!PerformChildRemoval(childToRemove))
            return false;

        // Abortion
        if (Checks.SafeEquals(childToRemove.InternalParent, this))
            childToRemove.InternalParent = default;

        // If the removed child is already in another tree, do not corrupt its state
        if (Checks.SafeEquals(childToRemove.BaseTree, InternalBaseTree))
            childToRemove.BaseTree = default;

        InternalBaseTree?.RemoveChild(This, childToRemove, false);
        return true;
    }
    /// <summary>Removes a child from this node's children list.</summary>
    /// <param name="childToRemove">The child to remove from this node's children list.</param>
    public bool RemoveChild(TValue childToRemove)
    {
        return RemoveChild(GetChild(childToRemove));
    }

    /// <summary>Performs the operations after a child was added to this tree.</summary>
    /// <param name="addedChild">The child that was added to the children collection.</param>
    protected abstract void RegisterAddedChild(TTreeNode addedChild);
    /// <summary>Performs the removal of a child.</summary>
    /// <param name="childToRemove">The child to remove from the children collection.</param>
    /// <returns><see langword="true"/> if the child was found and removed, otherwise <see langword="false"/>.</returns>
    protected abstract bool PerformChildRemoval(TTreeNode childToRemove);

    /// <summary>Makes this node a child of another tree node.</summary>
    /// <param name="other">The other tree node that will become this node's parent.</param>
    public void MakeChildOf(TTreeNode other)
    {
        BaseTree = other.BaseTree;
        Parent = other;
    }
    /// <summary>Makes this node a parent of another tree node.</summary>
    /// <param name="other">The other tree node that will become this node's child.</param>
    public void MakeParentOf(TTreeNode other) => other.MakeChildOf(This);

    /// <summary>Clones this <seealso cref="TreeNode{T}"/> and all its children recursively.</summary>
    /// <returns>The cloned <seealso cref="TreeNode{T}"/>. Its base tree is also equal to the original <seealso cref="TreeNode{T}"/>'s base tree.</returns>
    public TTreeNode Clone() => Clone(true);
    /// <summary>Clones this <seealso cref="TreeNode{T}"/> and all its children recursively.</summary>
    /// <param name="setBaseTree">Determines whether to set the base tree on the cloned instance.</param>
    /// <returns>The cloned <seealso cref="TreeNode{T}"/>. Its base tree is also equal to the original <seealso cref="TreeNode{T}"/>'s base tree.</returns>
    protected TTreeNode Clone(bool setBaseTree)
    {
        var result = InitializeNewNode(Value);
        AddChildrenToClonedInstance(result);

        if (setBaseTree)
            result.BaseTree = BaseTree;
        
        return result;
    }

    /// <summary>Clones this tree node's children to the cloned instance that is created within the <seealso cref="Clone()"/> method.</summary>
    /// <param name="result">The resulting instance to which to add the cloned children.</param>
    /// <remarks>The cloned children should also be cloned with the <seealso cref="Clone(bool)"/> method, but with the argument being <see langword="false"/>.</remarks>
    protected abstract void AddChildrenToClonedInstance(TTreeNode result);

    /// <summary>Traverses the subtree with this node as a root using pre-order. The elements are returned with <see langword="yield return"/>.</summary>
    /// <returns>The yielded values.</returns>
    public IEnumerable<TValue> TraversePreOrder()
    {
        foreach (var n in TraversePreOrderNodes())
            yield return n.Value;
    }
    /// <summary>Traverses the subtree with this node as a root using post-order. The elements are returned with <see langword="yield return"/>.</summary>
    /// <returns>The yielded values.</returns>
    public IEnumerable<TValue> TraversePostOrder()
    {
        foreach (var n in TraversePostOrderNodes())
            yield return n.Value;
    }
    /// <summary>Traverses the subtree with this node as a root using level-order. The elements are returned with <see langword="yield return"/>.</summary>
    /// <returns>The yielded values.</returns>
    public IEnumerable<TValue> TraverseLevelOrder()
    {
        foreach (var n in TraverseLevelOrderNodes())
            yield return n.Value;
    }

    /// <summary>Traverses the subtree with this node as a root using pre-order. The nodes are returned with <see langword="yield return"/>.</summary>
    /// <returns>The yielded nodes.</returns>
    public abstract IEnumerable<TTreeNode> TraversePreOrderNodes();
    /// <summary>Traverses the subtree with this node as a root using post-order. The nodes are returned with <see langword="yield return"/>.</summary>
    /// <returns>The yielded nodes.</returns>
    public abstract IEnumerable<TTreeNode> TraversePostOrderNodes();
    /// <summary>Traverses the subtree with this node as a root using level-order. The nodes are returned with <see langword="yield return"/>.</summary>
    /// <returns>The yielded nodes.</returns>
    public abstract IEnumerable<TTreeNode> TraverseLevelOrderNodes();

    /// <summary>Gets the tree view of this tree node as a subtree.</summary>
    /// <param name="childIndent">The length of the indentation for each subsequent child.</param>
    /// <returns>The tree view.</returns>
    public string GetTreeView(int childIndent = 4)
    {
        var sb = new StringBuilder().AppendLine(ToString());
        foreach (var l in GetTreeView(This, "", childIndent))
            sb.AppendLine(l);
        return sb.ToString().TrimEnd();
    }

    private static IEnumerable<string> GetTreeView(TTreeNode node, string indent, int childIndent)
    {
        const char expandingNonLastNodeBaseChar = '├';
        const char expandingLastNodeBaseChar = '└';
    
        var children = node.Children;
        
        if (!children.Any())
            yield break;
        
        char expandingNodeChar = expandingNonLastNodeBaseChar;
        var newIndent = $"{indent}|{new string(' ', childIndent - 1)}";
        
        for (int i = 0; i < children.Count; i++)
        {
            var c = children[i];

            if (i == children.Count - 1)
            {
                expandingNodeChar = expandingLastNodeBaseChar;
                newIndent = $"{indent}{new string(' ', childIndent)}";
            }

            yield return $"{indent}{expandingNodeChar}{new string('-', childIndent - 1)}{c}";
            foreach (var line in GetTreeView(c, newIndent, childIndent))
                yield return line;
        }
    }

    /// <summary>Gets an enumerator that traverses the subtree using pre-order traversal.</summary>
    /// <returns>The <seealso cref="IEnumerator{T}"/> that traverses the subtree using pre-order traversal.</returns>
    public IEnumerator<TValue> GetEnumerator() => TraversePreOrder().GetEnumerator();
    /// <summary>Gets an enumerator that traverses the subtree using pre-order traversal.</summary>
    /// <returns>The <seealso cref="IEnumerator"/> that traverses the subtree using pre-order traversal.</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Gets the hash code of this tree node, which is equal to the hash code of the value of this node.</summary>
    /// <returns>This tree node's hash code.</returns>
    public override int GetHashCode() => Value.GetHashCode();
    /// <summary>Gets the string representation of this tree node's value.</summary>
    /// <returns>The string representation of this tree node's value.</returns>
    public override string ToString() => Value.ToString();
}