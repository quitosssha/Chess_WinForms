using System;
using System.Drawing;
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

		public static Color BasicColor(int row, int column) =>
			(row + column) % 2 == 1
				? Color.FromArgb(118, 150, 86)
				: Color.FromArgb(238, 238, 210);

		public override string ToString()
		{
			return $"({Row}, {Column})";
		}

		public static int Distance(Cell a, Cell b) =>
			Math.Max(	Math.Abs(a.Row - b.Row), 
						Math.Abs(a.Column - b.Column));

		public static bool operator ==(Cell left, Cell right) =>
			left.Row == right.Row && left.Column == right.Column;
		public static bool operator !=(Cell left, Cell right) =>
			!(left == right);

		public override bool Equals(object obj)
		{
			return obj is Cell cell &&
				   Row == cell.Row &&
				   Column == cell.Column;
		}

		public override int GetHashCode()
		{
			int hashCode = 240067226;
			hashCode = hashCode * -1521134295 + Row.GetHashCode();
			hashCode = hashCode * -1521134295 + Column.GetHashCode();
			return hashCode;
		}
	}
}
