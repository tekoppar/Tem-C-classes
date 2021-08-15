using Tem;
namespace Tem.TemUI
{
    partial class BoundsControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.vector3Center = new Tem.TemUI.Vector3Control();
            this.label2 = new System.Windows.Forms.Label();
            this.vector3Extents = new Tem.TemUI.Vector3Control();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.MinimumSize = new System.Drawing.Size(45, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Center:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.vector3Center);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.vector3Extents);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(420, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(420, 40);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // vector3Center
            // 
            this.vector3Center.AutoSize = true;
            this.vector3Center.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.vector3Center.BackColor = System.Drawing.Color.Transparent;
            this.vector3Center.Location = new System.Drawing.Point(51, 0);
            this.vector3Center.Margin = new System.Windows.Forms.Padding(0);
            this.vector3Center.Name = "vector3Center";
            this.vector3Center.Size = new System.Drawing.Size(369, 20);
            this.vector3Center.TabIndex = 1;
            this.vector3Center.OnValueChanged += new System.EventHandler<Tem.TemUI.ValuechangedVec3EventArgs>(this.vector3Control_OnValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(3, 23);
            this.label2.MinimumSize = new System.Drawing.Size(45, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Extents:";
            // 
            // vector3Extents
            // 
            this.vector3Extents.AutoSize = true;
            this.vector3Extents.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.vector3Extents.BackColor = System.Drawing.Color.Transparent;
            this.vector3Extents.Location = new System.Drawing.Point(51, 20);
            this.vector3Extents.Margin = new System.Windows.Forms.Padding(0);
            this.vector3Extents.Name = "vector3Extents";
            this.vector3Extents.Size = new System.Drawing.Size(369, 20);
            this.vector3Extents.TabIndex = 2;
            this.vector3Extents.OnValueChanged += new System.EventHandler<Tem.TemUI.ValuechangedVec3EventArgs>(this.vector3Control_OnValueChanged);
            // 
            // BoundsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BoundsControl";
            this.Size = new System.Drawing.Size(420, 40);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Vector3Control vector3Center;
        private System.Windows.Forms.Label label2;
        private Vector3Control vector3Extents;
    }
}
