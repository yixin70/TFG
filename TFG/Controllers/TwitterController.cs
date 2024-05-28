using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using Python.Runtime;
using System.Globalization;
using TFG.Models;
using TFG.Services.Interfaces;
using TFG.ViewModels.Twitter;

namespace TFG.Controllers
{
    public class TwitterController : Controller
    {
        private readonly ITwitterLogService _twitterLogService;

        public TwitterController(ITwitterLogService twitterLogService)
        {
            _twitterLogService = twitterLogService;
        }
        public async Task<IActionResult> Index()
        {
            TwitterIndexVM vm = new TwitterIndexVM()
            {
                Logs = await _twitterLogService.Find()
            };


            return View("Index", vm);
        }

        public async Task<IActionResult> Refresh()
        {
            PythonEngine.Initialize();
            //PythonEngine.Compile(null, "main.py", RunFlagType.File);
            try
            {
                using (Py.GIL())  // Initialize Python engine and acquire the Python Global Interpreter Lock (GIL)
                {
                    PythonEngine.RunSimpleString(@"from twikit import Client
import csv

USERNAME = 'Montess1962'
EMAIL = 'leonardo.montess.1962@gmail.com'
PASSWORD = 'xxx555xxx222'

# Initialize client
client = Client('en-US')

client.login(
    auth_info_1=USERNAME ,
    auth_info_2=EMAIL,
    password=PASSWORD
)

tweets = client.get_user_tweets('1765730312523218944', 'Tweets')


with open('TwiiterLog.csv', 'w', newline='', encoding='utf-8') as csvfile: 
    spamwriter = csv.writer(csvfile, delimiter='¿')
    spamwriter.writerow(['TweetId', 'Text', 'DateTweeted', 'IsSuspicious'])
    
    for tweet in tweets:
        if ""#farmingnmx"" in tweet.text:
            isSuspicious = False
        else:
            isSuspicious = True

        spamwriter.writerow([tweet.id, tweet.text, tweet.created_at, isSuspicious])");
                }

                var path = @"TwiiterLog.csv"; // Habeeb, "Dubai Media City, Dubai"
                using (TextFieldParser csvParser = new TextFieldParser(path))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { "¿" });
                    csvParser.HasFieldsEnclosedInQuotes = true;

                    // Skip the row with the column names
                    csvParser.ReadLine();

                    while (!csvParser.EndOfData)
                    {
                        string[] fields = csvParser.ReadFields();

                        if (await _twitterLogService.FindOne(fields[0]) != null)
                            continue;

                        string format = "ddd MMM dd HH:mm:ss K yyyy";

                        TwitterLog log = new TwitterLog()
                        {
                            TweetId = fields[0],
                            Text = fields[1],
                            DateTweeted = DateTime.ParseExact(fields[2], format, CultureInfo.InvariantCulture),
                            IsSuspicious = bool.Parse(fields[3]),
                        };

                        await _twitterLogService.Save(log);

                    }
                }
            }
            catch (PythonException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                PythonEngine.Shutdown();
            }


            TwitterIndexVM vm = new TwitterIndexVM()
            {
                Logs = await _twitterLogService.Find()
            };


            return View("Index", vm);
        }
    }
}
