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
    public GameObject Proyectil;
    bool NoMove;
  
    Aura aura;
    
    void Awake()
    {
        Assert.IsNotNull(MapaInicial);
        Assert.IsNotNull(Proyectil);
    }
    // Start is called before the first frame update
    void Start()
    {
        Animador = GetComponent<Animator>();
        
        RB2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<MainCámera>().SetBound(MapaInicial);
        AttackCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        AttackCollider.enabled = false;
        aura = transform.GetChild(1).GetComponent<Aura>() ;
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento_Jugador();
        Animacion_Movimiento();
        Ataque_Principal();
        GuardarArma();
        Ataque_Cargado();
        NoMover();
    }
    void FixedUpdate()
    {
        RB2D.MovePosition(RB2D.position + Movimiento * Speed * Time.deltaTime);
    }
    void Movimiento_Jugador()
    {
        Movimiento = new Vector2(
                   Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")), Mathf.RoundToInt(Input.GetAxisRaw("Vertical")));

    }
    void Animacion_Movimiento()
    {
        if (NoMove == false)
        {
            
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
        }
    }
    void Ataque_Principal()
    {
        AnimatorStateInfo stateInfo = Animador.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("Player Attack");


        if (Input.GetAxis("Fire1") == 1 && !attacking)
        {
            StartCoroutine(Ataque());
            IEnumerator Ataque()
            {
                Animador.SetTrigger("Ataque");
                GamePad.SetVibration(PlayerIndex.One, 0.2f, 0.2f);
                Animador.SetBool("Desenvainada", true);
                yield return new WaitForSeconds(0.2f);
                GamePad.SetVibration(PlayerIndex.One, 0, 0);
            }

        }
        else if (Input.GetAxis("Fire1") == -1)
        {
            Animador.SetBool("Desenvainada", false);
        }
        if (Movimiento != Vector2.zero)
        {
            AttackCollider.offset = new Vector2(Movimiento.y /7, (Movimiento.x / 19) * -1);
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
    void GuardarArma()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Animador.SetBool("Desenvainada", false);
        }
    }
    void NoMover()
    {
        if (NoMove)
        {
            Movimiento = Vector2.zero;
        }
    }
    void Ataque_Cargado()
    {
        AnimatorStateInfo animatorStateInfo = Animador.GetCurrentAnimatorStateInfo(0);
        bool loading = animatorStateInfo.IsName("Player Attack");
        StartCoroutine(Hola());
        IEnumerator Hola()
        {
            if (Input.GetButton("ChargeAttack") && Input.GetButtonDown("Fire2"))
            {
                Animador.SetTrigger("Loading");
                aura.StartAura();
                NoMove = true;  
                Animador.SetBool("Walking", false);
               

            }
            else if (Input.GetButtonUp("ChargeAttack") && !(Input.GetButtonDown("Fire2")))
            {
                
                if (aura.isloaded() == true)
                {
                    Animador.SetTrigger("AtaqueCargado");
                    yield return new WaitForSeconds(0.5f);
                    float Angulo = Mathf.Atan2(Animador.GetFloat("MovimientoY"), Animador.GetFloat("MovimientoX")) * Mathf.Rad2Deg;
                    GameObject Slashe = Instantiate(Proyectil, transform.position, Quaternion.AngleAxis(Angulo, Vector3.forward));
                    Slash slash = Slashe.GetComponent<Slash>();
                    slash.mov.x = Animador.GetFloat("MovimientoX");
                    slash.mov.y = Animador.GetFloat("MovimientoY");
                    GamePad.SetVibration(PlayerIndex.One, 50, 50);
                    yield return new WaitForSeconds(0.5f);
                    GamePad.SetVibration(PlayerIndex.One, 0, 0);
                    yield return new WaitForSeconds(0.2f);
                    aura.AuraStop();
                }
                aura.AuraStop();
                
                
                NoMove = false;

            }
        }
    }
}
