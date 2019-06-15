using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class PlayerMovemente : MonoBehaviour
{
    
    public float Speed = 4f;
    Animator Animador;
    Rigidbody2D RB2D;
    Vector2 Movimiento;
    public GameObject MapaInicial;
    CircleCollider2D AttackCollider;
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
        AttackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
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


           if (Input.GetAxis("Fire1") == 1 && !attacking) 
         {

            Animador.SetTrigger("Ataque");
            Animador.SetBool("Desenvainada", true);
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
            AttackCollider.offset = new Vector2(Movimiento.y, (Movimiento.x / 3)*-1);
        }
        if (attacking)
        {
            float playbacktime = stateInfo.normalizedTime;
            if (playbacktime > 0.089 && playbacktime < 0.178)
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
