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
    private int Health = 5;


    private Rigidbody2D rb;
    private Animator Animator;

    public GameObject bulletObj;
    [SerializeField] private Jetpack jetpack;

    public GameObject defeatMenu;
    public GameObject cameraRef;


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

        // Activa animación "saltar"
        Animator.SetBool("isJumping", !Grounded);

        // Rotación basada en movimiento
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Dibujo de rayo
        Debug.DrawRay(transform.position, Vector3.down * 0.5f, Color.red);

        // Casteo de rayo para salto
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.5f))
            Grounded = true;
        else Grounded = false;

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

        // Destroy por caida
        if (transform.position.y <= -5.0f)
        {
            Destroy(gameObject);
            RamboLoses();
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


    // Manejo de vida
    public void HP_Manager(int Value)
    {
        Health += Value;
    
        if (Health <= 0)
        {
            Destroy(gameObject);
            RamboLoses();
        }

    }
   
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Horizontal, rb.velocity.y);
        
       
        // Jetpack
        jetpack.GetComponent<Jetpack>().IsOnAir(!Grounded);

        if (Grounded == false && currentFuel > 0.0f) currentFuel -= Time.deltaTime * 2;
        else if (currentFuel <= 0.0f) bCanJump = false;
        
        if (Grounded)
        {
            currentFuel += Time.deltaTime * 2;
            if (currentFuel >= maxFuel)
            {
                currentFuel = maxFuel;
                bCanJump = true;
            }
        }

        //Debug.Log(currentFuel);
    }

    private void RamboLoses()
    {
        cameraRef.GetComponent<Camera>().SetCameraMovement(false);
        defeatMenu.SetActive(true);
    }
}
