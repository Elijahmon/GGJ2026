using Mapbox.Map;
using Mapbox.Unity.Map;
using Mapbox.Unity.Map.TileProviders;
using Mapbox.Utils;
using UnityEngine;

public class Main : MonoBehaviour
{

    public static Main Instance { get; private set; }

    private bool IsMapInitialized = false;
    public bool IsMapReady { get; private set; }

    [SerializeField]
    CollectableManager _CollectableManager;
    [SerializeField]
    private AbstractMap _Map;
    [SerializeField]
    private int MapZoom = 16;
    [SerializeField]
    private PlayerController _Player;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        InitializReferences();
        DontDestroyOnLoad(_Map);

        IsMapReady = false;
    }

    private void Start()
    {
        StartCoroutine(InitializeMap(_Player.GetLastGPSLocation()));
    }

    private void InitializReferences()
    {
        Instance = this;

        if (_Map == null)
        {
            _Map = FindFirstObjectByType<AbstractMap>();
        }
        if (_Map == null)
        {
            Debug.LogError("Map reference not found");
        }

        SetMapTargetTransform(_Player.transform);

        if (_CollectableManager == null)
        {
            _CollectableManager = GetComponentInChildren<CollectableManager>();

        }
        if (_CollectableManager == null)
        {
            Debug.LogError("CollectableManager reference not found");
        }
    }

    private void SetMapTargetTransform(Transform target)
    {
        //TODO: Set player to target here?
    }

    System.Collections.IEnumerator InitializeMap(Vector2d startLocation)
    {
        if (IsMapInitialized) yield break;

        yield return null;

        _Map.Initialize(startLocation, MapZoom);
        IsMapInitialized = true;

        // Wait until map has a non-zero scale
        while (_Map == null || _Map.WorldRelativeScale <= 0)
        {
            yield return null;
        }


        IsMapReady = true;
        Debug.Log("Map is ready");

        OnMapReady();
    }

    void OnMapReady()
    {
        _Player.Init();
        _CollectableManager.SpawnDefaultCollectibles();
    }

    public void UpdateMapLocation(Vector2d GPSLocation)
    {
        if (IsMapInitialized && IsMapReady)
        {
            _Map.UpdateMap(GPSLocation);
            _CollectableManager.OnGPSUpdated(GPSLocation);
        }
    }

    public Vector3 GPSToWorldPosition(Vector2d GPSLocation)
    {
        if (!IsMapReady) return Vector3.zero;

        return _Map.GeoToWorldPosition(GPSLocation, true);
    }

    public Vector2d WorldToGPSLocation(Vector3 worldPosition)
    {
        if (!IsMapReady) return Vector2d.zero;

        return _Map.WorldToGeoPosition(worldPosition);
    }

    public Vector3 GetPlayerPosition()
    {
        return _Player.transform.position;
    }

    public void CollectMask(MaskSO mask)
    {
        _Player.CollectMask(mask);
    }

    public void ActivateMask(MaskSO mask)
    {
        _Player.EquipMask(mask);
    }
}
