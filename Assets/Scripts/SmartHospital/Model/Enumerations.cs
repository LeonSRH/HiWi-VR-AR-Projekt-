using System;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

/// <summary>
/// Enumerations of the Data Room Model
/// Created: 20.03.2019 By KS
/// </summary>
namespace SmartHospital.Controller.ExplorerMode.Rooms.Details
{
    public enum ApplicationMode
    {
        INVENTORY,
        ROOM,
        SIGN,
        NAVIGATION,
        NONE
    }
    public enum TrafficLightColor
    {
        RED,
        YELLOW,
        GREEN,
        GREY
    }

    public enum Form_Of_Adress
    {
        NONE,
        MR,
        MRS
    }

    public enum Title
    {
        [Description("")] NONE,
        [Description("Prof.")] PROF,
        [Description("Doc.")] DOC,
        [Description("Prof.-Dr.")] PROF_DOC,
        [Description("PD.-Dr.")] PD_DOC,
        [Description("Prof. Dr. Dr.")] PROF_DOC_DOC,
        [Description("PD Dr. Dr.")] PD_DOC_DOC
    }

    public enum Function_Type
    {
        EMPTY,
        MANAGEMENT,
        TECHNICAL,
        NURSING,
        TREATMENT_AND_INVESTIGATION,
        GENERAL,
        LOGISTICS,
        RESEARCH_TEACHING,
        OTHER
    }

    public enum Floor_Area
    {
        EMPTY,
        A,
        B,
        C,
        D,
        E,
        F
    }

    public enum Style
    {
        NONE,
        STYLE1,
        STYLE2,
        STYLE3,
        STYLE4,
        STYLE5
    }

    public enum Pictogramm
    {
        NONE,
        WOMEN,
        MEN,
        W_AND_M,
        DISABLED,
        WAITING,
        WC_SHOWER_DISABLED,
        DIAPER_CHANGING
    }

    public enum Access_Style
    {
        NONE,
        ELECTRICAL,
        MECHANICAL
    }

    public enum HttpRoutes
    {
        [Description("/")] EMPTY,
        [Description("/rooms")] ROOMS = 1,
        [Description("/inventory")] INVENTORY = 2,
        [Description("/workers")] WORKERS = 3,
        [Description("/workersList")] WORKERSLIST = 4,
        [Description("/inventoryList")] INVENTORYLIST = 5,
        [Description("/roomList")] ROOMLIST = 6
    }

    public static class EnumExtensionMethods
    {
        public static string GetDescription(Enum GenericEnum)
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (_Attribs != null && _Attribs.Count() > 0)
                {
                    return ((DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }

            return GenericEnum.ToString();
        }
    }
}