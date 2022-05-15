using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using System;

namespace RoomSizeCalculation
{

    public static class RoomSizeCalculator
    {
        
        public static TrafficLightColor CalculateTrafficLightColor(string size, int numberOfWorkers)
        {
            double roomSize = 0;
            if (!string.IsNullOrWhiteSpace(size))
                roomSize = Convert.ToDouble(size);

            if (numberOfWorkers == 0)
                return TrafficLightColor.GREY;

            double result = 0;
            for (int i = 1; i <= numberOfWorkers; i++)
            {
                if (i == 1)
                    result += 8;
                else
                    result += 6;
            }


            if (roomSize < result)
                if (((roomSize / 100) * 15) > roomSize - result)
                    return TrafficLightColor.YELLOW;

            if (roomSize > result)
                return TrafficLightColor.GREEN;

            return TrafficLightColor.RED;
        }
    }

}
