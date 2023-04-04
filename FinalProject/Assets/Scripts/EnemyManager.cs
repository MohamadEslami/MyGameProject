using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] S;
    public GameObject Enemy;
    public float TimeToInstantiate;

    public int NumberOfEnemys;
    public int EnemyCounter;

    void Start()
    {
        StartCoroutine(Instantiate_Enemy());
    }

    IEnumerator Instantiate_Enemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeToInstantiate);
            if (EnemyCounter < NumberOfEnemys)
            {
                int rand = Random.Range(0, S.Length);
                Instantiate(Enemy, S[rand].transform.position, Quaternion.identity);
                EnemyCounter++;
            }
        }
    }
}
