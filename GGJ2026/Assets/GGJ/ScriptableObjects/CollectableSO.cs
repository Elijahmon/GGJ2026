using Mapbox.Utils;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableSO", menuName = "Scriptable Objects/CollectableSO")]
public class CollectableSO : ScriptableObject
{
    public string DisplayName;
    public GameObject Prefab;
    public Vector2d GPSLocation;
    public float VisibilityRadius = 50f;
    public float CollectRadius = 10f;
}
