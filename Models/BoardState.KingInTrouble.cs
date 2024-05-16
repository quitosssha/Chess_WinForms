using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public partial class BoardState
    {
		private bool IsCheck(FigureColor playerColor)
		{
			var kingPos = FindKing(playerColor);
			return IsUnderAttack(kingPos, playerColor);
		}

		private bool IsCheckmate(FigureColor playerColor)
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

		private bool IsUnderAttack(Cell position, FigureColor playerColor)
		{
			for (int row = 0; row < Size; row++)
				for (int col = 0; col < Size; col++)
				{
					var figure = this[row, col];
					if (figure != null && figure.Color != playerColor)
					{
						var moves = figure.GetAllowedMoves(this, row, col);
						if (moves.Contains((position.Row, position.Column)))
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
