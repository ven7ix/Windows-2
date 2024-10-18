using System.Windows.Forms;

namespace winforms_2
{
    partial class FormRenderFunction
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBackgroudStyle = new System.Windows.Forms.Button();
            this.buttonChangeGraphColor = new System.Windows.Forms.Button();
            this.buttonSaveFunction = new System.Windows.Forms.Button();
            this.buttonRandomFunction = new System.Windows.Forms.Button();
            this.colorDialogGraphColor = new System.Windows.Forms.ColorDialog();
            this.panelGraph = new winforms_2.DoubleBufferedPanel();
            this.labelSelectedCoordinateXY = new System.Windows.Forms.Label();
            this.labelCoordinateXY = new System.Windows.Forms.Label();
            this.labelPointsCount = new System.Windows.Forms.Label();
            this.labelScale = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panelGraph.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBackgroudStyle);
            this.groupBox1.Controls.Add(this.buttonChangeGraphColor);
            this.groupBox1.Controls.Add(this.buttonSaveFunction);
            this.groupBox1.Controls.Add(this.buttonRandomFunction);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(667, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 450);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // buttonBackgroudStyle
            // 
            this.buttonBackgroudStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBackgroudStyle.Location = new System.Drawing.Point(0, 148);
            this.buttonBackgroudStyle.Name = "buttonBackgroudStyle";
            this.buttonBackgroudStyle.Size = new System.Drawing.Size(129, 71);
            this.buttonBackgroudStyle.TabIndex = 4;
            this.buttonBackgroudStyle.Text = "BG style";
            this.buttonBackgroudStyle.UseVisualStyleBackColor = true;
            this.buttonBackgroudStyle.Click += new System.EventHandler(this.ButtonBackgroudStyle_Click);
            // 
            // buttonChangeGraphColor
            // 
            this.buttonChangeGraphColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChangeGraphColor.Location = new System.Drawing.Point(0, 225);
            this.buttonChangeGraphColor.Name = "buttonChangeGraphColor";
            this.buttonChangeGraphColor.Size = new System.Drawing.Size(129, 71);
            this.buttonChangeGraphColor.TabIndex = 3;
            this.buttonChangeGraphColor.Text = "color";
            this.buttonChangeGraphColor.UseVisualStyleBackColor = true;
            this.buttonChangeGraphColor.Click += new System.EventHandler(this.ButtonChangeGraphColor_Click);
            // 
            // buttonSaveFunction
            // 
            this.buttonSaveFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveFunction.Location = new System.Drawing.Point(0, 379);
            this.buttonSaveFunction.Name = "buttonSaveFunction";
            this.buttonSaveFunction.Size = new System.Drawing.Size(129, 71);
            this.buttonSaveFunction.TabIndex = 2;
            this.buttonSaveFunction.Text = "save func";
            this.buttonSaveFunction.UseVisualStyleBackColor = true;
            this.buttonSaveFunction.Click += new System.EventHandler(this.ButtonSaveFunction_Click);
            // 
            // buttonRandomFunction
            // 
            this.buttonRandomFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRandomFunction.Location = new System.Drawing.Point(0, 302);
            this.buttonRandomFunction.Name = "buttonRandomFunction";
            this.buttonRandomFunction.Size = new System.Drawing.Size(129, 71);
            this.buttonRandomFunction.TabIndex = 1;
            this.buttonRandomFunction.Text = "random func";
            this.buttonRandomFunction.UseVisualStyleBackColor = true;
            this.buttonRandomFunction.Click += new System.EventHandler(this.ButtonRandomFunction_Click);
            // 
            // panelGraph
            // 
            this.panelGraph.Controls.Add(this.labelSelectedCoordinateXY);
            this.panelGraph.Controls.Add(this.labelCoordinateXY);
            this.panelGraph.Controls.Add(this.labelPointsCount);
            this.panelGraph.Controls.Add(this.labelScale);
            this.panelGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraph.Location = new System.Drawing.Point(0, 0);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(667, 450);
            this.panelGraph.TabIndex = 3;
            this.panelGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelGraph_Paint);
            this.panelGraph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PanelGraph_MouseClick);
            this.panelGraph.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelGraph_MouseDown);
            this.panelGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelGraph_MouseMove);
            this.panelGraph.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelGraph_MouseUp);
            this.panelGraph.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PanelGraph_MouseWheel);
            this.panelGraph.Resize += new System.EventHandler(this.PanelGraph_Resize);
            // 
            // labelSelectedCoordinateXY
            // 
            this.labelSelectedCoordinateXY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSelectedCoordinateXY.AutoSize = true;
            this.labelSelectedCoordinateXY.Enabled = false;
            this.labelSelectedCoordinateXY.Location = new System.Drawing.Point(571, 48);
            this.labelSelectedCoordinateXY.Name = "labelSelectedCoordinateXY";
            this.labelSelectedCoordinateXY.Size = new System.Drawing.Size(93, 13);
            this.labelSelectedCoordinateXY.TabIndex = 3;
            this.labelSelectedCoordinateXY.Text = "(selected) x: 0 y: 0";
            // 
            // labelCoordinateXY
            // 
            this.labelCoordinateXY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCoordinateXY.AutoSize = true;
            this.labelCoordinateXY.Enabled = false;
            this.labelCoordinateXY.Location = new System.Drawing.Point(621, 35);
            this.labelCoordinateXY.Name = "labelCoordinateXY";
            this.labelCoordinateXY.Size = new System.Drawing.Size(44, 13);
            this.labelCoordinateXY.TabIndex = 2;
            this.labelCoordinateXY.Text = "x: 0 y: 0";
            // 
            // labelPointsCount
            // 
            this.labelPointsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPointsCount.AutoSize = true;
            this.labelPointsCount.Location = new System.Drawing.Point(621, 22);
            this.labelPointsCount.Name = "labelPointsCount";
            this.labelPointsCount.Size = new System.Drawing.Size(13, 13);
            this.labelPointsCount.TabIndex = 1;
            this.labelPointsCount.Text = "0";
            // 
            // labelScale
            // 
            this.labelScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelScale.AutoSize = true;
            this.labelScale.Location = new System.Drawing.Point(621, 9);
            this.labelScale.Name = "labelScale";
            this.labelScale.Size = new System.Drawing.Size(13, 13);
            this.labelScale.TabIndex = 0;
            this.labelScale.Text = "1";
            // 
            // FormRenderFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelGraph);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(200, 100);
            this.Name = "FormRenderFunction";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.panelGraph.ResumeLayout(false);
            this.panelGraph.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRandomFunction;
        private System.Windows.Forms.Button buttonSaveFunction;
        private System.Windows.Forms.ColorDialog colorDialogGraphColor;
        private System.Windows.Forms.Label labelScale;
        private DoubleBufferedPanel panelGraph;
        private Button buttonChangeGraphColor;
        private Button buttonBackgroudStyle;
        private Label labelPointsCount;
        private Label labelCoordinateXY;
        private Label labelSelectedCoordinateXY;
    }
}

