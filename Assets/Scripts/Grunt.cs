using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletObj;

    [SerializeField] private float shootDistance = 2.0f;
    private float lastShoot;
    public float shootUpdate = 1.0f;
    private int Health = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        // Rotar al enemigo según posición de jugador
        Vector3 direction = player.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f,1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f,1.0f, 1.0f);

        // Disparo según distancia de jugador
        float distance = Mathf.Abs(player.transform.position.x - transform.position.x);
        if (distance < shootDistance && Time.time > lastShoot + shootUpdate)
        {
            Shoot();
            lastShoot = Time.time;
        }
       
    }


    // Disparo
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bullet = Instantiate(bulletObj, transform.position + direction, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }

    // Manejo de vida
    public void HP_Manager(int Value)
    {
        Health += Value;
        Debug.Log(Health);
        if (Health <= 0) Destroy(gameObject);
    }
}
