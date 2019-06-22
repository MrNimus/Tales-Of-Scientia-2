using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Aura : MonoBehaviour
{
    public float TiempoDeAparecer;
    Animator animator;
    Coroutine manager;
    bool loaded;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void StartAura()
    {
        manager = StartCoroutine(Manager());
        animator.Play("Stop");
    }
    public void AuraStop()
    {
        StopCoroutine(manager);
        animator.Play("Stop");
        loaded = false;
    }
    public IEnumerator Manager()
    {
        yield return new WaitForSeconds(TiempoDeAparecer);
        animator.Play("Play");
        loaded = true;
    }
    public bool isloaded()
    {
        return loaded;
    }
    void Update()
    {
        if (isloaded())
        {
            GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
        }
        else
        {
            
                GamePad.SetVibration(PlayerIndex.One, 0, 0);
            
        }
    }
}
