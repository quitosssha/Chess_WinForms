using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public partial class BoardState
    {
		public bool IsCheck(FigureColor playerColor)
		{
			var kingPos = FindKing(playerColor);
			return IsUnderAttack(playerColor, kingPos);
		}

		public bool IsCheckmate(FigureColor playerColor)
		{
			if (!IsCheck(playerColor)) return false;

			for (int row = 0; row < Size; row++)
				for (int col = 0; col < Size; col++)
				{
					var figure = this[row, col];
					if (figure != null && figure.Color == playerColor)
					{
						var currentCell = new Cell(row, col);
						var moves = figure.GetAllowedMoves(this, currentCell);
						foreach(var move in moves)
							if (TryMoveFigure(currentCell, move, simulate: true))
								return false;
					}
				}
			return true;
		}

		public bool IsUnderAttack(FigureColor playerColor, params Cell[] positions)
		{
			for (int row = 0; row < Size; row++)
				for (int col = 0; col < Size; col++)
				{
					var figure = this[row, col];
					if (figure != null && figure.Color != playerColor)
					{
						var moves = figure.GetAllowedMoves(this, row, col);
						foreach (var position in positions)
							if (moves.Contains((position.Row, position.Column)))
								return true;
					}
				}
			return false;
		}

		private Cell FindKing(FigureColor color)
		{
			Figure figure;
			for (int row = 0; row < Size; row++)
				for (int col = 0; col < Size; col++)
				{
					figure = this[row, col];
					if (figure is King && figure?.Color == color)
						return new Cell(row, col);
				}
			throw new Exception($"No {color.ToString().ToLower()} king on board");
		}
	}
}
