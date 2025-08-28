using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using io.harness.cfsdk.client.api;
using io.harness.cfsdk.client.dto;

namespace FeatureFlagDemo.Pages
{
    public class IndexModel : PageModel
    {
        // 10 imported demo IDs from your dark-mode-users group
        private static readonly string[] DemoIds = new[]
        {
            "user_Albert_Einstein",
            "user_Marie_Curie",
            "user_Carl_Sagan",
            "user_David_Attenborough",
            "user_Nikola_Tesla",
            "user_Vincent_van_Gogh",
            "user_Leonardo_da_Vinci",
            "user_Charles_Darwin",
            "user_Ludwig_van_Beethoven",
            "user_Jane_Goodall"
        };

        public bool ShowBetaBanner   { get; set; }
        public bool ShowDarkMode     { get; set; }
        public string CurrentUser    { get; set; } = "";
        public string CurrentLocation{ get; set; } = "";

        public void OnGet(string userId = "userTX")
        {
            // --- Banner Logic (unchanged) ---
            if (userId.Equals("userTX", StringComparison.OrdinalIgnoreCase))
            {
                CurrentUser     = "Test User - Texas";
                CurrentLocation = "Texas";
            }
            else
            {
                CurrentUser     = "Test User - New York";
                CurrentLocation = "New York";
            }

            var bannerTarget = new Target
            {
                Identifier = userId,
                Name       = CurrentUser,
                Attributes = new Dictionary<string,string>
                {
                    { "location", CurrentLocation }
                }
            };

            ShowBetaBanner = CfClient.Instance.boolVariation(
                "My_Test_Flag",
                bannerTarget,
                false
            );
            Console.WriteLine($"[FlagEval] userId={userId}, variation={ShowBetaBanner}");

            // --- Dark Mode Logic (new) ---
            var random = new Random();
            var pick   = DemoIds[random.Next(DemoIds.Length)];

            var darkTarget = new Target
            {
                Identifier = pick,
                Name       = pick
            };

            ShowDarkMode = CfClient.Instance.boolVariation(
                "Enable_Dark_Mode",
                darkTarget,
                false
            );
            Console.WriteLine($"[FlagEval] darkUser={pick}, variation={ShowDarkMode}");
        }
    }
}
