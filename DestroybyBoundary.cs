using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroybyBoundary : MonoBehaviour {


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Boundary")
        {
            return;
        }
        if(other.CompareTag("Enemy"))
        {   
            return;
        }
        Destroy(other.gameObject);            
    }
}
