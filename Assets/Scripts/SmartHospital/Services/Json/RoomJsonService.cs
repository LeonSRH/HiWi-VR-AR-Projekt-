using SmartHospital.Model;
using Newtonsoft.Json;
using SmartHospital.Controller.ExplorerMode.Rooms;
using System.Collections.Generic;

namespace SmartHospital.ExplorerMode.Services.JSON
{

    public sealed class RoomJsonService : JsonConverter
    {

        public static string SerializeRoom(ServerRoom room)
        {
            return JsonConvert.SerializeObject(room);
        }

        public static ServerRoom DeserializeRoom(string json)
        {
            return JsonConvert.DeserializeObject<ServerRoom>(json);
        }

        public static List<ServerRoom> DeserializeRooms(string json)
        {
            return JsonConvert.DeserializeObject<List<ServerRoom>>(json);
        }

        public static string SerializeRooms(List<ServerRoom> listOfRooms)
        {
            return JsonConvert.SerializeObject(listOfRooms);
        }

        public static string SerializeRoomDetails(ServerRoom details)
        {
            return JsonConvert.SerializeObject(details);
        }

        public static ServerRoom DeserializeRoomDetails(string json)
        {
            return JsonConvert.DeserializeObject<ServerRoom>(json);
        }
    }
}