using System.Collections.Generic;
using UnityEngine;
public class ChunkPlacer : MonoBehaviour
{
    /// <summary>
    /// Первый чанк на сцене
    /// </summary>
    [SerializeField] Chunk firstChunk;
    /// <summary>
    /// Массив доступных чанков
    /// </summary>
    [SerializeField] Chunk[] chunkPrefabs;

    /// <summary>
    /// Массив чанков существующих на сцене
    /// </summary>
    private List<Chunk> spawnedChunks = new List<Chunk>();
    /// <summary>
    /// Скрипт SphereController
    /// </summary>
    private SphereController sphereController;
    /// <summary>
    /// Уровень ускорения сферы
    /// </summary>
    private int boostLevel;
    /// <summary>
    /// Переменная показывающая мертв ли игрок
    /// </summary>
    private bool isPlayerDead;
    /// <summary>
    /// Transform сферы
    /// </summary>
    private Transform sphereTransform;
    private void Start()
    {
        //Сохранение Transform-а сферы в переменную
        sphereTransform = GameObject.Find("Sphere").GetComponent<Transform>();
        //Сохранение скрипта сферы в переменную
        sphereController = GameObject.Find("Sphere").GetComponent<SphereController>();
        //Сохранение экрана смерти в переменную
        isPlayerDead = sphereController.isDead;
        //Добавление первого чанка в существующие на сцене чанки
        spawnedChunks.Add(firstChunk);
    }
    private void Update()
    {
        //Перенос уровня ускорения из скрипта сферы
        boostLevel = sphereController.boost;
        //Если сфера почти у края чанка
        if (sphereTransform.position.x > spawnedChunks[spawnedChunks.Count - 1].end.position.x - 4f)
            SpawnChunk(boostLevel);      
    }
    /// <summary>
    /// Метод спавнящий чанки
    /// </summary>
    private void SpawnChunk(int boostLevel)
    {
        //Если игрок проиграл
        if(isPlayerDead)
            //Очистить лист заспавненных чанков
            spawnedChunks.Clear();
        //Переменная для хранения следующего чанка
        var ch = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
        //Вычисляем размер чанка исходя из текущего уровня ускорения
        ch.transform.localScale = new Vector3(boostLevel / 2 + 1, 1, 1);
        //Вычисляем разницу в размерах между текущим и предыдущим чанками
        var lengthDifference = ch.transform.localScale.x - spawnedChunks[spawnedChunks.Count - 1].transform.localScale.x;
        //Задаем будущее положение чанка
        var position = spawnedChunks[spawnedChunks.Count - 1].end.position - ch.begin.localPosition;
        //Вызываем чанк
        Chunk newChunk = Instantiate(ch, position + new Vector3(lengthDifference, 0, 0), Quaternion.identity);
        //Добавляем чанк в список заспавненных
        spawnedChunks.Add(newChunk);
        //Если количество чанков на сцене больше или равно 4
        if (spawnedChunks.Count >= 4)
        {
            //Уничтожаем первый чанк в списке запспавнненных
            Destroy(spawnedChunks[0].gameObject);
            //Удаляем его из списка заспавненных
            spawnedChunks.RemoveAt(0);
        }
    }
}