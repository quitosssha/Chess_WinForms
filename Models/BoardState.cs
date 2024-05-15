using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public class BoardState
	{
		public const int Size = 8;
		public Figure[,] Figures { get; private set; } = new Figure[Size, Size];
		public FigureColor currentMove { get; private set; } = FigureColor.White;

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

		private void SetupBackRow(FigureColor color, int row)
		{
			Figures[row, 0] = new Rook(color);
			Figures[row, 1] = new Knight(color);
			Figures[row, 2] = new Bishop(color);
			Figures[row, 3] = new King(color);
			Figures[row, 4] = new Queen(color);
			Figures[row, 5] = new Bishop(color);
			Figures[row, 6] = new Knight(color);
			Figures[row, 7] = new Rook(color);
		}

		public Figure this[int row, int column] =>
			Figures[row, column];

		public bool TryMoveFigure(int fromRow, int fromColumn, int toRow, int toColumn)
		{
			var figure = Figures[fromRow, fromColumn];
			if (figure != null)
			{
                Figures[fromRow, fromColumn] = null;
				Figures[toRow, toColumn] = figure;
			}
			var name = figure.GetType().Name;
			Console.WriteLine($"Moved {name} from ({7 - fromColumn},{fromRow}) to ({7 - toColumn},{toRow})");
			return true;
		}
	}
}
