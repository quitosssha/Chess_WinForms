using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public class Move
	{
		public Figure Figure { get; }
		public Cell From { get; }
		public Cell To { get; }
		public Move(Figure figure, Cell from, Cell to)
		{
			Figure = figure;
			From = from;
			To = to;
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}
	}
}
