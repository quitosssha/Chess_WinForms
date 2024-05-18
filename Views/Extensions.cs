using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public static class ChessLayoutPanelExtensions
    {
        public static void FixCellsSize(this TableLayoutPanel chessBoard)
        {
            for (int i = 0; i < BoardState.Size; i++)
            {
                chessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / (float)BoardState.Size));
                chessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
            }
        }

        public static void FillBoardWithCells(this TableLayoutPanel chessBoard)
        {
            for (int row = 0; row < BoardState.Size; row++)
                for (int col = 0; col < BoardState.Size; col++)
                {
                    var cell = new Panel()
                    {
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(0),
                        BackColor = (row + col) % 2 == 0 ? Color.FromArgb(118, 150, 86)
                                                        : Color.FromArgb(238, 238, 210),
                        Dock = DockStyle.Fill
                    };
                    chessBoard.Controls.Add(cell, col, row);
                }
        }

        public static Point GetPointFromControl(this TableLayoutPanel chessBoard, Control control)
        {
            var cellPosition = chessBoard.GetPositionFromControl(control);
            return new Point(cellPosition.Column, cellPosition.Row);
        }
    }
}
