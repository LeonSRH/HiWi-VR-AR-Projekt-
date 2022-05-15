using System.Collections;
using UnityEngine;

/// <summary>
///     1. Follow on player's X/Z plane
///     2. Snooth rotations around the player in 45 degree increments
/// </summary>
public class PlayerCameraController : MonoBehaviour {
    public float moveSpeed = 5;
    public Vector3 offsetPos;
    bool smoothRotating;
    public float smoothSpeed = 0.5f;

    public Transform target;
    Vector3 targetPos;

    Quaternion targetRotation;
    public float turnSpeed = 5;

    void Update() {
        MoveWithTarget();
        LookAtTarget();

        if (Input.GetKeyDown(KeyCode.G) && !smoothRotating) {
            StartCoroutine("RotateAroundTarget", 20);
        }

        if (Input.GetKeyDown(KeyCode.H) && !smoothRotating) {
            StartCoroutine("RotateAroundTarget", -20);
        }
    }

    void LookAtTarget() {
        targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void MoveWithTarget() {
        targetPos = target.position + offsetPos;
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    IEnumerator RotateAroundTarget(float angle) {
        var vel = Vector3.zero;
        var targetOffsetPos = Quaternion.Euler(0, angle, 0) * offsetPos;
        var dist = Vector3.Distance(offsetPos, targetOffsetPos);
        smoothRotating = true;

        while (dist > 0.02f) {
            offsetPos = Vector3.SmoothDamp(offsetPos, targetOffsetPos, ref vel, smoothSpeed);
            dist = Vector3.Distance(offsetPos, targetOffsetPos);
            yield return null;
        }

        smoothRotating = false;
        offsetPos = targetOffsetPos;
    }
}