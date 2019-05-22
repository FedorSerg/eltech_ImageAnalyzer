using System;
using System.Collections.Generic;

namespace ImageAnalyzer.SpecialClasses
{
    class Polygon
    {
        private int[] vertexNums;

        private Polygon(int[] vertexNums)
        {
            this.vertexNums = new int[vertexNums.Length];
            for (int i = 0; i < vertexNums.Length; i++)
            {
                this.vertexNums[i] = vertexNums[i];
            }
        }

        public int[] GetVertexNums()
        {
            return vertexNums;
        }

        public static Polygon[] GetPolygons(Vertex[] vertexArray, HashSet<Edge> edges)
        {
            List<Polygon> polygons = new List<Polygon>();
            List<Edge> edgeList = new List<Edge>();
            foreach (Edge edge in edges) edgeList.Add(edge);

            while (IsItStillRelevant(edgeList))
            {
                for (int i = 0; i < edgeList.Count; i++)
                {
                    for (int j = i + 1; j < edgeList.Count; j++)
                    {
                        if (edgeList[i].IsRelevant() && edgeList[j].IsRelevant())
                        {
                            if (edgeList[i].left == edgeList[j].left ||
                                edgeList[i].left == edgeList[j].right)
                            {
                                List<Vertex> baseVertexes = new List<Vertex>
                                {
                                    vertexArray[edgeList[i].right],
                                    vertexArray[edgeList[i].left]
                                };

                                int oneEnd = edgeList[i].left;
                                int vertexIdForHelp;
                                if (edgeList[j].left == oneEnd) vertexIdForHelp = edgeList[j].right;
                                else if (edgeList[j].right == oneEnd) vertexIdForHelp = edgeList[j].left;
                                else throw new Exception(" Wrong Edges ");

                                edgeList[i].SetItUsed();

                                List<int> baseVertexId = new List<int>();
                                baseVertexId.Add(edgeList[i].right);
                                baseVertexId.Add(edgeList[i].left);
                                for (int k = 0; k < vertexArray.Length; k++)
                                {
                                    if (baseVertexId.IndexOf(k) == -1 &&
                                        Plane(vertexArray[k], baseVertexes[0], baseVertexes[1], vertexArray[vertexIdForHelp]) &&
                                        (edgeList.Find(edge => edge.left == oneEnd && edge.right == k) != null ||
                                        edgeList.Find(edge => edge.right == oneEnd && edge.left == k) != null))
                                    {
                                        baseVertexes.Add(vertexArray[k]);
                                        baseVertexId.Add(vertexArray[k].GetId());

                                        foreach (Edge e in edgeList)
                                        {
                                            if (e.left == oneEnd && e.right == k)
                                            {
                                                oneEnd = e.right; e.SetItUsed();
                                            }
                                            else if (e.right == oneEnd && e.left == k)
                                            {
                                                oneEnd = e.left; e.SetItUsed();
                                            }
                                        }
                                        k = 0;
                                    }
                                }
                                Polygon newPolygon = new Polygon(baseVertexId.ToArray());
                                if (!polygons.Contains(newPolygon) /*&& no antiEdge*/)
                                {
                                    polygons.Add(newPolygon);
                                }
                            }
                        }
                    }
                }
            }
            return polygons.ToArray();
        }

        private static bool Plane(Vertex v, Vertex a, Vertex b, Vertex c)
        {
            return 0 ==
                (v.x - a.x) * ((b.y - a.y) * (c.z - a.z) - (c.y - a.y) * (b.z - a.z)) -
                (v.y - a.y) * ((b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z)) +
                (v.z - a.z) * ((b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y));
        }

        private static bool IsItStillRelevant(List<Edge> edgeList)
        {
            foreach (Edge e in edgeList)
            {
                if (!e.IsRelevant()) return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            Polygon polygon = obj as Polygon;
            if (polygon == null) { return false; }
            if (polygon.GetVertexNums().Length != vertexNums.Length) { return false; }

            for (int i = 0; i < vertexNums.Length; i++)
            {
                if (!Array.Exists(polygon.GetVertexNums(),
                    element => element == vertexNums[i]))
                {
                    return false;
                }
            }

            return true;
        }
        public override int GetHashCode()
        {
            int hashCode = -980702302;
            for (int i = 0; i < vertexNums.Length; i++)
            {
                hashCode += vertexNums[i].GetHashCode();
            }
            return hashCode;
        }
    }
}
