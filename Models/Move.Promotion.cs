using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public class PromotionMove : BoardAction
	{
		private Move pawnMove;
		private Move pawnDisappears;
		private Move figureAppears;

		public override string UciNotation
		{
			get => pawnMove.UciNotation + figureAppears.Figure.UciNotation;
		}

		public PromotionMove(Move pawnMove, Figure promoteTo)
		{
			this.pawnMove = pawnMove;
			GenerateTransformingMoves(promoteTo);
		}

		private void GenerateTransformingMoves(Figure promoteTo)
		{
			pawnDisappears = new Move(null, pawnMove.To, pawnMove.To, pawnMove.Figure, pawnMove.UpdateCell);
			figureAppears = new Move(promoteTo, pawnMove.To, pawnMove.To, null, pawnMove.UpdateCell);
		}

		public override IEnumerable<Cell> ChangedCells
		{
			get
			{
				yield return pawnMove.From;
				yield return pawnMove.To;
			}
		}

		public override void Execute()
		{
			pawnMove.Execute();
			pawnDisappears.Execute();
			figureAppears.Execute();
		}

		public override void Undo()
		{
			figureAppears.Undo();
			pawnDisappears.Undo();
			pawnMove.Undo();
		}
	}
}
