using System;
using UnityEngine;

namespace rachael
{
    public class NarratorScript : MonoBehaviour
    {
        public static string Date;

        public static string Time;

        public static string UserName;

        public static string DeviceName;

        public static string WeekDay;

        public static string timeCategory
        {
            get
            {
                string cat;
                int hour = DateTime.Now.Hour;
                cat = hour >= 6 && hour < 12 ? "morning" :
                    hour < 18 && hour >= 12 ? "afternoon" :
                    hour >= 18 && hour < 21 ? "evening" : "night";
           
                return cat;
            }
        }

        public static string activity
        {
            get
            {
                int hour = DateTime.Now.Hour;
                return hour >= 5 && hour < 9 ? "You're up early, aren't you." :
                    hour < 17 && hour >= 9 ?   "Shouldn't you be working at this time? Or is 9 to 5 not your style?" :
                    hour >= 17 && hour < 20 ?  "Now's a good time to go outside, enjoy the daylight... why are you here?" :
                    hour >= 20 && hour < 22 ?  "This time in the evening is perfect for playing video games, I'd say." :
                    "It's awfully late. What's wrong, can't sleep?";
            }
        }
        void Start()
        {
            UserName = Environment.UserName;
            DeviceName = SystemInfo.deviceName;
            WeekDay = System.DateTime.Now.DayOfWeek.ToString();
        
            if(UserName != null)
                Debug.Log("You are defined as" + UserName);
            Debug.Log("Your device name is: " + SystemInfo.deviceName);
        }

        // Update is called once per frame
        void Update()
        {
            Date = System.DateTime.Now.Day + "/" + System.DateTime.Now.Month + "/" + System.DateTime.Now.Year;
            Time = System.DateTime.Now.ToString("h:mm tt");
        }
    }
}