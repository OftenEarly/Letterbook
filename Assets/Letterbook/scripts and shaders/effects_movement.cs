using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effects_movement : MonoBehaviour
{
	private Vector3 origSize;
	private bool changeScale = true;
	public Vector3 newSize;
	public float timeMultiplier = 1;
	public bool randomjitter = true;


	// Use this for initialization
	void Start ()
	{
		origSize = transform.localScale;
		if (newSize == Vector3.zero)
		{
			newSize = origSize;
		}
		StartCoroutine ("PopScale");
		//StartCoroutine ("LerpUp");	
	}


	void Update()
	{
		if (changeScale)
		{
			transform.localScale = Vector3.Lerp (transform.localScale, newSize, Time.deltaTime * timeMultiplier);
		}
		else
		{
			transform.localScale = Vector3.Lerp (transform.localScale, origSize, Time.deltaTime * timeMultiplier);
		}
	}

	IEnumerator PopScale()
	{
		if (randomjitter)
		{
			yield return new WaitForSeconds (Random.Range (0.0f / timeMultiplier, 0.5f / timeMultiplier));
			changeScale = true;
			yield return new WaitForSeconds (Random.Range (0.0f / timeMultiplier, 0.5f / timeMultiplier));
			changeScale = false;
			StartCoroutine ("PopScale");
		}
		else
		{
			yield return new WaitForSeconds (timeMultiplier);
			changeScale = true;
			yield return new WaitForSeconds (timeMultiplier);
			changeScale = false;
			StartCoroutine ("PopScale");
		}
	}

}
