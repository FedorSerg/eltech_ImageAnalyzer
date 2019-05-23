using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ImageAnalyzer.SpecialClasses
{
    public class ImageHandler
    {
        private static Vertex[][] vertexiesFromImages = new Vertex[3][];
        private static Bitmap[] bitmaps = new Bitmap[3];
        private static HashSet<Edge> edges;
        private static HashSet<Edge> antiEdges;
        public enum ImageType { MAIN, LEFT, TOP }

        private ImageHandler() { }

        public static void OpenImages(ImageType type, PictureBox pb)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "png (*.png)|*.png|jpg (*.jpg)|*.jpg|bmp (*.bmp)|*.bmp"
            };

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName.Length > 0)
            {
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Image = Image.FromFile(ofd.FileName);
                Bitmap bm = new Bitmap(Image.FromFile(ofd.FileName));

                switch (type)
                {
                    case ImageType.MAIN:
                        vertexiesFromImages[0] = GetVertexFromImage(bm, type);
                        bitmaps[0] = bm;
                        break;
                    case ImageType.LEFT:
                        vertexiesFromImages[1] = GetVertexFromImage(bm, type);
                        bitmaps[1] = bm;
                        break;
                    case ImageType.TOP:
                        vertexiesFromImages[2] = GetVertexFromImage(bm, type);
                        bitmaps[2] = bm;
                        break;
                }
            }
        }

        public static void BuildAnObject(string name)
        {
            if ((vertexiesFromImages[0] != null) &&
                (vertexiesFromImages[1] != null) &&
                (vertexiesFromImages[2] != null))
            {
                Vertex[] vertexArray = Vertex.CreateVertexArray(
                    vertexiesFromImages[0],
                    vertexiesFromImages[1],
                    vertexiesFromImages[2]);

                new ObjectCreator(name, GetEdgesFromImage(vertexArray),
                    /*GetAntiEdgesFromImage(vertexArray), */vertexArray).CreateObjFile();
            }
            else
            {
                throw new Exception("Some images weren't handled ");
            }
        }

        // - - - - - - - - - -
        private static double GetTheScale(Bitmap bm)
        {
            string code = "";
            int interval = 0;

            int i = 0;
            while (bm.GetPixel(i, bm.Height - 2) != ColorInfo.bgColor)
            {
                interval++;
                i++;
            }
            interval = interval == 0 ? 1 : interval;

            i = 0;
            for (int j = 0; j < 2; j++)
            {
                while (bm.GetPixel(i, bm.Height - 1) != ColorInfo.bgColor)
                {
                    if (bm.GetPixel(i, bm.Height - 1) == ColorInfo.lineColor)
                    {
                        code += "1";
                    }
                    else if (bm.GetPixel(i, bm.Height - 1) == ColorInfo.fillColor)
                    {
                        code += "0";
                    }
                    i++;
                }
                code += ".";
            }
            code = code.Substring(0, code.Length - 1);
            //return DigitConverter.Convert2to10(code) / interval;
            return 1;
        }

        private static Vertex[] GetVertexFromImage(Bitmap bm, ImageType type)
        {
            List<Vertex> vertexList = new List<Vertex>();
            Vertex v = null;
            int zeroWidth = 0, zeroHeight = 0;

            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    if (bm.GetPixel(i, j) == ColorInfo.zeroVertexColor)
                    {
                        switch (type)
                        {
                            case ImageType.MAIN:
                                v = new Vertex(y: 0, z: 0);
                                break;

                            case ImageType.LEFT:
                                v = new Vertex(x: 0, z: 0);
                                break;

                            case ImageType.TOP:
                                v = new Vertex(x: 0, y: 0);
                                break;
                        }
                        zeroWidth = i;
                        zeroHeight = j;

                        List<VertexLocalCoords> coords = new List<VertexLocalCoords>();
                        coords.Add(new VertexLocalCoords(type, i, j));
                        v.SetLocalCoords(coords);
                        v.SetScale(GetTheScale(bm));
                        vertexList.Add(v);
                    }
                }
            }

            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    if (bm.GetPixel(i, j) == ColorInfo.vertexColor ||
                        bm.GetPixel(i, j) == ColorInfo.invisVertexColor)
                    {
                        switch (type)
                        {
                            case ImageType.MAIN:
                                v = new Vertex(
                                    y: i - zeroWidth + ((i - zeroWidth != 0) ? (i - zeroWidth > 0 ? 1 : -1) : 0),
                                    z: -j + zeroHeight + ((-j + zeroHeight != 0) ? (-j + zeroHeight > 0 ? 1 : -1) : 0));
                                break;
                            case ImageType.LEFT:
                                v = new Vertex(
                                    x: i - zeroWidth + ((i - zeroWidth != 0) ? (i - zeroWidth > 0 ? 1 : -1) : 0),
                                    z: -j + zeroHeight + ((-j + zeroHeight != 0) ? (-j + zeroHeight > 0 ? 1 : -1) : 0));
                                break;
                            case ImageType.TOP:
                                v = new Vertex(
                                    y: i - zeroWidth + ((i - zeroWidth != 0) ? (i - zeroWidth > 0 ? 1 : -1) : 0),
                                    x: j - zeroHeight + ((j - zeroHeight != 0) ? (j - zeroHeight > 0 ? 1 : -1) : 0));
                                break;
                        }
                        List<VertexLocalCoords> coords = new List<VertexLocalCoords>();
                        coords.Add(new VertexLocalCoords(type, i, j));
                        v.SetLocalCoords(coords);
                        v.SetScale(GetTheScale(bm));
                        vertexList.Add(v);
                    }
                }
            }

            return vertexList.ToArray();
        }

        private static HashSet<Edge> GetEdgesFromImage(Vertex[] vertexes)
        {
            edges = new HashSet<Edge>();
            for (int i = 0; i < vertexes.Length; i++)
            {
                for (int j = i + 1; j < vertexes.Length; j++)
                {
                    if (AreLinked(vertexes[i], vertexes[j]))
                    {
                        Edge e = new Edge(i, j);
                        edges.Add(e);
                    }
                }
            }
            return edges;
        }
        private static HashSet<Edge> GetAntiEdgesFromImage(Vertex[] vertexes)
        {
            antiEdges = new HashSet<Edge>();
            for (int i = 0; i < vertexes.Length; i++)
            {
                for (int j = i + 1; j < vertexes.Length; j++)
                {
                    if (AreAntiLinked(vertexes[i], vertexes[j]))
                    {
                        Edge e = new Edge(i, j);
                        antiEdges.Add(e);
                    }
                }
            }
            return antiEdges;
        }

        private static bool AreLinked(Vertex one, Vertex two)
        {
            bool areLinked = false;
            bool shouldStop = false;

            for (int type = 0; type <= 2 && !shouldStop; type++)
            {
                int X1 = one.GetLocalCoords()[type].GetI();
                int X2 = two.GetLocalCoords()[type].GetI();
                int Y1 = one.GetLocalCoords()[type].GetJ();
                int Y2 = two.GetLocalCoords()[type].GetJ();

                int minX = X1 < X2 ? X1 : X2;
                int maxX = X2 > X1 ? X2 : X1;

                for (int i = minX; i < maxX; i++)
                {
                    int j = Line(i, X1, Y1, X2, Y2);
                    if (IsThereLineInRadius(type, i, j))
                    {
                        areLinked = true;
                    }
                    else
                    {
                        areLinked = false;
                        shouldStop = true;
                        break;
                    }
                }

                int minY = Y1 < Y2 ? Y1 : Y2;
                int maxY = Y2 > Y1 ? Y2 : Y1;

                for (int i = minY; i < maxY; i++)
                {
                    int j = Line(i, Y1, X1, Y2, X2);
                    if (IsThereLineInRadius(type, j, i))
                    {
                        areLinked = true;
                    }
                    else
                    {
                        areLinked = false;
                        shouldStop = true;
                        break;
                    }
                }
            }
            return areLinked;
        }
        private static bool AreAntiLinked(Vertex one, Vertex two)
        {
            bool areAntiLinked = true;
            bool shouldStop = false;

            for (int type = 0; type <= 2 && !shouldStop; type++)
            {
                int X1 = one.GetLocalCoords()[type].GetI();
                int X2 = two.GetLocalCoords()[type].GetI();
                int Y1 = one.GetLocalCoords()[type].GetJ();
                int Y2 = two.GetLocalCoords()[type].GetJ();

                int minX = X1 < X2 ? X1 : X2;
                int maxX = X2 > X1 ? X2 : X1;

                for (int i = minX + 2; i < maxX - 2; i++)
                {
                    int j = Line(i, X1, Y1, X2, Y2);
                    if (IsThereLineInRadius(type, i, j) || bitmaps[type].GetPixel(i, j) == ColorInfo.bgColor)
                    {
                        areAntiLinked = false;
                        shouldStop = true;
                        break;
                    }
                    else
                    {
                        areAntiLinked = true;
                    }
                }

                int minY = Y1 < Y2 ? Y1 : Y2;
                int maxY = Y2 > Y1 ? Y2 : Y1;

                for (int i = minY + 2; i < maxY - 2; i++)
                {
                    int j = Line(i, Y1, X1, Y2, X2);
                    if (IsThereLineInRadius(type, j, i) || bitmaps[type].GetPixel(j, i) == ColorInfo.bgColor)
                    {
                        areAntiLinked = false;
                        shouldStop = true;
                        break;
                    }
                    else
                    {
                        areAntiLinked = true;
                    }
                }
            }
            return areAntiLinked;
        }

        private static bool IsThereLineInRadius(int num, int a, int b)
        {
            bool isSolidLine = bitmaps[num].GetPixel(a - 1, b - 1) == ColorInfo.lineColor;
            isSolidLine = isSolidLine || bitmaps[num].GetPixel(a - 1, b) == ColorInfo.lineColor;
            isSolidLine = isSolidLine || bitmaps[num].GetPixel(a - 1, b + 1) == ColorInfo.lineColor;
            isSolidLine = isSolidLine || bitmaps[num].GetPixel(a, b - 1) == ColorInfo.lineColor;
            isSolidLine = isSolidLine || bitmaps[num].GetPixel(a, b) == ColorInfo.lineColor;
            isSolidLine = isSolidLine || bitmaps[num].GetPixel(a, b + 1) == ColorInfo.lineColor;
            isSolidLine = isSolidLine || bitmaps[num].GetPixel(a + 1, b - 1) == ColorInfo.lineColor;
            isSolidLine = isSolidLine || bitmaps[num].GetPixel(a + 1, b) == ColorInfo.lineColor;
            isSolidLine = isSolidLine || bitmaps[num].GetPixel(a + 1, b + 1) == ColorInfo.lineColor;

            bool isInvisLine = bitmaps[num].GetPixel(a - 1, b - 1) == ColorInfo.invisLineColor ||
                bitmaps[num].GetPixel(a - 1, b) == ColorInfo.invisLineColor ||
                bitmaps[num].GetPixel(a - 1, b + 1) == ColorInfo.invisLineColor ||
                bitmaps[num].GetPixel(a, b - 1) == ColorInfo.invisLineColor ||
                bitmaps[num].GetPixel(a, b) == ColorInfo.invisLineColor ||
                bitmaps[num].GetPixel(a, b + 1) == ColorInfo.invisLineColor ||
                bitmaps[num].GetPixel(a + 1, b - 1) == ColorInfo.invisLineColor ||
                bitmaps[num].GetPixel(a + 1, b) == ColorInfo.invisLineColor ||
                bitmaps[num].GetPixel(a + 1, b + 1) == ColorInfo.invisLineColor;

            return isSolidLine || isInvisLine;
        }

        private static int Line(int value, int x1, int y1, int x2, int y2)
        {
            return (value - x1) * (y2 - y1) / (x2 - x1) + y1;
        }
    }
}
