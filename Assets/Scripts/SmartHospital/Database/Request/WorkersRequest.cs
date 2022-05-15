using Newtonsoft.Json;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHospital.Database.Request
{
    /// <summary>
    /// Request class for the <see cref="Worker"/> domain class
    /// </summary>
    public class WorkersRequest : Request<Worker>
    {
        public WorkersRequest(HttpRoutes httpRoute, HttpRoutes httpListRoute) : base(httpRoute, httpListRoute)
        {
        }

        public async Task<List<Worker>> GetWorkersByRoomId(string roomId)
        {
            var url = String.Format(ServerAddress + EnumExtensionMethods.GetDescription(HttpRequestType) + "/" + roomId);
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<Worker>>(result);

            return data;
        }

    }
}