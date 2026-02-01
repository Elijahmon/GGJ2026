using UnityEngine;
using System.Collections.Generic;

public class CollectableVisualsController : MonoBehaviour
{
    [SerializeField]
    Transform VisibilityVisualsRoot;
    [SerializeField]
    Transform InteractVisualsRoot;
    [SerializeField]
    Transform CollectedVisualsRoot;

    [SerializeField]
    List<Transform> CameraFacingObjects;

    Camera _LookAtCamera;

    private void Awake()
    {
        _LookAtCamera = Camera.main;
    }

    private void Update()
    {
        UpdateCameraFacingObjects();
    }

    void UpdateCameraFacingObjects()
    {
        foreach(var obj in CameraFacingObjects)
        {
            obj.LookAt(_LookAtCamera.transform);
        }  
    }

    public void OnActivateVisibilityVisuals()
    {
        VisibilityVisualsRoot.gameObject.SetActive(true);
    }

    public void OnDeactivateVisibilityVisuals()
    {
        VisibilityVisualsRoot.gameObject.SetActive(false);
    }

    public void OnActivateInteractVisuals()
    {
        InteractVisualsRoot.gameObject.SetActive(true);
    }

    public void OnDeactivateInteractVisuals()
    {
        InteractVisualsRoot.gameObject.SetActive(false);
    }
    public void OnActivateCollectedVisuals()
    {
        CollectedVisualsRoot.gameObject.SetActive(true);
    }

    public void OnDeactivateCollectedVisuals()
    {
        CollectedVisualsRoot.gameObject.SetActive(false);
    }
}
