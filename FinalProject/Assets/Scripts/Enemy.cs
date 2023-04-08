using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    private AIPath aiPath;
    private AIDestinationSetter ds;

    public float MoveSpeed;

    public GameObject Bullet;
    public GameObject Gun;
    public GameObject Blast;

    public float PowerShoot;

    public float distanceToShoot;
    private float LatShoot;

    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = MoveSpeed;

        ds = GetComponent<AIDestinationSetter>();

        if (GameObject.FindObjectOfType<Tank>() == null)
        {
            print("Cant Find Player!");
        }
        else
        {
            ds.target = GameObject.FindObjectOfType<Tank>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MyPosition = transform.position;
        Vector2 PlayerPosition = ds.target.position;

        float Distance_ = Vector2.Distance(MyPosition, PlayerPosition);

        if (Distance_ <= distanceToShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > LatShoot + PowerShoot)
        {
//            GetComponent<AudioSource>().Play();
            GameObject Bullet_ = Instantiate(Bullet, Gun.transform.position, Quaternion.identity) as GameObject;
            Bullet_.transform.right = transform.up;
            LatShoot = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "p_enemy")
        {
            //ds.target.GetComponent<Tank>().Kill += 1;
            Instantiate(Blast, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
