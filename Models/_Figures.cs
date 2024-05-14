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
		//public abstract IEnumerable<(int Row, int Column)> GetPossibleMoves();
	}

	public class King : Figure
	{
		public King(FigureColor color) : base(color) { }

	}

	public class Queen : Figure
	{
		public Queen(FigureColor color): base(color) { }
	}

	public class Rook : Figure
	{
		public Rook(FigureColor color) : base(color) { }
	}

	public class Bishop : Figure
	{
		public Bishop(FigureColor color) : base(color) { }
	}

	public class Knight : Figure
	{
		public Knight(FigureColor color) : base(color) { }
	}

	public class Pawn : Figure
	{
		public Pawn(FigureColor color) : base(color) { }
	}
}
