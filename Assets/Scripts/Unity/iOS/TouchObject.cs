using UnityEngine;

public class TouchObject : MonoBehaviour
{
    private float rotationRate = 0.5f;

    public GameObject building;

    private bool wasZoomingLastFrame; // Touch mode only
    private Vector2[] lastZoomPositions; // Touch mode only

    private static readonly float ZoomSpeedTouch = 0.1f;

    private static readonly float[] ZoomBounds = new float[] { 10f, 85f };

    private void Update()
    {
        
        switch (Input.touchCount)
        {
            case 1:
                wasZoomingLastFrame = false;

                // get the user touch inpun
                foreach (Touch touch in Input.touches)
                {

                    if (touch.phase == TouchPhase.Began)
                    {
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        building.transform.Rotate(
                            //touch.deltaPosition.y * rotationRate,
                            0, touch.deltaPosition.x * rotationRate, 0, Space.World);
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                    }
                }
                break;
            case 2:

                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!wasZoomingLastFrame)
                {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }
                else
                {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, ZoomSpeedTouch);

                    lastZoomPositions = newPositions;

                }
                break;

        }

        void ZoomCamera(float offset, float speed)
        {
            if (offset == 0)
            {
                return;
            }

            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
        }
    }
}
