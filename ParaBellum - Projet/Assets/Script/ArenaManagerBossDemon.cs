using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManagerBossDemon : MonoBehaviour
{
    public GameObject lightningPrefab;
    public GameObject warningPrefab;
    public Transform[] lightningSpawnPoints;
    public Transform[] warningSpawnPoints;
    public float minSpawnInterval;
    public float maxSpawnInterval;
    private bool canSpawnLightning = true;
    public int nbLightningWhenEvolved;
    public BossDemonMain main;

    private void Start()
    {
        main = GetComponent<BossDemonMain>();
        StartCoroutine(SpawnLightningCoroutine());
    }

    private System.Collections.IEnumerator SpawnLightningCoroutine()
    {
        while (true)
        {
            if (canSpawnLightning && main.evolveOnce == false)
            {
                SpawnLightning();
            }
            
            else if (canSpawnLightning && main.evolveOnce == true)
            {
                SpawnMultipleLightning();
            }

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnLightning()
    {
        int spawnIndex = Random.Range(2, lightningSpawnPoints.Length);
        Transform spawnPoint = lightningSpawnPoints[spawnIndex];
        Transform warningPoint = warningSpawnPoints[spawnIndex -2];

        Instantiate(warningPrefab, warningPoint.position, Quaternion.identity);
        StartCoroutine(SpawnLightningAfterDelay(spawnPoint.position));
    }
    
    private void SpawnMultipleLightning()
    {
        int spawnIndex;
        Transform[] spawnPoints = new Transform[nbLightningWhenEvolved];
        Transform[] warningPoints = new Transform[nbLightningWhenEvolved];
        
        
        for (int i = 0; i < nbLightningWhenEvolved; i++)
        {
            spawnIndex = Random.Range(2, lightningSpawnPoints.Length);
            spawnPoints[i] = lightningSpawnPoints[spawnIndex];
            warningPoints[i] = warningSpawnPoints[spawnIndex -2];
        }

        for (int i = 0; i < nbLightningWhenEvolved; i++)
        {
            Instantiate(warningPrefab, warningPoints[i].position, Quaternion.identity);
        }
        
        StartCoroutine(SpawnMultipleLightningAfterDelay(spawnPoints));
    }
    
    private IEnumerator SpawnLightningAfterDelay(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(lightningPrefab, spawnPosition, Quaternion.identity);
    }

    private IEnumerator SpawnMultipleLightningAfterDelay(Transform[] spawnPositions)
    {
        yield return new WaitForSeconds(2f);
        
        for (int i = 0; i < nbLightningWhenEvolved; i++)
        {
            Instantiate(lightningPrefab, spawnPositions[i].position, Quaternion.identity);
        }
    }

    // Méthode appelée pour désactiver la génération des éclairs (par exemple, lorsque le boss est mort)
    public void StopSpawningLightning()
    {
        canSpawnLightning = false;
    }
    
    public void ToggleSpawningLightning()
    {
        canSpawnLightning = true;
    }
}
