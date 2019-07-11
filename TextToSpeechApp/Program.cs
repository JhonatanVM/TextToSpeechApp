using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using TextToSpeechApp.Models;

namespace TextToSpeechApp
{
    class Program
    {
        static void Main(string[] args)
        {
            JObject input = GetInputFromUser();

            Console.WriteLine("\nGenerating speech...");
            Console.WriteLine("*****************************************************");

            var result = AlgorithmiaApiCaller.GetSpeechFromAlgorithmia(input.ToString());

            ShowResult(result);
        }

        private static void ShowResult(Response result)
        {
            if (result.IsError)
            {
                Console.WriteLine($"{result.ErrorCode}!! \nIt was not possible to generate the speech...");
            }
            else
            {
                try
                {
                    DownloadMP3File(result);

                    Console.WriteLine($"{result.ErrorCode}!!");

                    Console.WriteLine("\nDo you want to play it now?\n"
                                    + "[Y] - Yes\n"
                                    + "[N] - No");

                    string answer = "";
                    while (answer != "Y" && answer != "N")
                    {
                        answer = Console.ReadLine();
                        answer = answer.ToUpper();
                    }

                    if (answer == "Y")
                    {
                        Console.WriteLine("Here it is...");

                        PlayMP3File();
                    }
                    else
                    {
                        Console.WriteLine("OK...");
                    }
                }
                catch (WebException)
                {
                    Console.WriteLine("An error occurred while downloading data.");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error while downloading the file ...");
                }
                finally
                {
                    Console.WriteLine("Closing Application");
                    Thread.Sleep(1500);
                }
            }
        }

        private static JObject GetInputFromUser()
        {
            string text;
            do
            {
                Console.WriteLine("Write something to be spoken:");
                Console.WriteLine("*****************************************************");
                text = Console.ReadLine();

            } while (text.Length == 0);

            return  new JObject(
                    new JProperty("text", text),
                    new JProperty("voice", "en-US")
                    );
        }

        private static void DownloadMP3File(Response obj)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(new Uri(obj.UrlPath.Path), $"audio.mp3");
            }
        }

        private static void PlayMP3File()
        {
            using (var audioFile = new AudioFileReader(Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.mp3").First()))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
