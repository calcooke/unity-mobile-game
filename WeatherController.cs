using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {

    public GameObject SnowSystem;
    private const string API_KEY = "2169019c0358e3bb743ccdf56a1012ca";
    public string CityId;
    public int sunriseTime;
    public int sunsetTime;
    //public int currentTime;
    public float daylightFloat;
    public float opacityScale;
    public int actualSunriseValue;
    public int actualSunsetValue;
    public GameObject dayTrees;
    public GameObject daySky;
    
    void Start () {
       
        StartCoroutine(AdjustSkyToWeather());
        
    }
	
	void Update () {

        //Set the opacity of the daytime scenery to be at 1 when the daylightfloat gets to 0.5 (midday),
        // and then start deducting as daylight float get above 0.5 and moves towards 1

        if (daylightFloat <= 0.5){   // "Morning" time

            opacityScale = daylightFloat/0.5f;  //A simplified normalisation formula
            
        }
        else if (daylightFloat > 0.5)  // "Evening" time
        {
            opacityScale = 0.5f/daylightFloat; //A simplified normalisation formula

        }

        
        //Debug.Log("Evening opacity scale is" + opacityScale); //5579687
                                                                //5567595
                                                                //5544655
                                                                //5515868



        //This code sets the opacity/alpha values of the daytime trees and sky to be in sync with the daylight
        // This would be more efficient with an array of daytime objects, and will be implemented if I've time

        Color tempTreeColourData = dayTrees.GetComponent<SpriteRenderer>().color;
        tempTreeColourData.a = opacityScale;
        dayTrees.GetComponent<SpriteRenderer>().color = tempTreeColourData;

        Color tempSkyColourData = daySky.GetComponent<SpriteRenderer>().color;
        tempSkyColourData.a = opacityScale;
        daySky.GetComponent<SpriteRenderer>().color = tempSkyColourData;


    }

    IEnumerator AdjustSkyToWeather()
    {
        while (true)
        {
            string weatherUrl = "api.openweathermap.org/data/2.5/weather?id=4192205&APPID=2169019c0358e3bb743ccdf56a1012ca";   //Russian town value 510291
            WWW weatherWWW = new WWW(weatherUrl);
            yield return weatherWWW;
            //Debug.Log(weatherWWW.text);  //Logging out all the values in the returned URL
            //Debug.Log("Location data has been logged");
            JSONObject tempData = new JSONObject(weatherWWW.text); //Converting the returned values to a JSON object
            //Debug.Log("WWW And has been converted to JSON");
            JSONObject weatherDetails = tempData["weather"];
            JSONObject sunDetails = tempData["sys"];
            //JSONObject timeDetails = tempData["dt"];
            //JSONObject sunRise = tempData["sys"]["sunrise"];
            string WeatherType = weatherDetails[0]["main"].str;
            string sunRise = sunDetails["sunrise"].ToString();
            string sunSet = sunDetails["sunset"].ToString();
            //string theTime = timeDetails.ToString();
            int.TryParse(sunRise, out sunriseTime);   //Converting both sunrise and sunset strings to ints
            int.TryParse(sunSet, out sunsetTime);
            //int.TryParse(theTime, out currentTime);


            //Debug.Log(currentTime.GetType()); Checking if current time is actually an INT now
            //Debug.Log("Current time is   " + theTime); //Logging the current time
            Debug.Log("It is " + WeatherType); //Seeing if its snowing etc.
            //Debug.Log(sunDetails["sunrise"]);  //Logs out the JSON Number
            //Debug.Log(sunRise);  //This is the sunrise JSON number converted to a string
            //Debug.Log("Sunrise is " + sunriseTime);  //This is checking the sunrise string coverted to an int
            Debug.Log("Current time is " + EpochController.instance.currentTime); //Checking the current time
            //Debug.Log("Sunset time is " + sunsetTime);
            //Debug.Log(EpochController.instance.currentTime);
            //Debug.Log(sunriseTime.GetType());  //This is checking if the sunrise time is actually now an int  3098

            // Current time, sunset, sunrise
            createFloat(EpochController.instance.currentTime, actualSunsetValue, actualSunriseValue);  //Openweather sunrise and sunset data is actually wrong, so I have to hardcode the actual values created from www.unixtimestamp.com

            if (WeatherType == "Rain") 
                
            {
                
                SnowSystem.SetActive(true);
                //Debug.Log("Its snowing");
            }
            else
            {
                SnowSystem.SetActive(false);
                //Debug.Log("Its not snowing");
            }

            yield return new WaitForSeconds(60);
        }
    }

    public void findDayLength(int sunset, int sunrise)
    {

        //int dayLength = sunset - sunrise;
       // Debug.Log("Day length is " + dayLength);
        //createFloat(dayLength, sunset, sunrise);

    }

    //Normalising the current time to a float to get a current daylight value.

    public void createFloat(int theTime, int sunset, int sunrise)
    {
        
        if (theTime >= sunrise && theTime <= sunset) {
            daylightFloat = (float)(theTime - sunrise) / (sunset - sunrise);
            Debug.Log("Daylight value is " + daylightFloat);
        }
        else
        {
            daylightFloat = 0f;
            Debug.Log("Daylight value is " + daylightFloat);
        }
        

    }

    
}
