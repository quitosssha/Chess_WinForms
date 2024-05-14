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

namespace ChessGame
{
	public partial class ChessForm : Form
	{
		private TableLayoutPanel chessBoard = new TableLayoutPanel();

		public ChessForm()
		{
			InitializeComponent();
			InitializeChessBoard();
		}

		private void InitializeChessBoard()
		{
			chessBoard.RowCount = 8;
			chessBoard.ColumnCount = 8;
			chessBoard.BackColor = Color.DarkGray;
			chessBoard.Dock = DockStyle.None;

			chessBoard.FixCellsSize();

			Controls.Add(chessBoard);
			//chessBoard.FillBoardWithCells();
			DisplayBoardState(new BoardState());
			AdjustChessBoardSize();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			AdjustChessBoardSize();
		}

		private void AdjustChessBoardSize()
		{
			int minSize = Math.Min(ClientSize.Width, ClientSize.Height);
			chessBoard.Size = new Size(minSize, minSize);
			chessBoard.Location = new Point(
				(ClientSize.Width - minSize) / 2,
				(ClientSize.Height - minSize) / 2
				);
		}

		private void DisplayBoardState(BoardState boardState, bool fromBlack = false)
		{
			chessBoard.Controls.Clear();

			int rStart, cStart;
			Func<int, int> updateR, updateC;

			if (!fromBlack)
			{
				rStart = 7;
				cStart = 0;
				updateR = r => r - 1;
				updateC = c => c + 1;
			}
			else
			{
				rStart = 0;
				cStart = 7;
				updateR = r => r + 1;
				updateC = c => c - 1;
			}

			for (int row = rStart; fromBlack ? row < 8 : row >= 0; row = updateR(row))
			{
				for (int col = cStart; fromBlack ? col >= 0 : col < 8; col = updateC(col))
				{
					var figure = boardState[row, col];
					var pictureBox = new PictureBox()
					{
						Margin = new Padding(1),
						BackColor = (row + col) % 2 == 0 ? Color.FromArgb(118, 150, 86)
														: Color.FromArgb(238, 238, 210),
						Dock = DockStyle.Fill,
						SizeMode = PictureBoxSizeMode.StretchImage
					};

					if (figure != null)
					{
						string path = $"../../img/figures/{figure.Color.ToString().ToLower()}_{figure.GetType().Name.ToLower()}_default.png";
						try
						{
							pictureBox.Image = Image.FromFile(path);
						}
						catch (System.IO.FileNotFoundException)
						{
							Console.WriteLine($"File not found: {path}");
						}
					}

					chessBoard.Controls.Add(pictureBox, col, row);
					//chessBoard.SetCellPosition(pictureBox, new TableLayoutPanelCellPosition(col, row));
				}
			}
		}
	}

	public static class ChessLayoutPanelExtensions
	{
		public static void FixCellsSize(this TableLayoutPanel chessBoard)
		{
			for (int i = 0; i < 8; i++)
			{
				chessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
				chessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
			}
		}

		public static void FillBoardWithCells(this TableLayoutPanel chessBoard)
		{
			for (int row = 0; row < chessBoard.RowCount; row++)
				for (int col = 0; col < chessBoard.ColumnCount; col++)
				{
					PictureBox cell = new PictureBox()
					{
						Margin = new Padding(1),
						BackColor = (row + col) % 2 == 0 ? Color.DarkGreen : Color.Black,
						Dock = DockStyle.Fill
					};
					chessBoard.Controls.Add(cell, row, col);
				}
		}
	}
}
