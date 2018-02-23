using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.SpacePartitioning
{
    public class BinarySpacePartitioner : MonoBehaviour
    {
        static public List<Leaf> GenerateLeafs(int width, int height)
        {
            const int maxLeafSize = 20;

            List<Leaf> leafs = new List<Leaf>();

            Leaf root = new Leaf(0, 0, width, height);
            leafs.Add(root);

            int result;
            do {
                result = 0;
                List<Leaf> splitLeafs = new List<Leaf>();
                List<Leaf> newLeafs = new List<Leaf>();
                foreach(var leaf in leafs)
                {
                    if(leaf.Split())
                    {
                        splitLeafs.Add(leaf);
                        newLeafs.Add(leaf.leftChild);
                        newLeafs.Add(leaf.rightChild);
                        result++;
                    }
                }
                foreach( var leaf in newLeafs)
                {
                    leafs.Add(leaf);
                }
                foreach (var leaf in splitLeafs)
                {
                    leafs.Remove(leaf);
                }
            }
            while (result > 0);
            
            return leafs;
        }
    }
}
