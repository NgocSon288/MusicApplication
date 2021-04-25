using App.Common;
using App.DatabaseLocal.Services;
using App.Models;
using App.Services;
using App.UCs;
using FontAwesome.Sharp;
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

namespace App
{
    public partial class fLayout : Form
    {
        private readonly ISongPersonalService _songPersonalService;

        private IconButton currentBtn;
        private Panel leftBorderBtn;

        private int secondMin;
        private Image thumbnailMain;
        private int rotateThumbnail;

        public fLayout()
        {
            InitializeComponent();

            this._songPersonalService = new SongPersonalService();

            Constants.MainForm = this;
            Constants.MainMedia = media;

            Load();
        }

        #region Methods

        new private async void Load()
        {
            Constants.SongPersonals = _songPersonalService.GetAll();
            Constants.CurrentPlaylist = fPlaylist;

            imgLogo.BackgroundImage = new Bitmap(Constants.ROOT_PATH + "Assets/Images/logo-zing.png");
            imgLogo.BackgroundImageLayout = ImageLayout.Stretch;
            imgThumbnail.BackgroundImage = new Bitmap(Constants.ROOT_PATH + "Assets/Images/thumnail-default.png");
            imgThumbnail.BackgroundImageLayout = ImageLayout.Stretch;

            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 100);
            panelMenu.Controls.Add(leftBorderBtn);

            VisibleButton();

            Reset();

            thumbnailMain = UIHelper.ClipToCircle(imgThumbnail.BackgroundImage, Constants.FOOTER_BACKGROUND);
            imgThumbnail.BackgroundImage = thumbnailMain;

            // note test

            timerThumbnail.Start();
            timerTimeline.Start();
            timerSongName.Start();
        }

        private void ActivateButton(object senderBtn, bool isPlaylist = false)
        {
            if (senderBtn != null)
            {
                fPlaylist.Visible = true;
                panelContent.Visible = !isPlaylist;

                DisableButton();

                //Button transition
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Constants.ACTIVE_BUTTON_BG_COLOR;
                currentBtn.ForeColor = Constants.BORDER_MENU_LEFT_COLOR;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = Constants.BORDER_MENU_LEFT_COLOR;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //Left border button
                leftBorderBtn.BackColor = Constants.BORDER_MENU_LEFT_COLOR;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Constants.MEMU_LEFT_BACKGROUND;
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;

            //Show user control tương ứng
            var fPersonal = new fPersonal();
            UIHelper.ShowControl(fPersonal, panelContent);

            panelContent.Visible = true;
            fPlaylist.Visible = false;
        }

        private void VisibleButton()
        {
            btnPersonal.Visible = true;
            btnSongs.Visible = true;
            btnHistory.Visible = true;
            btnOrderFirst.Visible = true;
            btnOrderSecond.Visible = true;
        }

        public bool isPlaying()
        {
            return media.playState == WMPLib.WMPPlayState.wmppsPlaying;
        }

        public void LoadDataSong(Song song)
        {
            secondMin = 0;
            rotateThumbnail = 0;
            lblSongName.Left = 0;
            media.URL = song.URL;

            var request = WebRequest.Create(song.Thumbnail);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                thumbnailMain = UIHelper.ClipToCircle(Bitmap.FromStream(stream), Constants.FOOTER_BACKGROUND);
                imgThumbnail.BackgroundImage = thumbnailMain;
            }
            lblMinTime.Text = $"{(secondMin / 60).ToString().PadLeft(2, '0')}:{(secondMin % 60).ToString().PadLeft(2, '0')}";
            lblMaxTime.Text = $"{(song.Duration / 60).ToString().PadLeft(2, '0')}:{(song.Duration % 60).ToString().PadLeft(2, '0')}";
            progressBarSongTime.MaximumValue = song.Duration;
            progressBarSongTime.Value = secondMin;

            lblSongName.Text = song.DisplayName;
            lblArtistName.Text = song.ArtistsNames;

            media.Ctlcontrols.play();

            switch (Constants.CURRENT_PAGE)
            {
                case CURRENT_PAGE.PERSONAL:
                    Constants.CURRENT_PLAYLIST = CURRENT_PLAYLIST.PERSONA_PLAYLISTL;
                    break;
                case CURRENT_PAGE.PLAYLIST:
                    Constants.CURRENT_PLAYLIST = CURRENT_PLAYLIST.PLAYLIST_PLAYLIST;
                    break;
                case CURRENT_PAGE.HISTORY:
                    Constants.CURRENT_PLAYLIST = CURRENT_PLAYLIST.HISTORY_PLAYLIST;
                    break;
            }
        }

        public void ClickButtonPauseOrPlay()
        {
            if (isPlaying())
            {
                media.Ctlcontrols.pause();
                btnPlay.IconChar = IconChar.PauseCircle;
            }
            else
            {
                media.Ctlcontrols.play();
                btnPlay.IconChar = IconChar.PlayCircle;
            }
        }

