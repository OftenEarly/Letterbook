using UnityEngine;
using System.Collections;
using UnityEngine.UI;

 
public class UIEvents : MonoBehaviour


{
		//public void AlbumShapeEvent (TableShape tableShape)
	//	{
	//			if (tableShape == null) {
	//					return;
	//			}
	//
	//			TableShape.selectedShape = tableShape;
	//
	//			//LoadGameScene ();
	//	}


		public Transform ShapeMaker;

		public ShapesTable ShapesTable;

	IEnumerator Example()
	{
		yield return new WaitForSeconds(1/160);

		StartCoroutine(SceneLoader.LoadSceneAsync ("Game"));
	}

		
		public void PointerButtonEvent (Pointer pointer)
		{
				if (pointer == null) {
						return;
				}
				if (pointer.group != null) {
						ScrollSlider scrollSlider = GameObject.FindObjectOfType (typeof(ScrollSlider)) as ScrollSlider;
						if (scrollSlider != null) {
								scrollSlider.DisableCurrentPointer ();
								FindObjectOfType<ScrollSlider> ().currentGroupIndex = pointer.group.Index;
								scrollSlider.GoToCurrentGroup ();
						}
				}
		}

		public void LoadMainScene(){
			StartCoroutine(SceneLoader.LoadSceneAsync ("Main"));
		}

		public void LoadGameScene(){
			StartCoroutine(SceneLoader.LoadSceneAsync ("Game"));
		}

		public void LoadAlbumScene ()
		{
			if(!string.IsNullOrEmpty(ShapesManager.shapesManagerReference))
				StartCoroutine(SceneLoader.LoadSceneAsync (GameObject.Find(ShapesManager.shapesManagerReference).GetComponent<ShapesManager>().sceneName));
		}
		
		public void LoadSceneA ()
		{
			ShapesManager.shapesManagerReference = "AShapesManager";
			Instantiate (ShapeMaker);
			StartCoroutine(Example());
		}
		
		public void LoadLowercaseAlbumScene ()
		{
			ShapesManager.shapesManagerReference = "LShapesManager";
			Instantiate (ShapeMaker);
			StartCoroutine(Example());
			//
		}

		public void LoadUppercaseAlbumScene ()
	{
		ShapesManager.shapesManagerReference = "UShapesManager";
		Instantiate (ShapeMaker);
		StartCoroutine(Example());
		//
	}

		public void LoadSentenceAlbumScene ()
		{
			ShapesManager.shapesManagerReference = "SShapesManager";
			Instantiate (ShapeMaker);
			StartCoroutine(Example());
		}



		public void NextClickEvent ()
		{
			try{
				GameObject.FindObjectOfType<GameManager> ().NextShape ();
			}catch(System.Exception ex){

			}
		}

		public void PreviousClickEvent ()
		{
			try{
				GameObject.FindObjectOfType<GameManager> ().PreviousShape ();
			}catch(System.Exception ex){
			
			}
		}

		public void SpeechClickEvent ()
		{
				Shape shape = GameObject.FindObjectOfType<Shape> ();
				if (shape == null) {
						return;
				}
				shape.Spell ();
		}

		public void ResetShape ()
		{
				GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
				if (gameManager != null) {
					if(!gameManager.shape.completed){
							gameManager.DisableGameManager ();
							GameObject.Find ("ResetConfirmDialog").GetComponent<Dialog> ().Show ();
					}else{
						gameManager.ResetShape();
					}
				}
		}

		public void PencilClickEvent (Pencil pencil)
		{
				if (pencil == null) {
						return;
				}
				GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
				if (gameManager == null) {
						return;
				}
				if (gameManager.currentPencil != null) {
						gameManager.currentPencil.DisableSelection ();
						gameManager.currentPencil = pencil;
				}
				gameManager.SetShapeOrderColor ();
				pencil.EnableSelection ();
		}

		public void ResetConfirmDialogEvent (GameObject value)
		{
				if (value == null) {
						return;
				}
		
				GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
		
				if (value.name.Equals ("YesButton")) {
						Debug.Log ("Reset Confirm Dialog : Yes button clicked");
						if (gameManager != null) {
								gameManager.ResetShape ();
						}
			
				} else if (value.name.Equals ("NoButton")) {
						Debug.Log ("Reset Confirm Dialog : No button clicked");
				}

				value.GetComponentInParent<Dialog> ().Hide ();

				if (gameManager != null) {
						gameManager.EnableGameManager ();
				}
		}


		public void ResetGame(){
			DataManager.ResetGame ();
		}

		public void LeaveApp(){
			Application.Quit ();
		}
}
