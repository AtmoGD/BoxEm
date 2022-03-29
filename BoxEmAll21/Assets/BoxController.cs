using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private float respawnTime = 5f;
    [SerializeField] private float cooldownTime = 5f;

    private float currentCooldownTime = 0f;

    void Update()
    {
        currentCooldownTime -= Time.deltaTime;

        if(Input.GetMouseButtonDown(0) && currentCooldownTime <= 0f)
        {
            animator.SetTrigger("Hit");
            currentCooldownTime = cooldownTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy)
        {
            enemy.GetHit();
            StartCoroutine(SpawnEnemy());
        }
    }

    // private void OnCollisionEnter(Collision other) {
    //     // if(currentCooldownTime > 0f) return;

    //     Enemy enemy = other.gameObject.GetComponent<Enemy>();
    //     if(enemy != null)
    //     {
    //         enemy?.GetHit();
    //         StartCoroutine(SpawnEnemy());
    //     }
    // }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(respawnTime);
        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], enemySpawnPoint.position, Quaternion.identity);
    }
}
