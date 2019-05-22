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
            Edge[] edgeArray = new Edge[edges.Count];
            edges.CopyTo(edgeArray, 0);

            while (AreEdgesLeft(edgeArray))
            {
                for (int i = 0; i < edgeArray.Length; i++)
                {
                    for (int j = i + 1; j < edgeArray.Length; j++)
                    {
                        if (edgeArray[i] != null && edgeArray[j] != null)
                        {
                            HashSet<Vertex> vertexSet = new HashSet<Vertex>();
                            vertexSet.Add(vertexArray[edgeArray[i].left]);
                            vertexSet.Add(vertexArray[edgeArray[i].right]);
                            vertexSet.Add(vertexArray[edgeArray[j].left]);
                            vertexSet.Add(vertexArray[edgeArray[j].right]);

                            if (vertexSet.Count == 3)
                            {
                                List<Vertex> baseVertexes = new List<Vertex>();
                                foreach (Vertex v in vertexSet) { baseVertexes.Add(v); }

                                for (int k = 0; k < vertexArray.Length; k++)
                                {
                                    if (k != baseVertexes[0].GetId() &&
                                        k != baseVertexes[1].GetId() &&
                                        k != baseVertexes[2].GetId() &&
                                        Plane(vertexArray[k], baseVertexes[0],
                                        baseVertexes[1], baseVertexes[2]))
                                    {
                                        baseVertexes.Add(vertexArray[k]);
                                    }
                                }

                                baseVertexes.Sort((x, y) => x.GetId().CompareTo(y.GetId()));

                                int[] baseVertexId = new int[baseVertexes.Count];
                                for (int iter = 0; iter < baseVertexId.Length; iter++)
                                {
                                    baseVertexId[iter] = baseVertexes[iter].GetId();
                                }
                                Polygon newPolygon = new Polygon(baseVertexId);
                                if (!polygons.Contains(newPolygon))
                                {
                                    polygons.Add(newPolygon);
                                }

                                for (int iter = 0; iter < edgeArray.Length; iter++)
                                {
                                    if (edgeArray[iter] != null)
                                    {
                                        if (Array.Exists(baseVertexId, element => element == edgeArray[iter].left) &&
                                            Array.Exists(baseVertexId, element => element == edgeArray[iter].right))
                                        {
                                            edgeArray[iter].SetItUsed();
                                            if (!edgeArray[iter].IsRelevant()) edgeArray[iter] = null;
                                        }
                                    }
                                    GC.Collect();
                                }
                            }
                        }
                    }
                }
            }
            return polygons.ToArray();
        }

        private static bool AreEdgesLeft(Edge[] e)
        {
            for (int i = 0; i < e.Length; i++)
            {
                if (e[i] != null)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool Plane(Vertex v, Vertex a, Vertex b, Vertex c)
        {
            return 0 ==
                (v.x - a.x) * ((b.y - a.y) * (c.z - a.z) - (c.y - a.y) * (b.z - a.z)) -
                (v.y - a.y) * ((b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z)) +
                (v.z - a.z) * ((b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y));
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
