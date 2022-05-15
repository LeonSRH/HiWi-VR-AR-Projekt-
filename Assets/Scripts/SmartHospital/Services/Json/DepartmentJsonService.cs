using Newtonsoft.Json;
using SmartHospital.Model;
using System.Collections.Generic;
using System.Linq;

namespace SmartHospital.ExplorerMode.Services.JSON
{

    public sealed class DepartmentJsonService : JsonConverter
    {

        public static string SerializeDepartment(Department department)
        {
            return JsonConvert.SerializeObject(department);
        }

        public static Department DeserializeDepartment(string json)
        {
            return JsonConvert.DeserializeObject<Department>(json);
        }

        public static List<Department> DeserializeDepartments(string json)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Department>>(json).ToList();
        }
    }
}