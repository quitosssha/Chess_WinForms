using System.Collections.Generic;

namespace Chess
{
	public class Knight : Figure
	{
		public Knight(FigureColor color) : base(color) { }

		public override string UciNotation => "n";

		public override IEnumerable<Cell> GetAllowedMoves(BoardState state, Cell from)
		{
			var row = from.Row;
			var column = from.Column;
			foreach (var (dRow, dColumn) in knightDirections)
			{
				var newR = row + dRow;
				var newC = column + dColumn;
				if (!state.InBounds(newR, newC)) continue;

				var figure = state[newR, newC];
				if (figure == null || figure.Color != Color)
					yield return new Cell(newR, newC);
			}
		}
	}
}
