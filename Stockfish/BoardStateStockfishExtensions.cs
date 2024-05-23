using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
	public static class StockfishExtensions
	{
		public static void ExecuteUciGame(this BoardState state, string UciMoves)
		{
			foreach (var move in UciMoves.Split())
				TryMoveFigure(state, move);
		}

		public static bool TryMoveFigure(this BoardState state, string moveInUciNotation)
		{
            if (moveInUciNotation == "(none)")
            {
				Console.WriteLine("No move entered");
				return false;
            }
            var from = moveInUciNotation.Substring(0, 2).ParseUciMoveToCell();
			var to = moveInUciNotation.Substring(2, 2).ParseUciMoveToCell();
			Figure figure = null;
			if (moveInUciNotation.Length > 4)
			{
				figure = moveInUciNotation[4].ParseUciFigure(state.CurrentColorMove);
			}
			return state.TryMoveFigure(from, to, figureToPromote: figure);
		}

		public static Cell ParseUciMoveToCell(this string uciMove)
		{
			int column = BoardState.Size - 1 - (uciMove[0] - 97);
			int row = uciMove[1] - 49;
			return new Cell(row, column);
		}

		public static Figure ParseUciFigure(this char uciFigure, FigureColor color)
		{
			Dictionary<char, Figure> figures = new Dictionary<char, Figure>()
			{
				['q'] = new Queen(color),
				['r'] = new Rook(color),
				['n'] = new Knight(color),
				['b'] = new Bishop(color)
			};
			return figures[uciFigure];
		}
	}
}
