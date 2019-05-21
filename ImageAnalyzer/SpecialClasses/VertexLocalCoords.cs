namespace ImageAnalyzer.SpecialClasses
{
    public class VertexLocalCoords
    {
        public ImageHandler.ImageType imageType;
        private int i, j;

        public VertexLocalCoords(ImageHandler.ImageType type, int i, int j)
        {
            imageType = type;
            this.i = i;
            this.j = j;
        }

        public ImageHandler.ImageType GetImageType() { return imageType; }
        public int GetI() { return i; }
        public int GetJ() { return j; }

        public override string ToString()
        {
            return imageType.ToString() + ": i=" + i + " j=" + j;
        }
    }
}
