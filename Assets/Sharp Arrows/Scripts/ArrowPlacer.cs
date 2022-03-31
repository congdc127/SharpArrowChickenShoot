using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowPlacer : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private SphereCollider playerCollider;
    [SerializeField] private TextMeshPro arrowCountText;

    [Header(" Settings ")]
    [SerializeField] private Transform arrowsParent;
    [SerializeField] private Transform arrowPrefab;

    [Header(" Formation Settings ")]
    [Range(0f, 1f)]
    [SerializeField] private float radiusFactor;
    [Range(0f, 1f)]
    [SerializeField] private float angleFactor;

    [Header(" Events ")]
    [SerializeField] private GameEvent enemyTouchedEvent;

    // Start is called before the first frame update
    void Start()
    {
        UpdateArrowCount();
    }

    // Update is called once per frame
    void Update()
    {
        FermatSpiralPlacement();
        UpdateArrowCount();
    }

    public float GetPlayerColliderRadius()
    {
        return playerCollider.radius;
    }

    public void DoorTouchedCallback(Door door)
    {
        int currentArrowsAmount = arrowsParent.childCount;
        int targetArrowsAmount = door.GetTargetArrowCount(currentArrowsAmount);

        if (targetArrowsAmount < currentArrowsAmount)
            RemoveArrows(currentArrowsAmount - targetArrowsAmount);
        else
            AddArrows(targetArrowsAmount - currentArrowsAmount);

        UpdateFormationRadius();

        UpdatePlayerCollider();
        UpdateArrowCount();

        Taptic.Light();
    }

    private void UpdateFormationRadius()
    {
        radiusFactor = 0.15f * (1f - ((float)Mathf.Min(arrowsParent.childCount, 2000) / 2000));
    }

    public void EnemiesTouchedCallback(Collider[] touchedEnemies)
    {
        foreach (Collider enemy in touchedEnemies)
        {
            RemoveArrows(3);

            Enemy touchedEnemy = enemy.GetComponent<Enemy>();
            touchedEnemy.Die();

            enemyTouchedEvent.RaiseEvent(new EnemyTouchedEvent(enemy.transform.position));

        }

        Taptic.Light();
    }

    public void BalloonTouchedCallback(Collider[] touchedBalloons) 
    {
        foreach(Collider balloonCollider in touchedBalloons)
        {
            int arrowsToRemove = Mathf.Max(10, arrowsParent.childCount / 10);

            RemoveArrows(arrowsToRemove, true);
            balloonCollider.GetComponent<Balloon>().Pop();
        }

        Taptic.Light();

    }


    private void RemoveArrows(int amount, bool poppingBalloons = false)
    {
        for (int i = 0; i < amount; i++)
        {
            if(arrowsParent.childCount <= 0)
            {
                if(!poppingBalloons)
                {
                    SetGameover();
                    return;
                }
                else
                {
                    SetLevelComplete();
                    return;
                }
            }

            if(arrowsParent.childCount - 1 - i >= 0)
                Destroy(arrowsParent.GetChild(arrowsParent.childCount - 1 - i).gameObject);

            UpdateArrowCount();
        }

    }

    private void SetGameover()
    {
        playerController.SetGameover();
    }

    private void SetLevelComplete()
    {
        playerController.SetLevelComplete();
    }

    private void AddArrows(int amount)
    {
        for (int i = 0; i < amount; i++)
            AddArrow();        
    }

    private void AddArrow()
    {
        Instantiate(arrowPrefab, arrowsParent);
    }

    private void FermatSpiralPlacement()
    {
        float goldenAngle = 137.5f * angleFactor;

        for (int i = 0; i < arrowsParent.childCount; i++)
        {
            float x = radiusFactor * Mathf.Sqrt(i + 1) * Mathf.Cos(Mathf.Deg2Rad * goldenAngle * (i + 1));
            float z = radiusFactor * Mathf.Sqrt(i + 1) * Mathf.Sin(Mathf.Deg2Rad * goldenAngle * (i + 1));

            Vector3 runnerLocalPosition = new Vector3(x, z, 0);
            arrowsParent.GetChild(i).localPosition = Vector3.Lerp(arrowsParent.GetChild(i).localPosition, runnerLocalPosition, 0.1f);
        }
    }

    public float GetSquadRadius()
    {
        return radiusFactor * Mathf.Sqrt(arrowsParent.childCount);
    }


    private void UpdatePlayerCollider()
    {
        playerCollider.radius = GetSquadRadius();
    }

    private void UpdateArrowCount()
    {
        arrowCountText.text = arrowsParent.childCount.ToString();

        if (arrowsParent.childCount <= 0 && JetSystems.UIManager.IsGame())
        {
            if (!playerController.IsPoppingBalloons())
                SetGameover();
            else
                SetLevelComplete();
        }
    }
}

public class EnemyTouchedEvent : System.EventArgs
{
    public Vector3 enemyPosition;

    public EnemyTouchedEvent(Vector3 enemyPosition)
    {
        this.enemyPosition = enemyPosition;
    }
}