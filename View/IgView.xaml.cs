﻿using InstagramApiSharp.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Botex.scripts;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Logger;
using InstagramApiSharp;

namespace Botex.View
{
    /// sesja uzytkownika
    public partial class IgView : Window
    {
        private static UserSessionData user;

        public IgView()
        {
            InitializeComponent();
        }
        private async void loginButton_Click(object sender, EventArgs e)
        {
            user = new UserSessionData();
            user.UserName = loginInputBox.Text;
            user.Password = passwordInputBox.Text;
            IgApiClass.api = InstaApiBuilder.CreateBuilder()
                .SetUser(user)
                .UseLogger(new DebugLogger(LogLevel.All))
                .SetRequestDelay(RequestDelay.FromSeconds(0, 1))
                .Build();
            var IsLog = await IgApiClass.api.LoginAsync();
            if (IsLog.Succeeded)
            {
                MessageBox.Show("True");

            }
            else
            {
                MessageBox.Show("False");
            }

        }

        private async void followersButton_Click(object sender, EventArgs e)
        {
            var fs = await IgApiClass.api.UserProcessor.GetCurrentUserFollowersAsync(PaginationParameters.MaxPagesToLoad(1));
            foreach(var item in fs.Value)
            {
                
            }
        }
    }
}
