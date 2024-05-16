using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public class Move : ICommand
	{
		private readonly Figure figure;
		private readonly Cell from;
		private readonly Cell to;
		private readonly Figure capturedFigure;
		private readonly Action<Cell, Figure> updateCell;

		public Move(Figure figure, Cell from, Cell to, Figure capturedFigure, Action<Cell, Figure> updateCell)
		{
			this.figure = figure;
			this.from = from;
			this.to = to;
			this.capturedFigure = capturedFigure;
			this.updateCell = updateCell;
		}

		public void Execute()
		{
			updateCell(from, null);
			updateCell(to, figure);
		}

		public void Undo()
		{
			updateCell(from, figure);
			updateCell(to, capturedFigure);
		}
	}
}
