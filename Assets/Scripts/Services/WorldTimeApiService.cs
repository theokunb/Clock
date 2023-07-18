using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class WorldTimeApiService : TimeService
{
    private const string API_URL = "http://worldtimeapi.org/api/ip";

    protected override IEnumerator TaskGetTime(Action successCallback, Action errorCallback)
    {
        WWW request = new WWW(API_URL);

        yield return request;

        if (request.isDone)
        {
            MyTimeDate timeData = JsonConvert.DeserializeObject<MyTimeDate>(request.text);
            CurrentDateTime = ParseDateTime(timeData.dateTime);
            successCallback?.Invoke();
        }
        else
        {
            errorCallback?.Invoke();
        }
    }

    private DateTime ParseDateTime(string dateTime)
    {
        var date = Regex.Match(dateTime, @"^\d{4}-\d{2}-\d{2}").Value;
        var time = Regex.Match(dateTime, @"\d{2}:\d{2}:\d{2}").Value;

        return DateTime.Parse(string.Format("{0} {1}", date, time));
    }

    private struct MyTimeDate
    {
        public string dateTime { get; set; }
    }
}
