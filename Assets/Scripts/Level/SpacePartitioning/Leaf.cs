using UnityEngine;
using System.Collections;

namespace Game.SpacePartitioning
{
    public class Leaf
    {
        int minLeafSize = 6;

        public int x;
        public int y;
        public int width;
        public int height;

        public Leaf leftChild;
        public Leaf rightChild;

        public Leaf(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.width = w;
            this.height = h;
        }
        
        public bool Split()
        {
            if (leftChild != null || rightChild != null)
                return false; // we're already split! Abort!

            bool splitHorizontal = Random.value > 0.5f;
            if (width > height && width / height >= 1.25)
                splitHorizontal = false;
            else if (height > width && height / width >= 1.25)
                splitHorizontal = true;

            int max = (splitHorizontal ? height : width) - minLeafSize;
            if (max <= minLeafSize)
                return false;

            int split = Random.Range(minLeafSize, max + 1);

            if(splitHorizontal)
            {
                leftChild = new Leaf(x, y, width, split);
                rightChild = new Leaf(x, y, width, height - split);
            }
            else
            {
                leftChild = new Leaf(x, y, split, height);
                rightChild = new Leaf(x, y, width - split, height);
            }
            return true;
        }


    }
}

