using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;

public class Enemy : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject arrowsParent;
    public Animator chickenAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        collider.enabled = false;
        animator.Play("Die");
        this.chickenAnimator.Play("Die");
        UIManager.AddCoins(50);
        ShowArrows();
    }

    private void ShowArrows()
    {
        arrowsParent.SetActive(true);
    }
}
