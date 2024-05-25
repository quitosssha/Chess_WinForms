using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Chess
{
	public partial class ChessForm : Form
	{
		private void Click_Undo(object sender, EventArgs e)
		{
			if (boardState.Undo())
				DisplayBoardState(boardState.LastChangedCells);
		}

		private void Click_Redo(object sender, EventArgs e)
		{
			if (boardState.Redo())
				DisplayBoardState(boardState.LastChangedCells);
		}

		private void Click_FlipBoard(object sender, EventArgs e)
		{
			_viewFromBlack = !_viewFromBlack;
			chessBoard.UpdateCellsColors(_viewFromBlack);
			DisplayBoardState();
		}

		private void Click_CalculateMove(object sender, EventArgs e)
		{
			CalculateAndExecuteNextMove();
		}

		private void Click_ShowTutorial(object sender, EventArgs e)
		{
			var tutorial = new TutorialForm();
			tutorial.ShowDialog();
		}
	}
}
