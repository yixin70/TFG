using TFG.Services.Interfaces;
using InstagramApiSharp.API;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using Python.Runtime;
using TFG.Models;

namespace WorkerRefresh
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        public Worker(ILogger<Worker> logger,
                      IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            PythonEngine.Initialize();
            using (Py.GIL())
            {

                while (!stoppingToken.IsCancellationRequested)
                {
                    var tasks = new List<Task>
                    {
                        RefreshInstagram(stoppingToken),
                        RefreshTwitter(stoppingToken)
                    };

                    await Task.WhenAll(tasks);
                }
            }

            PythonEngine.Shutdown();
        }

        private async Task RefreshInstagram(CancellationToken stoppingToken)
        {

            _logger.LogInformation("RefreshInstagram started");
            try
            {

                using (var scope = _serviceProvider.CreateScope())
                {
                    var _instagramApiService = scope.ServiceProvider.GetRequiredService<IInstagramApiService>();
                    var _instagramLogService = scope.ServiceProvider.GetRequiredService<IInstagramLogService>();

                    IInstaApi _instaApi = await _instagramApiService.GetInstance();

                    if (!_instaApi.IsUserAuthenticated)
                        await _instaApi.LoginAsync();

                    var media = await _instaApi.UserProcessor.GetUserMediaAsync("leonardomontes1962", InstagramApiSharp.PaginationParameters.MaxPagesToLoad(6));
                    var user = await _instaApi.UserProcessor.GetCurrentUserAsync();
                    var stories = await _instaApi.StoryProcessor.GetUserStoryAsync(user.Value.Pk);


                    if (media != null)
                    {
                        foreach (var med in media.Value)
                        {
                            await _instagramLogService.SaveInstagramMedia(med);
                        }
                    }

                    if (stories != null)
                    {
                        foreach (var story in stories.Value.Items)
                        {
                            await _instagramLogService.SaveInstagramStory(story);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Instagram");
            }

            _logger.LogInformation("RefreshInstagram completed");
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }

        private async Task RefreshTwitter(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting RefreshTwitter...");

            try
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


                using (var scope = _serviceProvider.CreateScope())
                {
                    var _twitterLogService = scope.ServiceProvider.GetRequiredService<ITwitterLogService>();


                    var path = @"TwiiterLog.csv";
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

                _logger.LogInformation("RefreshTwitter completed");
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
            catch (PythonException ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError(ex, "Error in RefreshTwitter");
            }
        }
    }
}
