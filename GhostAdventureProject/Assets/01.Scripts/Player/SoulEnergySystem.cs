using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulEnergySystem : Singleton<SoulEnergySystem>
{
    public int maxEnergy = 10;
    public int currentEnergy;

    private void Start()
    {
        currentEnergy = maxEnergy;
    }

    public bool HasEnoughEnergy(int amount) => currentEnergy >= amount;

    public void Consume(int amount) // 에너지 소모
    {
        currentEnergy = Mathf.Max(currentEnergy - amount, 0);
        Debug.Log($"영혼 에너지 {amount} 소모, 현재: {currentEnergy}");
        SoulGauge.Instance.SetSoulGauge(currentEnergy);
    }

    public void Restore(int amount) // 에너지 회복
    {
        currentEnergy = Mathf.Min(currentEnergy + amount, maxEnergy);
        Debug.Log($"영혼 에너지 {amount} 회복, 현재: {currentEnergy}");
        SoulGauge.Instance.SetSoulGauge(currentEnergy);
    }
}
