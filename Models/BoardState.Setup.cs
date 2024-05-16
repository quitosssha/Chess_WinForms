using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chess
{
	public partial class BoardState
	{
		private void UpdateCell(Cell cell, Figure figure) => this[cell] = figure;

		private void SwapCurrentPlayer() =>
			currentColorMove = currentColorMove == FigureColor.White 
								? FigureColor.Black : FigureColor.White;

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

		private void ReportMove(Figure figure, Cell from, Cell to)
		{
			var color = figure.Color.ToString().ToLower();
			var name = figure.GetType().Name;
			Console.WriteLine
				($"Moved {color} {name} from ({Size - from.Column},{from.Row}) " +
				$"to ({Size - to.Column},{to.Row})");
		}
	}
}
