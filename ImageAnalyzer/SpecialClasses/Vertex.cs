using System;
using System.Collections.Generic;

namespace ImageAnalyzer.SpecialClasses
{
    public class Vertex
    {
        public int? x { get; set; }
        public int? y { get; set; }
        public int? z { get; set; }

        private int id;

        private List<VertexLocalCoords> locals = new List<VertexLocalCoords>(3);

        private double scale = 1;

        public Vertex(int? x = null, int? y = null, int? z = null)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void SetLocalCoords(List<VertexLocalCoords> coords)
        {
            bool isNew = true;
            foreach (VertexLocalCoords coord in coords)
            {
                foreach (VertexLocalCoords local in locals)
                {
                    if (coord.GetImageType() == local.GetImageType())
                    {
                        isNew = false;
                    }
                }
                if (isNew)
                {
                    locals.Add(coord);
                }
            }
        }
        public List<VertexLocalCoords> GetLocalCoords()
        {
            return locals;
        }

        public void SetId(int id) { this.id = id; }
        public int GetId() { return id; }

        public void SetScale(double scale)
        {
            this.scale = scale;
        }

        public double GetScale()
        {
            return scale;
        }

        public static Vertex[] CreateVertexArray(Vertex[] mainArray,
            Vertex[] leftArray, Vertex[] topArray)
        {
            List<Vertex> vertexList = new List<Vertex>();
            for (int i = 0; i < mainArray.Length; i++)
            {
                for (int j = 0; j < leftArray.Length; j++)
                {
                    for (int k = 0; k < topArray.Length; k++)
                    {
                        if (topArray[k].x == leftArray[j].x &&
                            mainArray[i].y == topArray[k].y &&
                            mainArray[i].z == leftArray[j].z)
                        {
                            Vertex newOne = mainArray[i] + leftArray[j];
                            newOne += topArray[k];
                            vertexList.Add(newOne);
                        }
                    }
                }
            }
            return vertexList.ToArray();
        }

        public static Vertex operator +(Vertex left, Vertex right)
        {
            if (left.x == null && right.x == null ||
                left.y == null && right.y == null ||
                left.z == null && right.z == null)
                throw new Exception("Exceptoin <null + null> in Vertex uniting ");

            Vertex result = new Vertex();
            result.x = left.x != null ? left.x : right.x;
            result.y = left.y != null ? left.y : right.y;
            result.z = left.z != null ? left.z : right.z;

            result.SetLocalCoords(left.GetLocalCoords());
            result.SetLocalCoords(right.GetLocalCoords());

            return result;
        }

        public static bool operator ==(Vertex left, Vertex right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vertex left, Vertex right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            var vertex = obj as Vertex;
            return vertex != null &&
                   EqualityComparer<int?>.Default.Equals(x, vertex.x) &&
                   EqualityComparer<int?>.Default.Equals(y, vertex.y) &&
                   EqualityComparer<int?>.Default.Equals(z, vertex.z);
        }

        public override int GetHashCode()
        {
            var hashCode = 373119288;
            hashCode = hashCode * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(x);
            hashCode = hashCode * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(y);
            hashCode = hashCode * -1521134295 + EqualityComparer<double?>.Default.GetHashCode(z);
            return hashCode;
        }
    }
}