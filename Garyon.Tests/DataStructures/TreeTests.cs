using Garyon.DataStructures.Trees;
using Garyon.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUnit.Assertions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace Garyon.Tests.DataStructures;

public class TreeTests
{
    private static readonly Tree<char> testTree = CreateTestTree();

    // The test tree is from an example that is showcased on this Wikipedia article
    // https://en.wikipedia.org/wiki/Tree_traversal

    /*
        F
        ├---B
        |   ├---A
        |   └---D
        |       ├---C
        |       └---E
        └---G
            └---I
                └---H
    */

    private static Tree<char> CreateTestTree()
    {
        var nodes = new Dictionary<char, TreeNode<char>>(9);
        for (char c = 'A'; c <= 'I'; c++)
            nodes.Add(c, new TreeNode<char>(c));

        nodes['F'].AddChildren(nodes['B'], nodes['G']);
        nodes['B'].AddChildren(nodes['A'], nodes['D']);
        nodes['D'].AddChildren(nodes['C'], nodes['E']);
        nodes['G'].AddChild(nodes['I']);
        nodes['I'].AddChild(nodes['H']);

        return new Tree<char>(nodes['F']);
    }

    #region Properties
    [Test]
    public async Task CountTest()
    {
        await Assert.That(testTree.Count).IsEqualTo(9);
    }
    [Test]
    public async Task HeightTest()
    {
        await Assert.That(testTree.Height).IsEqualTo(4);
    }
    [Test]
    public async Task BreadthTest()
    {
        await Assert.That(testTree.Breadth).IsEqualTo(4);
    }
    #endregion

    #region View
    [Test]
    public async Task GetTreeView()
    {
        const string expected4 =
            """
            F
            ├---B
            |   ├---A
            |   └---D
            |       ├---C
            |       └---E
            └---G
                └---I
                    └---H
            """;

        const string expected7 =
            """
            F
            ├------B
            |      ├------A
            |      └------D
            |             ├------C
            |             └------E
            └------G
                   └------I
                          └------H
            """;

        await Assert.That(testTree.GetTreeView()).IsEqualTo(expected4);
        await Assert.That(testTree.GetTreeView(7)).IsEqualTo(expected7);
    }
    #endregion

    #region Collection
    [Test]
    public async Task ContainsTest()
    {
        for (char c = 'A'; c <= 'I'; c++)
            await Assert.That(testTree).Contains(c);

        for (char c = 'J'; c <= 'Z'; c++)
            await Assert.That(testTree).DoesNotContain(c);
    }
    #endregion

    #region Children Handling
    private static async Task AssertContainedAgainstBaseTree(
        Tree<char> tree,
        TreeNode<char> child,
        char c,
        bool contained)
    {
        if (child is not null)
        {
            await Assert.That(child.Contains(c)).IsEqualTo(contained);
        }

        await Assert.That(tree.Contains(c)).IsEqualTo(contained);
        await Assert.That(testTree.Contains(c)).IsEqualTo(!contained);
    }

    [Test]
    public async Task GetNodeTest()
    {
        await Assert.That(testTree.GetNode('F')).IsEqualTo(testTree.Root);
    }

    [Test]
    public async Task AddChildTest()
    {
        var tree = new Tree<char>(testTree);
        var child = tree.GetNode('A');
        tree.AddChild(child, 'K');
        await AssertContainedAgainstBaseTree(tree, child, 'K', true);
        await Assert.That(tree.Count).IsEqualTo(testTree.Count + 1);
    }
    [Test]
    public async Task AddChildrenTest()
    {
        var tree = new Tree<char>(testTree);
        var child = tree.GetNode('B');
        char[] values = ['K', 'T', 'Y'];
        child.AddChildren(values);
        foreach (var v in values)
        {
            await AssertContainedAgainstBaseTree(tree, tree.Root, v, true);
        }

        await Assert.That(tree.Count).IsEqualTo(testTree.Count + 3);
    }
    [Test]
    public async Task RemoveChildrenTest()
    {
        var tree = new Tree<char>(testTree);
        tree.RemoveChildren('B', ['A', 'D']);
        await Assert.That(tree).Contains('B');
        await AssertContainedAgainstBaseTree(tree, tree.Root, 'A', false);
        await AssertContainedAgainstBaseTree(tree, tree.Root, 'D', false);
        await AssertContainedAgainstBaseTree(tree, tree.Root, 'C', false);
        await Assert.That(tree.Count).IsEqualTo(testTree.Count - 4);

        tree.RemoveRootChildren(['B', 'G']);
        await Assert.That(tree).Contains('F');
        await AssertContainedAgainstBaseTree(tree, tree.Root, 'B', false);
        await AssertContainedAgainstBaseTree(tree, tree.Root, 'G', false);
        await Assert.That(tree.Count).IsEqualTo(1);
    }
    [Test]
    public async Task RemoveNodeTest()
    {
        var tree = new Tree<char>(testTree);
        tree.RemoveNode('A');
        await AssertContainedAgainstBaseTree(tree, tree.Root, 'A', false);
        await Assert.That(tree.Count).IsEqualTo(testTree.Count - 1);

        var root = tree.Root;
        tree.Remove(root.Value);
        await AssertContainedAgainstBaseTree(tree, tree.Root, root.Value, false);
        await Assert.That(tree).IsEmpty();
    }
    [Test]
    public async Task RemoveNodesTest()
    {
        var tree = new Tree<char>(testTree);
        char[] values = ['A', 'C', 'F', 'G'];
        tree.RemoveNodes(values);
        foreach (var v in values)
        {
            await AssertContainedAgainstBaseTree(tree, tree.Root, v, false);
        }

        await Assert.That(tree).IsEmpty();
    }

