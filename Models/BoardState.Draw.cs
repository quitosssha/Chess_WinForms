using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public partial class BoardState
	{
		Dictionary<string, int> positionsRepetition = new Dictionary<string, int>();

		private bool IsDraw(FigureColor playerColor)
		{
			if (IsStalemate(playerColor)) return true;
			if (IsTripleRepetition()) return true;
			return false;
		}

		private bool IsTripleRepetition() => 
			positionsRepetition[this.ToString()] == 3;

		private bool IsStalemate(FigureColor playerColor)
		{
			if (IsCheck(playerColor))
				return false;
			for (int row = 0; row < Size; row++)
			for (int col = 0; col < Size; col++)
			{
				var currentCell = new Cell(row, col);
				if (this[currentCell] is Figure figure && figure.Color == playerColor)
					foreach (var move in figure.GetAllowedMoves(this, currentCell))
						if (TryMoveFigure(currentCell, move, simulate: true))
							return false;
			}
			return true;
		}
	}
}
