using Mapbox.Utils;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private PlayerGPSController _GPS;
    [SerializeField]
    private Camera _Camera;
    [SerializeField]
    private Plane _GroundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0));

    private Vector3 TargetMoveLocation;
    [SerializeField]
    private float MoveSpeed = 5.0f;
    [SerializeField]
    private float TurnSpeed = 1.0f;
    [SerializeField]
    private float DestinationThreshold = 0.5f;

    public enum PlayerMovementMode
    {
        TapToMove,
        GPS,
    }
    [SerializeField]
    PlayerMovementMode _MovementMode = PlayerMovementMode.GPS;
    public PlayerMovementMode MovementMode => _MovementMode;

    private List<MaskSO> _CollectedMasks = new List<MaskSO>();
    private MaskSO _ActiveMask = null;

    [SerializeField]
    PlayerUIController _UIController;

    [SerializeField]
    Animator _PlayerSpriteAnimator;

    void Awake()
    {
        //InitGPSController();
    }

    public void Init()
    {
        TargetMoveLocation = transform.position;

        if(_Camera == null)
        {
            _Camera = GetComponentInChildren<Camera>();
        }

        if(_UIController == null)
        {
            _UIController = GetComponentInChildren<PlayerUIController>();
        }

        _UIController.HideLoadOverlay();

        InitGPSController();
    }

    void InitGPSController()
    {
        if (_GPS == null)
        {
            _GPS = GetComponent<PlayerGPSController>();
            if (_GPS == null)
            {
                _GPS = this.AddComponent<PlayerGPSController>();
            }
        }

        if (_GPS == null)
        {
            Debug.LogError("PlayerController: Failed to initialize PlayerGPSController.");
        }

        switch(_MovementMode)
        {
            case PlayerMovementMode.TapToMove:
                // Do nothing, GPS is not needed
                break;
            case PlayerMovementMode.GPS:
                _GPS.StartPlayerGPSService();
                break;
        }
    }

    public Vector2d GetLastGPSLocation()
    {
        if (_GPS == null) return Vector2d.zero;
        return _GPS.LastGPSLocation;
    }


    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            SetMoveTargetLocation(Input.mousePosition);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            SetMoveTargetLocation(Input.GetTouch(0).position);
        }
    }


    void SetMoveTargetLocation(Vector2 screenPosition)
    {
        Ray ray = _Camera.ScreenPointToRay(screenPosition);

        if (_GroundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            hitPoint.y = transform.position.y;

            TargetMoveLocation = hitPoint;
        }
    }

    void Move()
    {
        if (HasReachedTargetMoveLocation())
        {
            _PlayerSpriteAnimator.SetBool("IsMoving", false);
            return;
        }
        _PlayerSpriteAnimator.SetBool("IsMoving", true);

        Vector3 delta = TargetMoveLocation - transform.position;

        delta = delta.normalized * MoveSpeed * Time.deltaTime;
        Vector3 target = transform.position + delta;

        if (delta != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(delta), TurnSpeed * Time.deltaTime);
        }

        _GPS.UpdatePlayerPosition(target);

        TargetMoveLocation -= delta;
    }

    bool HasReachedTargetMoveLocation()
    {
        return Vector3.Distance(transform.position, TargetMoveLocation) < DestinationThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Main.Instance.IsMapReady)
            return;

        if(MovementMode == PlayerMovementMode.TapToMove)
        {
            HandleInput();
            Move();
        }

        UpdateActiveMask();
    }

    public void CollectMask(MaskSO mask)
    {
        if(mask == null)
        {
            Debug.LogError("PlayerController: Attempted to collect null mask.");
            return;
        }

        _CollectedMasks.Add(mask);
        Debug.Log($"PlayerController: Collected mask {mask.DisplayName}.");

        OnMaskCollected();

    }

    void OnMaskCollected()
    {
        _UIController.OnMaskInventoryUpdated(_CollectedMasks, _ActiveMask);
    }

    public void EquipMask(MaskSO mask)
    {
        if(!_CollectedMasks.Contains(mask))
        {
            Debug.LogError("PlayerController: Attempted to equip mask not collected.");
            return;
        }

        if(_ActiveMask != null)
        {
            OnMaskRemoved(_ActiveMask);
        }

        _ActiveMask = mask;
        OnMaskEquipped(_ActiveMask);

        Debug.Log($"PlayerController: Equipped mask {mask.DisplayName}.");

        _UIController.OnMaskInventoryUpdated(_CollectedMasks, _ActiveMask);
        _UIController.OnMaskInventoryClose();
    }

    void OnMaskEquipped(MaskSO mask)
    {
        _UIController.UpdateEquippedMask(mask);

        switch(mask.Effect)
        {
            case MaskSO.MaskEffect.NONE:
                break;
            case MaskSO.MaskEffect.Qwizard:
                break;
            case MaskSO.MaskEffect.PoesMask:
                break;
        }
    }

    void OnMaskRemoved(MaskSO mask)
    {
        switch (mask.Effect)
        {
            case MaskSO.MaskEffect.NONE:
                break;
            case MaskSO.MaskEffect.Qwizard:
                break;
            case MaskSO.MaskEffect.PoesMask:
                break;
        }
    }

    void UpdateActiveMask()
    {
        if(_ActiveMask == null)
        {
            return;
        }
        switch(_ActiveMask.Effect)
        {
            case MaskSO.MaskEffect.NONE:
                break;
            case MaskSO.MaskEffect.Qwizard:
                break;
            case MaskSO.MaskEffect.PoesMask:
                break;
        }
    }
}
