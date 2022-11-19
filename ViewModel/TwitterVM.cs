using Botex.database;
using Botex.Model;
using Botex.scripts;
using Botex.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TweetSharp;

namespace Botex.ViewModel
{
    internal class TwitterVM
    {

        private static readonly string welcomeMsg = "Uruchomiono twitter\nWpisz 'wczytaj' aby wczytac twitter\nwpisz 'stworz' aby stworzyc twitter\nwpisz 'wyslij' aby wyslac recznie";


        private string customer_key;
        private string customer_key_secret;
        private string access_token;
        private string access_token_secret;

        

        private  TwitterService service;

        public TwitterVM(   )
        {
            
            PrintDefaultMsg(MainBotexView.myRespodRichTextBox);
            TextBoxDataChanging.textBoxClear(MainBotexView.myInputTextBox);
        }
        

        private void PrintDefaultMsg(RichTextBox targetRichTextBox)
        {
            RichTextBoxDataChanging.changeTextRichAnswerBox(welcomeMsg, targetRichTextBox);
        }

        public void setTweeterLogData(string ck, string cks, string at, string ats)
        {
            //Na starcie ustawiaj to potem wywoluj reszte
            customer_key = ck;
            customer_key_secret = cks;
            access_token = at;
            access_token_secret = ats;
            service = new TwitterService(customer_key, customer_key_secret, access_token, access_token_secret);
        }
        public void saveTweetToDb(int userId, string content, string group)
        {
            ToDbControl.ToDbTweet(userId, content, group);
        }

        public void sendTweetFromDb(string group)
        {
            TweetModel tweetModel = new TweetModel();
            tweetModel = getTweetFromDb( group);
            SendTweet(tweetModel.Content);
        }
        private TweetModel getTweetFromDb(string group)
        {
            return ToDbControl.FromDbTweet(group);
        }

        public void SendTweet(string _status)
        {
            service.SendTweet(new SendTweetOptions { Status = _status });
        }

    }
}
