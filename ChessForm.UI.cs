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
        private void InitializeChessBoard()
        {
            chessBoard.RowCount = BoardState.Size;
            chessBoard.ColumnCount = BoardState.Size;
            chessBoard.BackColor = Color.DarkGray;
            chessBoard.Dock = DockStyle.None;

            chessBoard.FixCellsSize();

            Controls.Add(chessBoard);
            chessBoard.FillBoardWithCells(_viewFromBlack);
            DisplayBoardState();
        }

        private void InitializeControlPanel()
        {
            controlPanel.Dock = DockStyle.None;
            controlPanel.BackColor = Color.LightGray;

            foreach(var button in CreateControlPanelButtons())
				controlPanel.Controls.Add(button);

            controlPanel.Controls.Add(CreateCurrentPlayerIndicator());
            
            Controls.Add(controlPanel);
        }

        private void AdjustAllSizes()
        {
            int minSize = Math.Min(ClientSize.Width, ClientSize.Height - controlPanel.Height);
            chessBoard.Size = new Size(minSize, minSize);
            chessBoard.Location = new Point((ClientSize.Width - minSize) / 2, 0);
			controlPanel.Height = (int)(ClientSize.Height * 0.1);
			controlPanel.Location = new Point(chessBoard.Location.X, chessBoard.Bottom);
            controlPanel.Width = chessBoard.Width;
            for (int i = 0; i < 3; i++)
            {
                if (controlPanel.Controls[i] is Button button)
                    AdjustButtonSize(button, i);
            }
            if (controlPanel.Controls[3] is PictureBox currentPlayerIndicator)
				currentPlayerIndicator.Location = new Point(
                    controlPanel.Width - currentPlayerIndicator.Width - 10, 
                    (controlPanel.Height - currentPlayerIndicator.Height) / 2);
		}
    }
}
