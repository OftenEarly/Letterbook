using UnityEngine;
using System.Collections;
using UnityEngine.UI;

 
[DisallowMultipleComponent]
public class WinDialog : MonoBehaviour
{

		/// <summary>
		/// Star sound effect.
		/// </summary>
		public AudioClip starSoundEffect;

		/// <summary>
		/// Win dialog animator.
		/// </summary>
		public Animator WinDialogAnimator;


		/// <summary>
		/// The level title text.
		/// </summary>
		public Text levelTitle;
		
		/// <summary>
		/// The effects audio source.
		/// </summary>
		private AudioSource effectsAudioSource;

		// Use this for initialization
		void Start ()
		{
				///Setting up the references
				if (WinDialogAnimator == null) {
						WinDialogAnimator = GetComponent<Animator> ();
				}
			

				if (effectsAudioSource == null) {
						effectsAudioSource = GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1];
				}
				
				if (levelTitle == null) {
						levelTitle = transform.Find ("Level").GetComponent<Text> ();
				}

				
		}

		/// <summary>
		/// When the GameObject becomes visible
		/// </summary>
		void OnEnable ()
		{
				//Hide the Win Dialog
				Hide ();
		}

		/// <summary>
		/// Show the Win Dialog.
		/// </summary>
		public void Show ()
		{
				if (WinDialogAnimator == null) {
						return;
				}
				WinDialogAnimator.SetTrigger ("Running");
		}

		/// <summary>
		/// Hide the Win Dialog.
		/// </summary>
		public void Hide ()
		{
				StopAllCoroutines ();
				WinDialogAnimator.SetBool ("Running", false);
		}

		/// <summary>
		/// Show sub stars effect.
		/// </summary>
		/// <param name="fadingStar">Fading star.</param>
		private void ShowEffect (Transform fadingStar)
		{
				if (fadingStar == null) {
						return;
				}
				StartCoroutine (ShowEffectCouroutine (fadingStar));
		}

		/// <summary>
		/// Shows sub stars effect couroutine.
		/// </summary>
		/// <returns>The effect couroutine.</returns>
		/// <param name="fadingStar">Fading star reference.</param>
		private IEnumerator ShowEffectCouroutine (Transform fadingStar)
		{
				yield return new WaitForSeconds (0.5f);
//				fadingStar.Find ("Effect").GetComponent<ParticleEmitter> ().emit = true;
		}

		/// <summary>
		/// Set the level title.
		/// </summary>
		/// <param name="value">Value.</param>
		public void SetLevelTitle (string value)
		{
				if (string.IsNullOrEmpty (value) || levelTitle == null) {
						return;
				}
				levelTitle.text = value;
		}


}