namespace winforms_2
{
    partial class BackgroundStyle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonChangeBackgroundColor = new System.Windows.Forms.Button();
            this.colorDialogChangeBackgroundColor = new System.Windows.Forms.ColorDialog();
            this.radioButtonSolid = new System.Windows.Forms.RadioButton();
            this.radioButtonGradient = new System.Windows.Forms.RadioButton();
            this.radioButtonHatch = new System.Windows.Forms.RadioButton();
            this.radioButtonTexture = new System.Windows.Forms.RadioButton();
            this.radioButtonPicture = new System.Windows.Forms.RadioButton();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonDeny = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonChangeBackgroundColor
            // 
            this.buttonChangeBackgroundColor.Location = new System.Drawing.Point(12, 331);
            this.buttonChangeBackgroundColor.Name = "buttonChangeBackgroundColor";
            this.buttonChangeBackgroundColor.Size = new System.Drawing.Size(197, 107);
            this.buttonChangeBackgroundColor.TabIndex = 0;
            this.buttonChangeBackgroundColor.Text = "Change BG color";
            this.buttonChangeBackgroundColor.UseVisualStyleBackColor = true;
            this.buttonChangeBackgroundColor.Click += new System.EventHandler(this.ButtonChangeBackgroundColor_Click);
            // 
            // radioButtonSolid
            // 
            this.radioButtonSolid.AutoSize = true;
            this.radioButtonSolid.Checked = true;
            this.radioButtonSolid.Location = new System.Drawing.Point(42, 26);
            this.radioButtonSolid.Name = "radioButtonSolid";
            this.radioButtonSolid.Size = new System.Drawing.Size(48, 17);
            this.radioButtonSolid.TabIndex = 1;
            this.radioButtonSolid.TabStop = true;
            this.radioButtonSolid.Text = "Solid";
            this.radioButtonSolid.UseVisualStyleBackColor = true;
            // 
            // radioButtonGradient
            // 
            this.radioButtonGradient.AutoSize = true;
            this.radioButtonGradient.Location = new System.Drawing.Point(42, 65);
            this.radioButtonGradient.Name = "radioButtonGradient";
            this.radioButtonGradient.Size = new System.Drawing.Size(65, 17);
            this.radioButtonGradient.TabIndex = 2;
            this.radioButtonGradient.Text = "Gradient";
            this.radioButtonGradient.UseVisualStyleBackColor = true;
            // 
            // radioButtonHatch
            // 
            this.radioButtonHatch.AutoSize = true;
            this.radioButtonHatch.Location = new System.Drawing.Point(42, 103);
            this.radioButtonHatch.Name = "radioButtonHatch";
            this.radioButtonHatch.Size = new System.Drawing.Size(54, 17);
            this.radioButtonHatch.TabIndex = 3;
            this.radioButtonHatch.Text = "Hatch";
            this.radioButtonHatch.UseVisualStyleBackColor = true;
            // 
            // radioButtonTexture
            // 
            this.radioButtonTexture.AutoSize = true;
            this.radioButtonTexture.Location = new System.Drawing.Point(42, 141);
            this.radioButtonTexture.Name = "radioButtonTexture";
            this.radioButtonTexture.Size = new System.Drawing.Size(61, 17);
            this.radioButtonTexture.TabIndex = 4;
            this.radioButtonTexture.TabStop = true;
            this.radioButtonTexture.Text = "Texture";
            this.radioButtonTexture.UseVisualStyleBackColor = true;
            // 
            // radioButtonPicture
            // 
            this.radioButtonPicture.AutoSize = true;
            this.radioButtonPicture.Location = new System.Drawing.Point(42, 178);
            this.radioButtonPicture.Name = "radioButtonPicture";
            this.radioButtonPicture.Size = new System.Drawing.Size(58, 17);
            this.radioButtonPicture.TabIndex = 5;
            this.radioButtonPicture.TabStop = true;
            this.radioButtonPicture.Text = "Picture";
            this.radioButtonPicture.UseVisualStyleBackColor = true;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(494, 321);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(265, 101);
            this.buttonConfirm.TabIndex = 6;
            this.buttonConfirm.Text = "confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);
            // 
            // buttonDeny
            // 
            this.buttonDeny.Location = new System.Drawing.Point(494, 214);
            this.buttonDeny.Name = "buttonDeny";
            this.buttonDeny.Size = new System.Drawing.Size(265, 101);
            this.buttonDeny.TabIndex = 7;
            this.buttonDeny.Text = "deny";
            this.buttonDeny.UseVisualStyleBackColor = true;
            this.buttonDeny.Click += new System.EventHandler(this.ButtonDeny_Click);
            // 
            // BackgroundStyle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonDeny);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.radioButtonPicture);
            this.Controls.Add(this.radioButtonTexture);
            this.Controls.Add(this.radioButtonHatch);
            this.Controls.Add(this.radioButtonGradient);
            this.Controls.Add(this.radioButtonSolid);
            this.Controls.Add(this.buttonChangeBackgroundColor);
            this.Name = "BackgroundStyle";
            this.Text = "BackgroundStyle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonChangeBackgroundColor;
        private System.Windows.Forms.ColorDialog colorDialogChangeBackgroundColor;
        private System.Windows.Forms.RadioButton radioButtonSolid;
        private System.Windows.Forms.RadioButton radioButtonGradient;
        private System.Windows.Forms.RadioButton radioButtonHatch;
        private System.Windows.Forms.RadioButton radioButtonTexture;
        private System.Windows.Forms.RadioButton radioButtonPicture;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonDeny;
    }
}