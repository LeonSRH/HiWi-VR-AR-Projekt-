using SmartHospital.Model;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created: 28.03.2019 By KS
/// </summary>
public class RoomInfoCache : MonoBehaviour
{
    #region Server Cache
    HashSet<Department> departments;
    HashSet<string> functionalAreas;
    HashSet<string> professionalGroups;
    /// <summary>
    ///     Placeholder for (Server) Functional Areas, Professional Groups, Departments
    /// </summary>
    public HashSet<string> ServerFunctionalAreas
    {
        get => functionalAreas;
        set => functionalAreas = value;
    }
    public HashSet<Department> ServerDepartments
    {
        get => departments;
        set => departments = value;
    }
    public HashSet<string> ServerProfessionalGroups
    {
        get => professionalGroups;
        set => professionalGroups = value;
    }

    #endregion
    #region Excel Cache

    HashSet<Department> excelDepartments;
    HashSet<FunctionalArea> excelFunctionalAreas;
    HashSet<Professional_Group> excelProfessionalGroups;
    HashSet<string> excelBuildingSections;
    /// <summary>
    ///     Placeholder for (Excel) Functional Areas, Professional Groups, Departments
    /// </summary>
    public HashSet<FunctionalArea> ExcelFunctionalAreas
    {
        get => excelFunctionalAreas;
        set => excelFunctionalAreas = value;
    }
    public HashSet<Department> ExcelDepartments
    {
        get => excelDepartments;
        set => excelDepartments = value;
    }
    public HashSet<Professional_Group> ExcelProfessionalGroups
    {
        get => excelProfessionalGroups;
        set => excelProfessionalGroups = value;
    }

    public HashSet<string> ExcelBuildingSections
    {
        get => excelBuildingSections;
        set => excelBuildingSections = value;
    }

    #endregion
    #region Get Cache
    /// <summary>
    ///     Search for already existing functional areas by name
    /// </summary>
    /// <param name="functionalAreaName"></param>
    /// <returns></returns>
    public string GetFunctionalAreaByName(string functionalAreaName, string type)
    {
        string output = "";

        if (type.Equals("server"))
            foreach (string area in ServerFunctionalAreas)
            {
                if (area.Equals(functionalAreaName))
                {
                    output = area;
                }
            }
        else
            foreach (FunctionalArea area in ExcelFunctionalAreas)
            {
                if (area.Name.Equals(functionalAreaName))
                {
                    output = area.Name;
                }
            }

        return output;
    }

    /// <summary>
    ///     Search for already existing professional groups by name
    /// </summary>
    /// <param name="professionalGroupName"></param>
    /// <returns></returns>
    public string GetProfessionalGroupByName(string professionalGroupName, string type)
    {
        string output = "";

        if (type.Equals("server"))
            foreach (string professionalGroup in ServerProfessionalGroups)
            {
                if (professionalGroup.Equals(professionalGroupName))
                {
                    output = professionalGroup;
                }
            }
        else
            foreach (Professional_Group professionalGroup in ExcelProfessionalGroups)
            {
                if (professionalGroup.Name.Equals(professionalGroupName))
                {
                    output = professionalGroup.Name;
                }
            }

        return output;
    }

    /// <summary>
    ///     Search for already existing department by name
    /// </summary>
    /// <param name="professionalGroupName"></param>
    /// <returns></returns>
    public Department GetDepartmentByName(string departmentName, string type)
    {
        Department output = new Department()
        {
            Name = "Leer",
            CostCentre = 9999,
            FunctionalAreas = new List<FunctionalArea>() { new FunctionalArea() { Name = "Leer", Type = "EMPTY" } }
        };

        if (type.Equals("server"))
            foreach (Department deparment in ServerDepartments)
            {
                if (deparment.Name.Equals(departmentName))
                {
                    output = deparment;
                }
            }
        else
            foreach (Department deparment in ExcelDepartments)
            {
                if (deparment.Name.Equals(departmentName))
                {
                    output = deparment;
                }
            }

        return output;
    }

    public Department GetDepartmentByCostCentre(string costCentre)
    {
        return new Department();
    }
    #endregion

    private void Start()
    {
        departments = new HashSet<Department>();
        functionalAreas = new HashSet<string>();
        professionalGroups = new HashSet<string>();
        excelBuildingSections = new HashSet<string>();
    }
}
