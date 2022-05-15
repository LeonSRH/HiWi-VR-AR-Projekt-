using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationExample : MonoBehaviour {

	[Header("OBJECT")]
	public GameObject notificationObject;
	public Animator notificationAnimator;

	[Header("OBJECT")]
	public Text titleObject;
	public Text descriptionObject;

	[Header("VARIABLES")]
	public string titleText;
	public string descriptionText;
	public string animationNameIn;
	public string animationNameOut;

#pragma warning disable CS0414 // Dem Feld "NotificationExample.isPlayed" wurde ein Wert zugewiesen, der aber nie verwendet wird.
	private bool isPlayed = false;
#pragma warning restore CS0414 // Dem Feld "NotificationExample.isPlayed" wurde ein Wert zugewiesen, der aber nie verwendet wird.

	void Start()
	{
		notificationObject.SetActive (false);
	}

	public void ShowNotification () 
	{
		notificationObject.SetActive (true);
		titleObject.text = titleText;
		descriptionObject.text = descriptionText;

		notificationAnimator.Play (animationNameIn);
		notificationAnimator.Play (animationNameOut);
	
	}
}
