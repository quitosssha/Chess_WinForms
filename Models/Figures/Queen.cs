using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	public class Queen : Figure
	{
		public Queen(FigureColor color): base(color) { }

		public override IEnumerable<Cell> GetAllowedMoves(BoardState state, Cell from)
			=> GetAllowedMovesInDirections(
				horizontalDirections.Concat(diagonalDirections).ToList(),
				state, 
				from);
	}
}
