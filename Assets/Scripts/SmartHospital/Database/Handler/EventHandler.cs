using SmartHospital.Common.Collections;
using SmartHospital.Database.Request;
using System;
using System.Net.Http;
using System.Text;
using UnityEngine;

namespace SmartHospital.ExplorerMode.Database.Handler
{
    public class EventHandler : MonoBehaviour
    {
#pragma warning disable CS0649 // Dem Feld "EventHandler.currentRequest" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
        RoomRequest currentRequest;
#pragma warning restore CS0649 // Dem Feld "EventHandler.currentRequest" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
        PriorityQueue<RoomRequest> requestQueue;

        void Awake()
        {
            requestQueue = new PriorityQueue<RoomRequest>(2);
        }

        void Update()
        {
            if (requestQueue.Any())
            {
                //StartCoroutine(requestQueue.Dequeue());
                //TODO send to rest handler
            }
        }

        public void StartRequest(RoomRequest request)
        {
            StartRequest(request, false);
        }

        void StartRequest(RoomRequest request, bool highPriority)
        {
            if (request == null)
            {
                Debug.LogError("Request is null");
            }
            else if (requestQueue.Any())
            {
                if (highPriority)
                {
                    requestQueue.Enqueue(request);
                }
                else
                {
                    requestQueue.Enqueue(request, 1);
                }
            }
            else
            {
                //StartCoroutine(request);
                //TODO Send to rest handler
            }
        }

        static string CreateAuthenticationString()
        {
            const string username = "super_hannes";
            const string password = "IBims1GeilerHannes!";
            var auth = username + ":" + password;

            auth = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(auth));
            auth = "Basic " + auth;
            return auth;
        }

        static void SetUpRequestHeader(ref HttpClient client)
        {
            client.DefaultRequestHeaders.Add("AUTHORIZATION", CreateAuthenticationString());
            client.DefaultRequestHeaders.Add("accept", "application/json");
        }
    }
}