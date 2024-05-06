using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb;

    public float speed = 1.0f;
    private Vector2 direction;
    private float lifeTime = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
    }
  
    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    // establecer dirección según disparo
    public void SetDirection(Vector2 value)
    {
        direction = value;
    }
}
