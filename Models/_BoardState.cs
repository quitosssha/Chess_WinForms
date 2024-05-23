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
		public IEnumerable<Cell> LastMove
		{
			get => movesHistory.Peek().ChangedCells;
		}

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


		public bool TryMoveFigure(Cell from, Cell to, bool simulate = false, Figure figureToPromote = null)
		{
			if (from == to) return false;

			var figure = this[from]
				?? throw new Exception($"No figure at position {from}");

			var allowedMoves = figure.GetAllowedMoves(this, from);

			if (figure.Color == CurrentColorMove && allowedMoves.Any(dst => to.Equals(dst)))
			{
				var moveToExecute = CreateBoardAction(figure, from, to, simulate, figureToPromote);
				ExecuteMove(moveToExecute);

				if (IsCheck(CurrentColorMove))
				{
					TerminateLastMove();
					return false;
				}

				if (simulate)
					TerminateLastMove();
				else
					FinalizeMove(from, to, figure, moveToExecute);
				return true;
			}

			return false;
		}

		private void FinalizeMove(Cell from, Cell to, Figure figure, BoardAction moveToExecute)
		{
			SwapCurrentPlayer();
			ReportMove(moveToExecute);
			LastChangedCells = moveToExecute.ChangedCells;
			if (IsCheckmate(CurrentColorMove))
				Console.WriteLine($"{CurrentColorMove} checkmated!");
			if (IsDraw(CurrentColorMove))
				Console.WriteLine("Draw!");
		}

		private BoardAction CreateBoardAction(Figure figure, Cell from, Cell to, bool simulate, Figure figureToPromote)
		{
			var capturedFigure = this[to];
			var move = new Move(figure, from, to, capturedFigure, UpdateCell);
			return move.IsCastling() ? new CastlingMove(move)
				: move.IsEnPassent() ? new EnPassentMove(move)
				: move.IsPromotion() ?
					simulate ? new PromotionMove(move, new Queen(figure.Color))
					: figureToPromote != null ? new PromotionMove(move, figureToPromote)
					: new PromotionMove(move, ChessForm.AskPlayerForFigure(figure.Color))
				: move as BoardAction;
		}

		public bool InBounds(int row, int column) =>
			row >= 0 && column >= 0 && row < Size && column < Size;

		public bool AreCellsEmpty(params Cell[] cells)
		{
			foreach (var cell in cells)
				if (this[cell] != null) return false;
			return true;
		}

		public string GetAllMovesInUCI() =>
			string.Join(" ", movesHistory.Select(m => m.UciNotation).Reverse());

		public override string ToString()
		{
			var str = new StringBuilder();
			for (int row = 0; row < Size; row++)
			{
				for (int col = 0; col < Size; col++)
				{
					var figure = this[row, col];
					if (figure == null)
					{
						str.Append(".");
						continue;
					}
					var notation = figure.ToString();
					if (figure.Color == FigureColor.Black)
						str.Append(notation);
					else
						str.Append(notation.ToUpper());
				}
				str.Append("/");
			}
			return str.ToString();
		}
	}
}
