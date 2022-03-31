using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;

public class Sky : MonoBehaviour
{
    [Header(" Settings ")]
    public Transform target;
    [SerializeField] private Material skyMaterial;
    [SerializeField] private Ambiance[] ambiances;

    // Start is called before the first frame update
    void Start()
    {
        SetNewAmbiance();
        UIManager.onNextLevelButtonPressed += SetNewAmbiance;
    }

    private void OnDestroy()
    {
        UIManager.onNextLevelButtonPressed -= SetNewAmbiance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }

    private void SetNewAmbiance()
    {
        Ambiance ambiance = ambiances[Random.Range(0, ambiances.Length)];

        skyMaterial.SetColor("_TopColor", ambiance.topColor);
        skyMaterial.SetColor("_BotColor", ambiance.botColor);
    }
}

[System.Serializable]
public class Ambiance
{
    public Color topColor;
    public Color botColor;
}
