using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public abstract class Figure
	{
		public abstract string UciNotation { get; }
		public Figure(FigureColor color)
		{
			Color = color;
		}
		public int TimesMoved { get; set; } = 0;
		public FigureColor Color { get; }
		public abstract IEnumerable<Cell> GetAllowedMoves(BoardState state, Cell from);
		public virtual IEnumerable<Cell> GetCellsUnderAttack(BoardState state, Cell from) =>
			GetAllowedMoves(state, from);

		protected readonly List<(int, int)> horizontalDirections = 
			new List<(int, int)>() { (-1, 0), (1, 0), (0, -1), (0, 1) };

		protected readonly List<(int, int)> diagonalDirections =
			new List<(int, int)>() { (-1, -1), (-1, 1), (1, -1), (1, 1) };

		protected List<(int, int)> knightDirections = new List<(int, int)>
		{
			(2, 1), (2, -1), (-2, 1), (-2, -1),
			(1, 2), (1, -2), (-1, 2), (-1, -2)
		};

		protected IEnumerable<Cell> GetAllowedMovesInDirections
			(List<(int, int)> directions, BoardState state, Cell from)
		{
			foreach (var (dRow, dColumn) in directions)
			{
				var row = from.Row;
				var column = from.Column;
				var newR = row + dRow;
				var newC = column + dColumn;

				while (state.InBounds(newR, newC))
				{
					var targetFigure = state[newR, newC];

					if (targetFigure == null)
						yield return new Cell(newR, newC);
					else
					{
						if (targetFigure.Color != Color)
							yield return new Cell(newR, newC);
						break;
					}

					newR += dRow;
					newC += dColumn;
				}
			}
		}
	}
}
