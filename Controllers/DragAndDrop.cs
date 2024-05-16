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
                bool allowedMove;

                var from = new Cell(sourcePosition.Y, sourcePosition.X);
                var to = new Cell(targetPosition.Y, targetPosition.X);

                var s = BoardState.Size;
                if (_viewFromBlack)
                    allowedMove = boardState.TryMoveFigure(from, to);
                else
					allowedMove = boardState.TryMoveFigure(from, to);

                if (allowedMove)
                {
                    if (sourcePictureBox.Parent is Panel sourceCell)
                        sourceCell.Controls.Remove(sourcePictureBox);
                    targetCell.Controls.Clear();
                    targetCell.Controls.Add(sourcePictureBox);
                    sourcePictureBox.Tag = targetPosition;
				}
            }
        }
    }
}
