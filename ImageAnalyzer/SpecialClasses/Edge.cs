using System;

namespace ImageAnalyzer.SpecialClasses
{
    class Edge
    {
        public int left { get; set; }
        public int right { get; set; }
        private byte polygonCounter;

        public Edge(int left, int right)
        {
            if (left == right)
            {
                throw new Exception("A vertex instead of an edge ");
            }
            this.left = left < right ? left : right;
            this.right = right > left ? right : left;
            polygonCounter = 0;
        }

        public void SetItUsed()
        {
            polygonCounter++;
        }
        public bool IsRelevant()
        {
            return polygonCounter < 2 ? true : false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj is Edge edge &&
                   left == edge.left &&
                   right == edge.right;
        }
        public override int GetHashCode()
        {
            var hashCode = -124503083;
            hashCode = hashCode * -1521134295 + left.GetHashCode();
            hashCode = hashCode * -1521134295 + right.GetHashCode();
            return hashCode;
        }
    }
}