        #endregion


        #region Menu animation

        private void imgLogo_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            Constants.CURRENT_PAGE = CURRENT_PAGE.PERSONAL;

            ActivateButton(sender);

            // Show user control tương ứng
            var fPersonal = new fPersonal();
            UIHelper.ShowControl(fPersonal, panelContent);
        }

        private void btnSongs_Click(object sender, EventArgs e)
        {
            Constants.CURRENT_PAGE = CURRENT_PAGE.PLAYLIST;

            ActivateButton(sender, true);
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            Constants.CURRENT_PAGE = CURRENT_PAGE.HISTORY;

            ActivateButton(sender);

            var fHistory = new fHistory();
            UIHelper.ShowControl(fHistory, panelContent);
        }

        private void btnOrderFirst_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            var fOrder = new fOrder();
            UIHelper.ShowControl(fOrder, panelContent);
        }

        private void btnOrderSecond_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            var fOrder = new fOrder();
            UIHelper.ShowControl(fOrder, panelContent);
        }



        #endregion


        #region Footer animation

        private void timerThumbnail_Tick(object sender, EventArgs e)
        {
            if (isPlaying())
            {
                rotateThumbnail += 3;
                Constants.CURRENT_ROTATION = rotateThumbnail;
                imgThumbnail.BackgroundImage = UIHelper.RotateImage(thumbnailMain, rotateThumbnail);
            }
        }

        private void timerTimeline_Tick(object sender, EventArgs e)
        {
            if (isPlaying())
            {
                secondMin++;
                lblMinTime.Text = $"{(secondMin / 60).ToString().PadLeft(2, '0')}:{(secondMin % 60).ToString().PadLeft(2, '0')}";
                progressBarSongTime.Value = secondMin;
            }
        }

        private void btnHeart_Click(object sender, EventArgs e)
        {
            if (lblSongName.Text == "Tên bài hát")
                return;

            var btn = sender as IconButton;

            if (btn.IconChar == IconChar.Heart)
            {
                btn.IconChar = IconChar.Heartbeat;
                btn.IconColor = Color.FromArgb(144, 0, 161);
            }
            else
            {
                btn.IconChar = IconChar.Heart;
                btn.IconColor = Color.White;
            }
        }

        private void btnRandom_MouseHover(object sender, EventArgs e)
        {
            var btn = sender as IconButton;

            btn.IconColor = Color.FromArgb(89, 85, 96);
            btn.BackColor = Color.FromArgb(18, 12, 28);
        }

        private void btnRandom_MouseDown(object sender, MouseEventArgs e)
        {
            var btn = sender as IconButton;

            btn.IconColor = Color.White;
            btn.BackColor = Color.FromArgb(18, 12, 28);
        }

        private void btnRandom_MouseLeave(object sender, EventArgs e)
        {
            var btn = sender as IconButton;

            btn.IconColor = Color.White;
            btn.BackColor = Color.FromArgb(18, 12, 28);
        }

        private void btnRandom_MouseUp(object sender, MouseEventArgs e)
        {
            var btn = sender as IconButton;

            btn.IconColor = Color.FromArgb(89, 85, 96);
            btn.BackColor = Color.FromArgb(18, 12, 28);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            ClickButtonPauseOrPlay();
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            media.settings.volume = trackBarVolume.Value;
            lblVolume.Text = $"{trackBarVolume.Value}%";

            if (trackBarVolume.Value == 0)
            {
                btnVolume.IconChar = IconChar.VolumeMute;
            }
            else if (trackBarVolume.Value == 100)
            {
                btnVolume.IconChar = IconChar.VolumeUp;
            }
            else
            {
                btnVolume.IconChar = IconChar.VolumeDown;
            }
        }

        private void progressBarSongTime_ValueChanged(object sender, EventArgs e)
        {
            secondMin = progressBarSongTime.Value;
            lblMinTime.Text = $"{(secondMin / 60).ToString().PadLeft(2, '0')}:{(secondMin % 60).ToString().PadLeft(2, '0')}";
            media.Ctlcontrols.currentPosition = progressBarSongTime.Value;
        }

        private void timerSongName_Tick(object sender, EventArgs e)
        {
            if (lblSongName.Text == "Tên bài hát")
                return;

            var width = lblSongName.Width;

            lblSongName.Left -= 2;

            if (lblSongName.Location.X + width <= 0 + 10)
            {
                lblSongName.Left = panelSongInfo.Width;
            }
        }

        #endregion

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            //foreach (Control item in Constants.CurrentPlaylist.Controls)
            //{
            //    item.Visible = true;
            //}
            //Constants.CurrentPlaylist.panelContent.SendToBack();
        }
    }
}
