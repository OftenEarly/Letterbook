using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

 
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

[DisallowMultipleComponent]
public class ShapesTable : MonoBehaviour
{


	/// <summary>
	/// Whether to save the last selected group or not.
	/// </summary>
	public bool saveLastSelectedGroup = true;

	/// <summary>
	/// The shapes list.
	/// </summary>
	public static List<TableShape> shapes;

	/// <summary>
	/// The groups parent.
	/// </summary>
	public Transform groupsParent;

	/// <summary>
	/// The shape prefab.
	/// </summary>
	public GameObject shapePrefab;
	
	/// <summary>
	/// The shapes group prefab.
	/// </summary>
	public GameObject shapesGroupPrefab;

	/// <summary>
	/// temporary transform.
	/// </summary>
	private Transform tempTransform;

	public float shapeScaleFactor = 0.7f;

	/// <summary>
	/// The Number of shapes per group.
	/// </summary>
	[Range (1, 100)]
	public int shapesPerGroup = 12;

	/// <summary>
	/// Number of columns per group.
	/// </summary>
	[Range (1, 10)]
	public int columnsPerGroup = 3;

	/// <summary>
	/// Whether to enable group grid layout.
	/// </summary>
	public bool EnableGroupGridLayout = true;

	/// <summary>
	/// The last shape that user reached.
	/// </summary>
	private Transform lastShape;

	/// <summary>
	/// The shapes manager.
	/// </summary>
	private ShapesManager shapesManager;

	void Awake ()
	{
		if (!string.IsNullOrEmpty (ShapesManager.shapesManagerReference)) {
			shapesManager = GameObject.Find (ShapesManager.shapesManagerReference).GetComponent<ShapesManager> ();
		} else {
			Debug.LogError ("You have to start the game from the Main scene");
		}

		//define the shapes list
		shapes = new List<TableShape> ();

		//Create new shapes
		StartCoroutine("CreateShapes");
	

		//Setup the last selected group index
		/*
		ScrollSlider scrollSlider = GameObject.FindObjectOfType<ScrollSlider> ();
		if (saveLastSelectedGroup && shapesManager != null) {
			scrollSlider.currentGroupIndex = shapesManager.lastSelectedGroup;
		}*/
	}



