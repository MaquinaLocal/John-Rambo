using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject winMenu;
    public GameObject characters;
    public GameObject cameraRef;

    private int enemyCount = 0;
    private int enemiesToWin = 4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Iniciar juego
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Oculta menú inicio
            mainMenu.SetActive(false);
            // Activa capa de personajes
            characters.SetActive(true);
            // Activa movimiento de cámara
            cameraRef.GetComponent<Camera>().SetCameraMovement(true);
        }

        // Salir del juego
        if (Input.GetKeyDown(KeyCode.Q))
        {
#if UNITY_EDITOR 
            EditorApplication.isPlaying = false;
#endif
        }

        // Reiniciar nivel
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Ganar
        if (enemyCount >= enemiesToWin)
        {
            winMenu.SetActive(true);
            cameraRef.GetComponent<Camera>().SetCameraMovement(false);
        }
    }

    public void CheckWinCondition()
    {
        enemyCount++;
        Debug.Log(enemyCount);
    }
}
