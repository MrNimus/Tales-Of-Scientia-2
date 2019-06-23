using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public float AntesDestruir;
    public Vector2 mov;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(mov.x, mov.y, 0) * speed * Time.deltaTime;
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Object")
        {
            yield return new WaitForSeconds(AntesDestruir);
            Destroy(gameObject);
        }
        else if (other.tag != "Player" && other.tag == "Attack")
        {
            Destroy(gameObject);
        }
        else
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    } 
}
