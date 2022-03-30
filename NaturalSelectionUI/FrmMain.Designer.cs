
namespace NaturalSelectionUI
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnNewGame = new System.Windows.Forms.Button();
            this.PbxField = new System.Windows.Forms.PictureBox();
            this.BtnLaunchCycle = new System.Windows.Forms.Button();
            this.LbxStatistics = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.PbxField)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnNewGame
            // 
            this.BtnNewGame.Location = new System.Drawing.Point(12, 12);
            this.BtnNewGame.Name = "BtnNewGame";
            this.BtnNewGame.Size = new System.Drawing.Size(184, 55);
            this.BtnNewGame.TabIndex = 0;
            this.BtnNewGame.Text = "Новая игра";
            this.BtnNewGame.UseVisualStyleBackColor = true;
            this.BtnNewGame.Click += new System.EventHandler(this.BtnInitialize_Click);
            // 
            // PbxField
            // 
            this.PbxField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PbxField.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.PbxField.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PbxField.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.PbxField.Location = new System.Drawing.Point(202, 5);
            this.PbxField.Name = "PbxField";
            this.PbxField.Size = new System.Drawing.Size(751, 629);
            this.PbxField.TabIndex = 1;
            this.PbxField.TabStop = false;
            this.PbxField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PbxFieldClick);
            this.PbxField.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PbxFieldMouseUp);
            this.PbxField.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.PbxFieldWheel);
            // 
            // BtnLaunchCycle
            // 
            this.BtnLaunchCycle.Location = new System.Drawing.Point(12, 73);
            this.BtnLaunchCycle.Name = "BtnLaunchCycle";
            this.BtnLaunchCycle.Size = new System.Drawing.Size(184, 55);
            this.BtnLaunchCycle.TabIndex = 2;
            this.BtnLaunchCycle.Text = "Запустить серию циклов";
            this.BtnLaunchCycle.UseVisualStyleBackColor = true;
            this.BtnLaunchCycle.Click += new System.EventHandler(this.BtnLaunchCycle_Click);
            // 
            // LbxStatistics
            // 
            this.LbxStatistics.FormattingEnabled = true;
            this.LbxStatistics.ItemHeight = 20;
            this.LbxStatistics.Location = new System.Drawing.Point(12, 143);
            this.LbxStatistics.Name = "LbxStatistics";
            this.LbxStatistics.Size = new System.Drawing.Size(184, 204);
            this.LbxStatistics.TabIndex = 3;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 646);
            this.Controls.Add(this.LbxStatistics);
            this.Controls.Add(this.BtnLaunchCycle);
            this.Controls.Add(this.PbxField);
            this.Controls.Add(this.BtnNewGame);
            this.Name = "FrmMain";
            this.Text = "Natural Selection 0.01 alpha";
            ((System.ComponentModel.ISupportInitialize)(this.PbxField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnNewGame;
        private System.Windows.Forms.PictureBox PbxField;
        private System.Windows.Forms.Button BtnLaunchCycle;
        private System.Windows.Forms.ListBox LbxStatistics;
    }
}

