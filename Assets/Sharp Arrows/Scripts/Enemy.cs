using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Animator animator;
    [SerializeField] private Collider collider;
    [SerializeField] private GameObject arrowsParent;

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
        ShowArrows();
    }

    private void ShowArrows()
    {
        arrowsParent.SetActive(true);
    }
}
