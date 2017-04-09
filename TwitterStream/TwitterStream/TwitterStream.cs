using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using System.Configuration;
using Tweetinvi.Models;

namespace TwitterStream
{
    class TwitterStream
    {
        static void Main(string[] args)
        {
            // Authentication
            Auth.SetUserCredentials(ConfigurationManager.AppSettings["ConsumerKey"], ConfigurationManager.AppSettings["ConsumerSecret"], ConfigurationManager.AppSettings["AccessToken"], ConfigurationManager.AppSettings["AccessTokenSecret"]);

            // Create the Stream
            var stream = Stream.CreateFilteredStream();

            // Keywords to Track
            stream.AddTrack("KEYWORD_TO_TRACK");

            // Limit to English 
            stream.AddTweetLanguageFilter(LanguageFilter.English);

            // Message so you know its running
            Console.WriteLine("I am listening to Twitter");

            // Called when a tweet maching the tracker is produced
            stream.MatchingTweetReceived += (sender, arguments) =>
            {
                Console.WriteLine(arguments.Tweet.Text);
            };

            stream.StartStreamMatchingAllConditions();           
        }
    }
}
