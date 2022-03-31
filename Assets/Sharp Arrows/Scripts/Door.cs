using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    [Header(" Settings ")]
    [SerializeField] private Bonus bonus;
    [SerializeField] private bool randomBonus;

    [Header(" Components ")]
    [SerializeField] private TextMeshPro doorText;
    [SerializeField] private SpriteRenderer doorRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if (randomBonus)
            bonus = BonusUtils.GetRandomBonus();

        ConfigureBonusText();
        ConfigureDoorColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConfigureBonusText()
    {
        doorText.text = BonusUtils.GetBonusString(bonus);
    }

    private void ConfigureDoorColor()
    {
        BonusUtils.BonusType bonusType = bonus.GetBonusType();

        if (bonusType == BonusUtils.BonusType.Add || bonusType == BonusUtils.BonusType.Multiply)
            doorRenderer.color = BonusUtils.blue;
        else
            doorRenderer.color = BonusUtils.red;
    }
        

    public int GetTargetArrowCount(int currentArrowCount)
    {
        return BonusUtils.GetNewAmount(currentArrowCount, bonus);
    }
}
