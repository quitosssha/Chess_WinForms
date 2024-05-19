using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public abstract class Figure
	{
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

	public class Queen : Figure
	{
		public Queen(FigureColor color): base(color) { }

		public override IEnumerable<Cell> GetAllowedMoves(BoardState state, Cell from)
			=> GetAllowedMovesInDirections(
				horizontalDirections.Concat(diagonalDirections).ToList(),
				state, 
				from);
	}

	public class Rook : Figure
	{
		public Rook(FigureColor color) : base(color) { }

		public override IEnumerable<Cell> GetAllowedMoves(BoardState state, Cell from)
			=> GetAllowedMovesInDirections(horizontalDirections, state, from);
	}

	public class Bishop : Figure
	{
		public Bishop(FigureColor color) : base(color) { }

		public override IEnumerable<Cell> GetAllowedMoves(BoardState state, Cell from)
			=> GetAllowedMovesInDirections(diagonalDirections, state, from);
	}

	public class Knight : Figure
	{
		public Knight(FigureColor color) : base(color) { }

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

	public class Pawn : Figure
	{
		public Pawn(FigureColor color) : base(color) { }

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
					if (targetFigure != null && targetFigure.Color != Color)
						yield return new Cell(newR, newC);
				}
			}
		}
	}
}
