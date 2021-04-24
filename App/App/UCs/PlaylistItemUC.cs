using App.Common;
using App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.UCs
{
    public partial class PlaylistItemUC : UserControl
    {
        public static int STT = 1;

        private Song Song;
        private static PlaylistItemUC CurrentPlaylistItem;

        public PlaylistItemUC(Song song)
        {
            InitializeComponent();

            this.Song = song;

            Load();
        }

        private Color EnterColor = Color.FromArgb(45, 37, 55);
        private Color LeaveColor = Color.FromArgb(23, 15, 35);

        private int VisualiationMusicCount = 5;

        #region Methods


        new private void Load()
        {
            this.MouseEnter += PlaylistItemEnter;
            this.MouseLeave += (o, e) =>
            {
                SetPlaylistItemColor(LeaveColor);
            };

            foreach (Control item in this.Controls)
            {
                item.MouseHover += PlaylistItemEnter;
            }


            this.MouseDoubleClick += PlayListItemMouseDoubleClick;

            lblSTT.Text = STT.ToString();
            switch (STT)
            {
                case 1:
                    lblSTT.ForeColor = Constants.COLOR_FIRST;
                    break;
                case 2:
                    lblSTT.ForeColor = Constants.COLOR_SECONDE;
                    break;
                case 3:
                    lblSTT.ForeColor = Constants.COLOR_THRID;
                    break;
                default:
                    lblSTT.ForeColor = Constants.COLOR_DEFAULT;
                    break;
            }

            STT++;

            var request = WebRequest.Create(Song.Thumbnail);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                imgThumbnail.BackgroundImage = Bitmap.FromStream(stream);
            }

            lblSongName.Text = Song.DisplayName;
            lblDuration.Text = $"{(Song.Duration / 60).ToString().PadLeft(2, '0')}:{(Song.Duration % 60).ToString().PadLeft(2, '0')}";
        }

        private void PlayListItemMouseDoubleClick(object sender, MouseEventArgs e)
        {
            PlaylistItemDoubleClick();
        }

        public void PlaylistItemDoubleClick()
        {
            if (!visualiation.Visible)
            {
                timerVisualiation.Start();
                visualiation.Visible = true;

                Reset(CurrentPlaylistItem);
                CurrentPlaylistItem = this;
            }
            else
            {
                timerVisualiation.Stop();
                visualiation.Visible = false;
            }
                
            Constants.MainForm.LoadDataSong(Song);
        }

        public void Reset(PlaylistItemUC item = null)
        {
            if (item != null && item != this)
            {
                item.timerVisualiation.Stop();
                item.visualiation.Visible = false;
            }
        }

        private void PlaylistItemEnter(object sender, EventArgs e)
        {
            SetPlaylistItemColor(EnterColor);
        }

        private void SetPlaylistItemColor(Color color, PlaylistItemUC PlaylistItemUC = null)
        {
            if (PlaylistItemUC == null)
            {
                this.BackColor = color;
                btnHeart.BackColor = color;
                btnEllipsis.BackColor = color;
            }
            else
            {
                PlaylistItemUC.BackColor = color;
                PlaylistItemUC.btnHeart.BackColor = color;
                PlaylistItemUC.btnEllipsis.BackColor = color;
            }
        }

        private void timerVisualiation_Tick(object sender, EventArgs e)
        {
            var urlImg = $"../../Assets/Images/visualiation-{new Random().Next(1, VisualiationMusicCount)}.png";

            visualiation.BackgroundImage = new Bitmap(urlImg);
            visualiation.BackgroundImageLayout = ImageLayout.Stretch;
            visualiation.BringToFront();
        }

        #endregion
    }
}
