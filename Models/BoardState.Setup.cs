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
			var name = figure.GetType().Name;
			Console.WriteLine
				($"Moved {name} from ({Size - from.Column},{from.Row}) " +
				$"to ({Size - to.Column},{to.Row})");
		}

		private bool KingIsChecked(FigureColor kingColor)
		{
			Cell kingPos = FindKing(kingColor);

			for (int row = 0; row < Size; row++)
				for (int col = 0; col < Size; col++)
				{
					var figure = this[row, col];
					if (figure != null)
					{
						var moves = figure.GetAllowedMoves(this, row, col);
						if (moves.Contains((kingPos.Row, kingPos.Column)))
							return true;
					}
				}
			return false;
		}

		private Cell FindKing(FigureColor color)
		{
			for (int row = 0; row < Size; row++)
				for (int col = 0; col < Size; col++)
				{
					var figure = this[row, col];
					if (figure is King && figure?.Color == color)
						return new Cell(row, col);
				}
			throw new Exception($"No {color.ToString().ToLower()} king on board");
		}
	}
}
