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

    private void Start()
    {
        currentCooldownTime = cooldownTime;
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        currentCooldownTime -= Time.deltaTime;

        if((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && currentCooldownTime <= 0f)
        {
            animator.SetTrigger("Hit");
            currentCooldownTime = cooldownTime;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void OnTriggerEnter(Collider other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy)
        {
            enemy.GetHit();
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(respawnTime);
        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], enemySpawnPoint.position, Quaternion.identity);
    }
}
