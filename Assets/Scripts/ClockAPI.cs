using UnityEngine;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections;
struct Data
{
    public long time;
}

public class ClockAPI : MonoBehaviour
{

    private string url = "https://yandex.com/time/sync.json";
    private string timeString;

    private void Start()
    {
        StartCoroutine(SendRequestEveryHour());
    }
    IEnumerator SendRequestEveryHour()
    {
        while (!Clock.main.PersonEnteredValues)
        {
            SendRequest();
            yield return new WaitForSeconds(3600); 
        }
    }

    async void SendRequest()
    {
        timeString = "";
        try
        {
            timeString = await GetTimeFromYandex();
            string[] timeParts = timeString.Split(":");

            Clock.main.H = int.Parse(timeParts[0]);
            Clock.main.M = int.Parse(timeParts[1]);
            Clock.main.S = int.Parse(timeParts[2]);
            Clock.main.CanMove = true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error fetching time: " + e.Message);
        }
    }

    private async Task<string> GetTimeFromYandex()
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Data data = JsonUtility.FromJson<Data>(responseBody);
        DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(data.time).DateTime;

        return dateTime.ToString("HH:mm:ss");
    }
}
