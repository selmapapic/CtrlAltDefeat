using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using DigitalNomads.Data;
using DigitalNomads.Models.Statistics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DigitalNomads.Controllers
{

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class StatisticsController : Controller
    {
        // GET: /<controller>/
        public IActionResult MeditationInfo()
        {
            return View();

        }

        private CtrlAltDefeatDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StatisticsController(CtrlAltDefeatDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            this._dbContext = dbContext;
            this._httpContextAccessor = httpContextAccessor;
        }


        public async Task<IActionResult> CalculateMeditationNeed(MeditationInfoReq req)
        {
            if(req.Day == 0 || req.Day == 6)
            {
                req.Day = 1;
            }

            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"Hours of sleep", "Day of week", "Hours from previous break", "Current mood", "Hours from previous meditation"},
                                Values = new string[,] {  { req.HoursOfSleep.ToString(), req.Day.ToString(), req.HoursFromPreviousBreak.ToString(), req.CurrentMood.ToString(), req.HoursFromPreviousMeditation.ToString() },  { "0", "0", "0", "0", "0" },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "gCPcBiHl3nqnXscWEu03gE0zcv7kvfAyoY5r5UyYGn+OtFDniijD1ml5bRiSDRKVrnIlR46N8J2Ouxz4yqmUIA=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/dfac26d08b5740aa9f52c2ee56a84c7d/services/1183180dfa4a463389d3a532ac1e8049/execute?api-version=2.0&details=true");



                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    string[] splitThis = result.Split('"');
                    string finalResult = splitThis[splitThis.Length - 18];
                   

                    int statisticData = Convert.ToInt32(finalResult);
                    if(statisticData == 1)
                    {
                        StatisticsResult st = new StatisticsResult
                        {
                            Message1 = "Mental health and peaceful mind is really important.",
                            Message2 = "You should go meditate and relax yourself"
                        };
                        return View("StatisticsResult", st);
                    }
                    else
                    {
                        StatisticsResult st = new StatisticsResult
                        {
                            Message1 = "It is great to see that you worry about your mental health.",
                            Message2 = "Keep working, and come back in few hours."
                        };
                        return View("StatisticsResult", st);
                    }
                 
                        
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }

                return View();

                
            }
        }
    }
}
