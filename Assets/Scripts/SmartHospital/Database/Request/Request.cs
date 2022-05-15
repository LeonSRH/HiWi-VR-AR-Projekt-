using Newtonsoft.Json;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SmartHospital.Database.Request
{
    /// <summary>
    /// class for server requests
    /// </summary>
    /// <typeparam name="T">parameter of the request obj type</typeparam>
    public abstract class Request<T> where T : new()
    {
        protected HttpClient httpClient { get; set; }

        protected const string ServerAddress = "http://localhost:8081";

        protected HttpRoutes HttpRequestType { get; }

        protected HttpRoutes HttpListRequestType { get; }

        IWebProxy proxy = WebRequest.GetSystemWebProxy();

        ICredentials credentials = new NetworkCredential("admin", "password");

        HttpClientHandler httpHandler;

        public Request(HttpRoutes httpRoute, HttpRoutes httpListRoute)
        {
            HttpRequestType = httpRoute;
            HttpListRequestType = httpListRoute;
        }

        public void SetupHttpClient()
        {
            httpHandler = new HttpClientHandler()
            {
                Proxy = proxy,
                Credentials = credentials
            };

            httpClient = new HttpClient(httpHandler);
        }

        public async Task<bool> Put(T obj)
        {
            var objSerialized = JsonConvert.SerializeObject(obj);
            var content = new StringContent(objSerialized, Encoding.UTF8, "application/json");


            var url = String.Format(ServerAddress + EnumExtensionMethods.GetDescription(HttpRequestType));
            var response = await httpClient.PutAsync(url, content);

            if (response.StatusCode == HttpStatusCode.Accepted)
                return true;
            else
                return false;
        }

        public async Task<bool> PutList(List<T> items)
        {
            string itemsSerialized = JsonConvert.SerializeObject(items);
            var content = new StringContent(itemsSerialized, Encoding.UTF8, "application/json");


            var url = String.Format(ServerAddress + EnumExtensionMethods.GetDescription(HttpRequestType) + EnumExtensionMethods.GetDescription(HttpListRequestType));
            var response = await httpClient.PutAsync(url, content);


            if (response.StatusCode == HttpStatusCode.Accepted)
                return true;
            else
                return false;
        }

        public async Task<T> GetById(string id)
        {

            var url = String.Format(ServerAddress + EnumExtensionMethods.GetDescription(HttpRequestType) + "/" + id);
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<T>(result);

            return data;

        }

        public async Task<List<T>> GetList()
        {
            var url = String.Format(ServerAddress + EnumExtensionMethods.GetDescription(HttpRequestType));
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<T>>(result);

            return data;
        }

        public async Task<bool> DeleteById(string id)
        {

            var url = String.Format(ServerAddress + EnumExtensionMethods.GetDescription(HttpRequestType) + "/" + id);
            var response = await httpClient.DeleteAsync(url);

            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }
    }
}