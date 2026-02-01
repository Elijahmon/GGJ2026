using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMaskInventoryItem : MonoBehaviour
{
    MaskSO Data;

    [SerializeField]
    Image maskImage;
    [SerializeField]
    TextMeshProUGUI maskLabel;
    public void Init(MaskSO mask)
    {
        Data = mask;
        maskLabel.text = Data.DisplayName;
        maskImage.sprite = Data.Icon;
        ToggleVisibility(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleVisibility(bool toggle)
    {
        gameObject.SetActive(toggle);
    }

    public void OnPressed()
    {
        Main.Instance.ActivateMask(Data);
    }
}
