using UnityEngine;

namespace SmartHospital.Model
{
    [RequireComponent(typeof(Collider))]
    public class ClientInventoryItem : MonoBehaviour
    {
        InventoryItem myItem;

        public InventoryItem MyItem
        {
            get => myItem;
            set
            { myItem = value; }
        }
        private void Awake()
        {
        }



    }

}
