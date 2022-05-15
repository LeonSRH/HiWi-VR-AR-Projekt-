using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SmartHospital.TrainingRoom
{
    public class FocusArrowOnNextPoint : MonoBehaviour
    {
        private bool active;

        public GameObject arrow;

        private GameObject[] points;

        private Transform rotationTarget;

        private GameObject startQuestpoint;


        private void Start()
        {
            active = false;
            points = new GameObject[5];
        }

        private void Update()
        {
            if (active)
            {
                arrow.SetActive(true);
                getClosestWaypoint();
                rotateTowards(rotationTarget);
            }
            else
            {
                arrow.SetActive(false);
            }
        }

        //gets closest waypoint and sets as rotation target
        private void getClosestWaypoint()
        {
            float dist = 1000;
            bool questPointsReady = true;

            foreach (GameObject point in points)
            {
                if (point != null)
                {
                    if (point.GetComponent<QuestTriggerer>().getEnabled())
                    {
                        questPointsReady = false;
                        float distance = Vector3.Distance(arrow.transform.position, point.transform.position);
                        if (dist > distance)
                        {
                            dist = distance;
                            rotationTarget = point.transform;
                        }
                        else if (dist <= 0 && distance > 1)
                        {
                            dist = distance;
                            rotationTarget = point.transform;
                        }
                    }

                }

            }

            if (questPointsReady)
            {
                rotationTarget = startQuestpoint.transform;
            }
        }

        //rotates the arrow to target position
        private void rotateTowards(Transform target)
        {
            Vector3 targetDir = target.position - arrow.transform.position;

            Vector3 newDir = Vector3.RotateTowards(arrow.transform.forward, targetDir, 100f, 100f);

            // Move our position a step closer to the target.
            arrow.transform.rotation = Quaternion.LookRotation(newDir);

        }

        public void setPoints(GameObject[] pointsObj)
        {
            points = pointsObj;
        }

        public void setStartPoint(GameObject start)
        {
            startQuestpoint = start;
        }

        public void setActive(bool status)
        {
            active = status;
        }
    }
}