﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Chess.Stockfish;

namespace Chess
{
	public partial class ChessForm : Form
	{
		private TableLayoutPanel chessBoard = new TableLayoutPanel();
		private Panel controlPanel = new Panel();
		private BoardState boardState = new BoardState();
		private bool _viewFromBlack = false;
		private StockfishEngine engine = new StockfishEngine("../../Stockfish/stockfish_distr/stockfish_engine.exe");

		public ChessForm()
		{
			InitializeChessBoard();
			InitializeControlPanel();
			InitializeComponent();
			AdjustAllSizes();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
            AdjustAllSizes();
		}

		private void DisplayBoardState(IEnumerable<Cell> cellsToUpdate = null)
		{
			if (cellsToUpdate != null)
				foreach (Cell c in cellsToUpdate)
				{
					var row = c.Row.InvertIfWhite(_viewFromBlack);
					var col = c.Column.InvertIfWhite(_viewFromBlack);
					DisplayCellAt(row, col);
				}
			else
				for (int row = 0; row < BoardState.Size; row++)
					for (int col = 0; col < BoardState.Size; col++)
						DisplayCellAt(row, col);

			if (controlPanel.Controls.Count != 0)
			{
				UpdateButtonsActivity();
				UpdateCurrentPlayerIndicator();
			}
		}

		private void DisplayCellAt(int row, int column)
		{
			var cell = GetCellAtPosition(row, column);

			if (cell.Controls.Count > 0)
				if (cell.Controls[0] is PictureBox existingPictureBox)
					DisableDragAndDrop(cell, existingPictureBox);

			cell.Controls.Clear();
			var figure = boardState[row.InvertIfWhite(_viewFromBlack), 
									column.InvertIfWhite(_viewFromBlack)];
			var pictureBox = ChessImages.CreatePictureBoxForFigure(figure, row, column);

			EnableDragAndDrop(cell, pictureBox);

			cell.Controls.Add(pictureBox);
		}

		private Panel GetCellAtPosition(int row, int col) =>
            chessBoard.GetControlFromPosition(col, row) as Panel
				//Impossible exception
				?? throw new Exception($"No \"Panel\" at position ({row}, {col})!");

		public static Figure AskPlayerForFigure(FigureColor color)
		{
			using (var form = new PromotionForm(color))
			{
				var result = form.ShowDialog();
				if (result == DialogResult.OK)
					return form.SelectedFigure;
				throw new Exception("No figure selected!");
			}
		}

		private async void CalculateAndExecuteNextMove(int depth = 15)
		{
			string initCommand = "position startpos moves " + boardState.GetAllMovesInUCI();
			string goCommand = $"go depth {depth}";
			engine.StartEngine();
			engine.SendCommand(initCommand);
			engine.SendCommand(goCommand);
			string recommendedMove = await engine.ReadBestMoveAsync();
			engine.StopEngine();
			Console.WriteLine(recommendedMove);

			string[] moveParts = recommendedMove.Split();
			string bestMove = moveParts[1];
			bool allowedMove = boardState.TryMoveFigure(bestMove);

			if (allowedMove)
				DisplayBoardState(boardState.LastChangedCells);
			else
				throw new Exception($"Can't execute move {bestMove}, sent by Stockfish!");

		}
	}
}
