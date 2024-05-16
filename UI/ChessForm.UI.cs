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
            chessBoard.FillBoardWithCells();
            DisplayBoardState(boardState);
        }

        private void InitializeControlPanel()
        {
            controlPanel.Dock = DockStyle.None;
            controlPanel.Height = 50;
            controlPanel.BackColor = Color.LightGray;

            Button button1 = new Button();
            button1.Text = "Кнопка 1";
            button1.Location = new Point(10, 10);

            Button button2 = new Button();
            button2.Text = "Кнопка 2";
            button2.Location = new Point(100, 10);

            Button button3 = new Button();
            button3.Text = "Кнопка 3";
            button3.Location = new Point(190, 10);

            controlPanel.Controls.Add(button1);
            controlPanel.Controls.Add(button2);
            controlPanel.Controls.Add(button3);

            Controls.Add(controlPanel);
        }

        private void AdjustChessBoardAndControlPanelSize()
        {
            int minSize = Math.Min(ClientSize.Width, ClientSize.Height - controlPanel.Height);
            chessBoard.Size = new Size(minSize, minSize);
            chessBoard.Location = new Point(
                (ClientSize.Width - minSize) / 2,
                0
                );
            controlPanel.Location = new Point(chessBoard.Location.X, chessBoard.Bottom);
            controlPanel.Width = chessBoard.Width;
        }
    }
}
