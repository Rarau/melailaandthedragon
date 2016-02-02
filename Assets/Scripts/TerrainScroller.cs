using UnityEngine;
using System.Collections;

public class TerrainScroller : MonoBehaviour {

    private float scrollSpeed = 0.1f;
    public float offSet;
    public Renderer LeftWall;
    public Renderer RightWall;
    public Renderer Floor;
    public GameManager enemyReference;
    // Use this for initialization
	void Start () {
   // enemyReference = GameObject.FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        //if (enemyReference.enemyCleared == true)
        {
            offSet = Time.time * scrollSpeed;
            LeftWall.material.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
            RightWall.material.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
            Floor.material.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
        }
    }
}
