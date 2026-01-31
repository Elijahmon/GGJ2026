using Mapbox.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private PlayerGPSController _GPS;


    void Awake()
    {
        //InitGPSController();
    }

    public void InitGPSController()
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
        else
        {
            _GPS.StartPlayerGPSService();
        }
    }

    public Vector2d GetLastGPSLocation()
    {
        if (_GPS == null) return Vector2d.zero;
        return _GPS.LastGPSLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
