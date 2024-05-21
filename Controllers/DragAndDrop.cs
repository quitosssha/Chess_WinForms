using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
    public partial class ChessForm : Form
    {
        public void EnableDragAndDrop(Panel cell, PictureBox pictureBox)
        {
            cell.AllowDrop = true;
            pictureBox.MouseDown += PictureBox_MouseDown;
            cell.DragOver += Cell_DragOver;
            cell.DragDrop += Cell_DragDrop;
        }

		public void DisableDragAndDrop(Panel cell, PictureBox pictureBox)
		{
            cell.AllowDrop = false;
			pictureBox.MouseDown -= PictureBox_MouseDown;
			cell.DragOver -= Cell_DragOver;
			cell.DragDrop -= Cell_DragDrop;
		}

		private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox?.Image != null)
            {
                pictureBox.DoDragDrop(pictureBox, DragDropEffects.Move);
            }
        }

        private void Cell_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Cell_DragDrop(object sender, DragEventArgs e)
        {
            if (sender is Panel targetCell && e.Data.GetData(typeof(PictureBox)) is PictureBox sourcePictureBox)
            {
                var sourcePosition = (Point)sourcePictureBox.Tag;
                var targetPosition = chessBoard.GetPointFromControl(targetCell);

                Func<int, int> f = pos => pos;
				if (!_viewFromBlack) f = pos => BoardState.Size - 1 - pos;

                var from = new Cell(f(sourcePosition.Y), f(sourcePosition.X));
                var to = new Cell(f(targetPosition.Y), f(targetPosition.X));

    			bool allowedMove = boardState.TryMoveFigure(from, to);

                if (allowedMove)
                    DisplayBoardState(boardState.LastChangedCells);
                    sourcePictureBox.Tag = targetPosition;
				
            }
        }
    }
}
