using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storm : MonoBehaviour
{

	private ParticleSystem ps;
	public List <GameObject> _list;
	public GameObject cloud;
	public GameObject stormCloud;
	public GameObject lightning;
	public Material stormCloudMat;

	public int cloudDensity = 200;

	public Transform innerDome;
	public Transform outerDome;

	public float spawnRadius = 1500;

	public float lowerSize = 1;
	public float upperSize = 5;

	public Color darkSkyColour;
	public Color darkCloudColour;
	public Color darkCloudColourVariant;

	public float durationS = 3000;
	public float durationC = 3000;
	private float t = 0;
	private float t2 = 0;
	public bool darkenClouds = false;
	public bool darkenSky = false;

	void Start()
	{
		StartCoroutine("CloudSpawner");
		darkenClouds = false;
		darkenSky = false;
	}

	void Update()
	{
		if (Input.GetKeyDown("space"))
		{
			var target = Camera.main.transform as Transform;
			GameObject newLightning = GameObject.Instantiate(lightning) as GameObject;
			Vector3 randPos = Random.onUnitSphere * spawnRadius/3;
			randPos.y = Mathf.Abs(randPos.y);
			newLightning.transform.localPosition = randPos;
			float randScale = Random.Range(lowerSize, upperSize);
			newLightning.transform.localScale = new Vector3(randScale,randScale,randScale);
			transform.LookAt(target);
		}

		innerDome.transform.Rotate(Vector3.up * Time.deltaTime);
		outerDome.transform.Rotate(Vector3.up * Time.deltaTime/2);
		if(darkenSky)
		{
			if (t < 1)
			{ 
				t += Time.deltaTime / durationS;
			}

			Camera.main.backgroundColor = Color.Lerp (Camera.main.backgroundColor, darkSkyColour, t);
		}
		if (darkenClouds)
		{
			if (t2 < 1)
			{ 
				t2 += Time.deltaTime / durationC;
			}
			for(int i=0; i<_list.Count; i++)
			{
				var cloudtemp = _list[i];
				if (i % 2 == 0)
				{
					var copy = cloudtemp.GetComponent<ParticleSystemRenderer> ().material;
					copy.color = Color.Lerp (copy.color, darkCloudColour, t2);
				}
				else
				{
					var copy2 = cloudtemp.GetComponent<ParticleSystemRenderer> ().material;
					copy2.color = Color.Lerp (copy2.color, darkCloudColourVariant, t2);
				}
			}
		}
	}

	IEnumerator CloudSpawner()
	{
		_list = new List<GameObject>();


		for(int i=0; i<cloudDensity/8; i++)
		{
			GameObject newCloud = GameObject.Instantiate(cloud) as GameObject;
			newCloud.transform.parent = innerDome.transform;
			ps = newCloud.GetComponent<ParticleSystem>();
			ps.Clear ();
			var main = ps.main;
			main.prewarm = true;
			ps.Play ();
			_list.Add( (GameObject)newCloud);
			Vector3 randPos = Random.onUnitSphere * spawnRadius/2;
			randPos.y = Mathf.Abs(randPos.y);
			newCloud.transform.localPosition = randPos;
			float randScale = Random.Range(lowerSize, upperSize);
			newCloud.transform.localScale = new Vector3(randScale,randScale,randScale);
		}
		yield return new WaitForSeconds (5.0f);
		for(int i=0; i<cloudDensity; i++)
		{
			GameObject newStormCloud = GameObject.Instantiate(stormCloud) as GameObject;
			newStormCloud.transform.parent = outerDome.transform;
			_list.Add( (GameObject)newStormCloud);
			Vector3 randPos = Random.onUnitSphere * spawnRadius;
			randPos.y = Mathf.Abs(randPos.y);
			newStormCloud.transform.localPosition = randPos;
			float randScale = Random.Range(lowerSize*2, upperSize*2);
			newStormCloud.transform.localScale = new Vector3(randScale,randScale,randScale);
		}
		yield return new WaitForSeconds (6.0f);
		darkenSky = true;
		darkenClouds = true;
	}
}
