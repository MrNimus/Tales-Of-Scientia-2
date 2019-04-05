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
        {

        }
    }
    void FixedUpdate()
    {
        RB2D.MovePosition(RB2D.position + Movimiento * Speed * Time.deltaTime);
    }
     
}
