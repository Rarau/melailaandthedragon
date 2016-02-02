
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine.UI;
using System;
public class FBTest : MonoBehaviour {



	public event EventHandler initializationFinishedEvent;
	
	void Start(){
		FBInit ();
	}
	
	public void FBInit(){
		Debug.Log ("FB INITIALISED");
		FB.Init (InitCallback, OnHideUnity);
	}
	
	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			
			var perms = new List<string>(){"public_profile", "email", "user_friends"};
			FB.LogInWithReadPermissions(perms, AuthCallback);
			
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}
	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
			initializationFinishedEvent.Invoke (this, null);
			
		} else {
			Debug.Log("User cancelled login");
		}
	}
	
	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}
}
