using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Header(" Effects ")]
    [SerializeField] private ParticleSystem coinParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCoinParticles(System.EventArgs enemyTouchedEventArgs)
    {
        EnemyTouchedEvent args = (EnemyTouchedEvent)enemyTouchedEventArgs;

        coinParticles.transform.position = args.enemyPosition + Vector3.up;
        coinParticles.Play();
    }
}
