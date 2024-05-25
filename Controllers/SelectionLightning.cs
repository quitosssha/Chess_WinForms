using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
	public partial class ChessForm : Form
	{
		public void EnableSelectionLightning(PictureBox pictureBox)
		{
			pictureBox.Click += HighlightPossibleMoves;
		}
		
		public void DisableSelectionLightning(PictureBox pictureBox)
		{
			pictureBox.Click -= HighlightPossibleMoves;
		}

		private void HighlightPossibleMoves(object sender, EventArgs e)
		{
			var pictureBox = sender as PictureBox;
			if (pictureBox?.Image != null)
			{
				var position = (Point)pictureBox.Tag;

				Func<int, int> f = pos => pos;
				if (!_viewFromBlack) f = pos => BoardState.Size - 1 - pos;

				var cell = new Cell(f(position.Y), f(position.X));
				var figure = boardState[cell];
				if (figure == null) return;

				foreach (var move in figure.GetAllowedMoves(boardState, cell))
					if (boardState.TryMoveFigure(cell, move, simulate: true))
					{
						var viewCell = chessBoard.GetControlFromPosition(f(cell.Column), f(cell.Row));
						viewCell.BackColor = Cell.SelectionColor;
					}
			}
		}
	}
}
