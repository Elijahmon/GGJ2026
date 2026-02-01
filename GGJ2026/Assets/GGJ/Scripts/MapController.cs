using Mapbox.Unity.Map;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MapController : MonoBehaviour
{

    public static MapController Instance;

    public AbstractMap Map => _Map;
    [SerializeField]
    private AbstractMap _Map;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DontDestroyOnLoad(this);
        InitializReferences();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
