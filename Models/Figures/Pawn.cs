using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	public partial class Pawn : Figure
	{
		public Pawn(FigureColor color) : base(color) { }

		public override string UciNotation => "";

		public override IEnumerable<Cell> GetAllowedMoves(BoardState state, Cell from)
		{
			var row = from.Row;
			var column = from.Column;
			int direction = Color == FigureColor.White ? 1 : -1;
			var newR = row + direction;

			if (state.InBounds(newR, column) && state[newR, column] == null)
			{
				yield return new Cell(newR, column);

				var startRow = Color == FigureColor.White ? 1 : 6;
				var dobleStepRow = row + direction * 2;
				if (row == startRow && state[dobleStepRow, column] == null)
					yield return new Cell(dobleStepRow, column);
			}

			for (int i = -1; i <= 1; i += 2)
			{ 
				int newC = column + i;
				if (state.InBounds(newR, newC))
				{
					var targetFigure = state[newR, newC];
					var to = new Cell(newR, newC);
					if (targetFigure != null && targetFigure.Color != Color
						|| AllowedEnPassent(state, from, to))
						yield return new Cell(newR, newC);
				}
			}
		}
	}
}
