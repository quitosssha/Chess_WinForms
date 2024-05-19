using System.Collections.Generic;

namespace Chess
{
	public class Bishop : Figure
	{
		public Bishop(FigureColor color) : base(color) { }

		public override string UciNotation => "b";

		public override IEnumerable<Cell> GetAllowedMoves(BoardState state, Cell from)
			=> GetAllowedMovesInDirections(diagonalDirections, state, from);
	}
}
