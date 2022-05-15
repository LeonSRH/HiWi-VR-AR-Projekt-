using SmartHospital.Model;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.ExplorerMode.Services.JSON;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace SmartHospital.ExplorerMode.Database.Handler
{

    public partial class RESTHandler
    {
        public IEnumerator GetRoomDetailsRequest(ServerRoom room, Action<ServerRoom> callback)
        {
            requestIsRunning = true;
            var request = UnityWebRequest.Get($"{ServerAddress}/rooms/{room.RoomName}/details");
            SetUpRequestHeader(ref request);

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {

                if (request.responseCode == 404)
                {
                    Debug.LogWarning($"Room {room.RoomName} was not found on server. Starting upload request");

                    StartRequest(CreateRoomRequest(room), true);

                    StartRequest(GetRoomDetailsRequest(room, callback), true);
                }
                else
                {
                    Debug.LogError(
                        $"Error in GetRoomDetails: {request.error} resulting in HTTP error {request.responseCode}");
                    callback(null);
                }
            }
            else
            {
                var json = request.downloadHandler.text;
                callback(RoomJsonService.DeserializeRoomDetails(json));
            }

            request.Dispose();
            requestIsRunning = false;
        }

        public IEnumerator UpdateRoomDetailsRequest(ServerRoom room, ServerRoom roomDetails)
        {
            requestIsRunning = true;
            var request = UnityWebRequest.Put($"{ServerAddress}/rooms/{room.RoomName}/details", "Invalid");
            SetUpRequestHeader(ref request);
            SetRequestData(ref request, RoomJsonService.SerializeRoomDetails(roomDetails));
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                if (request.responseCode == 404)
                {
                    Debug.LogWarning($"Room {room.RoomName} was not found on server. Starting upload request");

                    StartRequest(CreateRoomRequest(room), true);

                    StartRequest(UpdateRoomDetailsRequest(room, roomDetails), true);
                }
                else
                {
                    Debug.LogError(
                        $"Error in UpdateRoomDetails: {request.error} resulting in HTTP error {request.responseCode}");
                }
            }

            request.Dispose();
            requestIsRunning = false;
        }
    }

}