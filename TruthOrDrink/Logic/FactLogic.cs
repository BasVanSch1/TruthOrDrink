using Newtonsoft.Json;
using TruthOrDrink.Models;

namespace TruthOrDrink.Logic
{
    class FactLogic
    {
        public static async Task<string> GetRandomFact()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(Constants.RANDOM_FACT_API);
                var json = await response.Content.ReadAsStringAsync();
                var fact = JsonConvert.DeserializeObject<Fact>(json);

                return fact == null ? "Could not generate random fact" : fact.text;
            } catch (Exception) // bijvoorbeeld als er geen internet is
            {
                return "Could not generate random fact";
            }
        }
    }
}