	/// <summary>
	/// Creates the shapes in Groups.
	/// </summary>
	private IEnumerator CreateShapes ()
	{
		yield return 0;

		//Clear current shapes list
		shapes.Clear ();

		//The ID of the shape
		int ID = 0;
			
		//The scale ratio for the shape
		float ratio = Mathf.Max (Screen.width, Screen.height) / 1000.0f;

		//The group of the shape
		GameObject shapesGroup = null;

		//The index of the group
		int groupIndex = 0;


		groupsParent.gameObject.SetActive (false);

		//Create Shapes inside groups
		for (int i = 0; i < shapesManager.shapes.Count; i++) {

			if (i % shapesPerGroup == 0) {
				 groupIndex = (i / shapesPerGroup);
				shapesGroup = Group.CreateGroup (shapesGroupPrefab, groupsParent, groupIndex, columnsPerGroup);
				if (!EnableGroupGridLayout) {
					shapesGroup.GetComponent<GridLayoutGroup> ().enabled = false;
				}
	
			}
			//Create Shape
			ID = (i + 1); //the id of the shape
			GameObject tableShapeGameObject = Instantiate (shapePrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tableShapeGameObject.transform.SetParent (shapesGroup.transform); //setting up the shape's parent
			TableShape tableShapeComponent = tableShapeGameObject.GetComponent<TableShape> (); //get TableShape Component
			tableShapeComponent.ID = ID; //setting up shape ID
			tableShapeGameObject.name = "Shape-" + ID;//shape name
			tableShapeGameObject.transform.localScale = Vector3.one;
			tableShapeGameObject.GetComponent<RectTransform> ().offsetMax = Vector2.zero;
			tableShapeGameObject.GetComponent<RectTransform> ().offsetMin = Vector2.zero;

			GameObject uiShape = Instantiate (shapesManager.shapes [i].gamePrefab, Vector3.zero, Quaternion.identity) as GameObject;

			uiShape.transform.SetParent (tableShapeGameObject.transform.Find ("Content"));

			RectTransform rectTransform = tableShapeGameObject.transform.Find ("Content").GetComponent<RectTransform> ();
		
			uiShape.transform.localScale = new Vector3 (ratio * shapeScaleFactor, ratio * shapeScaleFactor);
			uiShape.GetComponent<RectTransform> ().anchoredPosition3D = Vector3.zero;

			List<Shape> shapeComponents = new List<Shape> ();
			if (uiShape.GetComponent<CompoundShape> () != null) {
				Shape[] tempS = uiShape.GetComponentsInChildren<Shape> ();
				foreach (Shape s in tempS) {
					shapeComponents.Add (s);
				}
			} else {
				shapeComponents.Add (uiShape.GetComponent<Shape> ());
			}

			int compoundID;
			for (int s = 0 ;s <shapeComponents.Count;s++) {
				CompoundShape compundShape = shapeComponents [s].transform.parent.GetComponent<CompoundShape> ();
				compoundID = 0;
				if (compundShape != null) {
					compoundID = compundShape.GetShapeIndexByInstanceID(shapeComponents[s].GetInstanceID());
				}

				shapeComponents[s].enabled = false;
				//release unwanted resources
				shapeComponents[s].GetComponent<Animator> ().enabled = false;
				shapeComponents[s].transform.Find ("TracingHand").gameObject.SetActive (false);
				shapeComponents[s].transform.Find ("Collider").gameObject.SetActive (false);

				Animator[] animators = shapeComponents[s].transform.GetComponentsInChildren<Animator> ();
				foreach (Animator a in animators) {
					a.enabled = false;
				}

				int from, to;
				string[] slices;
				List <Transform> paths = CommonUtil.FindChildrenByTag (shapeComponents[s].transform.Find ("Paths"), "Path");
				foreach (Transform p in paths) {
					slices = p.name.Split ('-');
					from = int.Parse (slices [1]);
					to = int.Parse (slices [2]);

					p.Find ("Start").gameObject.SetActive (false);
					Image img = CommonUtil.FindChildByTag (p, "Fill").GetComponent<Image> ();

					if (PlayerPrefs.HasKey (DataManager.GetPathStrKey(ID,compoundID,from,to,shapesManager))) {

						List<Transform> numbers = CommonUtil.FindChildrenByTag (p.transform.Find ("Numbers"), "Number");
						foreach (Transform n in numbers) {
							n.gameObject.SetActive (false);
						}
						img.fillAmount = 1;
						img.color =	DataManager.GetShapePathColor (ID, compoundID,from, to, shapesManager);
					}
				}
			}

			shapes.Add (tableShapeComponent);//add table shape component to the list
		}

			TableShape.selectedShape = shapes[0];

		//GameObject.Find ("Loading").SetActive (false);

		//groupsParent.gameObject.SetActive (true);

	}

	/// <summary>
	/// Settings up the shape contents in the table.
	/// </summary>
	/// <param name="tableShape">Table shape.</param>
	/// <param name="ID">ID of the shape.</param>
	/// <param name="groupIndex">Index of the group.</param>


	/// <summary>
	/// Set the selected group.
	/// </summary>
	/// <param name="groupIndex">Group index.</param>
	private void SetSelectedGroup(int groupIndex){
		//Setup the last selected group index

		groupIndex = 1;

	}

	/// <summary>
	/// Raise the change group event.
	/// </summary>
	/// <param name="currentGroup">Current group.</param>
	public void OnChangeGroup (int currentGroup)
	{
		if (saveLastSelectedGroup) {
			shapesManager.lastSelectedGroup = currentGroup;
		}
	}
}