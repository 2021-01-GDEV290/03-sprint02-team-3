using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool collide = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collide = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collide = false;
        }
    }

    private void Update()
    {
        if (collide && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(this.gameObject);
        }
    }
}
