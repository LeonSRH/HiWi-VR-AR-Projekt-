﻿using UnityEngine;

namespace UnityStandardAssets.Utility
{
	public class SmoothFollow : MonoBehaviour
	{

		// The target we are following
		[SerializeField]
#pragma warning disable CS0649 // Dem Feld "SmoothFollow.target" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
		private Transform target;
#pragma warning restore CS0649 // Dem Feld "SmoothFollow.target" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
		// The distance in the x-z plane to the target
		[SerializeField]
		private float distance = 10.0f;
		// the height we want the camera to be above the target
		[SerializeField]
		private float height = 5.0f;

		[SerializeField]
#pragma warning disable CS0649 // Dem Feld "SmoothFollow.rotationDamping" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "0".
		private float rotationDamping;
#pragma warning restore CS0649 // Dem Feld "SmoothFollow.rotationDamping" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "0".
		[SerializeField]
#pragma warning disable CS0649 // Dem Feld "SmoothFollow.heightDamping" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "0".
		private float heightDamping;
#pragma warning restore CS0649 // Dem Feld "SmoothFollow.heightDamping" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "0".

		// Use this for initialization
		void Start() { }

		// Update is called once per frame
		void LateUpdate()
		{
			// Early out if we don't have a target
			if (!target)
				return;

			// Calculate the current rotation angles
			var wantedRotationAngle = target.eulerAngles.y;
			var wantedHeight = target.position.y + height;

			var currentRotationAngle = transform.eulerAngles.y;
			var currentHeight = transform.position.y;

			// Damp the rotation around the y-axis
			currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

			// Damp the height
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

			// Convert the angle into a rotation
			var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

			// Set the position of the camera on the x-z plane to:
			// distance meters behind the target
			transform.position = target.position;
			transform.position -= currentRotation * Vector3.forward * distance;

			// Set the height of the camera
			transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

			// Always look at the target
			transform.LookAt(target);
		}
	}
}