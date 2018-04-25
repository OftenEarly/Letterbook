using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace EnglishTracingBook
{
	public class Path : MonoBehaviour
	{
		/// <summary>
		/// Whether to flip the direction of the path or not.
		/// </summary>
		public bool flip;

		/// <summary>
		/// Whether the path is completed or not.
		/// </summary>
		[HideInInspector]
		public bool completed;

		/// <summary>
		/// The fill method (Radial or Linear or Point).
		/// </summary>
		public FillMethod fillMethod;

		/// <summary>
		/// The type of the shape (Used for Vertical Fill method).
		/// </summary>
		public ShapeType type = ShapeType.Vertical;

		/// <summary>
		/// The angle offset in degree(For Linear Fill).
		/// </summary>
		public float offset = 90;

		/// <summary>
		/// The complete offset (The fill amount offset).
		/// </summary>
		public float completeOffset = 0.85f;

		/// <summary>
		/// The first number reference.
		/// </summary>
		public Transform firstNumber;

		/// <summary>
		/// The second number reference.
		/// </summary>
		public Transform secondNumber;

		/// <summary>
		/// Whether to run the auto fill or not.
		/// </summary>
		private bool autoFill;

		/// <summary>
		/// Whether to enable the quarter's restriction on the current angle or not.
		/// </summary>
		public bool quarterRestriction = true;

		/// <summary>
		/// The offset of the current radial angle(For Radial Fill) .
		/// </summary>
		public float radialAngleOffset = 0;

		/// <summary>
		/// The shape reference.
		/// </summary>
		[HideInInspector]
		public Shape shape;

		// Use this for initialization
		void Awake ()
		{
			shape = GetComponentInParent<Shape> ();

		}

		/// <summary>
		/// Auto fill.
		/// </summary>
		public void AutoFill ()
		{
			StartCoroutine ("AutoFillCoroutine");
		}

		/// <summary>
		/// Set the numbers status.
		/// </summary>
		/// <param name="status">the status value.</param>
		 public void SetGuideStatus (bool status)
		 {
		 	List<Transform> guides = CommonUtil.FindChildrenByTag (transform.Find ("Guide"), "Guide");
			foreach (Transform guide in guides) {
				if (guide == null)
					
		 			continue;

		 		if (status) {
		 			EnableStartCollider ();

		 	} else {
		 
		 		if (shape.enablePriorityOrder || completed) {
						DisableStartCollider ();
		 			}

		 			if (shape.enablePriorityOrder) {
		 			}
		 		}
					
		 	}
		 }
			

		/// <summary>
		/// Enable the start collider.
		/// </summary>
		public void EnableStartCollider ()
		{
			transform.Find ("Start").GetComponent<Collider2D> ().enabled = true;
			transform.Find ("Guide").gameObject.SetActive(true);
			Debug.Log ("EnablestartCOllider");

		}

		/// <summary>
		/// Disable the start collider.
		/// </summary>
		public void DisableStartCollider ()
		{
			transform.Find ("Start").GetComponent<Collider2D> ().enabled = false;
			transform.Find ("Guide").gameObject.SetActive(false);
			Debug.Log ("DisasblestartCOllider");
		}

		/// <summary>
		/// Reset the path.
		/// </summary>
		public void Reset ()
		{
			completed = false;
			if (!shape.enablePriorityOrder) {
				SetGuideStatus (true);
			}
			StartCoroutine ("ReleaseCoroutine");
		}


		/// <summary>
		/// Auto fill coroutine.
		/// </summary>
		/// <returns>The fill coroutine.</returns>
		private IEnumerator AutoFillCoroutine ()
		{
			Image image = CommonUtil.FindChildByTag (transform, "Fill").GetComponent<Image> ();
			while (image.fillAmount < 1) {
				image.fillAmount += 0.02f;
				yield return new WaitForSeconds (0.001f);
			}
		}

		/// <summary>
		/// Release the coroutine.
		/// </summary>
		/// <returns>The coroutine.</returns>
		private IEnumerator ReleaseCoroutine ()
		{
			Image image = CommonUtil.FindChildByTag (transform, "Fill").GetComponent<Image> ();
			while (image.fillAmount > 0) {
				image.fillAmount -= 0.02f;
				yield return new WaitForSeconds (0.005f);
			}
		}

		public enum ShapeType
		{

			Horizontal,
			Vertical
		}

		public enum FillMethod
		{
			Radial,
			Linear,
			Point
		}

		public enum CenterReference
		{
			PATH_GAMEOBJECT,
			FILL_GAMEOBJECT
		}
	}
}