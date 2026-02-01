using UnityEngine;
using System.Collections.Generic;

public class UIMaskInventoryController : MonoBehaviour
{
    [SerializeField]
    UIMaskInventoryItem itemPrefab;

    [SerializeField]
    RectTransform contentTransform;
    Dictionary<MaskSO, UIMaskInventoryItem> inventoryItems = new Dictionary<MaskSO, UIMaskInventoryItem>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void UpdateInventory(List<MaskSO> inventory, MaskSO active)
    {
        foreach (var item in inventory)
        {
            if(!inventoryItems.ContainsKey(item))
            {
                var uiItem = Instantiate(itemPrefab, contentTransform);
                uiItem.Init(item);
                inventoryItems[item] = uiItem;
            }
            else
            {
                if (item == active)
                {
                    inventoryItems[item].ToggleVisibility(false);
                }
                else
                {
                    inventoryItems[item].ToggleVisibility(true);
                }
            }
        }
        

    }
}
