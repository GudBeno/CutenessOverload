using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaBar : MonoBehaviour
{
    public Slider sprintBar;
    public Gradient staminaGradient;
    public Image staminaBarFil;

    public void SetMaxSprint(int stamina)
    {
        sprintBar.maxValue = stamina;
        sprintBar.value = stamina;

        staminaBarFil.color = staminaGradient.Evaluate(1f);
    }

    public void SetStamina(int stamina)
    {
        sprintBar.value = stamina;
        staminaBarFil.color = staminaGradient.Evaluate(sprintBar.normalizedValue);
    }
}
