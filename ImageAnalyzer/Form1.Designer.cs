namespace ImageAnalyzer
{
    partial class Form1
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
            this.mainImg = new System.Windows.Forms.PictureBox();
            this.mainImgButton = new System.Windows.Forms.Button();
            this.leftImg = new System.Windows.Forms.PictureBox();
            this.topImg = new System.Windows.Forms.PictureBox();
            this.leftImgButton = new System.Windows.Forms.Button();
            this.topImgButton = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.TextBox();
            this.findButton = new System.Windows.Forms.Button();
            this.fileName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mainImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topImg)).BeginInit();
            this.SuspendLayout();
            // 
            // mainImg
            // 
            this.mainImg.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.mainImg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainImg.Location = new System.Drawing.Point(8, 6);
            this.mainImg.Name = "mainImg";
            this.mainImg.Size = new System.Drawing.Size(300, 300);
            this.mainImg.TabIndex = 0;
            this.mainImg.TabStop = false;
            // 
            // mainImgButton
            // 
            this.mainImgButton.Location = new System.Drawing.Point(233, 282);
            this.mainImgButton.Name = "mainImgButton";
            this.mainImgButton.Size = new System.Drawing.Size(75, 24);
            this.mainImgButton.TabIndex = 1;
            this.mainImgButton.Text = "open";
            this.mainImgButton.UseVisualStyleBackColor = true;
            this.mainImgButton.Click += new System.EventHandler(this.MainImgButton_Click);
            // 
            // leftImg
            // 
            this.leftImg.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.leftImg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.leftImg.Location = new System.Drawing.Point(314, 6);
            this.leftImg.Name = "leftImg";
            this.leftImg.Size = new System.Drawing.Size(300, 300);
            this.leftImg.TabIndex = 2;
            this.leftImg.TabStop = false;
            // 
            // topImg
            // 
            this.topImg.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.topImg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.topImg.Location = new System.Drawing.Point(8, 312);
            this.topImg.Name = "topImg";
            this.topImg.Size = new System.Drawing.Size(300, 300);
            this.topImg.TabIndex = 2;
            this.topImg.TabStop = false;
            // 
            // leftImgButton
            // 
            this.leftImgButton.Location = new System.Drawing.Point(539, 282);
            this.leftImgButton.Name = "leftImgButton";
            this.leftImgButton.Size = new System.Drawing.Size(75, 24);
            this.leftImgButton.TabIndex = 3;
            this.leftImgButton.Text = "open";
            this.leftImgButton.UseVisualStyleBackColor = true;
            this.leftImgButton.Click += new System.EventHandler(this.LeftImgButton_Click);
            // 
            // topImgButton
            // 
            this.topImgButton.Location = new System.Drawing.Point(233, 588);
            this.topImgButton.Name = "topImgButton";
            this.topImgButton.Size = new System.Drawing.Size(75, 24);
            this.topImgButton.TabIndex = 4;
            this.topImgButton.Text = "open";
            this.topImgButton.UseVisualStyleBackColor = true;
            this.topImgButton.Click += new System.EventHandler(this.TopImgButton_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(314, 312);
            this.logs.Multiline = true;
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(300, 270);
            this.logs.TabIndex = 5;
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(539, 588);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(75, 24);
            this.findButton.TabIndex = 6;
            this.findButton.Text = "create";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(314, 591);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(219, 20);
            this.fileName.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 617);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.findButton);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.topImgButton);
            this.Controls.Add(this.leftImgButton);
            this.Controls.Add(this.topImg);
            this.Controls.Add(this.leftImg);
            this.Controls.Add(this.mainImgButton);
            this.Controls.Add(this.mainImg);
            this.Name = "Form1";
            this.Text = "Image analyzer";
            ((System.ComponentModel.ISupportInitialize)(this.mainImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainImg;
        private System.Windows.Forms.Button mainImgButton;
        private System.Windows.Forms.PictureBox leftImg;
        private System.Windows.Forms.PictureBox topImg;
        private System.Windows.Forms.Button leftImgButton;
        private System.Windows.Forms.Button topImgButton;
        private System.Windows.Forms.TextBox logs;
        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.TextBox fileName;
    }
}

