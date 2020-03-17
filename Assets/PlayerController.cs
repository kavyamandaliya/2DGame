using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-2, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(2, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector2(0, -2);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector2(0, 2);
        }
    }
    
}
