using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
	public class PromotionForm : Form
	{
		private const int ButtonSize = 60; // Размер стороны квадратной кнопки
		private const int ButtonMargin = 10; // Отступ между кнопками
		private const int StartPositionX = 10; // Начальная позиция первой кнопки по оси X
		private const int PositionY = 20; // Позиция кнопок по оси Y

		public Figure SelectedFigure;

		public PromotionForm(FigureColor color)
		{
			InitializeForm(color);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
				e.Cancel = true;
			else
				base.OnFormClosing(e);
		}

		private void InitializeForm(FigureColor color)
		{
			this.Text = "Выберите фигуру для превращения";
			this.ClientSize = new Size(ButtonSize * 4 + ButtonMargin * 5, ButtonSize + PositionY * 2);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.StartPosition = FormStartPosition.CenterParent;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.ControlBox = false;

			var coords = GenerateCoords();

			// Создание и добавление кнопок
			AddButton(new Queen(color), coords[0]);
			AddButton(new Rook(color), coords[1]);
			AddButton(new Knight(color), coords[2]);
			AddButton(new Bishop(color), coords[3]);
		}

		private Point[] GenerateCoords()
		{
			var coords = new Point[4];
			for (int i = 0; i < 4; i++)
			{
				coords[i] = new Point(StartPositionX + i * (ButtonSize + ButtonMargin), PositionY);
			}
			return coords;
		}

		private void AddButton(Figure figure, Point location, string figureType = "default")
		{
			var figureName = figure.GetType().Name.ToLower();
			var color = figure.Color.ToString().ToLower();
			Button button = new Button
			{
                Size = new Size(ButtonSize, ButtonSize),
				BackgroundImage = Image.FromFile($"../../img/figures/{color}_{figureName}_{figureType}.png"),
				BackgroundImageLayout = ImageLayout.Stretch,
				Location = location,
				Parent = this
			};
			button.Click += (sender, e) => { SelectedFigure = figure; this.DialogResult = DialogResult.OK; };
		}
	}
}
