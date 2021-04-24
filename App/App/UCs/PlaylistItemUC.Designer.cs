
namespace App.UCs
{
    partial class PlaylistItemUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblSTT = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btnEllipsis = new FontAwesome.Sharp.IconButton();
            this.btnHeart = new FontAwesome.Sharp.IconButton();
            this.lblArtistsName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblSongName = new System.Windows.Forms.Label();
            this.imgThumbnail = new System.Windows.Forms.PictureBox();
            this.visualiation = new System.Windows.Forms.PictureBox();
            this.timerVisualiation = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgThumbnail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.visualiation)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSTT
            // 
            this.lblSTT.AutoSize = true;
            this.lblSTT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSTT.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSTT.ForeColor = System.Drawing.Color.Red;
            this.lblSTT.Location = new System.Drawing.Point(27, 9);
            this.lblSTT.Name = "lblSTT";
            this.lblSTT.Size = new System.Drawing.Size(37, 41);
            this.lblSTT.TabIndex = 5;
            this.lblSTT.Text = "1";
            // 
            // btnEllipsis
            // 
            this.btnEllipsis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnEllipsis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEllipsis.FlatAppearance.BorderSize = 0;
            this.btnEllipsis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.btnEllipsis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.btnEllipsis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEllipsis.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEllipsis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnEllipsis.IconChar = FontAwesome.Sharp.IconChar.EllipsisH;
            this.btnEllipsis.IconColor = System.Drawing.Color.White;
            this.btnEllipsis.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEllipsis.Location = new System.Drawing.Point(969, 9);
            this.btnEllipsis.Margin = new System.Windows.Forms.Padding(1);
            this.btnEllipsis.Name = "btnEllipsis";
            this.btnEllipsis.Size = new System.Drawing.Size(48, 48);
            this.btnEllipsis.TabIndex = 21;
            this.btnEllipsis.UseVisualStyleBackColor = false;
            // 
            // btnHeart
            // 
            this.btnHeart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.btnHeart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeart.FlatAppearance.BorderSize = 0;
            this.btnHeart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.btnHeart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(37)))), ((int)(((byte)(55)))));
            this.btnHeart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeart.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHeart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnHeart.IconChar = FontAwesome.Sharp.IconChar.Heart;
            this.btnHeart.IconColor = System.Drawing.Color.White;
            this.btnHeart.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnHeart.Location = new System.Drawing.Point(900, 12);
            this.btnHeart.Margin = new System.Windows.Forms.Padding(1);
            this.btnHeart.Name = "btnHeart";
            this.btnHeart.Size = new System.Drawing.Size(48, 48);
            this.btnHeart.TabIndex = 20;
            this.btnHeart.UseVisualStyleBackColor = false;
            // 
            // lblArtistsName
            // 
            this.lblArtistsName.AutoSize = true;
            this.lblArtistsName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblArtistsName.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArtistsName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblArtistsName.Location = new System.Drawing.Point(332, 37);
            this.lblArtistsName.Name = "lblArtistsName";
            this.lblArtistsName.Size = new System.Drawing.Size(70, 15);
            this.lblArtistsName.TabIndex = 19;
            this.lblArtistsName.Text = "Tăng Phúc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Location = new System.Drawing.Point(141, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 37);
            this.label1.TabIndex = 18;
            this.label1.Text = "-";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDuration.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDuration.Location = new System.Drawing.Point(798, 25);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(54, 19);
            this.lblDuration.TabIndex = 17;
            this.lblDuration.Text = "00:00";
            // 
            // lblSongName
            // 
            this.lblSongName.AutoSize = true;
            this.lblSongName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSongName.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblSongName.Location = new System.Drawing.Point(331, 6);
            this.lblSongName.Name = "lblSongName";
            this.lblSongName.Size = new System.Drawing.Size(230, 22);
            this.lblSongName.TabIndex = 16;
            this.lblSongName.Text = "Chỉ là khong cùng nhau";
            // 
            // imgThumbnail
            // 
            this.imgThumbnail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imgThumbnail.Location = new System.Drawing.Point(241, 1);
            this.imgThumbnail.Name = "imgThumbnail";
            this.imgThumbnail.Size = new System.Drawing.Size(60, 60);
            this.imgThumbnail.TabIndex = 15;
            this.imgThumbnail.TabStop = false;
            // 
            // visualiation
            // 
            this.visualiation.Location = new System.Drawing.Point(107, 13);
            this.visualiation.Name = "visualiation";
            this.visualiation.Size = new System.Drawing.Size(100, 40);
            this.visualiation.TabIndex = 22;
            this.visualiation.TabStop = false;
            this.visualiation.Visible = false;
            // 
            // timerVisualiation
            // 
            this.timerVisualiation.Interval = 50;
            // 
            // PlaylistItemUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(15)))), ((int)(((byte)(35)))));
            this.Controls.Add(this.btnEllipsis);
            this.Controls.Add(this.btnHeart);
            this.Controls.Add(this.lblArtistsName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblSongName);
            this.Controls.Add(this.imgThumbnail);
            this.Controls.Add(this.visualiation);
            this.Controls.Add(this.lblSTT);
            this.Name = "PlaylistItemUC";
            this.Size = new System.Drawing.Size(1050, 70);
            ((System.ComponentModel.ISupportInitialize)(this.imgThumbnail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.visualiation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCustomLabel lblSTT;
        private FontAwesome.Sharp.IconButton btnEllipsis;
        private FontAwesome.Sharp.IconButton btnHeart;
        private System.Windows.Forms.Label lblArtistsName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblSongName;
        private System.Windows.Forms.PictureBox imgThumbnail;
        private System.Windows.Forms.PictureBox visualiation;
        private System.Windows.Forms.Timer timerVisualiation;
    }
}
