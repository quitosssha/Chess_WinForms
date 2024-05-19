using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public abstract class BoardAction : ICommand
	{
		public abstract IEnumerable<Cell> ChangedCells { get; }

		public abstract void Execute();

		public abstract void Undo();

		public abstract string UciNotation { get; }
	}
}
