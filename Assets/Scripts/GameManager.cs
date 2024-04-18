using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> animalPrefabs; // Animal Prefabs
    public Transform animalSpawnPoint; // Animal spawn position
    public float spawnRange = 10f; // Animal spawn range offset
    public float playerMoney; // Player Money
    public TextMeshProUGUI moneyText; // Player Money Text

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateMoneyText();
    }

    public void BuyAnimal(int cost)
    {
        if (playerMoney >= cost)
        {
            playerMoney -= cost;
            SpawnAnimal();
        }
        else
        {
            Debug.Log("????????????????????????????");
        }
    }

    void SpawnAnimal()
    {
        if (animalPrefabs.Count > 0 && animalSpawnPoint != null)
        {
            int index = Random.Range(0, animalPrefabs.Count);
            GameObject animalPrefab = animalPrefabs[index];

            // Calculate spawn position around the specified spawn point
            Vector3 spawnPos = new Vector3(
                animalSpawnPoint.position.x + Random.Range(-spawnRange, spawnRange),
                animalSpawnPoint.position.y, // Assuming animals are spawning on the ground/flat surface
                animalSpawnPoint.position.z + Random.Range(-spawnRange, spawnRange));

            Instantiate(animalPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void AddMoney(float amount)
    {
        playerMoney += amount;
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Money: {playerMoney}";
        }
    }
}
