using UnityEngine;

public class Collectable : MonoBehaviour
{
    CollectableSO _Data;
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(CollectableSO data)
    {
        _Data = data;
        OnPlayerPositionUpdated(Main.Instance.GetPlayerPosition());
    }

    public void OnPlayerPositionUpdated(Vector3 worldPos)
    {
        if(_Data == null)
        {
            Debug.LogError("Collectable: Data not initialized.");
            return;
        }

        float distance = Vector3.Distance(worldPos, this.transform.position);

        //Debug.Log($"Collectable: Distance to player is {distance} meters.");
    }
}
