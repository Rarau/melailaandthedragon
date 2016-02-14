using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public PlayerBattleAgent player;

    public float max;
    public float cur;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(player.hp / 100.0f, 1.0f, 1.0f);
	}
}
