using System;
using System.Collections.Generic;
using System.Drawing;
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
			undoButton.Click += Click_Undo;
			yield return undoButton;

			Button redoButton = new Button();
			redoButton.Image = Image.FromFile("../../img/redo.png");
			redoButton.Click += Click_Redo;
			yield return redoButton;

			Button flipBoardButton = new Button();
			flipBoardButton.Text = "KickFlip";
			flipBoardButton.TextAlign = ContentAlignment.TopLeft;
			flipBoardButton.Click += Click_FlipBoard;
			yield return flipBoardButton;
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
	}
}
