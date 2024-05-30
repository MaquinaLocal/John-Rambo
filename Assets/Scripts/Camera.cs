using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Player;
    private bool camera_movement = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) return;

        // Cámara que sigue al personaje en x
        /*
        Vector3 position = transform.position;
        position.x = Player.transform.position.x;
        transform.position = position;
        */

        // Camara que se mueve continuamente
        if(camera_movement == true)
            transform.position = transform.position + Vector3.right * Time.deltaTime;
    }

    public void SetCameraMovement(bool value)
    {
        camera_movement = value;
    }
}
