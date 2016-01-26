using UnityEngine;
using System.Collections;
using System.Linq;

public class ButtonController : MonoBehaviour {
    public GameObject[] buttons;
    
   // public ReelController[] reels;
    public Camera camera; //create camera object to affect the intended camera for raycast, else Camera.Main finds first main camera.
    Ray ray;
    RaycastHit rayHit;
	// Use this for initialization
	void Start () {
        buttons = GameObject.FindGameObjectsWithTag("buttons").OrderBy(go => go.name).ToArray(); // Sorts array of gameobjects in order by name. requires System.Linq
       // HandleController reel = gameObject.GetComponent<HandleController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandleController HandleScript = FindObjectOfType<HandleController>();
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayHit,100.0f) && Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (rayHit.collider.gameObject == buttons[i].gameObject)
                {
                    HandleScript.reels[i].Rotate(Random.Range(0, 5));
                    Debug.Log(i + "Clicked");
                }
            }
        }

       


	}
}
