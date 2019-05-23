using System.IO;
using System.Collections.Generic;

namespace ImageAnalyzer.SpecialClasses
{
    class ObjectCreator
    {
        private string Name;
        private string path;
        private HashSet<Edge> edges;
        //private HashSet<Edge> antiEdges;
        private Vertex[] vertexes;

        public ObjectCreator(string Name, HashSet<Edge> edges, /*HashSet<Edge> antiEdges,*/ Vertex[] vertexes)
        {
            this.Name = Name;
            path = @".\" + Name + ".obj";

            this.vertexes = new Vertex[vertexes.Length];
            for (int i = 0; i < vertexes.Length; i++)
            {
                this.vertexes[i] = vertexes[i];
            }
            this.edges = edges;
            //this.antiEdges = antiEdges;
        }

        public void CreateObjFile()
        {
            Polygon[] polgons = Polygon.GetPolygons(vertexes, edges/*, antiEdges*/);

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("# object " + Name);

                    for (int i = 0; i < vertexes.Length; i++)
                    {
                        sw.WriteLine("v  " +
                            vertexes[i].x * vertexes[i].GetScale() + " " +
                            vertexes[i].y * vertexes[i].GetScale() + " " +
                            vertexes[i].z * vertexes[i].GetScale());
                    }

                    sw.WriteLine("g " + Name);
                    for (int i = 0; i < polgons.Length; i++)
                    {
                        string manyVertexes = "f ";
                        int[] vertexNums = polgons[i].GetVertexNums();
                        for (int j = 0; j < vertexNums.Length; j++)
                        {
                            manyVertexes += (vertexNums[j] + 1) + " ";
                        }
                        sw.WriteLine(manyVertexes);
                    }
                }
            }
        }
    }
}
