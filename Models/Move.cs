using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public class Move : BoardAction
	{
		public Figure Figure { get; }
		public Cell From { get; }
		public Cell To { get; }
		public Figure CapturedFigure { get; }
		public Action<Cell, Figure> UpdateCell { get; }

		public override IEnumerable<Cell> ChangedCells
		{
			get
			{
				yield return From;
				yield return To; 
			}
		}

		public Move(Figure figure, Cell from, Cell to, Figure capturedFigure, Action<Cell, Figure> updateCell)
		{
			this.Figure = figure;
			this.From = from;
			this.To = to;
			this.CapturedFigure = capturedFigure;
			this.UpdateCell = updateCell;
		}

		public override void Execute()
		{
			if (Figure != null)
				Figure.TimesMoved++;
			UpdateCell(From, null);
			UpdateCell(To, Figure);
		}

		public override void Undo()
		{
			if (Figure != null)
				Figure.TimesMoved--;
			UpdateCell(From, Figure);
			UpdateCell(To, CapturedFigure);
		}

		public bool IsCastling() =>
			Figure is King && Cell.Distance(From, To) > 1;

		public bool IsEnPassent() =>
			Figure is Pawn && CapturedFigure == null && From.Column != To.Column;
	}
}
