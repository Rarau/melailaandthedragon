using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour {
    public GameObject[] buttons;
    public GameObject slime;
    public GameObject[] enemyType;
    public int enemyAmount = 5;
    List<GameObject> enemies;


    public Camera camera; //create camera object to affect the intended camera for raycast, else Camera.Main finds first main camera.
    Ray ray;
    RaycastHit rayHit;
	// Use this for initialization
	void Start () {
        buttons = GameObject.FindGameObjectsWithTag("buttons").OrderBy(go => go.name).ToArray(); // Sorts array of gameobjects in order by name. requires System.Linq

        //enemies = new List<GameObject>();
        //for (int i = 0; i< enemyAmount;i++)
        //{
        //    GameObject obj = (GameObject)Instantiate(slime);
        //    obj.SetActive(false);
        //    enemies.Add(obj);
        //}                                                     //Testing out pooling

       // HandleController reel = gameObject.GetComponent<HandleController>();
    }

    void Update ()
    {


        if (Input.GetKeyDown("x"))
        {
            GameObject enemy = GameObject.FindGameObjectWithTag("Slime");
            Destroy(enemy);
        }
        //if (GameObject.FindGameObjectsWithTag("Slime") == null)
        //{
        //    Instantiate()
        //}
        HandleController HandleScript = FindObjectOfType<HandleController>();
        ReelController ReelScript = FindObjectOfType<ReelController>();
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayHit,100.0f) && Input.GetMouseButtonDown(0) /*&& ReelScript.rotating == true*/)
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
