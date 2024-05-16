using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // C�mara que sigue al personaje en x
        Vector3 position = transform.position;
        position.x = Player.transform.position.x;
        transform.position = position;
    }
}
