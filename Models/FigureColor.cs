namespace Chess
{
	public enum FigureColor
    {
        White,
        Black
    }

    public static class FigureColorExtensions
    {
        public static void Swap(ref this FigureColor color) =>
            color = color == FigureColor.White ? FigureColor.Black : FigureColor.White;
    }
}
