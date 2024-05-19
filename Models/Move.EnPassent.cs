using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public class EnPassentMove : BoardAction
	{
		private Move pawnMove;
		private Move captureOpponent;

		public EnPassentMove(Move pawnMove)
		{
			this.pawnMove = pawnMove;
			captureOpponent = GenerateCapture(pawnMove);
		}

		private Move GenerateCapture(Move pawnMove)
		{
			var row = pawnMove.From.Row;
			var column = pawnMove.To.Column;
			var cell = new Cell(row, column);
			var opponentColor = pawnMove.Figure.Color == FigureColor.White 
				? FigureColor.Black : FigureColor.White;
			return new Move(
				null,
				cell,
				cell,
				new Pawn(opponentColor),
				pawnMove.UpdateCell);
		}

		public override IEnumerable<Cell> ChangedCells
		{
			get
			{
				yield return pawnMove.From;
				yield return pawnMove.To;
				yield return captureOpponent.From;
			}
		}

		public override void Execute()
		{
			pawnMove.Execute();
			captureOpponent.Execute();
		}

		public override void Undo()
		{
			pawnMove.Undo();
			captureOpponent.Undo();
		}
	}
}
