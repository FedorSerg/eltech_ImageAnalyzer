using System.Drawing;

namespace ImageAnalyzer.SpecialClasses
{
    static class ColorInfo
    {
        public static readonly Color bgColor = Color.FromArgb(128, 128, 128);
        public static readonly Color fillColor = Color.FromArgb(255, 255, 255);

        public static readonly Color lineColor = Color.FromArgb(0, 0, 0);
        public static readonly Color invisLineColor = Color.FromArgb(0, 255, 0);

        public static readonly Color vertexColor = Color.FromArgb(0, 0, 255);
        public static readonly Color zeroVertexColor = Color.FromArgb(255, 0, 255);
        public static readonly Color invisVertexColor = Color.FromArgb(255, 255, 0);

        public static readonly Color curcleColor = Color.FromArgb(255, 0, 0);
    }
}
