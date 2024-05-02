using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamboMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float Horizontal;   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Horizontal, rb.velocity.y);
    }
}
