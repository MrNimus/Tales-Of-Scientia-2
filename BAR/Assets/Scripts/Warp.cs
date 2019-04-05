using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class Warp : MonoBehaviour
{
    public GameObject Objetivo;
    public GameObject Mapa;

    bool Iniciar = false;
    bool IsFadeIn = false;
    float Alpha = 0;
    float fadetime = 2f;

    GameObject Area;

	
    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        Assert.IsNotNull(Mapa);
        Area = GameObject.FindGameObjectWithTag("Area");
        
    }
    IEnumerator OnTriggerEnter2D (Collider2D other)
    {
        FadeIn();
        other.GetComponent<Animator>().enabled = false;
        other.GetComponent<PlayerMovemente>().enabled = false;
        yield return new WaitForSeconds(fadetime);
            other.transform.position = Objetivo.transform.GetChild(0).transform.position;
            Camera.main.GetComponent<MainCámera>().SetBound(Mapa);
        FadeOut();
        other.GetComponent<Animator>().enabled = true;
        other.GetComponent<PlayerMovemente>().enabled = true;
        StartCoroutine(Area.GetComponent<Area>().ShowArea(Objetivo.name));

    }

    void OnGUI()
    {
        if (!Iniciar)
        {
            return;
        }
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Alpha);

        Texture2D Textura;
        Textura = new Texture2D(1, 1);
        Textura.SetPixel(0, 0, Color.black);
        Textura.Apply();

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Textura);

        if (IsFadeIn)
        {
            Alpha = Mathf.Lerp(Alpha, 1.1f, fadetime * Time.deltaTime);
        }
        else
        {

            Alpha = Mathf.Lerp(Alpha, -0.1f, fadetime * Time.deltaTime);
            if (Alpha<0)
            {
                Iniciar = false;
            }
        }

    }

    void FadeIn()
    {
        Iniciar = true;
        IsFadeIn = true;
    }

    void FadeOut()
    {
        IsFadeIn = false;
    }

}
