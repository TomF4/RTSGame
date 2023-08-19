using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text woodText;
    public TMP_Text stoneText;
    public TMP_Text ironText;
    public TMP_Text moneyText;
    public ResourceManager resourceManager;

    void Update()
    {
        woodText.text =  resourceManager.GetResource(ResourceType.Wood).ToString() ?? "N/A";
        stoneText.text = resourceManager.GetResource(ResourceType.Stone).ToString() ?? "N/A";
    }
}