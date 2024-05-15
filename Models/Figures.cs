using System;
using System.Collections.Generic;
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
		public FigureColor Color { get; }
		public abstract IEnumerable<(int Row, int Column)> GetAllowedMoves
			(BoardState state, int row, int column);
	}

	public class King : Figure
	{
		public King(FigureColor color) : base(color) { }

		public override IEnumerable<(int Row, int Column)> GetAllowedMoves
			(BoardState state, int row, int column)
		{
			for (int i = -1; i <= 1; i++)
				for (int j = -1; j <= 1; j++)
				{
					var newR = row + i; 
					var newC = column + j;
					if (!state.InBounds(newR, newC)) continue;

					var targetColor = state[newR, newC]?.Color;
					if (targetColor == Color) continue;

					yield return (newR, newC);
				}
		}
	}

	public class Queen : Figure
	{
		public Queen(FigureColor color): base(color) { }

		public override IEnumerable<(int Row, int Column)> GetAllowedMoves(BoardState state, int row, int column)
		{
			throw new NotImplementedException();
		}
	}

	public class Rook : Figure
	{
		public Rook(FigureColor color) : base(color) { }

		public override IEnumerable<(int Row, int Column)> GetAllowedMoves(BoardState state, int row, int column)
		{
			throw new NotImplementedException();
		}
	}

	public class Bishop : Figure
	{
		public Bishop(FigureColor color) : base(color) { }

		public override IEnumerable<(int Row, int Column)> GetAllowedMoves(BoardState state, int row, int column)
		{
			throw new NotImplementedException();
		}
	}

	public class Knight : Figure
	{
		public Knight(FigureColor color) : base(color) { }

		public override IEnumerable<(int Row, int Column)> GetAllowedMoves(BoardState state, int row, int column)
		{
			throw new NotImplementedException();
		}
	}

	public class Pawn : Figure
	{
		public Pawn(FigureColor color) : base(color) { }

		public override IEnumerable<(int Row, int Column)> GetAllowedMoves(BoardState state, int row, int column)
		{
			int newR = Color == FigureColor.White ? row + 1 : row - 1;
			for (int i = -1; i <= 1; i++)
			{ 
				int newC = column + i;
				if (!state.InBounds(newR, newC)) continue;

                if (i == 0 && state[newR, newC] == null)
					yield return (newR, newC);
				
				if (i != 0 && state[newR, newC] != null && state[newR, newC].Color != Color)
					yield return (newR, newC);
			}
		}
	}
}
