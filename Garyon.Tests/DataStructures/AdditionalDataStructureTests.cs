using Garyon.DataStructures;
using Garyon.DataStructures.Trees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.DataStructures;

public class AdditionalDataStructureTests
{
    [Test]
    public async Task QueueSetConstructorAndCopyEdgeCasesTest()
    {
        var comparerOnly = new QueueSet<string>(StringComparer.OrdinalIgnoreCase);
        var queueSet = new QueueSet<string>(4, StringComparer.OrdinalIgnoreCase);

        await Assert.That(comparerOnly.Enqueue("Beta")).IsTrue();
        await Assert.That(comparerOnly.Enqueue("beta")).IsFalse();
        await Assert.That(queueSet.Enqueue("Alpha")).IsTrue();
        await Assert.That(queueSet.Enqueue("alpha")).IsFalse();
        await Assert.That(queueSet.Peek()).IsEqualTo("Alpha");

        var array = new string[2];
        queueSet.CopyTo(array, 1);

        await Assert.That(array[0]).IsNull();
        await Assert.That(array[1]).IsEqualTo("Alpha");
        await Assert.That(((IEnumerable)queueSet).GetEnumerator().MoveNext()).IsTrue();
        await Assert.That(queueSet.Dequeue()).IsEqualTo("Alpha");
        await Assert.That(queueSet.IsEmpty).IsTrue();
    }

    [Test]
    public async Task QueueSetRangeRemovalCapsAtCountTest()
    {
        var queueSet = new QueueSet<int>(4);
        queueSet.EnqueueRange([1, 2, 2, 3]);

        var removed = queueSet.DequeueRange(10).ToArray();

        await Assert.That(removed.SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(queueSet.IsEmpty).IsTrue();
    }

    [Test]
    public async Task StackSetConstructorAndCopyEdgeCasesTest()
    {
        var comparerOnly = new StackSet<string>(StringComparer.OrdinalIgnoreCase);
        var stackSet = new StackSet<string>(4, StringComparer.OrdinalIgnoreCase);

        await Assert.That(comparerOnly.Push("Beta")).IsTrue();
        await Assert.That(comparerOnly.Push("beta")).IsFalse();
        await Assert.That(stackSet.Push("Alpha")).IsTrue();
        await Assert.That(stackSet.Push("alpha")).IsFalse();
        await Assert.That(stackSet.Peek()).IsEqualTo("Alpha");

        var array = new string[2];
        stackSet.CopyTo(array, 1);

        await Assert.That(array[0]).IsNull();
        await Assert.That(array[1]).IsEqualTo("Alpha");
        await Assert.That(((IEnumerable)stackSet).GetEnumerator().MoveNext()).IsTrue();
        await Assert.That(stackSet.Pop()).IsEqualTo("Alpha");
        await Assert.That(stackSet.IsEmpty).IsTrue();
    }

    [Test]
    public async Task StackSetRangeRemovalCapsAtCountTest()
    {
        var stackSet = new StackSet<int>(4);
        stackSet.PushRange([1, 2, 2, 3]);

        var removed = stackSet.PopRange(10).ToArray();

        await Assert.That(removed.SequenceEqual([3, 2, 1])).IsTrue();
        await Assert.That(stackSet.IsEmpty).IsTrue();
    }

    [Test]
    public async Task ConcurrentSetSetOperationsTest()
    {
        var set = new ConcurrentSet<int>();
        set.UnionWith([1, 2, 3]);

        await Assert.That(set.Add(2)).IsFalse();
        await Assert.That(set.IsSupersetOf([1, 3])).IsTrue();
        await Assert.That(set.IsProperSupersetOf([1, 3])).IsTrue();
        await Assert.That(set.Overlaps([0, 3])).IsTrue();

        set.SymmetricExceptWith([2, 4]);

        await Assert.That(set.SetEquals([1, 3, 4])).IsTrue();
        await Assert.That(set.TryRemove(4)).IsTrue();
        await Assert.That(set.SetEquals([1, 3])).IsTrue();
    }

    [Test]
    public async Task ConcurrentSetInterfaceAndComparerOperationsTest()
    {
        ISet<string> set = new ConcurrentSet<string>(StringComparer.OrdinalIgnoreCase);

        await Assert.That(set.Add("Alpha")).IsTrue();
        await Assert.That(set.Add("alpha")).IsFalse();
        await Assert.That(set.Count).IsEqualTo(1);
        await Assert.That(((ConcurrentSet<string>)set).IsEmpty).IsFalse();
        await Assert.That(set.IsReadOnly).IsFalse();
        await Assert.That(set.IsSubsetOf(new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "alpha", "beta" })).IsTrue();
        await Assert.That(set.IsProperSubsetOf(new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "alpha", "beta" })).IsTrue();

