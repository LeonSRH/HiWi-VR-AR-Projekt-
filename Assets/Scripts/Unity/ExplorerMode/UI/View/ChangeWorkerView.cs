using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <inheritdoc />
/// <summary>
/// Change Worker View for the <see cref="SmartHospital.Model.Worker" /> struct.
/// Created 22.05.2019 by KS
/// </summary>
public class ChangeWorkerView : MonoBehaviour
{
    string _title;
    string _firstName;
    string _lastName;
    int _cardNumber;
    int _employeeNumber;
    string _function;
    string _department;
    bool _hasWorkspace;
    Worker _worker;

    public event Action OnSaveButtonClicked;
    public event Action OnCancelButtonClicked;

    // Edit Only
    public TMP_Dropdown TitleDropdown { get; set; }
    public TMP_InputField FirstNameInput { get; set; }
    public TMP_InputField LastNameInput { get; set; }
    public TMP_InputField CardNumberInput { get; set; }
    public TMP_InputField EmployeeNumberInput { get; set; }
    public TMP_InputField FunctionInput { get; set; }
    public TMP_Dropdown DepartmentDropdown { get; set; }

    public TMP_Dropdown HasWorkspaceDropdown { get; set; }

    //Parent object
    public Transform ChangeWorkerPanel { get; set; }

    //Buttons
    public Button CancelButton { get; set; }
    public Button SaveButton { get; set; }

    LoadCSVRoomData roomDetailsExcelInfo;
    HashSet<string> departments = new HashSet<string>();

    public bool IsActive { get; set; }

    void Start()
    {
        // Setup Buttons
        SaveButton.onClick.AddListener(() => OnSaveButtonClicked?.Invoke());
        CancelButton.onClick.AddListener(() => OnCancelButtonClicked?.Invoke());

        OnCancelButtonClicked += ChangeWorkerView_OnCancelButtonClicked;
        OnSaveButtonClicked += ChangeWorkerView_OnCancelButtonClicked;


        //Load data for Dropdowns
        roomDetailsExcelInfo = FindObjectOfType<LoadCSVRoomData>();

        foreach (Department dp in roomDetailsExcelInfo.Departments)
        {
            if (!dp.Name.Equals(""))
                departments.Add(dp.Name);
        }

        SetupDropDowns();
        ChangeWorkerPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Fills dropdown with existing data
    /// </summary>
    void SetupDropDowns()
    {
        //Fill Department Dropdown
        if (DepartmentDropdown != null)
        {
            DepartmentDropdown.ClearOptions();
            DepartmentDropdown.AddOptions(departments.ToList());
        }

        //Fill TitleDropdown
        if (TitleDropdown != null)
        {
            var values = Enum.GetValues(typeof(Title)).Cast<Title>();
            List<string> options = new List<string>();

            foreach (Title title in values)
            {
                options.Add(title.ToString());
            }

            if (TitleDropdown != null)
            {
                TitleDropdown.ClearOptions();
                TitleDropdown.AddOptions(options);
            }

        }

        HasWorkspaceDropdown.ClearOptions();
        HasWorkspaceDropdown.AddOptions(new List<string>() { "Ja", "Nein" });
    }


    /// <summary>
    /// Action function for disable the change worker view window
    /// </summary>
    private void ChangeWorkerView_OnCancelButtonClicked()
    {
        ChangeWorkerPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Worker in the change worker view
    /// </summary>
    public Worker Worker
    {
        get
        {
            Worker outputWorker = new Worker
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Title = this.Title,
                Employee_Id = this.EmployeeNumber,
                Staff_Id = this.CardNumber,
                Function = this.Function,
                HasWorkspace = this.HasWorkspace


            };
            _worker = outputWorker;
            return _worker;
        }
        set
        {
            _worker = value;
            FirstName = _worker.FirstName;
            LastName = _worker.LastName;
            Title = _worker.Title;
            EmployeeNumber = _worker.Employee_Id;
            CardNumber = _worker.Staff_Id;
            Function = _worker.Function;
            if (_worker.Department != null)
                Department = _worker.Department.Name;
            HasWorkspace = _worker.HasWorkspace;

        }
    }


    #region Worker Attrubutes

    public bool HasWorkspace
    {
        get
        {
            var indexText = HasWorkspaceDropdown.options[0].text;
            if (indexText.Equals("Ja"))
                _hasWorkspace = true;

            else
                _hasWorkspace = false;

            return _hasWorkspace;

        }
        set
        {
            _hasWorkspace = value;
            var index = 0;
            switch (_hasWorkspace)
            {
                case true:
                    index = HasWorkspaceDropdown.options.FindIndex((i) => { return i.text.Equals("Ja"); });
                    break;
                case false:
                    index = HasWorkspaceDropdown.options.FindIndex((i) => { return i.text.Equals("Nein"); });
                    break;
            }
            HasWorkspaceDropdown.value = index;
            HasWorkspaceDropdown.Select();

        }
    }
    public string Title
    {
        get
        {
            _title = TitleDropdown.options[0].text;
            return _title;
        }
        set
        {
            _title = value;
            if (TitleDropdown != null)
            {
                var index = TitleDropdown.options.FindIndex((i) => { return i.text.Equals(_title); });
                TitleDropdown.value = index;
                TitleDropdown.Select();
            }
        }
    }

    public string FirstName
    {
        get
        {
            _firstName = FirstNameInput.text;
            return _firstName;
        }
        set
        {
            _firstName = value;
            if (FirstNameInput != null)
                FirstNameInput.text = _firstName;
        }
    }

    public string LastName
    {
        get
        {
            _lastName = LastNameInput.text;
            return _lastName;
        }
        set
        {
            _lastName = value;
            if (LastNameInput != null)
                LastNameInput.text = _lastName;
        }
    }

    public int CardNumber
    {
        get
        {
            _cardNumber = Convert.ToInt32(CardNumberInput.text);
            return _cardNumber;
        }
        set
        {
            _cardNumber = value;
            if (CardNumberInput != null)
                CardNumberInput.text = _cardNumber.ToString();


        }
    }

    public int EmployeeNumber
    {
        get
        {
            _employeeNumber = Convert.ToInt32(EmployeeNumberInput.text);
            return _employeeNumber;
        }
        set
        {
            _employeeNumber = value;
            if (EmployeeNumberInput != null)
                EmployeeNumberInput.text = _employeeNumber.ToString();
        }
    }

    public string Function
    {
        get
        {
            _function = FunctionInput.text;
            return _function;
        }
        set
        {
            _function = value;
            if (FunctionInput != null)
                FunctionInput.text = _function;
        }
    }

    public string Department
    {
        get
        {
            _department = DepartmentDropdown.options[0].text;
            return _department;
        }
        set
        {
            _department = value;

            if (DepartmentDropdown != null)
            {

                var index = DepartmentDropdown.options.FindIndex((i) => { return i.text.Equals(_department); });
                DepartmentDropdown.value = index;
                DepartmentDropdown.Select();
            }
        }
    }

    #endregion
}