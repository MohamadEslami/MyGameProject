using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float MoveSpeed;

    public GameObject Exp;

    void Update()
    {
        transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Instantiate(Exp, collision.contacts[0].point, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
