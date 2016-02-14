using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnemyHealthBar : MonoBehaviour 
{
    Slider slider;
    EnemyBattleAgent enemy;
	// Use this for initialization
	void Start ()
    {
        slider = GetComponent<Slider>();

	}

    public void SetTarget(EnemyBattleAgent enemy)
    {
        this.enemy = enemy;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(enemy != null)
            slider.value = enemy.hp / enemy.initalHP;
    }

}
