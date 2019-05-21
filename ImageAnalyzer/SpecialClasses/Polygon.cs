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
                            HashSet<int> vertexSet = new HashSet<int>();
                            vertexSet.Add(edgeArray[i].left);
                            vertexSet.Add(edgeArray[i].right);
                            vertexSet.Add(edgeArray[j].left);
                            vertexSet.Add(edgeArray[j].right);

                            if (vertexSet.Count == 3)
                            {
                                int[] vertexes = new int[3];
                                vertexSet.CopyTo(vertexes);
                                for (int k = 0; k < vertexArray.Length; k++)
                                {
                                    if (Array.IndexOf(vertexes, k) == -1 &&
                                        Plane(vertexArray[k],
                                        vertexArray[vertexes[0]],
                                        vertexArray[vertexes[1]],
                                        vertexArray[vertexes[2]]))
                                    {
                                        int[] jfh = new int[vertexes.Length + 1];
                                        vertexes.CopyTo(jfh, 0);
                                        vertexes = jfh; jfh = null;
                                        vertexes[vertexes.Length - 1] = k;
                                    }
                                }
                                polygons.Add(new Polygon(vertexes));

                                edgeArray[i].SetItUsed();
                                edgeArray[j].SetItUsed();

                                if (!edgeArray[i].IsRelevant()) edgeArray[i] = null;
                                if (!edgeArray[j].IsRelevant()) edgeArray[j] = null;
                            }
                        }
                    }
                }
            }
            return polygons.ToArray();
        }

        private static bool AreEdgesLeft(Edge[] e)
        {
            bool result = false;
            for (int i = 0; i < e.Length; i++)
            {
                if (e[i] != null)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private static bool Plane(Vertex v, Vertex a, Vertex b, Vertex c)
        {
            return 0 ==
                (v.x - a.x) * ((b.y - a.y) * (c.z - a.z) - (c.y - a.y) * (b.z - a.z)) -
                (v.y - a.y) * ((b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z)) +
                (v.z - a.z) * ((b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y));
        }
    }
}
