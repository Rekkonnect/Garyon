using Garyon.DataStructures;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Garyon.Extensions;
using System;

namespace Garyon.Tests.DataStructures
{
    [Parallelizable(ParallelScope.Children)]
    public class TreeTests
    {
        private static Tree<char> testTree;

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

        [OneTimeSetUp]
        public static void InitializeTestTree()
        {
            var nodes = new Dictionary<char, TreeNode<char>>(9);
            for (char c = 'A'; c <= 'I'; c++)
                nodes.Add(c, new TreeNode<char>(c));

            nodes['F'].AddChildren(nodes['B'], nodes['G']);
            nodes['B'].AddChildren(nodes['A'], nodes['D']);
            nodes['D'].AddChildren(nodes['C'], nodes['E']);
            nodes['G'].AddChild(nodes['I']);
            nodes['I'].AddChild(nodes['H']);

            testTree = new Tree<char>(nodes['F']);
        }

        #region Properties
        [Test]
        public void CountTest()
        {
            Assert.AreEqual(9, testTree.Count);
        }
        [Test]
        public void HeightTest()
        {
            Assert.AreEqual(4, testTree.Height);
        }
        [Test]
        public void BreadthTest()
        {
            Assert.AreEqual(4, testTree.Breadth);
        }
        #endregion

        #region View
        [Test]
        public void GetTreeView()
        {
            string expected4 =
@"
F
├---B
|   ├---A
|   └---D
|       ├---C
|       └---E
└---G
    └---I
        └---H
".Trim();
            
            string expected7 =
                @"
F
├------B
|      ├------A
|      └------D
|             ├------C
|             └------E
└------G
       └------I
              └------H
".Trim();

            Assert.AreEqual(expected4, testTree.GetTreeView());
            Assert.AreEqual(expected7, testTree.GetTreeView(7));
        }
        #endregion

        #region Collection
        [Test]
        public void ContainsTest()
        {
            for (char c = 'A'; c <= 'I'; c++)
                Assert.IsTrue(testTree.Contains(c));
            
            for (char c = 'J'; c <= 'Z'; c++)
                Assert.IsFalse(testTree.Contains(c));
        }
        #endregion
        
        #region Children Handling
        private void AssertContainedAgainstBaseTree(Tree<char> tree, TreeNode<char> child, char c, bool contained)
        {
            if (child is not null)
                Assert.AreEqual(contained, child.Contains(c));
            Assert.AreEqual(contained, tree.Contains(c));
            Assert.AreEqual(!contained, testTree.Contains(c));
        }

        [Test]
        public void GetNodeTest()
        {
            Assert.AreEqual(testTree.Root, testTree.GetNode('F'));
        }
        
        [Test]
        public void AddChildTest()
        {
            var tree = new Tree<char>(testTree);
            var child = tree.GetNode('A');
            tree.AddChild(child, 'K');
            AssertContainedAgainstBaseTree(tree, child, 'K', true);
            Assert.AreEqual(testTree.Count + 1, tree.Count);
        }
        [Test]
        public void AddChildrenTest()
        {
            var tree = new Tree<char>(testTree);
            var child = tree.GetNode('B');
            char[] values = { 'K', 'T', 'Y' };
            child.AddChildren(values);
            foreach (var v in values)
                AssertContainedAgainstBaseTree(tree, tree.Root, v, true);
            Assert.AreEqual(testTree.Count + 3, tree.Count);
        }
        [Test]
        public void RemoveChildrenTest()
        {
            var tree = new Tree<char>(testTree);
            tree.RemoveChildren('B', new[] { 'A', 'D' });
            Assert.IsTrue(tree.Contains('B'));
            AssertContainedAgainstBaseTree(tree, tree.Root, 'A', false);
            AssertContainedAgainstBaseTree(tree, tree.Root, 'D', false);
            AssertContainedAgainstBaseTree(tree, tree.Root, 'C', false);
            Assert.AreEqual(testTree.Count - 4, tree.Count);

            tree.RemoveRootChildren(new[] { 'B', 'G' });
            Assert.IsTrue(tree.Contains('F'));
            AssertContainedAgainstBaseTree(tree, tree.Root, 'B', false);
            AssertContainedAgainstBaseTree(tree, tree.Root, 'G', false);
            Assert.AreEqual(1, tree.Count);
        }
        [Test]
        public void RemoveNodeTest()
        {
            var tree = new Tree<char>(testTree);
            tree.RemoveNode('A');
            AssertContainedAgainstBaseTree(tree, tree.Root, 'A', false);
            Assert.AreEqual(testTree.Count - 1, tree.Count);

            var root = tree.Root;
            tree.Remove(root.Value);
            AssertContainedAgainstBaseTree(tree, tree.Root, root.Value, false);
            Assert.AreEqual(0, tree.Count);
        }
        [Test]
        public void RemoveNodesTest()
        {
            var tree = new Tree<char>(testTree);
            char[] values = { 'A', 'C', 'F', 'G' };
            tree.RemoveNodes(values);
            foreach (var v in values)
                AssertContainedAgainstBaseTree(tree, tree.Root, v, false);
            Assert.AreEqual(0, tree.Count);
        }

