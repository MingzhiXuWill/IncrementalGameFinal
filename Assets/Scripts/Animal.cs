using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public string animalName;
    public int cost;
    public float incomePerSec;
    public float growthValue; // 成长值
    public float growthRate; // 成长率，定义了动物成长的速度

    private float totalIncome; // 累计收入

    // 在Unity编辑器中设置动物的属性
    void Start()
    {
        Animator m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.speed = 0.2f;
    }

    void Update()
    {
        // 假设我们想每秒调用一次来增加玩家的金钱
        if (GameManager.instance != null)
        {
            GameManager.instance.AddMoney(EarnIncome(Time.deltaTime));
        }
    }

    public float EarnIncome(float deltaTime)
    {
        // 基于时间的收入计算，确保公平计算不同帧率下的收入
        return incomePerSec * deltaTime;
    }

    private void Grow(float deltaTime)
    {
        // 根据deltaTime增加成长值
        growthValue += growthRate * deltaTime;
        // 你可以在这里添加逻辑，比如当成长值达到一定水平时，动物可以"进化"或改变状态
    }
}
