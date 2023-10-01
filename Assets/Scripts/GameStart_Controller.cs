using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using TMPro;

public class GameStart_Controller : MonoBehaviour
{
    [SerializeField] GameObject smallEnemy;
    [SerializeField] GameObject mediumEnemy;
    [SerializeField] GameObject LargeEnemy;

    [SerializeField] List<Vector3> spawnpoints;

    [SerializeField] int numberOfSpawnsSmall = 5;
    [SerializeField] int numberOfSpawnsMedium = 3;
    [SerializeField] int numberOfSpawnsLarge = 1;

    [SerializeField] GameObject waveCounter;

    [SerializeField] int currWave = 1;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(5f);
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfSpawnsSmall; i++)
        {
            GameObject currSmallEnemy = Instantiate(smallEnemy, transform);
            currSmallEnemy.transform.position = spawnpoints[Random.RandomRange(0, spawnpoints.Count)];
        }
        numberOfSpawnsSmall += 5;

        for (int i = 0; i < numberOfSpawnsMedium; i++)
        {
            GameObject currMediumEnemy = Instantiate(mediumEnemy, transform);
            currMediumEnemy.transform.position = spawnpoints[Random.RandomRange(0, spawnpoints.Count)];
        }

        numberOfSpawnsMedium += 3;

        for (int i = 0; i < numberOfSpawnsLarge; i++)
        {
            GameObject currLargeEnemy = Instantiate(LargeEnemy, transform);
            currLargeEnemy.transform.position = spawnpoints[Random.RandomRange(0, spawnpoints.Count)];
        }

        numberOfSpawnsLarge += 1;
    }

    public void FixedUpdate()
    {
        if(transform.childCount == 0)
        {
            currWave++;
            SpawnEnemies();
            SetWaveCounter();

        }
    }

    private void SetWaveCounter()
    {
        waveCounter.GetComponent<TMP_Text>().SetText("Wave " + currWave.ToString());
        waveCounter.GetComponent<Animation>().Play();
    }


}
