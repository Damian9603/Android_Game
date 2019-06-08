using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : HealthUI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        //healthSlider = uiPrefab;
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    protected override void LateUpdate()
    {
        
    }

    protected override void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            float healtPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healtPercent;
        }
    }
}
