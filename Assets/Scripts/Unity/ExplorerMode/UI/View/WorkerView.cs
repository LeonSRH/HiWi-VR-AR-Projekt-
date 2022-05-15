using SmartHospital.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <inheritdoc />
/// <summary>
/// View for the <see cref="T:SmartHospital.Model.Worker" /> struct.
/// Colab commit comment! 
/// </summary>
public class WorkerView : MonoBehaviour
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

    public event Action OnEditButtonClicked;

    // View Only
    public TextMeshProUGUI TitleText { get; set; }
    public TextMeshProUGUI FirstNameText { get; set; }
    public TextMeshProUGUI LastNameText { get; set; }
    public TextMeshProUGUI CardNumberText { get; set; }
    public TextMeshProUGUI EmployeeNumberText { get; set; }
    public TextMeshProUGUI FunctionText { get; set; }
    public TextMeshProUGUI DepartmentText { get; set; }

    public TextMeshProUGUI HasWorkspaceText { get; set; }

    public Transform WorkerViewPanel { get; set; }
    public Button EditButton;

    public ChangeWorkerView changeWorkerView;

    void Start()
    {
        changeWorkerView = FindObjectOfType<ChangeWorkerView>();
        // Setup Buttons
        EditButton.onClick.AddListener(() => OnEditButtonClicked?.Invoke());


        OnEditButtonClicked += () =>
        {
            changeWorkerView.ChangeWorkerPanel.gameObject.SetActive(!changeWorkerView.ChangeWorkerPanel.gameObject.activeSelf);
            changeWorkerView.Worker = Worker;
        };

    }

    public Worker Worker
    {
        get
        {
            var roomScriptManager = FindObjectOfType<RoomInfoCache>();
            _worker = new Worker
            {
                FirstName = FirstName,
                LastName = LastName,
                Title = Title,
                Employee_Id = EmployeeNumber,
                Staff_Id = CardNumber,
                Function = Function,
                Department = roomScriptManager.GetDepartmentByName(Department, "client"),
                HasWorkspace = HasWorkspace
            };

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
            Department = _worker.Department.Name;
            HasWorkspace = _worker.HasWorkspace;

        }
    }

    public bool IsActive { get; set; }

    public bool HasWorkspace
    {
        get
        {
            if (HasWorkspaceText.text.Equals("Ja"))
                _hasWorkspace = true;
            else
                _hasWorkspace = false;
            return _hasWorkspace;
        }
        set
        {
            _hasWorkspace = value;
            if (_hasWorkspace)
                HasWorkspaceText.text = "Ja";
            else
                HasWorkspaceText.text = "Nein";
        }
    }
    public string Title
    {
        get
        {
            _title = TitleText.text;
            return _title;
        }
        set
        {
            _title = value;
            if (TitleText != null)
                TitleText.text = _title;
        }
    }

    public string FirstName
    {
        get
        {
            _firstName = FirstNameText.text;
            return _firstName;
        }
        set
        {
            _firstName = value;

            if (FirstNameText != null)
                FirstNameText.text = _firstName;
        }
    }

    public string LastName
    {
        get
        {
            _lastName = LastNameText.text;
            return _lastName;
        }
        set
        {
            _lastName = value;

            if (LastNameText != null)
                LastNameText.text = _lastName;
        }
    }

    public int CardNumber
    {
        get
        {
            _cardNumber = Convert.ToInt32(CardNumberText.text);
            return _cardNumber;
        }
        set
        {
            _cardNumber = value;

            if (CardNumberText != null)
                CardNumberText.SetText(_cardNumber.ToString());

        }
    }

    public int EmployeeNumber
    {
        get
        {
            _employeeNumber = Convert.ToInt32(EmployeeNumberText.text);
            return _employeeNumber;
        }
        set
        {
            _employeeNumber = value;
            if (EmployeeNumberText != null)
                EmployeeNumberText.SetText(_employeeNumber.ToString());
        }
    }

    public string Function
    {
        get
        {
            _function = FunctionText.text;
            return _function;
        }
        set
        {
            _function = value;
            if (FunctionText != null)
                FunctionText.SetText(_function);
        }
    }

    public string Department
    {
        get
        {
            _department = DepartmentText.text;
            return _department;
        }
        set
        {
            _department = value;
            if (DepartmentText != null)
                DepartmentText.SetText(_department);
        }
    }
}