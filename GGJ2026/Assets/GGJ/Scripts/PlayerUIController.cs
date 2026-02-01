using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerUIController : MonoBehaviour
{

    [SerializeField]
    Image _EquippedMaskImage;

    [SerializeField]
    UIMaskInventoryController _MaskInventory;
    [SerializeField]
    Button _MaskInventoryButton;

    [SerializeField]
    GameObject _LoadingOverlay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _MaskInventory.gameObject.SetActive(false);
        _MaskInventoryButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideLoadOverlay()
    {
        _LoadingOverlay.SetActive(false);
    }

    public void OnMaskInventoryUpdated(List<MaskSO> inventory, MaskSO active)
    {
        if(inventory.Count > 0)
        {
            _MaskInventoryButton.gameObject.SetActive(true);
            _MaskInventory.UpdateInventory(inventory, active);
        }
        else
        {
            _MaskInventoryButton.gameObject.SetActive(false);
        }
    }

    public void OnMaskInventoryOpen()
    {
        _MaskInventory.Open();
        _MaskInventoryButton.gameObject.SetActive(false);   
    }

    public void OnMaskInventoryClose()
    {
        _MaskInventory.Close();
        _MaskInventoryButton.gameObject.SetActive(true);
    }

    public void UpdateEquippedMask(MaskSO mask)
    {
        if(mask != null)
        {
            _EquippedMaskImage.sprite = mask.Icon;
            _EquippedMaskImage.gameObject.SetActive(true);
        }
        else
        {
            _EquippedMaskImage.gameObject.SetActive(false);
        }
        
    }
}
