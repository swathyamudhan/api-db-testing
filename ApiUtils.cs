using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System;

namespace Tests
{
    public class ApiUtils
    {

        public static string getRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
             Console.WriteLine("---------------------httpwebresponse " +response);
            var jsonString = toJSONString(response);
            Console.WriteLine("----------------------jsonstring " + jsonString);
            response.Close();
            return jsonString;
        }

        /* Calls a given endpoint, extracts a token named data, converts it to string and returns it */
        public static string getAllRequest(string url)
        {
            var jsonString = getRequest(url);
            Console.WriteLine("--------------------1nd jsonstring "+jsonString);
            var parsedResponse = JObject.Parse(jsonString);
            Console.WriteLine("--------------------2nd parsedResponse "+parsedResponse);
            return parsedResponse.SelectToken("data").ToString();
        }
        /**
        ** Converts HttpWebResponse to a JSON string
         */
        public static string toJSONString(HttpWebResponse response)
        {
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}