using Newtonsoft.Json;
using SimpleWebRequests.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection;

namespace SimpleWebRequests
{
    /// <summary>Class for REST requests</summary>
    public class RESTRequest
    {
        #region Dynamic methods

        /// <summary>GET request to the api that responses in JSON format</summary>
        /// <param name="url">String with url and parameters for GET request</param>
        /// <returns>Dynamic object with response data</returns>
        public static dynamic GetWithJsonResponse(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                }
                else
                    throw new HttpRequestException(string.Format("Error in {0} with param url: {1}", url, MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>GET request to the api that responses in JSON format</summary>
        /// <param name="url">Url of request</param>
        /// <param name="requestParameters">Parameters for GET request as key-value pairs</param>
        /// <returns>Dynamic object with response data</returns>
        public static dynamic GetWithJsonResponse(string url, IEnumerable<KeyValuePair<string, string>> requestParameters)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string urlWithParams = string.Format("{0}?{1}", url, KeyValueToStringConverter.Convert(requestParameters));
                HttpResponseMessage response = client.GetAsync(urlWithParams).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                }
                else
                    throw new HttpRequestException(string.Format("Error in {0} with param url: {1}", url, MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>Async GET request to the api that responses in JSON format</summary>
        /// <param name="url">String with url and parameters for GET request</param>
        /// <returns>Dynamic object with response data</returns>
        public static Task<dynamic> GetWithJsonResponseAsync(string url)
        {
            return Task.Run(() => GetWithJsonResponse(url));
        }

        /// <summary>Async GET request to the api that responses in JSON format</summary>
        /// <param name="url">Url of request</param>
        /// <param name="requestParameters">Parameters for GET request as key-value pairs</param>
        /// <returns>Dynamic object with response data</returns>
        public static Task<dynamic> GetWithJsonResponseAsync(string url, IEnumerable<KeyValuePair<string, string>> requestParameters)
        {
            return Task.Run(() => GetWithJsonResponse(url, requestParameters));
        }

        /// <summary>GET request to the api that responses in text/html format</summary>
        /// <param name="url">String with url and parameters for GET request</param>
        /// <returns>String with response data</returns>
        public static string GetWithHtmlResponse(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsStringAsync().Result;
                else
                    throw new HttpRequestException(string.Format("Error in {0} with param url: {1}", url, MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>GET request to the api that responses in text/html format</summary>
        /// <param name="url">String with url</param>
        /// <param name="requestParameters">Parameters for GET request as key-value pairs</param>
        /// <returns>String with response data</returns>
        public static string GetWithHtmlResponse(string url, IEnumerable<KeyValuePair<string, string>> requestParameters)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

                string urlWithParams = string.Format("{0}?{1}", url, KeyValueToStringConverter.Convert(requestParameters));
                HttpResponseMessage response = client.GetAsync(urlWithParams).Result;
                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsStringAsync().Result;
                else
                    throw new HttpRequestException(string.Format("Error in {0} with param url: {1}", url, MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>Async GET request to the api that responses in text/html format</summary>
        /// <param name="url">String with url and parameters for GET request</param>
        /// <returns>String with response data</returns>
        public static Task<string> GetWithHtmlResponseAsync(string url)
        {
            return Task.Run(() => GetWithHtmlResponse(url));
        }

        /// <summary>Async GET request to the api that responses in text/html format</summary>
        /// <param name="url">String with url</param>
        /// <param name="requestParameters">Parameters for GET request as key-value pairs</param>
        /// <returns>String with response data</returns>
        public static Task<string> GetWithHtmlResponseAsync(string url, IEnumerable<KeyValuePair<string, string>> requestParameters)
        {
            return Task.Run(() => GetWithHtmlResponse(url, requestParameters));
        }

        /// <summary>GET request to the api that responses in XML format</summary>
        /// <param name="url">String with url and parameters for GET request</param>
        /// <returns>Dynamic object with response data</returns>
        public static dynamic GetWithXmlResponse(string url)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            var xDoc = XDocument.Load(request.GetResponse().GetResponseStream());

            return XMLToDynamicConverter.Convert(xDoc.Elements().First());
        }

        /// <summary>GET request to the api that responses in XML format</summary>
        /// <param name="url">String with url</param>
        /// <param name="requestParameters">Parameters for GET request as key-value pairs</param>
        /// <returns>Dynamic object with response data</returns>
        public static dynamic GetWithXmlResponse(string url, IEnumerable<KeyValuePair<string, string>> requestParameters)
        {
            string urlWithParams = string.Format("{0}?{1}", url, KeyValueToStringConverter.Convert(requestParameters));
            var request = WebRequest.Create(urlWithParams) as HttpWebRequest;
            request.Credentials = CredentialCache.DefaultNetworkCredentials;
            var xDoc = XDocument.Load(request.GetResponse().GetResponseStream());

            return XMLToDynamicConverter.Convert(xDoc.Elements().First());
        }

        /// <summary>Async GET request to the api that responses in XML format</summary>
        /// <param name="url">String with url and parameters for GET request</param>
        /// <returns>Dynamic object with response data</returns>
        public static Task<dynamic> GetWithXmlResponseAsync(string url)
        {
            return Task.Run(() => GetWithXmlResponse(url));
        }

        /// <summary>Async GET request to the api that responses in XML format</summary>
        /// <param name="url">String with url</param>
        /// <param name="requestParameters">Parameters for GET request as key-value pairs</param>
        /// <returns>Dynamic object with response data</returns>
        public static Task<dynamic> GetWithXmlResponseAsync(string url, IEnumerable<KeyValuePair<string, string>> requestParameters)
        {
            return Task.Run(() => GetWithXmlResponse(url, requestParameters));
        }

        /// <summary>POST request with body of "x-www-form-urlencoded" type to the api that responses in JSON</summary>
        /// <param name="url">String with url for POST request</param>
        /// <param name="postRequestBody">Body of POST request that will be serialized to "application/x-www-form-urlencoded" media type</param>
        /// <returns>Dynamic object with response data</returns>
        public static dynamic PostAsUrlEncodedWithJsonResponse(string url, IEnumerable<KeyValuePair<string, string>> postRequestBody)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(url, new FormUrlEncodedContent(postRequestBody)).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                }
                else
                    throw new HttpRequestException(string.Format("Error in {0} with params url: {1} and body: {2} ", MethodBase.GetCurrentMethod().Name, url, postRequestBody.ToString()));
            }
        }

        /// <summary>Async POST request with body of "x-www-form-urlencoded" type to the api that responses in JSON</summary>
        /// <param name="url">String with url for POST request</param>
        /// <param name="postRequestBody">Body of POST request that will be serialized to "application/x-www-form-urlencoded" media type</param>
        /// <returns>Dynamic object with response data</returns>
        public static Task<dynamic> PostAsUrlEncodedWithJsonResponseAsync(string url, IEnumerable<KeyValuePair<string, string>> postRequestBody)
        {
            return Task.Run(() => PostAsUrlEncodedWithJsonResponse(url, postRequestBody));
        }

        /// <summary>POST request with body of JSON type to the api that responses in JSON</summary>
        /// <param name="url">String with url for POST request</param>
        /// <param name="postRequestBody">Body of POST request that will be serialized to JSON</param>
        /// <returns>Dynamic object with response data</returns>
        public static dynamic PostAsJsonWithJsonResponse(string url, object postRequestBody)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(postRequestBody), Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return JsonConvert.DeserializeObject<ExpandoObject>(response.Content.ReadAsStringAsync().Result);
                }
                else
                    throw new HttpRequestException(string.Format("Error in {0} with params url: {1} and body: {2} ", MethodBase.GetCurrentMethod().Name, url, postRequestBody.ToString()));
            }
        }

        /// <summary>Async POST request with body of JSON type to the api that responses in JSON</summary>
        /// <param name="url">String with url for POST request</param>
        /// <param name="postRequestBody">Body of POST request that will be serialized to JSON</param>
        /// <returns>Dynamic object with response data</returns>
        public static Task<dynamic> PostAsJsonWithJsonResponseAsync(string url, object postRequestBody)
        {
            return Task.Run(() => PostAsJsonWithJsonResponse(url, postRequestBody));
        }

        /// <summary>DELETE request to the api that responses in JSON format</summary>
        /// <param name="url">String with url and parameters for DELETE request</param>
        /// <returns>Dynamic object with response data</returns>
        public static dynamic DeleteWithJsonResponse(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                HttpResponseMessage response = client.DeleteAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                }
                else
                    throw new HttpRequestException(string.Format("Error in {0} with param url: {1}", url, MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>Async DELETE request to the api that responses in JSON format</summary>
        /// <param name="url">String with url and parameters for DELETE request</param>
        /// <returns>Dynamic object with response data</returns>
        public static Task<dynamic> DeleteWithJsonResponseAsync(string url)
        {
            return Task.Run(() => DeleteWithJsonResponse(url));
        }

        #endregion


        #region Generic methods

        /// <summary>GET request to the api that responses in JSON format</summary>
        /// <param name="url">String with url and parameters for GET request</param>
        /// <returns>Object of given generic type with response data</returns>
        public static T GetWithJsonResponse<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                else
                    throw new HttpRequestException(string.Format("Error in {0} with param url: {1}", url, MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>GET request to the api that responses in JSON format</summary>
        /// <param name="url">String with url</param>
        /// <param name="requestParameters">Parameters for GET request as key-value pairs</param>
        /// <returns>Object of given generic type with response data</returns>
        public static T GetWithJsonResponse<T>(string url, IEnumerable<KeyValuePair<string, string>> requestParameters)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string urlWithParams = string.Format("{0}?{1}", url, KeyValueToStringConverter.Convert(requestParameters));
                HttpResponseMessage response = client.GetAsync(urlWithParams).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                else
                    throw new HttpRequestException(string.Format("Error in {0} with param url: {1}", url, MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>Async GET request to the api that responses in JSON format</summary>
        /// <param name="url">String with url and parameters for GET request</param>
        ///<returns>Object of given generic type with response data</returns>
        public static Task<T> GetWithJsonResponseAsync<T>(string url)
        {
            return Task.Run(() => GetWithJsonResponse<T>(url));
        }

        /// <summary>Async GET request to the api that responses in JSON format</summary>
        /// <param name="url">String with url</param>
        /// <param name="requestParameters">Parameters for GET request as key-value pairs</param>
        /// <returns>Object of given generic type with response data</returns>
        public static Task<T> GetWithJsonResponseAsync<T>(string url, IEnumerable<KeyValuePair<string, string>> requestParameters)
        {
            return Task.Run(() => GetWithJsonResponse<T>(url, requestParameters));
        }

        /// <summary>POST request with body of "x-www-form-urlencoded" type to the api that responses in JSON</summary>
        /// <param name="url">String with url for POST request</param>
        /// <param name="postRequestBody">Body of POST request that will be serialized to "application/x-www-form-urlencoded" media type</param>
        /// <returns>Object of given generic type with response data</returns>
        public static T PostAsUrlEncodedWithJsonResponse<T>(string url, IEnumerable<KeyValuePair<string, string>> postRequestBody)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(url, new FormUrlEncodedContent(postRequestBody)).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                else
                    throw new HttpRequestException(string.Format("Error in {0} with params url: {1} and body: {2} ", MethodBase.GetCurrentMethod().Name, url, postRequestBody.ToString()));
            }
        }

        /// <summary>Async POST request with body of "x-www-form-urlencoded" type to the api that responses in JSON</summary>
        /// <param name="url">String with url for POST request</param>
        /// <param name="postRequestBody">Body of POST request that will be serialized to "application/x-www-form-urlencoded" media type</param>
        /// <returns>Object of given generic type with response data</returns>
        public static Task<T> PostAsUrlEncodedWithJsonResponseAsync<T>(string url, IEnumerable<KeyValuePair<string, string>> postRequestBody)
        {
            return Task.Run(() => PostAsUrlEncodedWithJsonResponse<T>(url, postRequestBody));
        }

        /// <summary>POST request with body of JSON type to the api that responses in JSON</summary>
        /// <param name="url">String with url for POST request</param>
        /// <param name="postRequestBody">Body of POST request that will be serialized to JSON</param>
        /// <returns>Object of given generic type with response data</returns>
        public static T PostAsJsonWithJsonResponse<T>(string url, object postRequestBody)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(postRequestBody), Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                else
                    throw new HttpRequestException(string.Format("Error in {0} with params url: {1} and body: {2} ", MethodBase.GetCurrentMethod().Name, url, postRequestBody.ToString()));
            }
        }

        /// <summary>Async POST request with body of JSON type to the api that responses in JSON</summary>
        /// <param name="url">String with url for POST request</param>
        /// <param name="postRequestBody">Body of POST request that will be serialized to JSON</param>
        /// <returns>Object of given generic type with response data</returns>
        public static Task<T> PostAsJsonWithJsonResponseAsync<T>(string url, object postRequestBody)
        {
            return Task.Run(() => PostAsJsonWithJsonResponse<T>(url, postRequestBody));
        }

        /// <summary>DELETE request to the api that responses in JSON format</summary>
        /// <param name="url">String with url and parameters for DELETE request</param>
        /// <returns>Object of given generic type with response data</returns>
        public static T DeleteWithJsonResponse<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                HttpResponseMessage response = client.DeleteAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    response.Content.Headers.ContentType.MediaType = "application/json";
                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }
                else
                    throw new HttpRequestException(string.Format("Error in {0} with param url: {1}", url, MethodBase.GetCurrentMethod().Name));
            }
        }

        /// <summary>Async DELETE request to the api that responses in JSON format</summary>
        /// <param name="url">String with url and parameters for DELETE request</param>
        /// <returns>Dynamic object with response data</returns>
        public static Task<T> DeleteWithJsonResponseAsync<T>(string url)
        {
            return Task.Run(() => DeleteWithJsonResponse<T>(url));
        }

        #endregion
    }
}
