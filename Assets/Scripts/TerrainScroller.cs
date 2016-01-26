using UnityEngine;
using System.Collections;

public class TerrainScroller : MonoBehaviour {

    private float scrollSpeed = 0.1f;
    public float offSet;
    public Renderer LeftWall;
    public Renderer RightWall;
    public Renderer Floor;
    // Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        offSet = Time.time * scrollSpeed;
        LeftWall.material.SetTextureOffset("_MainTex", new Vector2(offSet, 0));
        RightWall.material.SetTextureOffset("_MainTex", new Vector2(-offSet, 0));
        Floor.material.SetTextureOffset("_MainTex", new Vector2(0,-offSet));
    }
}
