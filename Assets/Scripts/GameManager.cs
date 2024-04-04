using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> animalPrefabs; // �洢����Prefabs���б�
    public float spawnRangeX = 10f; // X���ϵ����ɷ�Χ
    public float spawnRangeZ = 10f; // Z���ϵ����ɷ�Χ
    public float playerMoney; // ��ҵĽ�Ǯ
    public TextMeshProUGUI moneyText; // TMP Text���������

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
        // �������Ƿ����㹻�Ľ�Ǯ������
        if (playerMoney >= cost)
        {
            playerMoney -= cost; // ������ҵĽ�Ǯ
            SpawnAnimal(); // ����ɹ������ɶ���
        }
        else
        {
            Debug.Log("û���㹻�Ľ�Ǯ����������");
        }
    }

    void SpawnAnimal()
    {
        if (animalPrefabs.Count > 0)
        {
            // ���б������ѡ��һ��Prefab
            int index = Random.Range(0, animalPrefabs.Count);
            GameObject animalPrefab = animalPrefabs[index];

            // �������λ��
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));

            // ���ɶ���Prefab
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
