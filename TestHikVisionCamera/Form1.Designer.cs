namespace TestHikVisionCamera
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.opencamera = new System.Windows.Forms.Button();
			this.openstream = new System.Windows.Forms.Button();
			this.closestream = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.closecamera = new System.Windows.Forms.Button();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.settimer = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Blue;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(2, 2);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(550, 580);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// opencamera
			// 
			this.opencamera.Dock = System.Windows.Forms.DockStyle.Fill;
			this.opencamera.ForeColor = System.Drawing.Color.Blue;
			this.opencamera.Location = new System.Drawing.Point(2, 2);
			this.opencamera.Margin = new System.Windows.Forms.Padding(2);
			this.opencamera.Name = "opencamera";
			this.opencamera.Size = new System.Drawing.Size(198, 135);
			this.opencamera.TabIndex = 1;
			this.opencamera.Text = "打开相机";
			this.opencamera.UseCompatibleTextRendering = true;
			this.opencamera.UseVisualStyleBackColor = true;
			this.opencamera.Click += new System.EventHandler(this.opencamera_Click);
			// 
			// openstream
			// 
			this.openstream.Dock = System.Windows.Forms.DockStyle.Fill;
			this.openstream.ForeColor = System.Drawing.Color.Blue;
			this.openstream.Location = new System.Drawing.Point(2, 141);
			this.openstream.Margin = new System.Windows.Forms.Padding(2);
			this.openstream.Name = "openstream";
			this.openstream.Size = new System.Drawing.Size(198, 135);
			this.openstream.TabIndex = 2;
			this.openstream.Text = "软件触发";
			this.openstream.UseCompatibleTextRendering = true;
			this.openstream.UseVisualStyleBackColor = true;
			this.openstream.Click += new System.EventHandler(this.openstream_Click);
			// 
			// closestream
			// 
			this.closestream.Dock = System.Windows.Forms.DockStyle.Fill;
			this.closestream.ForeColor = System.Drawing.Color.Blue;
			this.closestream.Location = new System.Drawing.Point(2, 280);
			this.closestream.Margin = new System.Windows.Forms.Padding(2);
			this.closestream.Name = "closestream";
			this.closestream.Size = new System.Drawing.Size(198, 135);
			this.closestream.TabIndex = 3;
			this.closestream.Text = "软件触发获取图像";
			this.closestream.UseCompatibleTextRendering = true;
			this.closestream.UseVisualStyleBackColor = true;
			this.closestream.Click += new System.EventHandler(this.closestream_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.Chartreuse;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.opencamera, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.openstream, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.closestream, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.closecamera, 0, 3);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(761, 2);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(202, 559);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// closecamera
			// 
			this.closecamera.Dock = System.Windows.Forms.DockStyle.Fill;
			this.closecamera.ForeColor = System.Drawing.Color.Blue;
			this.closecamera.Location = new System.Drawing.Point(2, 419);
			this.closecamera.Margin = new System.Windows.Forms.Padding(2);
			this.closecamera.Name = "closecamera";
			this.closecamera.Size = new System.Drawing.Size(198, 138);
			this.closecamera.TabIndex = 5;
			this.closecamera.Text = "关闭相机";
			this.closecamera.UseCompatibleTextRendering = true;
			this.closecamera.UseVisualStyleBackColor = true;
			this.closecamera.Click += new System.EventHandler(this.closecamera_Click);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.00469F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.99531F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 205F));
			this.tableLayoutPanel3.Controls.Add(this.pictureBox1, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 2, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(2);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(965, 584);
			this.tableLayoutPanel3.TabIndex = 9;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.button2, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.button1, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.settimer, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(556, 2);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 289F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 241F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(201, 580);
			this.tableLayoutPanel2.TabIndex = 5;
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.Red;
			this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button2.ForeColor = System.Drawing.Color.Blue;
			this.button2.Location = new System.Drawing.Point(3, 342);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(195, 235);
			this.button2.TabIndex = 10;
			this.button2.Text = "关闭定时";
			this.button2.UseVisualStyleBackColor = false;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Lime;
			this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1.ForeColor = System.Drawing.Color.Blue;
			this.button1.Location = new System.Drawing.Point(3, 53);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(195, 283);
			this.button1.TabIndex = 9;
			this.button1.Text = "打开定时";
			this.button1.UseVisualStyleBackColor = false;
			// 
			// settimer
			// 
			this.settimer.BackColor = System.Drawing.Color.Magenta;
			this.settimer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.settimer.Location = new System.Drawing.Point(3, 3);
			this.settimer.Name = "settimer";
			this.settimer.Size = new System.Drawing.Size(195, 21);
			this.settimer.TabIndex = 11;
			// 
			// timer1
			// 
			this.timer1.Interval = 500;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(965, 584);
			this.Controls.Add(this.tableLayoutPanel3);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button opencamera;
        private System.Windows.Forms.Button openstream;
        private System.Windows.Forms.Button closestream;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button closecamera;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox settimer;
        private System.Windows.Forms.Timer timer1;
    }
}

