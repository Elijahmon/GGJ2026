using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    CollectableVisualsController Visuals;
    //[SerializeField]
    //Transform InteractPrompt;
    public CollectableSO Data;

    private bool IsCollected = false;

    void Awake()
    {
        if(Visuals == null)
        {
            Visuals = GetComponentInChildren<CollectableVisualsController>();
        }

        ToggleCollected(false);
        //if (InteractPrompt == null)
        //{
        //    InteractPrompt = transform.Find("InteractPrompt");
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(CollectableSO data)
    {
        Data = data;
        OnPlayerPositionUpdated(Main.Instance.GetPlayerPosition());
    }

    public void OnPlayerPositionUpdated(Vector3 worldPos)
    {
        if(Data == null)
        {
            Debug.LogError("Collectable: Data not initialized.");
            return;
        }

        float distance = Vector3.Distance(worldPos, this.transform.position);

        if (!IsCollected)
        {
            ToggleVisibilty(distance <= Data.VisibilityRadius);

            //if (InteractPrompt != null)
            //{
            ToggleInteract(distance <= Data.CollectRadius);
            //}
            //Debug.Log($"Collectable: Distance to player is {distance} meters.");
        }
    }

    void ToggleVisibilty(bool toggle)
    {
        if(toggle)
        {
            Visuals.OnActivateVisibilityVisuals();
        }
        else
        {
            Visuals.OnDeactivateVisibilityVisuals();
        }    
    }

    void ToggleInteract(bool toggle)
    {

        if (toggle)
        {
            Visuals.OnActivateInteractVisuals();
        }
        else
        {
            Visuals.OnDeactivateInteractVisuals();
        }
    }

    void ToggleCollected(bool toggle)
    {
        if (toggle)
        {
            Visuals.OnActivateCollectedVisuals();
        }
        else
        {
            Visuals.OnDeactivateCollectedVisuals();
        }
    }

    public void OnCollectButtonPressed()
    {
        Main.Instance.CollectMask(Data.MaskReward);

        IsCollected = true;
        ToggleInteract(false);
        ToggleCollected(true);

        //Destroy(InteractPrompt.gameObject);
    }
}
