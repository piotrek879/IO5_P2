using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Botex.Model
{
    internal class TweetModel
    {
        private int idTweet;
        private string content;
        private string group;

        public int IdTweet { get { return idTweet; } set { idTweet = value; } }
        public string Content { get { return content; } set { content = value; } }
        public string Group { get { return group; } set { group = value; } }
    }
}
