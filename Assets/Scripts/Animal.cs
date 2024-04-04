using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public string animalName;
    public int cost;
    public float incomePerSec;
    public float growthValue; // �ɳ�ֵ
    public float growthRate; // �ɳ��ʣ������˶���ɳ����ٶ�

    private float totalIncome; // �ۼ�����

    // ��Unity�༭�������ö��������
    void Start()
    {
        Animator m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.speed = 0.2f;
    }

    void Update()
    {
        // ����������ÿ�����һ����������ҵĽ�Ǯ
        if (GameManager.instance != null)
        {
            GameManager.instance.AddMoney(EarnIncome(Time.deltaTime));
        }
    }

    public float EarnIncome(float deltaTime)
    {
        // ����ʱ���������㣬ȷ����ƽ���㲻ͬ֡���µ�����
        return incomePerSec * deltaTime;
    }

    private void Grow(float deltaTime)
    {
        // ����deltaTime���ӳɳ�ֵ
        growthValue += growthRate * deltaTime;
        // ���������������߼������統�ɳ�ֵ�ﵽһ��ˮƽʱ���������"����"��ı�״̬
    }
}
