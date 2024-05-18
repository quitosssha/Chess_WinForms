using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public static class Extensions
    {
        public static void FixCellsSize(this TableLayoutPanel chessBoard)
        {
            for (int i = 0; i < BoardState.Size; i++)
            {
                chessBoard.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / (float)BoardState.Size));
                chessBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
            }
        }

        public static void FillBoardWithCells(this TableLayoutPanel chessBoard, bool viewFromBlack)
        {
            for (int row = 0; row < BoardState.Size; row++)
                for (int col = 0; col < BoardState.Size; col++)
                {
                    var boardRow = row.InvertIfWhite(viewFromBlack);
                    var boardColumn = col.InvertIfWhite(viewFromBlack);
                    var cell = new Panel()
                    {
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(0),
                        BackColor = Cell.BasicColor(boardRow, boardColumn),
                        Dock = DockStyle.Fill
                    };
                    chessBoard.Controls.Add(cell, boardRow, boardColumn);
                }
        }

        public static void UpdateCellsColors(this TableLayoutPanel chessBoard, bool viewFromBlack)
        {
            for (int row = 0; row < BoardState.Size; row++)
                for (int col = 0; col < BoardState.Size; col++)
                {
					var boardRow = row.InvertIfWhite(viewFromBlack);
					var boardColumn = col.InvertIfWhite(viewFromBlack);
                    if (chessBoard.GetControlFromPosition(col, row) is Panel cell)
                        cell.BackColor = Cell.BasicColor(row, col);
                }
        }

        public static Point GetPointFromControl(this TableLayoutPanel chessBoard, Control control)
        {
            var cellPosition = chessBoard.GetPositionFromControl(control);
            return new Point(cellPosition.Column, cellPosition.Row);
        }

        public static int InvertIfWhite(this int number, bool viewFromBlack)
        {
            if (number >= BoardState.Size) throw new ArgumentException();
			return viewFromBlack? number : BoardState.Size - 1 - number;
        }
    }
}
