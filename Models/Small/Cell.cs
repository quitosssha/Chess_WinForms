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
	}
}
