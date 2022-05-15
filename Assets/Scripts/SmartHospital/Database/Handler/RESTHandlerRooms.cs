using System;
using System.Collections;
using System.Collections.Generic;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.ExplorerMode.Services.JSON;
using UnityEngine;
using UnityEngine.Networking;

namespace SmartHospital.ExplorerMode.Database.Handler
{

    public partial class RESTHandler
    {
        public IEnumerator GetRoomsRequest(Action<List<ServerRoom>> callback)
        {
            requestIsRunning = true;
            var request = UnityWebRequest.Get($"{ServerAddress}/rooms");
            SetUpRequestHeader(ref request);

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError($"Error in GetRooms: {request.error} resulting in HTTP error {request.responseCode}");
            }
            else
            {
                var json = request.downloadHandler.text;
                print(json);
                callback(RoomJsonService.DeserializeRooms(json));
            }

            request.Dispose();
            requestIsRunning = false;
        }

        public IEnumerator GetRoomRequest(string id, Action<ServerRoom> callback)
        {
            requestIsRunning = true;
            var request = UnityWebRequest.Get($"{ServerAddress}/rooms/{id}");
            SetUpRequestHeader(ref request);

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError($"Error in GetRoom: {request.error} resulting in HTTP error {request.responseCode}");
            }
            else
            {
                var json = request.downloadHandler.text;
                callback(RoomJsonService.DeserializeRoom(json));
            }

            request.Dispose();
            requestIsRunning = false;
        }


        public IEnumerator CreateRoomRequest(ServerRoom room)
        {
            requestIsRunning = true;
            var request = GenerateCreateRoomRequest(room);
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError($"Error in CreateRoom: {request.error} resulting in HTTP error {request.responseCode}");
            }

            request.Dispose();
            requestIsRunning = false;
        }

        public IEnumerator UpdateRoomRequest(ServerRoom room)
        {
            requestIsRunning = true;
            var request = GenerateUpdateRoomRequest(room);
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError($"Error in UpdateRoom: {request.error} resulting in HTTP error {request.responseCode}");
            }

            request.Dispose();
            requestIsRunning = false;
        }

        UnityWebRequest GenerateCreateRoomRequest(ServerRoom room)
        {
            var request = UnityWebRequest.Post($"{ServerAddress}/rooms", "Invalid");
            SetUpRequestHeader(ref request);
            SetRequestData(ref request, RoomJsonService.SerializeRoom(room));
            return request;
        }

        UnityWebRequest GenerateUpdateRoomRequest(ServerRoom room)
        {
            var request = UnityWebRequest.Put($"{ServerAddress}/rooms", "Invalid");
            SetUpRequestHeader(ref request);
            SetRequestData(ref request, RoomJsonService.SerializeRoom(room));
            return request;
        }
    }
}