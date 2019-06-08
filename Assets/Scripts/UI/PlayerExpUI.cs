using UnityEngine.UI;
using UnityEngine;

public class PlayerExpUI : MonoBehaviour
{
    public GameObject uiPrefab;
    protected Transform ui;
    protected Image expSlider;
    // Start is called before the first frame update
    protected void Start()
    {
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                expSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        //healthSlider = uiPrefab;
        GetComponent<CharacterStats>().OnExpChanged += OnExpChanged;
    }

    protected void LateUpdate()
    {

    }

    protected void OnExpChanged(int nextLvlExp, int currentExp)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            float lvlPercent = ((float)currentExp / nextLvlExp);
            expSlider.fillAmount = lvlPercent;
        }
    }
}
