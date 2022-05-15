using SmartHospital.ExplorerMode.Services.JSON;
using SmartHospital.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace SmartHospital.ExplorerMode.Database.Handler
{
    public partial class RESTHandler
    {
        public IEnumerator GetDepartmentsRequest(Action<List<Department>> callback)
        {
            requestIsRunning = true;
            var request = UnityWebRequest.Get($"{ServerAddress}/departments");
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
                callback(DepartmentJsonService.DeserializeDepartments(json));
            }

            request.Dispose();
            requestIsRunning = false;
        }

        public IEnumerator GetDepartmentsRequest(string id, Action<List<Department>> callback)
        {
            requestIsRunning = true;
            var request = UnityWebRequest.Get($"{ServerAddress}/rooms/{id}/departments");
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
                callback(DepartmentJsonService.DeserializeDepartments(json));
            }

            request.Dispose();
            requestIsRunning = false;
        }

        public IEnumerator CreateDepartmentRequest(Department newDepartment)
        {
            requestIsRunning = true;
            var idRequest = GenerateCreateDepartmentRequest(newDepartment);
            SetUpRequestHeader(ref idRequest);
            yield return idRequest.SendWebRequest();

            if (idRequest.isNetworkError || idRequest.isHttpError)
            {
                Debug.LogError(
                    $"Error in CreateDepartment-Create: {idRequest.error} resulting in HTTP error {idRequest.responseCode}");
            }

            idRequest.Dispose();
            requestIsRunning = false;

        }

        public IEnumerator UpdateDepartmentRequest(Department updatedDepartment)
        {
            requestIsRunning = true;
            print($"Updating {updatedDepartment.Name}");
            var request = UnityWebRequest.Put($"{ServerAddress}/departments", "Invalid");
            SetUpRequestHeader(ref request);
            SetRequestData(ref request, DepartmentJsonService.SerializeDepartment(updatedDepartment));
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError($"Error in UpdateDepartment: {request.error} resulting in HTTP error {request.responseCode}");
            }

            requestIsRunning = false;
        }

        UnityWebRequest GenerateCreateDepartmentRequest(Department department)
        {
            var request = UnityWebRequest.Post($"{ServerAddress}/departments", "Invalid");
            SetUpRequestHeader(ref request);
            SetRequestData(ref request, DepartmentJsonService.SerializeDepartment(department));
            return request;
        }

        UnityWebRequest GenerateUpdateRoomRequest(Department department)
        {
            var request = UnityWebRequest.Put($"{ServerAddress}/departments", "Invalid");
            SetUpRequestHeader(ref request);
            SetRequestData(ref request, DepartmentJsonService.SerializeDepartment(department));
            return request;
        }
    }
}