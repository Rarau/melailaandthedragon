using UnityEngine;
using System.Collections;
using System.Linq;

public class ButtonController : MonoBehaviour {
    public GameObject[] buttons;
    public Camera camera;
    Ray ray;
    RaycastHit rayHit;
	// Use this for initialization
	void Start () {
        buttons = GameObject.FindGameObjectsWithTag("buttons").OrderBy(go => go.name).ToArray(); // Sorts array of gameobjects in order by name. requires System.Linq
    }
	
	// Update is called once per frame
	void Update ()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayHit,100.0f) && Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (rayHit.collider.gameObject == buttons[i].gameObject)
                {
                    Debug.Log(i + "Clicked");
                }
            }

        }

        if (gameObject)


	}
}
