using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider col;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject cellRoot;
    [SerializeField] private List<Rigidbody> enemyCells = new List<Rigidbody>();

    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource hitSound;

    [SerializeField] private float force = 150f;
    [SerializeField] private float forceXRange = 5f;
    [SerializeField] private float forceYRange = 5f;


    [SerializeField] private float destroyTimeMax = 5f;
    [SerializeField] private float destroyTimeMin = 2f;

    private float randX
    {
        get
        {
            return Random.Range(-forceXRange, forceXRange);
        }
    }

    private float randY
    {
        get
        {
            return Random.Range(-forceYRange, forceYRange);
        }
    }

    private float destroyTime
    {
        get
        {
            return Random.Range(destroyTimeMin, destroyTimeMax);
        }
    }

    public void GetHit()
    {
        Destroy(col);

        enemy.SetActive(false);
        cellRoot.SetActive(true);

        hitSound?.Play();

        foreach (Rigidbody cell in enemyCells)
        {
            cell.AddForce(Vector3.forward * force + Vector3.right * randX + Vector3.up * randY);
            Destroy(cell.gameObject, destroyTime);
        }

        Destroy(gameObject, destroyTimeMax);
    }

    public void PlayStartSound()
    {
        startSound?.Play();
    }
}
