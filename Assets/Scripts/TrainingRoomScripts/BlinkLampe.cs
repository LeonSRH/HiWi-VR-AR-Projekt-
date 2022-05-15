using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital.TrainingRoom
{
    public class BlinkLampe : MonoBehaviour
    {
        private int state;
        public Material red;
        public Material grey;


        IEnumerator NewState()
        {
            yield return new WaitForSeconds(1.0f);
            int rnd = Random.Range(0, 3);

            SetStateNumber(rnd);

        }

        void ChangeMaterialBasedOnState()
        {
            //======================================== Grey
            if (state == 0)
            {
                GetComponent<Renderer>().material = grey;

            }

            //======================================== Green
            if (state == 1)
            {
                GetComponent<Renderer>().material.color = Color.green;
            }

            //======================================== Red
            if (state == 2)
            {
                GetComponent<Renderer>().material = red;

            }


        }

        public void SetStateNumber(int st)
        {

            state = st;
            ChangeMaterialBasedOnState();
        }



    }
}