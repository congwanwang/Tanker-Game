using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    PlayerController controller;

    public void SetController(PlayerController controller)
    {
        this.controller = controller;
    }

    public PlayerController GetController()
    {
        return controller;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            controller.HitEnemy();
        }
    }
}