        [Test]
        public void SetChildrenTest()
        {
            var tree = new Tree<char>(testTree);
            char[] oldValues = tree.GetNode('A').Children.Select(c => c.Value).ToArray();
            char[] values = { 'X', 'Y', 'Z' };
            tree.SetChildren('A', values);
            foreach (var v in values)
                AssertContainedAgainstBaseTree(tree, tree.Root, v, true);
            foreach (var v in oldValues)
                AssertContainedAgainstBaseTree(tree, tree.Root, v, false);
            Assert.AreEqual(testTree.Count + 3, tree.Count);
        }
        [Test]
        public void ClearTest()
        {
            var tree = new Tree<char>(testTree);
            char[] oldValues = tree.TraversePreOrderNodes().Select(c => c.Value).ToArray();
            tree.Clear();

            Assert.AreEqual(0, tree.Count);
            foreach (var v in oldValues)
                AssertContainedAgainstBaseTree(tree, tree.Root, v, false);
        }
        [Test]
        public void ClearChildrenTest()
        {
            var tree = new Tree<char>(testTree);
            var node = tree.GetNode('G');
            char[] oldValues = node.Children.Select(c => c.Value).ToArray();

            tree.ClearChildren(node);
            Assert.AreEqual(0, node.ChildrenCount);
            foreach (var v in oldValues)
                AssertContainedAgainstBaseTree(tree, tree.Root, v, false);
        }
        #endregion

        [Test]
        public void TreeConstructionTest()
        {
            int originalCount = testTree.Count;
            
            var tree = new Tree<char>(testTree);

            int newCount = testTree.Count;
            Assert.AreEqual(originalCount, newCount);
            
            ValidateTreeNodes(tree);
            ValidateTreeNodes(testTree);
            
            // Since a brand new instance was initialized, ensure their roots are completely independent
            tree.Root = new TreeNode<char>();
            Assert.AreNotEqual(tree.Root, testTree.Root);
        }

        [Test]
        public void RootTest()
        {
            var tree = new Tree<char>(testTree);

            // I would love to have a way to ensure the cached count is not changed
            // without exposing the field through a property
            tree.Root = tree.Root;
            Assert.AreEqual(tree.Count, testTree.Count);

            Assert.Throws<InvalidOperationException>(() => tree.Root = tree.Root.Children[0].Children[0]);

            var newRoot = tree.Root.Children[0].Children[0];
            tree.RemoveNode(newRoot);
            tree.Root = newRoot;
            Assert.AreNotEqual(tree.Count, testTree.Count);

            tree.Root = new TreeNode<char>();
            Assert.AreNotEqual(tree.Count, testTree.Count);
        }

        private static void ValidateTreeNodes<T>(Tree<T> tree)
        {
            foreach (var n in tree.TraversePreOrderNodes())
            {
                Assert.AreEqual(tree, n.BaseTree);
                foreach (var c in n.Children)
                    Assert.AreEqual(n, c.Parent);
            }
        }
        
        #region Traversal
        [Test]
        public void PreOrderTraversalTest()
        {
            char[] expected = { 'F', 'B', 'A', 'D', 'C', 'E', 'G', 'I', 'H' };
            TraversalTest(expected, testTree.TraversePreOrder());
            TraversalTest(expected, testTree.TraversePreOrderNodes().Select(n => n.Value).ToArray());
        }
        [Test]
        public void PostOrderTraversalTest()
        {
            char[] expected = { 'A', 'C', 'E', 'D', 'B', 'H', 'I', 'G', 'F' };
            TraversalTest(expected, testTree.TraversePostOrder());
            TraversalTest(expected, testTree.TraversePostOrderNodes().Select(n => n.Value).ToArray());
        }
        [Test]
        public void LevelOrderTraversalTest()
        {
            char[] expected = { 'F', 'B', 'G', 'A', 'D', 'I', 'C', 'E', 'H' };
            TraversalTest(expected, testTree.TraverseLevelOrder());
            TraversalTest(expected, testTree.TraverseLevelOrderNodes().Select(n => n.Value).ToArray());
        }

        private void TraversalTest(char[] expected, IEnumerable<char> actual)
        {
            var traversal = actual.ToArray();
            Assert.AreEqual(expected.Length, traversal.Length);
            for (int i = 0; i < expected.Length; i++)
                Assert.AreEqual(expected[i], traversal[i], i.ToString());
        }
        #endregion

        #region Equality
        [Test]
        public void EqualsTest()
        {
            var newTree = new Tree<char>(testTree);
            Assert.AreEqual(newTree, testTree);
        }
        #endregion
    }
}
