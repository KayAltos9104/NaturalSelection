
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
            this.BtnTest = new System.Windows.Forms.Button();
            this.PbxField = new System.Windows.Forms.PictureBox();
            this.BtnLaunchCycle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PbxField)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnTest
            // 
            this.BtnTest.Location = new System.Drawing.Point(12, 12);
            this.BtnTest.Name = "BtnTest";
            this.BtnTest.Size = new System.Drawing.Size(128, 55);
            this.BtnTest.TabIndex = 0;
            this.BtnTest.Text = "Test";
            this.BtnTest.UseVisualStyleBackColor = true;
            this.BtnTest.Click += new System.EventHandler(this.BtnTest_Click);
            // 
            // PbxField
            // 
            this.PbxField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PbxField.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.PbxField.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PbxField.Location = new System.Drawing.Point(146, 5);
            this.PbxField.Name = "PbxField";
            this.PbxField.Size = new System.Drawing.Size(632, 481);
            this.PbxField.TabIndex = 1;
            this.PbxField.TabStop = false;
            // 
            // BtnLaunchCycle
            // 
            this.BtnLaunchCycle.Location = new System.Drawing.Point(12, 73);
            this.BtnLaunchCycle.Name = "BtnLaunchCycle";
            this.BtnLaunchCycle.Size = new System.Drawing.Size(128, 55);
            this.BtnLaunchCycle.TabIndex = 2;
            this.BtnLaunchCycle.Text = "Один цикл";
            this.BtnLaunchCycle.UseVisualStyleBackColor = true;
            this.BtnLaunchCycle.Click += new System.EventHandler(this.BtnLaunchCycle_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 498);
            this.Controls.Add(this.BtnLaunchCycle);
            this.Controls.Add(this.PbxField);
            this.Controls.Add(this.BtnTest);
            this.Name = "FrmMain";
            this.Text = "Natural Selection 0.01 alpha";
            ((System.ComponentModel.ISupportInitialize)(this.PbxField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnTest;
        private System.Windows.Forms.PictureBox PbxField;
        private System.Windows.Forms.Button BtnLaunchCycle;
    }
}

