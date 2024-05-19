using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
	public class CastlingMove : BoardAction
	{
		private Move kingMove;
		private Move rookMove;

		public override string UciNotation
		{
			get => kingMove.UciNotation;
		}

		public CastlingMove(Move kingMove)
		{
			this.kingMove = kingMove;
			rookMove = GenerateRookMove(kingMove);
		}

		public override IEnumerable<Cell> ChangedCells
		{
			get
			{
				foreach(var change in kingMove.ChangedCells)
					yield return change;
				foreach (var change in rookMove.ChangedCells)
					yield return change;
			}
		}

		public override void Execute()
		{
			kingMove.Execute();
			rookMove.Execute();
		}

		public override void Undo()
		{
			kingMove.Undo();
			rookMove.Undo();
		}

		private Move GenerateRookMove (Move kingMove)
		{;
			Cell rookPosition;
			Cell rookDestination;

			bool isShort = kingMove.To == new Cell(kingMove.To.Row, 1);

			rookPosition = isShort ? new Cell(kingMove.To.Row, 0)
									: new Cell(kingMove.To.Row, 7);
			rookDestination = isShort ? new Cell(kingMove.To.Row, 2)
									: new Cell(kingMove.To.Row, 4);

			return new Move(
				new Rook(kingMove.Figure.Color),
				rookPosition,
				rookDestination,
				capturedFigure: null,
				kingMove.UpdateCell);
		}
	}
}
