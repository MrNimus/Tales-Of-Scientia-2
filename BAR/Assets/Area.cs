 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Area : MonoBehaviour
{
    Animator Anim;

    void Start()
    {
        Anim = GetComponent<Animator>();

    }

    public IEnumerator ShowArea(string name)
    {
        Anim.Play("Area_Show");
        transform.GetChild(0).GetComponent<Text>().text = name;
        transform.GetChild(1).GetComponent<Text>().text = name;
        yield return new WaitForSeconds(2f);
        Anim.Play("Area_FadeOut");

    }
}
