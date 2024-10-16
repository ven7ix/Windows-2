using System;
using System.Drawing;
using System.Windows.Forms;

namespace winforms_2
{
    public partial class BackgroundStyle : Form
    {
        public Color panelBackColor = Color.White;
        public event Action<Color, string> OnDataSubmitted;

        public BackgroundStyle()
        {
            InitializeComponent();
        }

        private void ButtonChangeBackgroundColor_Click(object sender, EventArgs e)
        {
            colorDialogChangeBackgroundColor.Color = panelBackColor;
            if (colorDialogChangeBackgroundColor.ShowDialog() == DialogResult.Cancel)
                return;
            panelBackColor = colorDialogChangeBackgroundColor.Color;
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            string selectedStyle = "";

            if (radioButtonSolid.Checked)
                selectedStyle = "Solid";
            else if (radioButtonGradient.Checked)
                selectedStyle = "Gradient";
            else if (radioButtonHatch.Checked)
                selectedStyle = "Hatch";
            else if (radioButtonTexture.Checked)
                selectedStyle = "Texture";
            else if (radioButtonPicture.Checked)
                selectedStyle = "Picture";

            OnDataSubmitted?.Invoke(panelBackColor, selectedStyle);
            Close();
        }

        private void ButtonDeny_Click(object sender, EventArgs e)
        {
            Close();

        }
    }
}
