using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class RamboMovement : MonoBehaviour
{
    private float Horizontal;
    public float MovSpeed = 3.0f;
    public float JumpForce = 1.0f;
    private bool Grounded;

    private Rigidbody2D rb;
    private Animator Animator;

    public GameObject bulletObj;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        // Desplazamiento
        Horizontal = Input.GetAxisRaw("Horizontal") * MovSpeed;

        // Activa animación de "correr"
        Animator.SetBool("isRunning", Horizontal != 0.0f);

        // Rotación basada en movimiento
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Dibujo de rayo
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);

        // Casteo de rayo para salto
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
            Grounded = true;
        else Grounded = false;

        // Salto
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        // Disparo
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Shoot();
        }
    }


    private void Jump()
    {
        rb.AddForce(Vector2.up * JumpForce);
    }


    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(bulletObj, transform.position + direction * 1.0f, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);

    }

   
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Horizontal, rb.velocity.y);
    }

}
