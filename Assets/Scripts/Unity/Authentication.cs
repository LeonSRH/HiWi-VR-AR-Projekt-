using System;
using TMPro;
using UnityEngine;

public class Authentication : MonoBehaviour
{
     MainView mainView;

    public static string UserName { get;  set; }

    // Start is called before the first frame update
    void Start()
    {
        mainView = GameObject.FindObjectOfType<MainView>();
        //string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        mainView.UserName = getName();
    }



    private void Update()
    {
       /// mainView.UserName = getName();
    }

    private static string getName()
    {
        UserName = Environment.UserName;
        return UserName;
    }
}
