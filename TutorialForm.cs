using System;
using System.Drawing;
using System.Windows.Forms;

namespace Chess
{
	public class TutorialForm : Form
	{
		public TutorialForm()
		{
			InitializeForm();
		}

		private void InitializeForm()
		{
			this.Text = "Как играть?";
			this.ClientSize = new Size(300, 200);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.StartPosition = FormStartPosition.CenterParent;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			AddTextLabel();
			AddLinkLabel();
			AddOKButton();
		}

		private void AddTextLabel()
		{
			Label textLabel = new Label();
			textLabel.Text = "Drag a figure to make a move.";
			textLabel.Location = new Point(10, 20);
			textLabel.Size = new Size(280, 30);
			textLabel.Font = new Font(textLabel.Font.FontFamily, 12);
			this.Controls.Add(textLabel);
		}

		private void AddLinkLabel()
		{
			LinkLabel linkLabel = new LinkLabel();
			linkLabel.Text = "Open rules";
			linkLabel.Location = new Point(10, 50);
			linkLabel.Size = new Size(280, 30);
			linkLabel.Font = new Font(linkLabel.Font.FontFamily, 12);
			linkLabel.LinkClicked += LinkLabel_LinkClicked;
			linkLabel.AutoSize = false;
			linkLabel.AutoEllipsis = true;
			linkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
			this.Controls.Add(linkLabel);
		}

		private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://ru.wikipedia.org/wiki/Правила_шахмат");
		}

		private void AddOKButton()
		{
			Button okButton = new Button();
			okButton.Text = "ОК";
			okButton.Location = new Point(100, 130);
			okButton.Size = new Size(100, 30);
			okButton.Click += OkButton_Click;
			this.Controls.Add(okButton);
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
