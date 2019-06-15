using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using XInputDotNetPure;


public class PlayerMovemente : MonoBehaviour
{
    
    public float Speed = 4f;
    Animator Animador;
    Rigidbody2D RB2D;
    Vector2 Movimiento;
    public GameObject MapaInicial;
    BoxCollider2D AttackCollider;
    public float A, D;
    
    void Awake()
    {
        Assert.IsNotNull(MapaInicial);
    }
    // Start is called before the first frame update
    void Start()
    {
        Animador = GetComponent<Animator>();
        RB2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<MainCámera>().SetBound(MapaInicial);
        AttackCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        AttackCollider.enabled = false; 
       
    }

    // Update is called once per frame
    void Update()
    {
        
        Movimiento = new Vector2(
            Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Movimiento != Vector2.zero)
        {
            Animador.SetFloat("MovimientoX", Movimiento.x);
            Animador.SetFloat("MovimientoY", Movimiento.y);
            Animador.SetBool("Walking", true);
        }
        else
        {
            Animador.SetBool("Walking", false);
        }
        AnimatorStateInfo stateInfo = Animador.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("Player Attack");


           if (Input.GetAxis("Fire1") == 1 && !attacking ) 
         {
            StartCoroutine(Ataque());
            IEnumerator Ataque()
            {
                Animador.SetTrigger("Ataque");
                GamePad.SetVibration(PlayerIndex.One, A, D);
                Animador.SetBool("Desenvainada", true);
                yield return new WaitForSeconds(0.2f);
                GamePad.SetVibration(PlayerIndex.One, 0, 0);
            }

        }
        else if (Input.GetAxis("Fire1") == -1)
        {
            Animador.SetBool("Desenvainada", false);
        }
        
      

        if (Input.GetKeyDown(KeyCode.R))
        {
            Animador.SetBool("Desenvainada", false);
        }
        if (Movimiento !=Vector2.zero)
        {
            AttackCollider.offset = new Vector2(Movimiento.y/5, (Movimiento.x /19)*-1);
        }
        if (attacking)
        {
            float playbacktime = stateInfo.normalizedTime;
            if (playbacktime > 0.0120 && playbacktime < 0.178)
            {
                AttackCollider.enabled = true;
            }
            else
            {
                AttackCollider.enabled = false;
            }
            
        }
    }
    void FixedUpdate()
    {
        RB2D.MovePosition(RB2D.position + Movimiento * Speed * Time.deltaTime);
    }
     
}
