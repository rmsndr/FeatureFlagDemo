using Microsoft.AspNetCore.Mvc.RazorPages;
using io.harness.cfsdk.client.api;
using io.harness.cfsdk.client.dto;

namespace FeatureFlagDemo.Pages
{
    public class IndexModel : PageModel
    {
        public bool ShowBetaBanner { get; set; }
        public string CurrentUser { get; set; } = "";
        public string CurrentLocation { get; set; } = "";

        public void OnGet(string userId = "userTX")
        {
            // Simulate users
            if (userId == "userTX")
            {
                CurrentUser = "Test User - Texas";
                CurrentLocation = "Texas";
            }
            else
            {
                CurrentUser = "Test User - New York";
                CurrentLocation = "New York";
            }

            var target = new Target
            {
                Identifier = userId,
                Name = CurrentUser,
                Attributes = new Dictionary<string, string>
                {
                    { "location", CurrentLocation }
                }
            };

            // Use lower-camel method on the singleton in 1.7.x
            ShowBetaBanner = CfClient.Instance.boolVariation("My_Test_Flag", target, false);
			Console.WriteLine($"[FlagEval] userId={userId}, variation={ShowBetaBanner}");
        }
    }
}
