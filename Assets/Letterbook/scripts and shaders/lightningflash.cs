using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningflash : MonoBehaviour
{
	
	private bool shrink = false;

	void Start ()
	{
		StartCoroutine ("FlashScale");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (shrink)
		{
			transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(0,0,0), Time.deltaTime * 20);
		}
		//else
			//transform.localScale = Vector3.Lerp (transform.localScale, new Vector3(0,0,0), Time.deltaTime * 5);
	}

	IEnumerator FlashScale()
	{
		yield return new WaitForSeconds (0.2f);
		shrink = true;
	}
}
