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
		public Figure[,] Figures { get; private set; } = new Figure[8,8];

		public BoardState()
		{
			SetupBackRow(FigureColor.White, 0);

			for (int i = 0; i < 8; i++)
			{
				Figures[1, i] = new Pawn(FigureColor.White);
				Figures[6, i] = new Pawn(FigureColor.Black);
			}

			SetupBackRow(FigureColor.Black, 7);
		}

		private void SetupBackRow(FigureColor color, int row)
		{
			Figures[row, 0] = new Rook(color);
			Figures[row, 1] = new Knight(color);
			Figures[row, 2] = new Bishop(color);
			Figures[row, 3] = new Queen(color);
			Figures[row, 4] = new King(color);
			Figures[row, 5] = new Bishop(color);
			Figures[row, 6] = new Knight(color);
			Figures[row, 7] = new Rook(color);
		}

		public Figure this[int row, int column] =>
			Figures[row, column];
	}
}
