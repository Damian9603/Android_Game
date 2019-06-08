using UnityEngine;
using UnityEngine.UI;

public class PlayerManaUI : MonoBehaviour
{
    public GameObject uiPrefab;
    protected Transform ui;
    protected Image manaSlider;
    // Start is called before the first frame update
    protected void Start()
    {
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                manaSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        //healthSlider = uiPrefab;
        GetComponent<CharacterStats>().OnManaChanged += OnManaChanged;
    }

    protected void LateUpdate()
    {

    }

    protected void OnManaChanged(int maxMana, int currentMana)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            float manaPercent = (float)currentMana / maxMana;
            manaSlider.fillAmount = manaPercent;
        }
    }
}