    [Test]
    public async Task SetChildrenTest()
    {
        var tree = new Tree<char>(testTree);
        char[] oldValues = tree.GetNode('A').Children.Select(c => c.Value).ToArray();
        char[] values = ['X', 'Y', 'Z'];
        tree.SetChildren('A', values);
        foreach (var v in values)
        {
            await AssertContainedAgainstBaseTree(tree, tree.Root, v, true);
        }

        foreach (var v in oldValues)
        {
            await AssertContainedAgainstBaseTree(tree, tree.Root, v, false);
        }

        await Assert.That(tree.Count).IsEqualTo(testTree.Count + 3);
    }
    [Test]
    public async Task ClearTest()
    {
        var tree = new Tree<char>(testTree);
        char[] oldValues = tree.TraversePreOrderNodes().Select(c => c.Value).ToArray();
        tree.Clear();

        await Assert.That(tree).IsEmpty();
        foreach (var v in oldValues)
        {
            await AssertContainedAgainstBaseTree(tree, tree.Root, v, false);
        }
    }
    [Test]
    public async Task ClearChildrenTest()
    {
        var tree = new Tree<char>(testTree);
        var node = tree.GetNode('G');
        char[] oldValues = node.Children.Select(c => c.Value).ToArray();

        tree.ClearChildren(node);
        await Assert.That(node.ChildrenCount).IsZero();
        foreach (var v in oldValues)
        {
            await AssertContainedAgainstBaseTree(tree, tree.Root, v, false);
        }
    }
    #endregion

    [Test]
    public async Task TreeConstructionTest()
    {
        int originalCount = testTree.Count;

        var tree = new Tree<char>(testTree);

        int newCount = testTree.Count;
        await Assert.That(newCount).IsEqualTo(originalCount);

        await ValidateTreeNodes(tree);
        await ValidateTreeNodes(testTree);

        // Since a brand new instance was initialized, ensure their roots are completely independent
        tree.Root = new TreeNode<char>();
        await Assert.That(testTree.Root).IsNotEqualTo(tree.Root);
    }

    [Test]
    public async Task RootTest()
    {
        var tree = new Tree<char>(testTree);

        // I would love to have a way to ensure the cached count is not changed
        // without exposing the field through a property
        tree.Root = tree.Root;
        await Assert.That(testTree.Count).IsEqualTo(tree.Count);

        Assert.Throws<InvalidOperationException>(() =>
        {
            tree.Root = tree.Root.Children[0].Children[0];
        });

        var newRoot = tree.Root.Children[0].Children[0];
        tree.RemoveNode(newRoot);
        tree.Root = newRoot;
        await Assert.That(testTree.Count).IsNotEqualTo(tree.Count);

        tree.Root = new TreeNode<char>();
        await Assert.That(testTree.Count).IsNotEqualTo(tree.Count);
    }

    private async static Task ValidateTreeNodes<T>(Tree<T> tree)
    {
        foreach (var n in tree.TraversePreOrderNodes())
        {
            await Assert.That(n.BaseTree).IsEqualTo(tree);
            foreach (var c in n.Children)
                await Assert.That(c.Parent).IsEqualTo(n);
        }
    }

    #region Traversal
    [Test]
    public async Task PreOrderTraversalTestAsync()
    {
        char[] expected = ['F', 'B', 'A', 'D', 'C', 'E', 'G', 'I', 'H'];
        await TraversalTest(expected, testTree.TraversePreOrder());
        await TraversalTest(expected, testTree.TraversePreOrderNodes().Select(n => n.Value).ToArray());
    }
    [Test]
    public async Task PostOrderTraversalTestAsync()
    {
        char[] expected = ['A', 'C', 'E', 'D', 'B', 'H', 'I', 'G', 'F'];
        await TraversalTest(expected, testTree.TraversePostOrder());
        await TraversalTest(expected, testTree.TraversePostOrderNodes().Select(n => n.Value).ToArray());
    }
    [Test]
    public async Task LevelOrderTraversalTestAsync()
    {
        char[] expected = ['F', 'B', 'G', 'A', 'D', 'I', 'C', 'E', 'H'];
        await TraversalTest(expected, testTree.TraverseLevelOrder());
        await TraversalTest(expected, testTree.TraverseLevelOrderNodes().Select(n => n.Value).ToArray());
    }

    private static async Task TraversalTest(char[] expected, IEnumerable<char> actual)
    {
        var traversal = actual.ToArray();
        await Assert.That(traversal.Count).IsEqualTo(expected.Length);
        for (int i = 0; i < expected.Length; i++)
            await Assert.That(traversal[i]).IsEqualTo(expected[i]).Because(i.ToString());
    }
    #endregion

    #region Equality
    [Test]
    public async Task EqualsTest()
    {
        var newTree = new Tree<char>(testTree);
        await Assert.That(testTree).IsEqualTo(newTree);
    }
    #endregion
}
