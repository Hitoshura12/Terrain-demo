using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider hpSlider;
    public Gradient hpGradient;
     public Image hpImage;
    public Player_SO hpStats;
    public float hpBarSlideSpeed =1f;

   

    private void Awake()
    {
        PlayerController.OnPlayerHit += PlayerController_OnPlayerHit;
        SetMaxHealth();
        Debug.Log("Hello from Awake");
    }

    private void PlayerController_OnPlayerHit(int obj)
    {
        
        
        SetHpBarIndicator();
        Debug.Log("Hello from Set current HP" + obj);
    }

    private void SetHpBarIndicator()
    {
  
        float previousHpValue = hpSlider.value;
        Debug.Log(hpSlider.value);
        float elapsedTime = 0f;
        while (elapsedTime< hpBarSlideSpeed)
        {
            elapsedTime += Time.deltaTime;
            hpSlider.value = Mathf.Lerp(previousHpValue, hpStats.Health, elapsedTime / hpBarSlideSpeed);
            hpImage.color = hpGradient.Evaluate(hpSlider.normalizedValue);
            Debug.Log(hpSlider.value);
        
        }
        hpSlider.value = hpStats.Health;


    }

    public void SetMaxHealth()
    {
        hpSlider.maxValue = hpStats.BaseHealth;
        hpSlider.value = hpStats.Health;
        hpGradient.Evaluate(hpSlider.value);
    }
    private void OnDisable()
    {
        PlayerController.OnPlayerHit -= PlayerController_OnPlayerHit;
    }
}
