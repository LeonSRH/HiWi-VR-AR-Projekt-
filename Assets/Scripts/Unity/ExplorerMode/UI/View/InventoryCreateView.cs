using System;
using System.Linq;
using UnityEngine;

public partial class InventoryCreateView : MonoBehaviour
{
    string _itemName;
    double _itemCost;
    double _itemSize;
    double _itemMass;
    string _itemDesignation;
    string _itemProducer;
    string _itemProductGroup;
    string _itemCode;

    public event Action OnSaveButtonClick;
    public event Action OnCancelButtonClick;

    public string ItemName
    {
        get
        {
            _itemName = ItemNameInput.text;

            return _itemName;
        }
        set
        {
            _itemName = value;
            if (ItemNameInput != null)
                ItemNameInput.text = _itemName;
        }
    }

    public double ItemCost
    {
        get
        {
            try
            {
                _itemCost = Convert.ToDouble(ItemCostInput.text);
            }
            catch
            {
                Debug.Log("Convert of ItemCost went wrong.");
                throw new NullReferenceException();
            }

            return _itemCost;
        }
        set
        {
            _itemCost = value;
            if (ItemCostInput != null)
                ItemCostInput.text = _itemCost.ToString();
        }
    }

    public string ItemDesignation
    {
        get
        {
            _itemDesignation = ItemDesignationInput.text;

            return _itemDesignation;
        }
        set
        {
            _itemDesignation = value;
            if (ItemDesignationInput != null)
                ItemDesignationInput.text = _itemDesignation;
        }
    }

    public double ItemSize
    {
        get
        {
            try
            {
                _itemSize = Convert.ToDouble(ItemSizeInput.text);
            }
            catch
            {
                Debug.Log($"Convert from Size went wrong");
                return 0;
            }

            return _itemSize;
        }
        set
        {
            _itemSize = value;
            if (ItemSizeInput != null)
                ItemSizeInput.text = _itemSize.ToString();
        }
    }

    public string ItemCode
    {
        get
        {
            _itemCode = ItemCodeInput.text;
            if (_itemCode.Equals(""))
            {
                Debug.Log("ItemCode can not be empty");
                throw new NullReferenceException();
            }
            return _itemCode;
        }
        set
        {
            _itemCode = value;
            if (ItemCodeInput != null)
                ItemCodeInput.text = _itemCode;

        }
    }

    public double ItemMass
    {
        get
        {
            try
            {
                _itemMass = Convert.ToDouble(ItemMassInput.text);
            }
            catch
            {
                throw new Exception("Cannot convert mass into double");
            }

            return _itemMass;
        }
        set
        {
            _itemMass = value;
            if (ItemMassInput != null)
                ItemMassInput.text = _itemMass.ToString();
        }
    }

    public string ItemProductGroup
    {
        get
        {

            _itemProductGroup = ProductGroupDropdown.options[0].text;

            return _itemProductGroup;
        }
        set
        {
            _itemProductGroup = value;
            var index = ProductGroupDropdown.options.Where((data, i) => data.text[0].Equals(_itemProductGroup))
                .Select((data, i) => i).GetEnumerator().Current;
            if (ProductGroupDropdown != null)
                ProductGroupDropdown.value = index;
        }
    }

    public string ItemProducer
    {
        get
        {
            _itemProducer = ItemProducerDropdown.options[0].text;
            return _itemProducer;
        }
        set
        {
            _itemProducer = value;
            var index = ItemProducerDropdown.options.Where((data, i) => data.text[0].Equals(_itemProducer))
                .Select((data, i) => i).GetEnumerator().Current;
            if (ItemProducerDropdown != null)
                ItemProducerDropdown.value = index;
        }
    }
}
