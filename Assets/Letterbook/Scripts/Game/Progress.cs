using UnityEngine;
using System.Collections;
using UnityEngine.UI;

 
public class Progress : MonoBehaviour
{
		/// <summary>
		/// The star off sprite.
		/// </summary>
		public Sprite starOff;
		
		/// <summary>
		/// The star on sprite.
		/// </summary>
		public Sprite starOn;

		/// <summary>
		/// The level stars.
		/// </summary>
		public Image[] levelStars;

		/// <summary>
		/// The game manager reference.
		/// </summary>
		public GameManager gameManager;

		/// <summary>
		/// The progress image.
		/// </summary>
		public Image progressImage;


		// Use this for initialization
		void Start ()
		{
				if (progressImage == null) {
						progressImage = GetComponent<Image> ();
				}

				if (gameManager == null) {
						gameManager = GameObject.FindObjectOfType<GameManager> ();
				}
		}


}
