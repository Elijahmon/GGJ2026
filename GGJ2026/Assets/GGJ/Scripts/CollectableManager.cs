using Mapbox.Map;
using Mapbox.Utils;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    [SerializeField]
    private List<CollectableSO> defaultCollectables = new List<CollectableSO>();

    private List<Collectable> spawnedCollectables = new List<Collectable>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }
    public void SpawnDefaultCollectibles()
    {
        foreach (var data in defaultCollectables)
        {
            Vector3 worldPos = Main.Instance.GPSToWorldPosition(data.GPSLocation);

            Collectable collectable = Instantiate(data.Prefab, worldPos, Quaternion.identity).GetComponent<Collectable>();
            collectable.Init(data);
            spawnedCollectables.Add(collectable);

            Debug.Log($"Spawned collectable {data.DisplayName} at {worldPos} ({data.GPSLocation}");
        }
    }

    public void OnGPSUpdated(Vector2d GPSLocation)
    {
        foreach (var collectable in spawnedCollectables)
        {
            Vector3 worldPos = Main.Instance.GPSToWorldPosition(GPSLocation);
            collectable.OnPlayerPositionUpdated(worldPos);
        }
    }
}
