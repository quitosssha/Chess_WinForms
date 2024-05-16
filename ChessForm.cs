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
		private bool _viewFromBlack;

		public ChessForm()
		{
			InitializeComponent();
			InitializeChessBoard();
			InitializeControlPanel();

            AdjustChessBoardAndControlPanelSize();
        }

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
            AdjustChessBoardAndControlPanelSize();
		}

		private void DisplayBoardState(BoardState boardState, bool fromBlackSide = true)
		{
			_viewFromBlack = fromBlackSide;
            for (int row = 0; row < BoardState.Size; row++)
				for (int col = 0; col < BoardState.Size; col++)
				{
					var cell = GetCellAtPosition(row, col);
                    cell.Controls.Clear();
					var figure = boardState[_viewFromBlack ? row : BoardState.Size - 1 - row,
                                            _viewFromBlack ? col : BoardState.Size - 1 - col];
					var pictureBox = ChessImages.CreatePictureBoxForFigure(figure, row, col);

					EnableDragAndDrop(cell, pictureBox);

					cell.Controls.Add(pictureBox);
				}
		}

		private Panel GetCellAtPosition(int row, int col) =>
            chessBoard.GetControlFromPosition(col, row) as Panel
				//Impossible exception
				?? throw new Exception($"No \"Panel\" at position ({row}, {col})!");
    }
}
