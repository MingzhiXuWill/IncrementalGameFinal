using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public string animalName;
    public int cost;
    public float incomePerSec;

    public float growthTimeMax = 10;
    public float growthTimer = 0;
    public float growthStageMax = 3;
    public float growthStage = 1;

    void Start()
    {
        Animator m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.speed = 0.2f;
    }

    void Update()
    {
        Grow();

        if (GameManager.instance != null)
        {
            GameManager.instance.AddMoney(GetTouristIncome());
        }

        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            // Create a ray from the camera to our mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits any collider (3D)
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit collider is this animal
                if (hit.collider.gameObject == gameObject)
                {
                    Feed(); // Call the Feed function
                }
            }
        }
    }

    

    public float GetTouristIncome()
    {
        return incomePerSec * Time.deltaTime;
    }
    
    private void Grow()
    {
        if (growthTimer <= growthTimeMax)
        {
            growthTimer += Time.deltaTime;
        }
    }

    private bool IsFullyGrown()
    {
        return growthTimer >= growthTimeMax;
    }

    private void Feed()
    {
        if (growthStage < growthStageMax && IsFullyGrown())
        {
            growthStage++;
            growthTimer = 0;
        }
    }
}
