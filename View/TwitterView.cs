using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace Botex.View
{
    class TwitterView
    {
        private static string customer_key = "";
        private static string customer_key_secret = "";
        private static string access_token = "";
        private static string access_token_secret = "";

        private static TwitterService service = new TwitterService(customer_key, customer_key_secret, access_token, access_token_secret);

        public TwitterView()
        {
            SendTweet("hello World");
        }

        private static void SendTweet(string _status)
        {
            service.SendTweet(new SendTweetOptions { Status = _status });
        }

    }

}
