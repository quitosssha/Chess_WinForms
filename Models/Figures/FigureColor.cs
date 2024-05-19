namespace Chess
{
	public enum FigureColor
    {
        White,
        Black
    }

    public static class FigureColorExtensions
    {
        public static FigureColor Reverse(ref this FigureColor color) =>
            color == FigureColor.White ? FigureColor.Black : FigureColor.White;
    }
}
