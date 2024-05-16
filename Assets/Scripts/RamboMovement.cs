using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;

public class RamboMovement : MonoBehaviour
{
    private float Horizontal;
    private bool Grounded;
    private bool bCanJump;
    private bool bIsOnAir;

    public float MovSpeed = 3.0f;
    public float JumpForce = 1.0f;
    public float maxFuel = 10.0f;
    private float currentFuel;


    private Rigidbody2D rb;
    private Animator Animator;

    public GameObject bulletObj;
    [SerializeField] private Jetpack jetpack;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        currentFuel = maxFuel;
        bCanJump = true;
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
        /*
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);

        // Casteo de rayo para salto
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
            Grounded = true;
        else Grounded = false;
        */

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && bCanJump)
        {
            Jump();
        }

        // Disparo
        if (Input.GetKeyDown(KeyCode.J))
        {
            Shoot();
        }


        // Combustible
        /*
        if (currentFuel > 0.0f && bCanJump == true) { currentFuel -= Time.deltaTime; }
        else if (currentFuel <= maxFuel) { bCanJump = false; currentFuel += Time.deltaTime; }
        else { bCanJump = true; }
        */

        //Debug.Log(currentFuel);
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
        
        Vector2 rbVelocity = rb.velocity;
        if (Horizontal != 0.0f) rbVelocity.y = 0.0f;

        // Jetpack
        if (rbVelocity.y != 0.0f)
        {
            bIsOnAir = true;
            jetpack.GetComponent<Jetpack>().IsOnAir(bIsOnAir);
        }
        else
        {
            bIsOnAir = false;
            jetpack.GetComponent<Jetpack>().IsOnAir(bIsOnAir);
        }

            if (bIsOnAir && currentFuel > 0.0f) currentFuel -= Time.deltaTime * 2;
        else if (currentFuel <= 0.0f) bCanJump = false;
        
        if (bIsOnAir == false)
        {
            currentFuel += Time.deltaTime * 2;
            if (currentFuel >= maxFuel)
            {
                currentFuel = maxFuel;
                bCanJump = true;
            }
        }

        Debug.Log(rbVelocity.y);
        Debug.Log(currentFuel);
    }

}
