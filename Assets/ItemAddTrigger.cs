using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemAddTrigger : MonoBehaviour
{

    public Transform AllItemList;
    public Button AddButton;

    private Action ActivateAllItemList;


    // Start is called before the first frame update
    void Start()
    {

        AddButton.onClick.AddListener(() => ActivateAllItemList?.Invoke());

        ActivateAllItemList += () =>
        {

            AllItemList.gameObject.SetActive(!AllItemList.gameObject.activeSelf);
        };

    }


}
