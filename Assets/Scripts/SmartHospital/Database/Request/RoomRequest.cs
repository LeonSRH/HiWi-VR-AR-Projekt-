using Newtonsoft.Json;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHospital.Database.Request
{
    /// <summary>
    /// Request class for ServerRoom actions
    /// </summary>
    public class RoomRequest : Request<ServerRoom>
    {
        public RoomRequest(HttpRoutes httpRoute, HttpRoutes httpListRoute) : base(httpRoute, httpListRoute)
        {
        }

        /// <summary>
        /// TODO: Export to DepartmentRequest
        /// </summary>
        /// <returns>returns a List of all Departments</returns>
        public async Task<List<Department>> GetDepartments()
        {
            var url = String.Format(ServerAddress + EnumExtensionMethods.GetDescription(HttpRequestType) + "/departments");
            var response = await httpClient.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<List<Department>>(result);

            return data;

        }
    }
}