        set.IntersectWith(["Alpha"]);
        await Assert.That(set.Contains("ALPHA")).IsTrue();

        string[] copied = new string[1];
        set.CopyTo(copied, 0);
        await Assert.That(copied[0]).IsEqualTo("Alpha");

        set.ExceptWith(["ALPHA"]);
        await Assert.That(set.Count).IsEqualTo(0);

        ((ICollection<string>)set).Add("Beta");
        await Assert.That(((IEnumerable)set).GetEnumerator().MoveNext()).IsTrue();
        await Assert.That(set.Remove("beta")).IsTrue();
        await Assert.That(((ConcurrentSet<string>)set).IsEmpty).IsTrue();

        set.Add("Gamma");
        ((ConcurrentSet<string>)set).Clear();
        await Assert.That(((ConcurrentSet<string>)set).IsEmpty).IsTrue();
    }

    [Test]
    public async Task FlexInitDictionaryCreatesValuesForKeysTest()
    {
        var dictionary = new FlexInitDictionary<string, List<int>>(["a", "b"]);
        dictionary["a"].Add(1);
        var clone = dictionary.Clone();

        await Assert.That(dictionary.Count).IsEqualTo(2);
        await Assert.That(dictionary["a"].SequenceEqual([1])).IsTrue();
        await Assert.That(dictionary["b"]).IsEmpty();
        await Assert.That(clone["a"].SequenceEqual([1])).IsTrue();
    }

    [Test]
    public async Task FlexDictionaryInterfaceMembersTest()
    {
        IDictionary<int, string> dictionary = new FlexDictionary<int, string>(
        [
            new KeyValuePair<int, string>(1, "one"),
            new KeyValuePair<int, string>(2, "two"),
        ], "default");

        dictionary.Add(new KeyValuePair<int, string>(3, "three"));
        var copied = new KeyValuePair<int, string>[3];
        dictionary.CopyTo(copied, 0);

        await Assert.That(dictionary.IsReadOnly).IsFalse();
        await Assert.That(dictionary.Keys.SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(dictionary.Values.SequenceEqual(["one", "two", "three"])).IsTrue();
        await Assert.That(dictionary.Contains(new KeyValuePair<int, string>(2, "two"))).IsTrue();
        await Assert.That(copied.Contains(new KeyValuePair<int, string>(3, "three"))).IsTrue();
        await Assert.That(((IEnumerable)dictionary).GetEnumerator().MoveNext()).IsTrue();
        await Assert.That(dictionary.Remove(new KeyValuePair<int, string>(3, "three"))).IsTrue();
        await Assert.That(dictionary[4]).IsEqualTo("default");
    }

    [Test]
    public async Task FlexInitDictionaryConstructorVariantsTest()
    {
        var capacityDictionary = new FlexInitDictionary<int, List<string>>(4);
        var kvpDictionary = new FlexInitDictionary<int, List<string>>(
        [
            new KeyValuePair<int, List<string>>(1, ["one"]),
        ]);

        capacityDictionary[2].Add("two");

        await Assert.That(capacityDictionary[2].SequenceEqual(["two"])).IsTrue();
        await Assert.That(kvpDictionary[1].SequenceEqual(["one"])).IsTrue();
    }

    [Test]
    public async Task ValueCounterDictionaryConstructorVariantsTest()
    {
        var capacityDictionary = new ValueCounterDictionary<string>(4);
        var initialValueDictionary = new ValueCounterDictionary<string>(["a", "b"], 3);
        var kvpDictionary = new ValueCounterDictionary<string>(
        [
            new KeyValuePair<string, int>("x", 7),
        ]);

        capacityDictionary.Add("a", 2);
        capacityDictionary.AdjustCounters("a", "b", 1);

        await Assert.That(capacityDictionary["a"]).IsEqualTo(5);
        await Assert.That(capacityDictionary["b"]).IsEqualTo(5);
        await Assert.That(initialValueDictionary["a"]).IsEqualTo(3);
        await Assert.That(kvpDictionary["x"]).IsEqualTo(7);
    }

    [Test]
    public async Task InterlinkedDictionaryMaintainsBothDirectionsTest()
    {
        var dictionary = new InterlinkedDictionary<int, string>();
        dictionary.Add(1, "one");
        dictionary.Add(2, "two");

        dictionary[1] = "uno";
        dictionary["two"] = 22;

        await Assert.That(dictionary.Count).IsEqualTo(2);
        await Assert.That(dictionary.ValueOrDefault(1)).IsEqualTo("uno");
        await Assert.That(dictionary.ValueOrDefault("uno")).IsEqualTo(1);
        await Assert.That(dictionary.ValueOrDefault(2)).IsNull();
        await Assert.That(dictionary.ValueOrDefault("two")).IsEqualTo(22);
        await Assert.That(dictionary.ValueOrDefault(22)).IsEqualTo("two");

        await Assert.That(dictionary.Remove("uno")).IsTrue();
        await Assert.That(dictionary.Contains(1)).IsFalse();
        await Assert.That(dictionary.Contains("uno")).IsFalse();
    }

    [Test]
    public async Task InterlinkedDictionaryCopyClearAndFailureBranchesTest()
    {
        var dictionary = new InterlinkedDictionary<int, string>();
        dictionary.Add(1, "one");
        dictionary.Add(2, "two");
        var copy = new InterlinkedDictionary<int, string>(dictionary);

        await Assert.That(copy.Values1.SequenceEqual([1, 2])).IsTrue();
        await Assert.That(copy.Values2.SequenceEqual(["one", "two"])).IsTrue();
        await Assert.That(copy[1]).IsEqualTo("one");
        await Assert.That(copy["two"]).IsEqualTo(2);
        await Assert.That(copy.Remove(1)).IsTrue();
        await Assert.That(copy.Remove(1)).IsFalse();
        copy.Add(1, "one");
        await Assert.That(copy.Remove(3)).IsFalse();
        await Assert.That(copy.Remove("missing")).IsFalse();

        var nullableDictionary = new InterlinkedDictionary<string, string>();
        Assert.Throws<ArgumentNullException>(() => nullableDictionary.Add(null!, "null-key"));
        Assert.Throws<ArgumentException>(() => copy.Add(1, "three"));
        Assert.Throws<ArgumentException>(() => copy.Add(3, "one"));

        copy.Clear();
        await Assert.That(copy.Count).IsEqualTo(0);
    }

    [Test]
    public async Task BinaryTreeTraversesAndRemovesChildrenTest()
    {
        var tree = new BinaryTree<int>(4);
        tree.Root.AddChildren(2, 6);
        tree.Root.LeftChild.AddChildren(1, 3);
        tree.Root.RightChild.AddChildren(5, 7);

        await Assert.That(tree.TraverseInOrder().SequenceEqual([1, 2, 3, 4, 5, 6, 7])).IsTrue();
        await Assert.That(tree.TraversePreOrder().SequenceEqual([4, 2, 1, 3, 6, 5, 7])).IsTrue();
        await Assert.That(tree.TraversePostOrder().SequenceEqual([1, 3, 2, 5, 7, 6, 4])).IsTrue();
        await Assert.That(tree.TraverseLevelOrder().SequenceEqual([4, 2, 6, 1, 3, 5, 7])).IsTrue();
        await Assert.That(tree.Count).IsEqualTo(7);

        tree.RemoveChildren(4, [2]);

        await Assert.That(tree.TraverseInOrder().SequenceEqual([4, 5, 6, 7])).IsTrue();
        await Assert.That(tree.Count).IsEqualTo(4);
    }

    [Test]
    public async Task BinaryTreeConstructorVariantsTest()
    {
        var empty = new BinaryTree<int>();
        var rootNode = new BinaryTreeNode<int>(10);
        var fromRootNode = new BinaryTree<int>(rootNode);
        var copy = new BinaryTree<int>(fromRootNode);
        var nodeWithBaseTree = new BinaryTreeNode<int>(fromRootNode, 11);
        var childFromParent = new BinaryTreeNode<int>(rootNode, 12);
        var parentWithChildren = new BinaryTreeNode<int>(
            rootNode,
            new BinaryTreeNode<int>(1),
            new BinaryTreeNode<int>(2),
            13);

        await Assert.That(empty.Count).IsEqualTo(0);
        await Assert.That(fromRootNode.Root.Value).IsEqualTo(10);
        await Assert.That(copy.Root.Value).IsEqualTo(10);
        await Assert.That(nodeWithBaseTree.BaseTree).IsEqualTo(fromRootNode);
        await Assert.That(childFromParent.Parent).IsEqualTo(rootNode);
        await Assert.That(parentWithChildren.ChildrenCount).IsEqualTo(2);
    }

    [Test]
    public async Task BinaryTreeAddSetClearChildrenBranchesTest()
    {
        var tree = new BinaryTree<int>(10);
        var left = new BinaryTreeNode<int>(5);
        var right = new BinaryTreeNode<int>(15);

        tree.SetChildren(10, left, right);
        await Assert.That(tree.Root.LeftChild).IsEqualTo(left);
        await Assert.That(tree.Root.RightChild).IsEqualTo(right);

        tree.ClearChildren(tree.Root);
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(0);

        tree.AddChild(tree.Root, 3);
        tree.AddChildren(tree.Root, new BinaryTreeNode<int>(2), new BinaryTreeNode<int>(4));
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(2);

        tree.RemoveRootChildren([3]);
        await Assert.That(tree.Root.LeftChild).IsNull();
        await Assert.That(tree.Root.RightChild!.Value).IsEqualTo(2);
    }

    [Test]
    public async Task BinaryTreeNodeChildrenSetterAndRemovalBranchesTest()
    {
        var tree = new BinaryTree<int>(10);
        var left = new BinaryTreeNode<int>(5);
        var right = new BinaryTreeNode<int>(15);

        tree.Root.Children = [left, right];
        await Assert.That(tree.Root.GetChild(5)).IsEqualTo(left);
        await Assert.That(tree.Root.GetChild(15)).IsEqualTo(right);
        await Assert.That(tree.Root.GetChild(20)).IsNull();

        tree.Root.RemoveChildren([right]);
        await Assert.That(tree.Root.RightChild).IsNull();

        tree.Root.RemoveChildren([5]);
        await Assert.That(tree.Root.LeftChild).IsNull();
    }

    [Test]
    public async Task BinarySearchTreeFindsAndRejectsDuplicateNodesTest()
    {
        var tree = new BinarySearchTree<int>(4);
        tree.Root.AddNodes(2, 6, 1, 3, 5, 7);

        await Assert.That(tree.Find(5).Value).IsEqualTo(5);
        await Assert.That(tree.Find(8)).IsNull();
        await Assert.That(tree.Root.AddNode(3)).IsFalse();
        await Assert.That(tree.TraverseInOrder().SequenceEqual([1, 2, 3, 4, 5, 6, 7])).IsTrue();
    }

    [Test]
    public async Task BinarySearchTreeConstructorVariantsTest()
    {
        var empty = new BinarySearchTree<int>();
        var rootNode = new BinarySearchTreeNode<int>(10);
        var fromRootNode = new BinarySearchTree<int>(rootNode);
        var copy = new BinarySearchTree<int>(fromRootNode);
        var nodeWithBaseTree = new BinarySearchTreeNode<int>(fromRootNode, 11);
        var childFromParent = new BinarySearchTreeNode<int>(rootNode, 12);
        var parentWithChildren = new BinarySearchTreeNode<int>(
            rootNode,
            new BinarySearchTreeNode<int>(1),
            new BinarySearchTreeNode<int>(20),
            10);

        await Assert.That(empty.Count).IsEqualTo(0);
        await Assert.That(fromRootNode.Root.Value).IsEqualTo(10);
        await Assert.That(copy.Root.Value).IsEqualTo(10);
        await Assert.That(nodeWithBaseTree.BaseTree).IsEqualTo(fromRootNode);
        await Assert.That(childFromParent.Parent).IsEqualTo(rootNode);
        await Assert.That(parentWithChildren.LeftChild!.Value).IsEqualTo(1);
        await Assert.That(parentWithChildren.RightChild!.Value).IsEqualTo(20);
    }

    [Test]
    public async Task BinarySearchTreeOrdersChildrenAndThrowsForInvalidChildrenTest()
    {
        var root = new BinarySearchTreeNode<int>(10);
        var right = new BinarySearchTreeNode<int>(20);
        var left = new BinarySearchTreeNode<int>(1);

        root.AddChildren(right, left);

        await Assert.That(root.LeftChild).IsEqualTo(left);
        await Assert.That(root.RightChild).IsEqualTo(right);

        Assert.Throws<ArgumentException>(() =>
        {
            root.Children = [new BinarySearchTreeNode<int>(30), new BinarySearchTreeNode<int>(40)];
        });
    }

    [Test]
    public async Task GenericTreeConstructorVariantsTest()
    {
        var empty = new Tree<int>();
        var rootNode = new TreeNode<int>(1);
        var fromRootNode = new Tree<int>(rootNode);
        var copy = new Tree<int>(fromRootNode);
        await Assert.That(empty.Count).IsEqualTo(0);
        await Assert.That(fromRootNode.Root.Value).IsEqualTo(1);
        await Assert.That(copy.Root.Value).IsEqualTo(1);
        Assert.Throws<NullReferenceException>(() => new TreeNode<int>(fromRootNode, 2));
        Assert.Throws<NullReferenceException>(() => new TreeNode<int>(rootNode, 3));
    }

    [Test]
    public async Task GenericTreeNodePropertyBranchesTest()
    {
        var tree = new Tree<int>(1);
        var child = new TreeNode<int>(2);

        tree.Root.Children = null!;
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(0);

        tree.Root.Children = [child];
        await Assert.That(child.BaseTree).IsEqualTo(tree);
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(1);

        child.Parent = null!;
        tree.Root.RemoveChild(child);
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(0);
    }

    [Test]
    public async Task FlexInitAndValueCounterKeyValueConstructorsUseProvidedPairsTest()
    {
        IEnumerable<KeyValuePair<string, List<int>>> flexPairs =
        [
            new("first", [1, 2]),
        ];
        IEnumerable<KeyValuePair<string, int>> counterPairs =
        [
            new("first", 4),
        ];

        var flexDictionary = new FlexInitDictionary<string, List<int>>(flexPairs);
        var counterDictionary = new ValueCounterDictionary<string>(counterPairs);

        await Assert.That(flexDictionary["first"].SequenceEqual([1, 2])).IsTrue();
        await Assert.That(counterDictionary["first"]).IsEqualTo(4);
    }

    [Test]
    public async Task TreeCopyConstructorsCloneIndependentRootsTest()
    {
        var generic = new Tree<int>(1);
        generic.Root.AddChildren(2, 3);
        var binary = new BinaryTree<int>(4);
        binary.Root.AddChildren(2, 6);
        var search = new BinarySearchTree<int>(4);
        search.Root.AddNodes(2, 6);

        var genericCopy = new Tree<int>(generic);
        var binaryCopy = new BinaryTree<int>(binary);
        var searchCopy = new BinarySearchTree<int>(search);

        generic.Root.RemoveChild(2);
        binary.Root.RemoveChild(2);
        search.Root.RemoveChild(2);

        await Assert.That(genericCopy.TraversePreOrder().SequenceEqual([1, 2, 3])).IsTrue();
        await Assert.That(binaryCopy.TraverseInOrder().SequenceEqual([2, 4, 6])).IsTrue();
        await Assert.That(searchCopy.TraverseInOrder().SequenceEqual([2, 4, 6])).IsTrue();
    }

    [Test]
    public async Task TreeCollectionInterfaceMembersTest()
    {
        ICollection<int> tree = new Tree<int>(1);
        ((Tree<int>)tree).Root.AddChildren(2, 3);
        var array = new int[4];

        tree.CopyTo(array, 1);

        await Assert.That(tree.IsReadOnly).IsFalse();
        await Assert.That(array.SequenceEqual([0, 1, 2, 3])).IsTrue();
        await Assert.That(((IEnumerable)tree).GetEnumerator().MoveNext()).IsTrue();
        Assert.Throws<NotSupportedException>(() => tree.Add(4));
    }

    [Test]
    public async Task TreeEqualityHandlesNullCountAndValueMismatchesTest()
    {
        var first = new Tree<int>(1);
        first.Root.AddChildren(2, 3);
        var same = new Tree<int>(1);
        same.Root.AddChildren(2, 3);
        var differentCount = new Tree<int>(1);
        differentCount.Root.AddChild(2);
        var differentValue = new Tree<int>(1);
        differentValue.Root.AddChildren(2, 4);

        _ = first.Count;
        _ = differentCount.Count;

        await Assert.That(first.Equals(null)).IsFalse();
        await Assert.That(first.Equals(differentCount)).IsFalse();
        await Assert.That(first.Equals(differentValue)).IsFalse();
        await Assert.That(first.Equals(same)).IsTrue();
        await Assert.That(first.Equals((object)same)).IsTrue();
        await Assert.That(first.GetHashCode()).IsNotEqualTo(0);
    }

    [Test]
    public async Task TreeAdjustmentRejectsNullAndForeignNodesTest()
    {
        var tree = new Tree<int>(1);
        var foreignTree = new Tree<int>(10);

        tree.SetChildren((TreeNode<int>)null!, [new TreeNode<int>(2)]);
        tree.SetChildren(foreignTree.Root, [new TreeNode<int>(11)]);
        tree.AddChild((TreeNode<int>)null!, 2);
        tree.AddChild(foreignTree.Root, new TreeNode<int>(11));
        tree.AddChildren((TreeNode<int>)null!, [new TreeNode<int>(3)]);
        tree.AddChildren(foreignTree.Root, [new TreeNode<int>(12)]);
        tree.RemoveChildren((TreeNode<int>)null!, [2]);
        tree.RemoveChildren(foreignTree.Root, [10]);
        tree.ClearChildren((TreeNode<int>)null!);
        tree.ClearChildren(foreignTree.Root);

        await Assert.That(tree.Count).IsEqualTo(1);
        await Assert.That(foreignTree.Count).IsEqualTo(1);
    }

    [Test]
    public async Task TreeValueAdjustmentOverloadsRespectPropagationFlagsTest()
    {
        var tree = new Tree<int>(1);
        var child = new TreeNode<int>(2);
        var removable = new TreeNode<int>(3);

        tree.SetChildren(1, new List<TreeNode<int>> { child }, false);
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(0);

        tree.SetChildren(1, [2, 3]);
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(2);

        tree.RemoveChildren(1, [2], false);
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(2);

        tree.RemoveChildren(1, [tree.Root.GetChild(2)], false);
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(2);

        tree.Root.AddChild(removable);
        await Assert.That(tree.RemoveNode(removable, false)).IsTrue();
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(3);

        await Assert.That(tree.RemoveChild(tree.Root, tree.Root, true)).IsTrue();
        await Assert.That(tree.Count).IsEqualTo(0);
    }

    [Test]
    public async Task TreeNodeConstructorAndRemovalEdgeCasesTest()
    {
        var root = new TreeNode<int>(1);
        var constructed = new TreeNode<int>(root, [new TreeNode<int>(3)], 2);
        await Assert.That(constructed.ChildrenCount).IsEqualTo(1);

        var tree = new Tree<int>(1);
        var child = tree.Root.AddChild(2);
        var foreign = new TreeNode<int>(3);

        await Assert.That(tree.Root.RemoveNode((TreeNode<int>)null!)).IsFalse();
        await Assert.That(tree.Root.RemoveNode(foreign)).IsFalse();
        await Assert.That(tree.Root.RemoveNode(tree.Root)).IsTrue();
        await Assert.That(tree.Count).IsEqualTo(0);

        var secondTree = new Tree<int>(10);
        secondTree.Root.AddChild(child);
        await Assert.That(secondTree.Root.RemoveChild(child)).IsTrue();
        await Assert.That(child.BaseTree).IsNull();
    }

    [Test]
    public async Task BinaryTreeNodeConstructorAndChildrenBranchesTest()
    {
        var parent = new BinaryTreeNode<int>(10);
        var left = new BinaryTreeNode<int>(5);
        var right = new BinaryTreeNode<int>(15);
        var node = new BinaryTreeNode<int>(parent, left, right, 12);

        await Assert.That(node.LeftChild).IsEqualTo(left);
        await Assert.That(node.RightChild).IsEqualTo(right);
        await Assert.That(node.Children.SequenceEqual([left, right])).IsTrue();

        Assert.Throws<ArgumentOutOfRangeException>(() => node.Children = [right]);
        Assert.Throws<ArgumentOutOfRangeException>(() => node.Children = []);
    }

    [Test]
    public async Task BinarySearchTreeNodeConstructorAndRotationBranchesTest()
    {
        var parent = new BinarySearchTreeNode<int>(10);
        var left = new BinarySearchTreeNode<int>(5);
        var right = new BinarySearchTreeNode<int>(15);
        var node = new BinarySearchTreeNode<int>(parent, left, right, 12);

        await Assert.That(node.LeftChild).IsEqualTo(left);
        await Assert.That(node.RightChild).IsEqualTo(right);
        await Assert.That(node.Children.SequenceEqual([left, right])).IsTrue();

        Assert.Throws<ArgumentOutOfRangeException>(() => node.Children = [new BinarySearchTreeNode<int>(11)]);
    }

    [Test]
    public async Task BaseTreeAndNodeBranchesAreCoveredByMinimalSubclassTest()
    {
        var tree = new TestTree(1);
        tree.AttachRoot();
        var left = new TestTreeNode(2);
        var right = new TestTreeNode(3);
        tree.Root.Children = [left, right];
        var detached = new TestTreeNode(tree, 4);
        var parentCtor = new TestTreeNode(tree.Root, 5);

        detached.BaseTree = tree;
        parentCtor.Parent = tree.Root;
        tree.Root.MakeParentOf(detached);

        await Assert.That(detached.BaseTree).IsNull();
        await Assert.That(parentCtor.Parent).IsNull();
        await Assert.That(tree.Root.Depth).IsEqualTo(0);
        await Assert.That(left.Depth).IsEqualTo(1);
        await Assert.That(left.BaseTreeHeight).IsEqualTo(2);
        await Assert.That(tree.Root.Breadth).IsEqualTo(2);
        await Assert.That(tree.Root.Descendants).IsEqualTo(2);
        await Assert.That(tree.Root.GetNode(3)).IsEqualTo(right);
        await Assert.That(tree.Root.GetTreeView(2).Contains("3")).IsTrue();
        await Assert.That(((IEnumerable)tree.Root).GetEnumerator().MoveNext()).IsTrue();
        await Assert.That(tree.Root.GetHashCode()).IsEqualTo(1.GetHashCode());

        await Assert.That(tree.RemoveNode(left, false)).IsTrue();
        await Assert.That(tree.Root.ChildrenCount).IsEqualTo(2);

        tree.Root.RemoveNodes([1]);
        await Assert.That(tree.Root).IsNull();
    }
}

