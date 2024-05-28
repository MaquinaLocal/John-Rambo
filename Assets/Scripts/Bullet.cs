using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody2D rb;

    public float speed = 1.0f;
    private Vector2 direction;
    private int Damage = 1;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
  
    void FixedUpdate()
    {
        rb.velocity = direction * speed;
    }

    // establecer direcci�n seg�n disparo
    public void SetDirection(Vector2 value)
    {
        direction = value;
    }

    // Destroy que se llama en el �ltimo sprite de bullet animation
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    // Colisi�n
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RamboMovement Player = collision.GetComponent<RamboMovement>();
        Grunt Enemy = collision.GetComponent<Grunt>();

        if (Player) Player.HP_Manager(-Damage);
        if (Enemy) Enemy.HP_Manager(-Damage);

        DestroyBullet();
    }
}
