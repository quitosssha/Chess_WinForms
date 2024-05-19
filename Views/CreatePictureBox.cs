using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public static class ChessImages
    {
        public static PictureBox CreatePictureBoxForFigure(Figure figure, int row = 0, int column = 0, string figureType = "default")
        {
            var pictureBox = new PictureBox()
            {
                Margin = new Padding(0),
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Tag = new Point(column, row)
            };

            if (figure != null)
            {
                var color = figure.Color.ToString().ToLower();
                var figureName = figure.GetType().Name.ToLower();
                string path = $"../../img/figures/{color}_{figureName}_{figureType}.png";
                try
                {
                    pictureBox.Image = Image.FromFile(path);
                }
                catch (System.IO.FileNotFoundException)
                {
                    Console.WriteLine($"File not found: {path}");
                }
            }

            return pictureBox;
        }
    }
}
