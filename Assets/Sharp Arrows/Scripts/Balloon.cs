using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balloon : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Renderer renderer;
    [SerializeField] private Collider collider;
    [SerializeField] private TextMeshPro multiplierText;
    [SerializeField] private GameEvent popEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Configure(float multiplier, Color color)
    {
        multiplierText.text = "x" + multiplier.ToString("F2");
        renderer.material.SetColor("_BaseColor", color);
    }

    public void Pop()
    {
        popEvent.RaiseEvent(new EnemyTouchedEvent(transform.position));

        renderer.enabled = false;
        multiplierText.enabled = false;
        collider.enabled = false;
    }
}
