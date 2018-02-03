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

            bool didSplit = true;
            while (didSplit)
            {
                didSplit = false;
                foreach(var leaf in leafs)
                {
                    if(leaf.leftChild == null && leaf.rightChild == null)
                    {
                        if(leaf.width > maxLeafSize || leaf.width > maxLeafSize)
                        {
                            if(leaf.Split())
                            {
                                leafs.Add(leaf.leftChild);
                                leafs.Add(leaf.rightChild);
                                didSplit = true;
                            }
                        }
                    }
                }
            }
            return leafs;
        }
    }
}

