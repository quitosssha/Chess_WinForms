using Chess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Chess
{
	public partial class ChessForm : Form
	{
		private TableLayoutPanel chessBoard = new TableLayoutPanel();
		private Panel controlPanel = new Panel();
		private BoardState boardState = new BoardState();
		private bool _viewFromBlack = false;

		public ChessForm()
		{
			InitializeComponent();
			InitializeChessBoard();
			InitializeControlPanel();

            AdjustAllSizes();
        }

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
            AdjustAllSizes();
		}

		private void DisplayBoardState(BoardState boardState, IEnumerable<Cell> cellsToUpdate = null)
		{
			if (cellsToUpdate != null)
				foreach (Cell c in cellsToUpdate)
				{
					var row = _viewFromBlack ? c.Row : BoardState.Size - 1 - c.Row;
					var col = _viewFromBlack ? c.Column : BoardState.Size - 1 - c.Column;
					DisplayCellAt(row, col);
				}
			else
				for (int row = 0; row < BoardState.Size; row++)
					for (int col = 0; col < BoardState.Size; col++)
					{
						var cell = GetCellAtPosition(row, col);
						DisplayCellAt(row, col);
					}
		}

		private void DisplayCellAt(int row, int column)
		{
			var cell = GetCellAtPosition(row, column);

			if (cell.Controls.Count > 0)
				if (cell.Controls[0] is PictureBox existingPictureBox)
					DisableDragAndDrop(cell, existingPictureBox);

			cell.Controls.Clear();
			var figure = boardState[_viewFromBlack ? row : BoardState.Size - 1 - row,
									_viewFromBlack ? column : BoardState.Size - 1 - column];
			var pictureBox = ChessImages.CreatePictureBoxForFigure(figure, row, column);

			EnableDragAndDrop(cell, pictureBox);

			cell.Controls.Add(pictureBox);
		}

		private Panel GetCellAtPosition(int row, int col) =>
            chessBoard.GetControlFromPosition(col, row) as Panel
				//Impossible exception
				?? throw new Exception($"No \"Panel\" at position ({row}, {col})!");
    }
}
