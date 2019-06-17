using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class Breakable : MonoBehaviour
{
    public string Destruction_Anim;
    public float Time;
    Animator Anim;


    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();    
    }

    IEnumerator OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "Attack")
        {
            GamePad.SetVibration(PlayerIndex.One, 100f, 100f);
            Anim.Play(Destruction_Anim);                  
            yield return new WaitForSeconds(Time);
            
            foreach (Collider2D c in GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            GamePad.SetVibration(PlayerIndex.One, 0, 0);

        }
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = Anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(Destruction_Anim) && stateInfo.normalizedTime >= 0.5)
        {
            Destroy(gameObject);
        }
    }
}
