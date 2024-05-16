using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
	public partial class ChessForm : Form
	{
		private void Click_Undo(object sender, EventArgs e)
		{
			if (boardState.Undo());
				DisplayBoardState(boardState);
		}

		private void Click_Redo(object sender, EventArgs e)
		{
			if (boardState.Redo());
				DisplayBoardState(boardState);
		}

		private void Click_FlipBoard(object sender, EventArgs e)
		{
			_viewFromBlack = !_viewFromBlack;
			DisplayBoardState(boardState);
		}
	}
}
