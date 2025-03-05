using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaminaUI : MonoBehaviour
{
    public TextMeshProUGUI StaminaText;
    private PlayerStamina PlayerStamina;

    void Start()
    {
        PlayerStamina = GetComponent<PlayerStamina>();
        UpdateStaminaUI();
    }

    public void UpdateStaminaUI()
    {
        StaminaText.text = $"{PlayerStamina.CurrentStamina} / {PlayerStamina.MaxStamina}";
    }
}
