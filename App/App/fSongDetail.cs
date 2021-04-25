using App.Common;
using App.Models;
using App.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class fSongDetail : UserControl
    {
        private readonly ISongCategoryService _songCategoryService;

        private Song Song;
        private Image thumbnailMain;
        private List<LyricLine> Lyrics;

        private Label PreviousLabel;

        public fSongDetail(Song song)
        {
            InitializeComponent();


            this._songCategoryService = new SongCategoryService();
            this.Song = song;

            Load();

        }

        #region Methods

        new private void Load()
        {
            Lyrics = new List<LyricLine>();

            LoadDetail();

            timerThumbnail.Start();
            timerLyricLeft.Start();
        }

        private async void LoadDetail()
        {
            lblSongName.Text = Song.DisplayName;
            lblSongName.Left = this.Width / 2 - lblSongName.Width / 2;

            var request = WebRequest.Create(Song.Thumbnail);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                thumbnailMain = UIHelper.ClipToCircle(Bitmap.FromStream(stream), Constants.FOOTER_BACKGROUND);
                imgThumbnail.BackgroundImage = thumbnailMain;
            }

            lblCategory.Text = (await _songCategoryService.GetAll()).FirstOrDefault(c => c.ID == Song.CategorySongID)?.DisplayName ?? "Không xác định";

            lblPerformer.Text = Song.Performer;
            lblArtistsNames.Text = Song.ArtistsNames;

            // Get lyric
            HttpClient client = new HttpClient();
            var dataString = await client.GetStringAsync(Song.Lyric);

            Lyrics = dataString.Split('\n').ToList().Select(m =>
            {
                return new LyricLine()
                {
                    // max = 68
                    Time = ConvertToSecond(m.Substring(1, 8)),
                    Line = m.Substring(11)
                };
            }).ToList();

            foreach (var item in Lyrics)
            {
                var lbl = new Label();
                lbl.AutoSize = false;
                lbl.Width = flpLyrics.Width -40;
                lbl.Height = 55;
                lbl.Padding = new Padding(0, 0, 0, 0);
                lbl.Margin = new Padding(0, 0, 0, 0);
                lbl.Text = item.Line;
                lbl.ForeColor = Color.FromArgb(202, 202, 202);
                lbl.Font = new Font("Consolas", 14  , FontStyle.Bold);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.Tag = item.Time;

                flpLyrics.Controls.Add(lbl);
            }

        }

        private double ConvertToSecond(string s)
        {
            // 00:17.42
            double m = double.Parse(s.Substring(0, 2));
            double sec = double.Parse(s.Substring(3, 5));

            return m * 60 + sec;
        }

        private string GetLyricLine(double time)
        {
            try
            {
                for (int i = 0; i < Lyrics.Count; i++)
                {
                    if (i == 0)
                    {
                        if (time <= Lyrics[0].Time)
                        {
                            return "...";
                        }
                    }
                    else if (time >= Lyrics[i - 1].Time && time <= Lyrics[i].Time)
                    {
                        return Lyrics[i - 1].Line;
                    }
                }

                return Lyrics[Lyrics.Count - 1].Line;
            }
            catch (Exception)
            {
                return "...";
            }
        }

        private Label GetLyricLineLabel(double time)
        {
            var list = flpLyrics.Controls;
            var count = list.Count;

            try
            {
                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        if (time <= Lyrics[0].Time)
                        {
                            return null;
                        }
                    }
                    else if (time >= Lyrics[i - 1].Time && time <= Lyrics[i].Time)
                    {
                        return list[i - 1] as Label;
                    }
                }

                return list[count - 1] as Label;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion



        #region Header 

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            Constants.MainForm.WindowState = FormWindowState.Minimized;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // trở về playlist
            if (Constants.CURRENT_PLAYLIST == CURRENT_PLAYLIST.PLAYLIST_PLAYLIST)
            {
                foreach (Control item in Constants.CurrentPlaylist.Controls)
                {
                    item.Visible = true;
                }
                Constants.CurrentPlaylist.panelContent.SendToBack();
                Constants.CurrentPlaylist.panelContent.Controls.Clear();
                Constants.CurrentPlaylistItemUC.Focus();
            }
        }

        private void btnBack_MouseEnter(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.FromArgb(23, 15, 35);
            btnBack.IconColor = Color.FromArgb(255, 34, 101);
            btnBack.ForeColor = Color.FromArgb(255, 34, 101);
        }

        private void btnBack_MouseLeave(object sender, EventArgs e)
        {
            btnBack.BackColor = Color.FromArgb(23, 15, 35);
            btnBack.IconColor = Color.FromArgb(68, 226, 255);
            btnBack.ForeColor = Color.FromArgb(68, 226, 255);
        }


        #endregion

        private void timerThumbnail_Tick(object sender, EventArgs e)
        {
            if (Constants.MainForm.isPlaying())
            {
                imgThumbnail.BackgroundImage = UIHelper.RotateImage(thumbnailMain, Constants.CURRENT_ROTATION);
            }
        }

        private void timerLyricLeft_Tick(object sender, EventArgs e)
        {
            // handle left
            lblLyricLine.Text = GetLyricLine(Constants.MainMedia.Ctlcontrols.currentPosition);
            lblLyricLine.Left = pnlLeft.Width / 2 - lblLyricLine.Width / 2;


            // handle right
            var lbl = GetLyricLineLabel(Constants.MainMedia.Ctlcontrols.currentPosition);
            if(lbl != null)
            {
                if(PreviousLabel != null)
                {
                    PreviousLabel.ForeColor = Color.FromArgb(202, 202, 202);
                }

                PreviousLabel = lbl;

                lbl.ForeColor = Color.FromArgb(68, 226, 255);

                flpLyrics.ScrollControlIntoView(lbl);
            }
        }
    }

    public class LyricLine
    {
        public double Time { get; set; }

        public string Line { get; set; }
    }
}
