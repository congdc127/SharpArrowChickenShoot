using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLine : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private Transform balloonsParent;
    [SerializeField] private float zSpacing;

    // Start is called before the first frame update
    void Start()
    {
        PlaceBonusLines();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaceBonusLines()
    {
        // Get the z spacing of the bonus lines
        Vector3 startPosition = transform.position; // + Vector3.forward * (transform.position.z + zSpacing / 2);
        int bonusLinesCount = balloonsParent.childCount;

        // Spawn the bonus lines
        for (int i = 0; i < balloonsParent.childCount; i++)
        {
            // Define the bonus line position
            Vector3 targetPosition = startPosition + Vector3.forward * zSpacing * i;

            // Define the target color
            Color targetColor = Color.HSVToRGB((float)i / bonusLinesCount, 0.8f, 0.9f);

            // Set the target amount
            float targetAmount = 1 + i * 0.2f;// (i + 1) * 10;

            Balloon balloon = balloonsParent.GetChild(i).GetComponent<Balloon>();
            balloon.Configure(targetAmount, targetColor);
            balloon.transform.position = targetPosition;

            // Now we can spawn the bonus line
            //SpawnBonusLine(targetPosition, targetColor, targetAmount);
        }
    }
}
