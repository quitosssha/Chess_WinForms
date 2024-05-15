using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (sourcePictureBox.Parent is Panel sourceCell)
                    sourceCell.Controls.Remove(sourcePictureBox);

                targetCell.Controls.Clear();
                targetCell.Controls.Add(sourcePictureBox);

                var sourcePosition = (Point)sourcePictureBox.Tag;
                var targetPosition = chessBoard.GetPointFromControl(targetCell);

                var s = BoardState.Size;
                if (_viewFromBlack)
                    boardState.MoveFigure(sourcePosition.Y, sourcePosition.X,
                                            targetPosition.Y, targetPosition.X);
                else
                    boardState.MoveFigure(s - 1 - sourcePosition.Y, s - 1 - sourcePosition.X,
                                            s - 1 - targetPosition.Y, s - 1 - targetPosition.X);

                sourcePictureBox.Tag = targetPosition;
            }
        }
    }
}
