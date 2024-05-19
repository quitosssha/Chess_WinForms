using System.Collections.Generic;
using System.Linq;

namespace Chess
{
	public class King : Figure
	{
		public King(FigureColor color) : base(color) { }
		public override IEnumerable<Cell> GetCellsUnderAttack(BoardState state, Cell from)
		{
			foreach (var move in GetCommonMoves(state, from))
				yield return move;
		}

		public override IEnumerable<Cell> GetAllowedMoves
			(BoardState state, Cell from)
		{
			foreach (var move in GetCommonMoves(state, from))
				yield return move;

			var row = from.Row;
			var column = from.Column;
			Cell[] shortCastling = new[] { 
				new Cell(row, column - 1), 
				new Cell(row, column - 2) };
			Cell[] longCastling = new[] {
				new Cell(row, column + 1),
				new Cell(row, column + 2) };
			Cell[] LongCastlingFull = longCastling.Append(new Cell(row, column + 3)).ToArray();

			if (TimesMoved == 0)
			{

				var rook = state[row, 0];
				if (rook is Rook && rook.TimesMoved == 0 
					&& state.AreCellsEmpty(LongCastlingFull)
					&& !state.IsUnderAttack(Color, longCastling))
				{
					yield return new Cell(row, column + 2);
				}

				rook = state[row, 7];
				if (rook is Rook && rook.TimesMoved == 0
					&& state.AreCellsEmpty(shortCastling)
					&& !state.IsUnderAttack(Color, shortCastling))
				{
					yield return new Cell(row, column - 2);
				}
			}
		}

		private IEnumerable<Cell> GetCommonMoves(BoardState state, Cell from)
		{
			for (int i = -1; i <= 1; i++)
				for (int j = -1; j <= 1; j++)
				{
					var newR = from.Row + i;
					var newC = from.Column + j;
					if (!state.InBounds(newR, newC)) continue;

					var targetColor = state[newR, newC]?.Color;
					if (targetColor == Color) continue;

					yield return new Cell(newR, newC);
				}
		}
	}
}
