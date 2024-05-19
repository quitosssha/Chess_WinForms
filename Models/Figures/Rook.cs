using System.Collections.Generic;

namespace Chess
{
	public class Rook : Figure
	{
		public Rook(FigureColor color) : base(color) { }

		public override string UciNotation => "r";

		public override IEnumerable<Cell> GetAllowedMoves(BoardState state, Cell from)
			=> GetAllowedMovesInDirections(horizontalDirections, state, from);
	}
}
