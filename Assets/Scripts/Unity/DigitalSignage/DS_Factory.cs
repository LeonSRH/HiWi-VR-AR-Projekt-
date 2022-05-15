using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DS_Factory : MonoBehaviour
{

    public Rigidbody TextElement;
    public Rigidbody ColorField;
    public Rigidbody Icon;

    public TextMeshPro Konsole;
    private string KosolenText;


    public DS_Element[] AllElements;





    // Start is called before the first frame update
    void Start()
    {

        AllElements = new DS_Element[50];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CreateElement( Rigidbody _element)
    {

       Rigidbody clone = Instantiate(_element, transform.position, transform.rotation);
       Add_DS_ElementTo_AllElements(clone);
        Konsole.text = ReturnAllElements();

    }



    string ReturnAllElements()
    {
        string _text = "";

        for (int i = 0; i < 50; i++)
        {

            if (AllElements[i] != null)
            {

                _text += AllElements[i].name + "-" + i.ToString();
                _text += "\n";
            }

        }



        return _text; 
    }


    void Add_DS_ElementTo_AllElements(Rigidbody _element)
    {
        DS_Element _DS_Element = _element.GetComponent<DS_Element>();


        for (int i = 0; i < 50; i++)
        {

            if (AllElements[i] == null)
            {

               
                AllElements[i] = _DS_Element;
                AllElements[i].name = _element.name + i.ToString();


                return;
            }

        }


    }
    

    
}
