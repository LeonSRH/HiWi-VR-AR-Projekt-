using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using SmartHospital.ExplorerMode.Services.JSON;
using SmartHospital.Common.Collections;

namespace SmartHospital.ExplorerMode.Database.Handler {
    public partial class RESTHandler : MonoBehaviour {
        const string ServerAddress = "localhost:8080";
#pragma warning disable CS0414 // Dem Feld "RESTHandler.requestIsRunning" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        bool requestIsRunning;
#pragma warning restore CS0414 // Dem Feld "RESTHandler.requestIsRunning" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        HttpClient client;

        void Awake() {
            //var messageHandler = 
            client = new HttpClient();
        }

        public void StartRequest(IEnumerator request) {
            throw new NotImplementedException("Moved to EventHandler");
        }

        static void SetUpRequestHeader(ref UnityWebRequest request) {
            throw new NotImplementedException("Moved to EventHandler");
        }

        static void SetRequestData(ref UnityWebRequest request, string data) {
            throw new NotImplementedException("Moved to EventHandler");
        }

        static void StartRequest(IEnumerator request, bool boolean) {
            throw new NotImplementedException("Moved to EventHandler");
        }
    }
}