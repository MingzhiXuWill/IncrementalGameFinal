using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Animal : MonoBehaviour
{
    public string animalName;
    public int cost;
    public float incomePerSec;

    public Vector3 patrolPointA = new Vector3(0, 0, 0);
    public Vector3 patrolPointB = new Vector3(10, 0, 0);
    public float patrolSpeed = 5f;
    public Vector2 patrolPause = new Vector3(1f, 2f);
    private Vector3 patrolTarget;
    private bool isWaiting = false;

    public float growthTimeMax = 10;
    public float growthTimer = 0;
    public float growthStageMax = 3;
    public float growthStage = 1;

    public float animatorSpeed = 0.2f;

    public GameObject visual;

    private Animator m_Animator;

    private GameObject feedIcon;

    void Start()
    {
        m_Animator = visual.transform.GetChild(1).GetComponent<Animator>();
        m_Animator.speed = animatorSpeed;

        NewPatrolPosition();
        feedIcon = transform.GetChild(1).gameObject;
    }

    void Update()
    {
        Grow();
        Patrol();

        if (GameManager.instance != null)
        {
            GameManager.instance.AddMoney(GetIncome());
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == visual.transform.GetChild(1).gameObject)
                {
                    Feed();
                }
            }
        }
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolTarget, patrolSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, patrolTarget) < 0.1f && !isWaiting)
        {
            StartCoroutine(PauseAndMoveToNewPosition()); // Start the pause and move coroutine
            isWaiting = true;
        }
        transform.LookAt(patrolTarget);
    }

    IEnumerator PauseAndMoveToNewPosition()
    {
        m_Animator.speed = 0f;

        float pauseDuration = Random.Range(patrolPause.x, patrolPause.y); // Random pause between 1 and 5 seconds
        yield return new WaitForSeconds(pauseDuration); // Wait for the pause duration
        NewPatrolPosition(); // Move to a new position after the pause
    }


    void NewPatrolPosition()
    {
        m_Animator.speed = animatorSpeed;

        isWaiting = false;

        float minX = Mathf.Min(patrolPointA.x, patrolPointB.x);
        float maxX = Mathf.Max(patrolPointA.x, patrolPointB.x);
        float minZ = Mathf.Min(patrolPointA.z, patrolPointB.z);
        float maxZ = Mathf.Max(patrolPointA.z, patrolPointB.z);
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        patrolTarget = new Vector3(randomX, 0, randomZ); // Assumed movement is horizontal on the XZ plane
    }


    public float GetIncome()
    {
        return incomePerSec * Time.deltaTime;
    }
    
    private void Grow()
    {
        if (growthStage >= growthStageMax)
        {
            feedIcon.SetActive(false);
            return;
        }

        if (growthTimer <= growthTimeMax)
        {
            if (feedIcon.gameObject.activeInHierarchy == true)
            {
                feedIcon.SetActive(false);
            }

            growthTimer += Time.deltaTime;
        }
        else 
        {
            if (feedIcon.gameObject.activeInHierarchy == false)
            {
                feedIcon.SetActive(true);
            }
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
