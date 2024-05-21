using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
	public partial class ChessForm
	{
		private IEnumerable<Button> CreateControlPanelButtons()
		{
			Button undoButton = new Button();
			undoButton.Image = Image.FromFile("../../img/undo.png");
			//undoButton.BackgroundImageLayout = ImageLayout.Stretch;
			undoButton.Click += Click_Undo;
			undoButton.Enabled = false;
			yield return undoButton;

			Button redoButton = new Button();
			redoButton.Image = Image.FromFile("../../img/redo.png");
			//redoButton.BackgroundImageLayout = ImageLayout.Stretch;
			redoButton.Click += Click_Redo;
			redoButton.Enabled = false;
			yield return redoButton;

			Button flipBoardButton = new Button();
			flipBoardButton.BackgroundImage = Image.FromFile("../../img/flipBoard.png");
			flipBoardButton.BackgroundImageLayout = ImageLayout.Stretch;
			flipBoardButton.Click += Click_FlipBoard;
			yield return flipBoardButton;

			Button robotButton = new Button();
			robotButton.BackgroundImage = Image.FromFile("../../img/calculate.png");
			robotButton.BackgroundImageLayout = ImageLayout.Stretch;
			robotButton.Click += Click_CalculateMove;
			yield return robotButton;
		}

		private void UpdateButtonsActivity()
		{
			var undo = boardState.Undo(execute: false);
			var redo = boardState.Redo(execute: false);

			if (controlPanel.Controls[0] is Button undoButton)
				if (undo) undoButton.Enabled = true;
				else undoButton.Enabled = false;

			if (controlPanel.Controls[1] is Button redoButton)
				if (redo) redoButton.Enabled = true;
				else redoButton.Enabled = false;
		}

		private void AdjustButtonSize(Button button, int number)
		{
			var side = (int)(ClientSize.Height * 0.08);
			var margin = (int)(ClientSize.Height * 0.01);
			var leftShift = margin + (margin + side) * number;
			var buttonSize = new Size(side, side);

			button.Location = new Point(leftShift, margin);
			button.Size = buttonSize;
		}

		private PictureBox CreateCurrentPlayerIndicator()
		{
			var currentPlayerIndicator = new CircularPictureBox();
			currentPlayerIndicator.Size = new Size(20, 20); // Размер кружка
			currentPlayerIndicator.BackColor = TransformFigureColorToColor(boardState.CurrentColorMove);
			return currentPlayerIndicator;
		}

		private void UpdateCurrentPlayerIndicator()
		{
			if (controlPanel.Controls[3] is PictureBox indicator)
				indicator.BackColor = TransformFigureColorToColor(boardState.CurrentColorMove);
		}

		private Color TransformFigureColorToColor(FigureColor figureColor) =>
			figureColor == FigureColor.White ? Color.White : Color.Black;
	}
}
