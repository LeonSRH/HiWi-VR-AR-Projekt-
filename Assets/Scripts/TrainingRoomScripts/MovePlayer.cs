using UnityEngine;

namespace SmartHospital.TrainingRoom {
    public class MovePlayer : MonoBehaviour {
        float angle;


        Animator anim;
        Transform cam;

        Vector2 input;

        Vector3 rotateValue;
        Quaternion targetRotation;
        public float turnSpeed = 2;


        public float velocity = 5;
        float xRotation;
        float yRotation;


        void Start() {
            cam = Camera.main.transform;
            anim = GetComponent<Animator>();
        }

        void Update() {
            GetInput();

            if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) {
                return;
            }

            CalculateDirection();
            Rotate();
            Move();
        }

        void GetInput() {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            anim.SetFloat("BlendX", input.x);
            anim.SetFloat("BlendY", input.y);
        }


        void CalculateDirection() {
            angle = Mathf.Atan2(input.x, input.y);
            angle = Mathf.Rad2Deg * angle;
            angle += cam.eulerAngles.y;
        }


        void Rotate() {
            targetRotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }


        void Move() {
            transform.position += transform.forward * velocity * Time.deltaTime;
        }
    }
}