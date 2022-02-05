using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ChunkController : MonoBehaviour
{
    [Header("Objects From Scene"), SerializeField]
    private SphereController sphere;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Chunk firstChunk;

    [Header("Prefabs"), SerializeField]
    private List<Chunk> chunkPrefabs = new List<Chunk>();

    private List<Chunk> spawnedChunks = new List<Chunk>();

    private void Start()
    {
        spawnedChunks.Add(firstChunk);
    }

    private void Update()
    {
        if (gameController.IsGame() && sphere.transform.position.x > spawnedChunks.Last().End.position.x - 4f)
        {
            SpawnChunk(gameController.GetBoost());
        }
    }

    public void RemoveSpawnedChuncks()
    {
        foreach(var chunk in spawnedChunks)
        {
            if(chunk != firstChunk)
            {
                Destroy(chunk.gameObject);
            }
        }
        spawnedChunks.RemoveAll(x => x != firstChunk);
    }

    private void SpawnChunk(int boostLevel)
    {
        var randomChunkPrefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Count)];

        Chunk newChunk = Instantiate(randomChunkPrefab);

        var position = spawnedChunks[spawnedChunks.Count - 1].End.position - randomChunkPrefab.Begin.localPosition;
        var lengthDifference = randomChunkPrefab.transform.localScale.x - spawnedChunks[spawnedChunks.Count - 1].transform.localScale.x;
        newChunk.transform.position = position + new Vector3(lengthDifference, 0, 0);

        newChunk.transform.localScale = new Vector3(boostLevel / 2 + 1, 1, 1);

        spawnedChunks.Add(newChunk);
        if (spawnedChunks.Count >= 4)
        {
            Destroy(spawnedChunks[1].gameObject);
            spawnedChunks.RemoveAt(1);
        }
    }
}