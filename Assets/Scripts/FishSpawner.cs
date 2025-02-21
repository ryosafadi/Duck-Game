using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    [System.Serializable]
    public class FishSpawnData
    {
        public GameObject fishPrefab; // Prefab of the fish
        public float shallowWeight;  // Spawn weight in shallow depth
        public float mediumWeight;   // Spawn weight in medium depth
        public float deepWeight;     // Spawn weight in deep depth
    }

    public List<FishSpawnData> fishToSpawn; // List of fish and their spawn weights
    public Transform spawnArea;            // Area where fish can spawn
    public float shallowDepth = -5f;       // Y position for shallow depth
    public float mediumDepth = -15f;       // Y position for medium depth

    void Start()
    {
        SpawnFish();
    }

    void SpawnFish()
    {
        for (int i = 0; i < 50; i++)
        {
            Vector3 pos = GetRandomSpawnPosition();

            // Calculate total weight based on depth
            float totalWeight = 0f;
            foreach (var fishData in fishToSpawn)
            {
                totalWeight += GetWeightForDepth(fishData, pos);
            }

            // Pick a random weight value
            float randomValue = Random.Range(0f, totalWeight);

            // Determine which fish to spawn based on the random value
            foreach (var fishData in fishToSpawn)
            {
                float weight = GetWeightForDepth(fishData, pos);
                if (randomValue < weight)
                {
                    Spawn(fishData.fishPrefab);
                    break;
                }
                randomValue -= weight;
            }
        }
    }

    float GetWeightForDepth(FishSpawnData fishData, Vector3 spawnPos)
    {
        // Determine spawn weight based on spawn position
        if (spawnPos.y > shallowDepth)
        {
            return fishData.shallowWeight; // Shallow depth
        }
        else if (spawnPos.y > mediumDepth)
        {
            return fishData.mediumWeight;  // Medium depth
        }
        else
        {
            return fishData.deepWeight;    // Deep depth
        }
    }

    void Spawn(GameObject fishPrefab)
    {
        // Randomize spawn position within the spawn area
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instantiate the fish
        Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Get a random position within the spawn area
        Vector3 center = spawnArea.position;
        Vector3 size = spawnArea.localScale;

        float x = center.x + Random.Range(-size.x / 2f, size.x / 2f);
        float y = center.y + Random.Range(-size.y / 2f, size.y / 2f);

        return new Vector3(x, y, center.z);
    }
}