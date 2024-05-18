using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public readonly struct Cell
	{
		public int Row { get; }
		public int Column { get; }
		public Cell(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public override string ToString()
		{
			return $"({Row}, {Column})";
		}

		public static bool operator ==(Cell left, Cell right) =>
			left.Row == right.Row && left.Column == right.Column;
		public static bool operator !=(Cell left, Cell right) =>
			!(left == right);

		public static int Distance(Cell a, Cell b) =>
			Math.Max(	Math.Abs(a.Row - b.Row), 
						Math.Abs(a.Column - b.Column));
	}
}
