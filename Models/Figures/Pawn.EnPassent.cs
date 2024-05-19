using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public partial class Pawn
	{
		private bool AllowedEnPassent(BoardState state, Cell from, Cell to)
		{
			if (state[from] is Pawn pawn
				&& pawn.OnEnPassentLine(from))
			{
				if (RelevantOpponentsMove(state, from, to))
					if (state[new Cell(from.Row, to.Column)] is Pawn)
						return true;
			}
			return false;
		}

		private bool RelevantOpponentsMove(BoardState state, Cell from, Cell to)
		{
			var opponentsRequiredMove = GetOpponentsRequiredMove(from, to);
			var lastChangedCells = state.LastMove.ToArray();
			foreach (var cell in lastChangedCells)
				if (!opponentsRequiredMove.Contains(cell))
					return false;
			return true;
		}

		private bool OnEnPassentLine(Cell position)
		{
			int enPassentRow = Color == FigureColor.White ? 4 : 3;
			return position.Row == enPassentRow;
		}

		private IEnumerable<Cell> GetOpponentsRequiredMove(Cell position, Cell targetToEnPassent)
		{
			var column = targetToEnPassent.Column;
			var startRow = Color == FigureColor.White ? 6 : 1;
			var doubleStepRow = Color == FigureColor.White ? 4 : 3;

			yield return new Cell(startRow, column);
			yield return new Cell(doubleStepRow, column);
		}
	}
}
