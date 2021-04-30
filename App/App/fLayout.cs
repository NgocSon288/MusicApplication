﻿using App.Common;
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

        #region Events

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            NextOrPrevious(false);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            NextOrPrevious();
        }

        #endregion

        #region Methods

        new private async void Load()
        {
            Constants.SongPersonals = _songPersonalService.GetAll();
            Constants.CurrentPlaylist = fPlaylist;
            Constants.CurrentPersonal = fPersonal;
            btnNext.Click += BtnNext_Click;
            btnPrevious.Click += BtnPrevious_Click;
            btnRandom.Click += BtnRandom_Click;
            

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

        public void NextOrPrevious(bool isNext = true, bool isRandom = false)
        {
            // find next PlayListItemUC in this;
            if (Constants.CURRENT_SONG_PLAYING == CURRENT_SONG_PLAYING.PLAYLIST_SONG_PLAYING)
            {
                if (Constants.CurrentPlaylistItemUC != null && Constants.IsPlaulistReady)
                {
                    var index = 0;
                    foreach (PlaylistItemUC item in fPlaylist.flpPlaylist.Controls)
                    {
                        if (Constants.CurrentPlaylistItemUC == item)
                        {
                            break;
                        }
                        index++;
                    }
                    // find next index
                    if (!isRandom)
                    {
                        if (isNext)
                        {
                            index = index == fPlaylist.flpPlaylist.Controls.Count - 1 ? 0 : index + 1;
                        }
                        else
                        {
                            index = index == 0 ? fPlaylist.flpPlaylist.Controls.Count - 1 : index - 1;
                        }
                    }
                    else
                    {
                        var i = index;
                        while (i == index)
                        {
                            i = new Random().Next(0, fPlaylist.flpPlaylist.Controls.Count);
                        }

                        index = i;
                    }


                    // find next PlayListItemPUC
                    var itemUC = fPlaylist.flpPlaylist.Controls[index] as PlaylistItemUC;
                    itemUC.PlayListItemMouseDoubleClick(itemUC, null);

                    // Load SongDetail
                    if (Constants.CurrentPlaylist.panelContent.Controls.Count > 0)
                    {
                        fSongDetail fSongDetail = new fSongDetail(itemUC.Song);
                        UIHelper.ShowControl(fSongDetail, Constants.CurrentPlaylist.panelContent);
                    }
                }
            }
            else if (Constants.CURRENT_SONG_PLAYING == CURRENT_SONG_PLAYING.PERSONA_SONG_PLAYING)
            {
                if (Constants.CurrentPlaylistItemPUC != null && Constants.IsPersonalReady)
                {
                    var index = 0;
                    foreach (PlaylistItemPUC item in fPersonal.flpPlaylist.Controls)
                    {
                        if (Constants.CurrentPlaylistItemPUC == item)
                        {
                            break;
                        }
                        index++;
                    }

                    // find next index 
                    if (!isRandom)
                    {
                        if (isNext)
                        {
                            index = index == fPersonal.flpPlaylist.Controls.Count - 1 ? 0 : index + 1;
                        }
                        else
                        {
                            index = index == 0 ? fPersonal.flpPlaylist.Controls.Count - 1 : index - 1;
                        }
                    }
                    else
                    {
                        var i = index;
                        while (i == index)
                        {
                            i = new Random().Next(0, fPersonal.flpPlaylist.Controls.Count);
                        }

                        index = i;
                    }
                    // find next PlayListItemPUC
                    var itemPUC = fPersonal.flpPlaylist.Controls[index] as PlaylistItemPUC;
                    itemPUC.PlayListItemMouseDoubleClick(itemPUC, null);

                    // Load SongDetail
                    if (Constants.CurrentPersonal.panelContent.Controls.Count > 0)
                    {
                        fSongDetail fSongDetail = new fSongDetail(itemPUC.Song);
                        UIHelper.ShowControl(fSongDetail, Constants.CurrentPersonal.panelContent);
                    }
                }
            }
        }

        private void ActivateButton(object senderBtn)
        {
            if (senderBtn != null)
            {
                // panelContent.Visible = true;

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

            ResetRoot();
        }

        private void ResetRoot()
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
                Constants.CurrentPlaylistItemUC?.Focus();
            }
            else if (Constants.CURRENT_PLAYLIST == CURRENT_PLAYLIST.PERSONA_PLAYLISTL)
            {
                foreach (Control item in Constants.CurrentPersonal.Controls)
                {
                    item.Visible = true;
                }
                Constants.CurrentPersonal.panelContent.SendToBack();
                Constants.CurrentPersonal.panelContent.Controls.Clear();
                Constants.CurrentPlaylistItemPUC?.Focus();
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

            // UIHelper.ShowControl(fPersonal, panelContent);

            fPersonal.Visible = true;
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
            if (song.URL == media.URL)
            {
                return;
            }

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
            lblMinTime.Text = $"{(0 / 60).ToString().PadLeft(2, '0')}:{(0 % 60).ToString().PadLeft(2, '0')}";
            lblMaxTime.Text = $"{(song.Duration / 60).ToString().PadLeft(2, '0')}:{(song.Duration % 60).ToString().PadLeft(2, '0')}";
            progressBarSongTime.MaximumValue = song.Duration;
            progressBarSongTime.Value = 0;

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

            fPersonal.Visible = true;
            //panelContent.Controls.Clear();
            fPlaylist.Visible = false;
        }

        private void btnSongs_Click(object sender, EventArgs e)
        {
            Constants.CURRENT_PAGE = CURRENT_PAGE.PLAYLIST;

            ActivateButton(sender);

            fPlaylist.Visible = true;
            // panelContent.Visible = false;
            fPersonal.Visible = false;
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            Constants.CURRENT_PAGE = CURRENT_PAGE.HISTORY;

            ActivateButton(sender);

            var fHistory = new fHistory();
            // UIHelper.ShowControl(fHistory, panelContent);
        }

        private void btnOrderFirst_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            var fOrder = new fOrder();
            // UIHelper.ShowControl(fOrder, panelContent);
        }

        private void btnOrderSecond_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);

            var fOrder = new fOrder();
            // UIHelper.ShowControl(fOrder, panelContent);
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            NextOrPrevious(true, true);
        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            //foreach (Control item in Constants.CurrentPlaylist.Controls)
            //{
            //    item.Visible = true;
            //}
            //Constants.CurrentPlaylist.panelContent.SendToBack(); 
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
                var second = (int)media.Ctlcontrols.currentPosition;
                lblMinTime.Text = $"{(second / 60).ToString().PadLeft(2, '0')}:{(second % 60).ToString().PadLeft(2, '0')}";
                progressBarSongTime.Value = second;
            }

            
            var duration = media?.Ctlcontrols?.currentPosition;
            var duration1 = Constants.CurrentPlaylistItemPUC?.Song.Duration;
            var duration2 = Constants.CurrentPlaylistItemUC?.Song.Duration;
            if ((Constants.CurrentPlaylistItemPUC !=null && (int)duration == duration1) || (Constants.CurrentPlaylistItemUC != null && (int)duration == duration2))
            {
                MessageBox.Show("finished");
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
            var second = progressBarSongTime.Value;
            lblMinTime.Text = $"{(second / 60).ToString().PadLeft(2, '0')}:{(second % 60).ToString().PadLeft(2, '0')}";
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
    }
}
