namespace Rubik
{
    public class RubikSide
    {
        public readonly RubikFace[,] Faces;
        public RubikFace[] UNeighbours;
        public RubikFace[] DNeighbours;
        public RubikFace[] LNeighbours;
        public RubikFace[] RNeighbours;

        public RubikSide(RubikColor color)
        {
            Faces = new RubikFace[3, 3];
            for (var y = 0; y < 3; y++)
            {
                for (var x = 0; x < 3; x++)
                    Faces[x, y] = new RubikFace(color);
            }
        }
    }
}