using UnityEngine;
using System.Collections;
using System;

 
[DisallowMultipleComponent]
public class TableShape : MonoBehaviour
{
		/// <summary>
		/// The selected shape.
		/// </summary>
		public static TableShape selectedShape;

		/// <summary>
		/// Table Shape ID.
		/// </summary>
		public int ID = -1;

		// Use this for initialization
		void Start ()
		{
		Debug.Log ("before_selection table shape");
				///Setting up the ID for Table Shape
				if (ID == -1) {
						string [] tokens = gameObject.name.Split ('-');
						if (tokens != null) {
								ID = int.Parse (tokens [1]);
						}
				}
		}
}