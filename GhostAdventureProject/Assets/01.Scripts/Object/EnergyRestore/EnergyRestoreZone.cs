using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRestoreZone : MonoBehaviour
{
    [SerializeField] private int bonusRestoreAmount;
    [SerializeField] private float reduceInterval;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoulEnergySystem.Instance.SetRestoreBoost(reduceInterval, SoulEnergySystem.Instance.baseRestoreAmount + bonusRestoreAmount);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoulEnergySystem.Instance.ResetRestoreBoost();
        }
    }

}