file sealed class TestTree : BaseTree<int, TestTree, TestTreeNode>
{
    public TestTree() { }
    public TestTree(int rootValue)
        : base(rootValue) { }
    public TestTree(TestTreeNode root)
        : base(root) { }
    public TestTree(TestTree tree)
        : base(tree) { }

    public void AttachRoot()
    {
        Root?.Attach(this, null);
    }

    protected override TestTreeNode InitializeNewNode(int value = default) => new(value);
    protected override TestTreeNode InitializeNewNode(TestTree baseTree, int value = default) => new(baseTree, value);
    protected override TestTreeNode InitializeNewNode(TestTreeNode parentNode, int value = default) => new(parentNode, value);

    public override void ClearChildren(TestTreeNode node, bool propagate)
    {
        if (!PrepareChildrenAdjustmentOperation(node))
            return;

        if (propagate)
            node.Children = [];
    }
}

file sealed class TestTreeNode : BaseTreeNode<int, TestTree, TestTreeNode>
{
    private List<TestTreeNode> children = [];

    public TestTreeNode(int value = default)
        : base(value) { }
    public TestTreeNode(TestTree baseTree, int value = default)
        : base(baseTree, value) { }
    public TestTreeNode(TestTreeNode parentNode, int value = default)
        : base(parentNode, value) { }

    public void Attach(TestTree tree, TestTreeNode parent)
    {
        InternalBaseTree = tree;
        InternalParent = parent;
        foreach (var child in children)
            child.Attach(tree, this);
    }

    public override List<TestTreeNode> Children
    {
        get => new(children);
        set
        {
            children = value ?? [];
            foreach (var child in children)
                child.Attach(InternalBaseTree, this);
        }
    }

    public override int ChildrenCount => children.Count;
    public override int Height => IsLeaf ? 1 : children.Max(c => c.Height + 1);

    protected override TestTreeNode InitializeNewNode(int value = default) => new(value);
    protected override TestTreeNode InitializeNewNode(TestTree baseTree, int value = default) => new(baseTree, value);
    protected override TestTreeNode InitializeNewNode(TestTreeNode parentNode, int value = default) => new(parentNode, value);

    public override TestTreeNode GetChild(int value) => children.FirstOrDefault(c => c.Value == value);

    protected override void RegisterAddedChild(TestTreeNode addedChild)
    {
        if (!children.Contains(addedChild))
            children.Add(addedChild);
        addedChild.Attach(InternalBaseTree, this);
    }

    protected override bool PerformChildRemoval(TestTreeNode childToRemove)
    {
        return children.Remove(childToRemove);
    }

    protected override void AddChildrenToClonedInstance(TestTreeNode result)
    {
        foreach (var child in children)
            result.RegisterAddedChild(child.Clone());
    }

    public override IEnumerable<TestTreeNode> TraversePreOrderNodes()
    {
        yield return this;
        foreach (var child in children)
            foreach (var node in child.TraversePreOrderNodes())
                yield return node;
    }

    public override IEnumerable<TestTreeNode> TraversePostOrderNodes()
    {
        foreach (var child in children)
            foreach (var node in child.TraversePostOrderNodes())
                yield return node;
        yield return this;
    }

    public override IEnumerable<TestTreeNode> TraverseLevelOrderNodes()
    {
        Queue<TestTreeNode> nodes = new();
        nodes.Enqueue(this);
        while (nodes.Count > 0)
        {
            var node = nodes.Dequeue();
            yield return node;
            foreach (var child in node.children)
                nodes.Enqueue(child);
        }
    }
}
