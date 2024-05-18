using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public partial class BoardState
	{
		public const int Size = 8;
		public Figure[,] Figures { get; private set; } = new Figure[Size, Size];
		public FigureColor CurrentColorMove { get; private set; } = FigureColor.White;
		public IEnumerable<Cell> LastChangedCells { get; private set; }

		public BoardState()
		{
			SetupBackRow(FigureColor.White, 0);

			for (int i = 0; i < Size; i++)
			{
				Figures[1, i] = new Pawn(FigureColor.White);
				Figures[Size - 2, i] = new Pawn(FigureColor.Black);
			}

			SetupBackRow(FigureColor.Black, Size - 1);
		}
		
		public Figure this[int row, int column]
		{
			get => Figures[row, column];
			private set => Figures[row, column] = value;
		}
		public Figure this[Cell cell]
		{
			get => this[cell.Row, cell.Column];
			private set => this[cell.Row, cell.Column] = value;
		}

		public bool TryMoveFigure(Cell from, Cell to, bool simulate = false)
		{
			var figure = this[from]
				?? throw new Exception($"No figure at position {from}");

			var allowedMoves = figure.GetAllowedMoves(this, from);

			if (figure.Color == CurrentColorMove && allowedMoves.Any(dst => to.Equals(dst)))
			{
				var capturedFigure = this[to];
				var move = new Move(figure, from, to, capturedFigure, UpdateCell);
				BoardAction moveToExecute = move.IsCastling() ? new CastlingMove(move) as BoardAction : move;
				ExecuteMove(moveToExecute);
				//if (castlingMove != null)
				//	ExecuteMove(new CastlingMove(move));
				//else
				//	ExecuteMove(move);

				if (IsCheck(CurrentColorMove))
				{
					TerminateLastMove();
					return false;
				}

				if (simulate)
					TerminateLastMove();
				else
				{
					SwapCurrentPlayer();
					ReportMove(figure, from, to);
					LastChangedCells = moveToExecute.ChangedCells;
					if (IsCheckmate(CurrentColorMove))
						Console.WriteLine($"{CurrentColorMove} checkmated!");
				}
				return true;
			}

			return false;
		}

		public bool InBounds(int row, int column) =>
			row >= 0 && column >= 0 && row < Size && column < Size;

		public bool AreCellsEmpty(params Cell[] cells)
		{
			foreach (var cell in cells)
				if (this[cell] != null) return false;
			return true;
		}
	}
}
