using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BonusUtils
{
    public enum BonusType { Add, Subtract, Multiply, Divide }

    static int[] addValues = {1, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 80, 90, 100 };
    static int[] multiplyValues = {1, 2, 3 };
    static int[] subtractValues = {1, 2, 5, 7, 10, 15, 30};
    static int[] divideValues = { 1, 2, 3 };

    public static Color blue = new Color(0f, .75f, 1f, 1f);
    public static Color red  = new Color(1f, .43f, .43f, 1f);

    public static Bonus GetRandomBonus()
    {
        BonusType randomBonusType = GetRandomBonusType();
        int value = 0;

        switch(randomBonusType)
        {
            case BonusType.Add:
                value = addValues[Random.Range(0, addValues.Length)];
                break;

            case BonusType.Subtract:
                value = subtractValues[Random.Range(0, subtractValues.Length)];
                break;

            case BonusType.Multiply:
                value = multiplyValues[Random.Range(0, multiplyValues.Length)];
                break;

            case BonusType.Divide:
                value = divideValues[Random.Range(0, divideValues.Length)];
                break;
        }

        return new Bonus(randomBonusType, value);
    }

    public static int GetNewAmount(int currentAmount, Bonus bonus)
    {
        switch(bonus.GetBonusType())
        {
            case BonusType.Add:
                return currentAmount + bonus.GetValue();

            case BonusType.Subtract:
                return currentAmount - bonus.GetValue();

            case BonusType.Multiply:
                return (currentAmount * bonus.GetValue());

            case BonusType.Divide:
                return currentAmount / bonus.GetValue();
        }

        return 0;
    }

    public static string GetBonusString(Bonus bonus)
    {
        string bonusString = null;

        switch(bonus.GetBonusType())
        {
            case BonusType.Add:
                bonusString += "+";
                break;

            case BonusType.Subtract:
                bonusString += "-";
                break;

            case BonusType.Multiply:
                bonusString += "x";
                break;

            case BonusType.Divide:
                bonusString += "÷";
                break;
        }

        bonusString += bonus.GetValue();

        return bonusString;
    }

    private static BonusType GetRandomBonusType()
    {
        BonusType[] bonusTypes = (BonusType[])System.Enum.GetValues(typeof(BonusType));
        return bonusTypes[Random.Range(0, bonusTypes.Length)];
    }
}

[System.Serializable]
public struct Bonus
{
    [SerializeField] private BonusUtils.BonusType bonusType;
    [SerializeField] private int value;

    public Bonus(BonusUtils.BonusType bonusType, int value)
    {
        this.bonusType = bonusType;
        this.value = value;
    }

    public BonusUtils.BonusType GetBonusType()
    {
        return bonusType;
    }

    public int GetValue()
    {
        return value;
    }
}
