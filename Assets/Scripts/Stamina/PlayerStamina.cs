using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public int MaxStamina = 10;
    public int CurrentStamina;

    private StaminaUI StaminaUI;

    void Awake()
    {
        CurrentStamina = MaxStamina;
        StaminaUI = GetComponent<StaminaUI>();
    }

    public void IncreaseStamina(int stamina)
    {
        CurrentStamina += stamina;
        StaminaUI.UpdateStaminaUI();
    }

    public void DecreaseStamina(int stamina)
    {
        CurrentStamina -= stamina;
        if (CurrentStamina > 0)
        {
            StaminaUI.UpdateStaminaUI();
        }
        else
        {
            StaminaUI.StaminaText.text = $"0 / {MaxStamina}";
            gameObject.GetComponent<PlayerHealth>().Die();
        }
    }
}
