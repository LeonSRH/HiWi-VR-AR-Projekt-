using Newtonsoft.Json;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHospital.Database.Request
{
    /// <summary>
    /// Request class for the inventory
    /// </summary>
    public class InventoryRequest : Request<InventoryItem>
    {
        public InventoryRequest(HttpRoutes httpRoute, HttpRoutes httpListRoute) : base(httpRoute, httpListRoute)
        {
        }

        public async Task<List<InventoryItem>> GetInventoryListForRoom(string roomId)
        {
            var url = String.Format(ServerAddress + EnumExtensionMethods.GetDescription(HttpRequestType) + "/room/" + roomId);
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<InventoryItem>>(result);

            return data;
        }

    }
}