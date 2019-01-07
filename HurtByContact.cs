using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HurtByContact : MonoBehaviour {
    [SerializeField]
    private Slider HpBar;
    float damage = 0.1f;
    int health = 10;

    private void Start()
    {
        HpBar.value = health;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if(other.GetComponent<Bullet>().GetController() != GetComponent<PlayerController>())
            {
                HpBar.value -= damage;
                if (HpBar.value <= 0)
                {
                    GameController.instance.playerGameOver(GetComponent<PlayerController>().getOpenID());
                }
            }
        }
    }
}
