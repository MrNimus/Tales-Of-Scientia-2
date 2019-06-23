using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float RadioVision; // Vision del enemigo
    public float RadioAtaque; // Area de ataque
    public float Velocidad;   // Velocidad
    GameObject Jugador;        // Objetivo
    Vector3 PosicionInicial; // Posicion
    Animator Anim; //Animador del Enemigo
    Rigidbody2D Rigidbody; // Cuerpo del Enemigo
    void Start()
    {
        Jugador = GameObject.FindGameObjectWithTag("Player");
        PosicionInicial = transform.position;
        Anim = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 Objetivo = PosicionInicial;

        RaycastHit2D Hit = Physics2D.Raycast(transform.position, Jugador.transform.position - transform.position, RadioVision, 1<< LayerMask.NameToLayer("Default"));
        Vector3 Forward = transform.TransformDirection(Jugador.transform.position - transform.position);
        Debug.DrawRay(transform.position, Forward, Color.red);
        if (Hit.collider != null)
        {
            if (Hit.collider.tag == "Player" /*|| Hit.collider.tag == "PlayerAnt" || Hit.collider.tag == "PlayerNext"*/)
            {
                Objetivo = Jugador.transform.position;
            }
        }

        float distancia = Vector3.Distance(Objetivo, transform.position);
        Vector3 dir = (Objetivo - transform.position).normalized;

        if (Objetivo != PosicionInicial && distancia < RadioAtaque)
        {
            Anim.SetFloat("MovX", dir.x);
            Anim.SetFloat("MovY", dir.y);
            Anim.Play("WALK", -1, 0);
        }
        else
        {
            Rigidbody.MovePosition(transform.position + dir * Velocidad * Time.deltaTime);
            Anim.speed = 1;
            Anim.SetFloat("MovX", dir.x);
            Anim.SetFloat("MovY", dir.y);
            Anim.SetBool("Chasing", true);
        }

        if (Objetivo == PosicionInicial && distancia < 0.02f)
            
        {
            transform.position = PosicionInicial;
            Anim.SetBool("Chasing", false);
        }
        Debug.DrawLine(transform.position, Objetivo, Color.green);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RadioAtaque);
        Gizmos.DrawWireSphere(transform.position, RadioVision);
    }
}
