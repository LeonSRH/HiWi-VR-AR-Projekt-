using System;
using System.Collections;
using System.Collections.Generic;
using SmartHospital.ExplorerMode.Rooms.TagSystem;
using SmartHospital.ExplorerMode.Services.JSON;
using UnityEngine;
using UnityEngine.Networking;

namespace SmartHospital.ExplorerMode.Database.Handler
{

    public partial class RESTHandler
    {
        public IEnumerator GetTagsRequest(Action<List<Tag>> callback)
        {
            requestIsRunning = true;
            var request = UnityWebRequest.Get($"{ServerAddress}/tags");
            SetUpRequestHeader(ref request);
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(
                    $"Error in GetTags: {request.error} resulting in HTTP error {request.responseCode}");
            }
            else
            {
                var json = request.downloadHandler.text;
                callback(TagJsonService.DeserializeTags(json));
            }

            request.Dispose();
            requestIsRunning = false;
        }

        public IEnumerator GetTagsRequest(string id, Action<List<Tag>> callback)
        {
            requestIsRunning = true;
            var request = UnityWebRequest.Get($"{ServerAddress}/rooms/{id}/tags");
            SetUpRequestHeader(ref request);
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(
                    $"Error in UpdateRoomDetails: {request.error} resulting in HTTP error {request.responseCode}");
            }
            else
            {
                var json = request.downloadHandler.text;
                callback(TagJsonService.DeserializeTags(json));
            }

            request.Dispose();
            requestIsRunning = false;
        }

        public IEnumerator CreateTagRequest(Tag newTag)
        {
            requestIsRunning = true;
            print($"Create tag: {newTag.Color}");
            var idRequest = UnityWebRequest.Get($"{ServerAddress}/tags/new_id");
            SetUpRequestHeader(ref idRequest);
            yield return idRequest.SendWebRequest();

            if (idRequest.isNetworkError || idRequest.isHttpError)
            {
                Debug.LogError(
                    $"Error in CreateTag-GetID: {idRequest.error} resulting in HTTP error {idRequest.responseCode}");
            }
            else
            {
                newTag.ID = Convert.ToInt64(idRequest.downloadHandler.text);
                var request = UnityWebRequest.Post($"{ServerAddress}/tags", "Invalid");
                SetUpRequestHeader(ref request);
                SetRequestData(ref request, TagJsonService.SerializeTag(newTag));

                yield return request.SendWebRequest();

                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.LogError(
                        $"Error in CreateTag: {request.error} resulting in HTTP error {request.responseCode}");
                }

                request.Dispose();
            }

            idRequest.Dispose();
            requestIsRunning = false;
        }

        public IEnumerator UpdateTagRequest(Tag updatedTag)
        {
            requestIsRunning = true;
            print($"Updating {updatedTag.Name}, {updatedTag.ID}");
            var request = UnityWebRequest.Put($"{ServerAddress}/tags", "Invalid");
            SetUpRequestHeader(ref request);
            SetRequestData(ref request, TagJsonService.SerializeTag(updatedTag));
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError($"Error in UpdateTag: {request.error} resulting in HTTP error {request.responseCode}");
            }

            requestIsRunning = false;
        }
    }
}