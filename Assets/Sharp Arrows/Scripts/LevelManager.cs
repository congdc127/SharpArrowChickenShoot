using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;
using Cinemachine;
using Dreamteck.Splines;

public class LevelManager : MonoBehaviour
{
    public delegate void OnLevelInstantiated();
    public static OnLevelInstantiated onLevelInstantiated;

    [Header(" Settings ")]
    [SerializeField] private GameObject[] levelsPrefabs;
    int level;

    private void Awake()
    {
        level = PlayerPrefs.GetInt("LEVEL");

        UIManager.onNextLevelButtonPressed += SpawnNextLevel;
    }

    private void OnDestroy()
    {
        UIManager.onNextLevelButtonPressed -= SpawnNextLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnLevel()
    {
        int correctedLevelIndex = level % levelsPrefabs.Length;

        transform.Clear();
        GameObject currentLevel = Instantiate(levelsPrefabs[correctedLevelIndex], transform);

        onLevelInstantiated?.Invoke();

        LeanTween.delayedCall(Time.deltaTime, ShowSpline);
    }

    private void SpawnNextLevel()
    {
        level++;
        PlayerPrefs.SetInt("LEVEL", level);

        SpawnLevel();

       AdManager.instance.ShowInterstitialAd();
    }

    private void ShowSpline()
    {
        SplineMesh splineMesh = FindObjectOfType<SplineMesh>();
        splineMesh.RebuildImmediate();
    }
}
