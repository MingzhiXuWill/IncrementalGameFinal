using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> animalPrefabs; // 存储动物Prefabs的列表
    public float spawnRangeX = 10f; // X轴上的生成范围
    public float spawnRangeZ = 10f; // Z轴上的生成范围
    public float playerMoney; // 玩家的金钱
    public TextMeshProUGUI moneyText; // TMP Text对象的引用

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
        // 检查玩家是否有足够的金钱购买动物
        if (playerMoney >= cost)
        {
            playerMoney -= cost; // 减少玩家的金钱
            SpawnAnimal(); // 购买成功后生成动物
        }
        else
        {
            Debug.Log("没有足够的金钱购买这个动物。");
        }
    }

    void SpawnAnimal()
    {
        if (animalPrefabs.Count > 0)
        {
            // 从列表中随机选择一个Prefab
            int index = Random.Range(0, animalPrefabs.Count);
            GameObject animalPrefab = animalPrefabs[index];

            // 随机生成位置
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));

            // 生成动物Prefab
            Instantiate(animalPrefab, spawnPos, animalPrefab.transform.rotation);
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
