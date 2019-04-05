using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCámera : MonoBehaviour
{
    Transform Objetivo;
    public GameObject Jugador;
    bool interior = false;
    Vector2 Velocity;

    public float Smooth = 3f;
    float tLX, tLY, bRX, bRY;
   void Awake()
    {
        Objetivo = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void Start()
    {
        Screen.SetResolution(1366, 768, true);
    }
    public void SetBound(GameObject Map)
    {
        Tiled2Unity.TiledMap config = Map.GetComponent<Tiled2Unity.TiledMap>();
        float CameraSize = Camera.main.orthographicSize;

        tLX = (Map.transform.position.x + CameraSize+6);
        tLY = Map.transform.position.y - CameraSize;
        bRX = (Map.transform.position.x + config.NumTilesWide-6) - CameraSize;
        bRY = (Map.transform.position.y - config.NumTilesHigh) + CameraSize;
    }
    void Update()
    {
        if (!Screen.fullScreen)
        {
            Screen.SetResolution(1366, 768, true);
        }

        if (Input.GetKey("escape")) 
        {
            Application.Quit();
        }
        float Posx = Mathf.Round(
                Mathf.SmoothDamp(transform.position.x, Objetivo.position.x, ref Velocity.x, Smooth)*100) / 100;
        float Posy = Mathf.Round(
                Mathf.SmoothDamp(transform.position.y, Objetivo.position.x, ref Velocity.y, Smooth) * 100) / 100;

        transform.position = new Vector3(
                 Mathf.Clamp(Posx, tLX, bRX),
                 Mathf.Clamp(Posy, bRY, tLY),
                 transform.position.z
                 );




    }

   

}
