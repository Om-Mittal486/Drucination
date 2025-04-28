using UnityEngine;
using TMPro;

public class GemManager : MonoBehaviour
{
    public static GemManager instance;
    public int gemsCollected = 0;

    public TextMeshProUGUI gemCountTMP;  // TextMeshPro component

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectGem()
    {
        gemsCollected++;
        UpdateGemUI();
    }

    void UpdateGemUI()
    {
        gemCountTMP.text = gemsCollected.ToString();
    }

    public void UseGem()
    {
    if (gemsCollected > 0)
        {
        gemsCollected--;
        UpdateGemUI();
        }
    }
}
