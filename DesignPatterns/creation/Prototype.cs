using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.creation
{
    class WebPage : ICloneable
    {
        public string Url { get; set; }
        public string HTMLContent { get; set; }
        public WebPage(string url)
        {
            Url = url;
            using (var client = new HttpClient())
            {
                HTMLContent = client.GetStringAsync(url).GetAwaiter().GetResult();
            }
        }

        public object Clone()
            => this.MemberwiseClone();
    }

    class LogicalTest : ICloneable
    {
        public bool ToggleOne { get; set; }
        public bool ToggleTwo { get; set; }
        public bool ToggleThree { get; set; }
        public int LightOne { get; set; }
        public int LightTwo { get; set; }

        public LogicalTest()
        {
            ToggleOne = false;
            ToggleTwo = true;
            ToggleThree = false;
            LightOne = 0;
            LightTwo = 1;
        }

        public void SwitchLightToBlue()
        {
            ToggleTwo = false;
            ToggleThree = true;
            LightOne = 2;
            LightTwo = 1;
        }
        public void SwitchSoundOn()
        {
            ToggleTwo = true;
            ToggleOne = true;
        }
        public void DiscoAmbiance()
        {
            if (ToggleOne)
            {
                ToggleTwo = false;
                ToggleThree = !ToggleThree;
            }
            else
            {
                LightTwo -= LightOne + 6;
                ToggleThree = !ToggleTwo;
            }
        }

        public object Clone() => this.MemberwiseClone();
    }

    class Reader
    {

        public void Read()
        {
            var googlePage = new WebPage("http://www.google.com");

            var google2 = googlePage.Clone();
        }

        public void LogicalTest()
        {
            var test =new LogicalTest();
            test.SwitchLightToBlue();
            test.SwitchSoundOn();
            test.DiscoAmbiance();

            var test2 = test.Clone();

        }
    }
}
