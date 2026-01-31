using Mapbox.Utils;
using UnityEngine;

public class PlayerGPSController : MonoBehaviour
{

    [Header("GPS Config")]
    [SerializeField]
    private float GPSUpdateInterval = 3.0f; // Time in seconds between GPS updates
    [SerializeField]
    private float LocationSmoothingFactor = 0.25f; // Smoothing factor for position updates
    [SerializeField]
    private float GPSStartUpDelay = 20.0f; // Delay before starting GPS service
    public bool IsGPSEnabled { get; private set; }

    public Vector2d LastGPSLocation { get; private set; }
    private float GPSTimer = 0.0f;

    [Header("Debug")]
    [SerializeField]
    private bool OverrideGPSLocation = false;
    [SerializeField]
    private Vector2d OverrideLocation = new Vector2d(39.30828329216619f, -76.62128196818767f);
    [SerializeField]
    private float OverrideMoveSpeedMetersPerSecond = 10f;

    void Start()
    {
        //IsGPSEnabled = false;
        //StartCoroutine(StartGpsService());
    }

    public void StartPlayerGPSService()
    {
        IsGPSEnabled = false;
        StartCoroutine(StartGpsService());
    }

    // Update is called once per frame
    void Update()
    {
        GPSTimer += Time.deltaTime;
        if (GPSTimer >= GPSUpdateInterval)
        {
            if (IsGPSEnabled)
            {
                UpdateGPSLocation(false);
            }
            else
            {
                UpdateOverrideLocation(OverrideLocation);
            }

            GPSTimer = 0f;
        }
    }

    System.Collections.IEnumerator StartGpsService()
    {
        IsGPSEnabled = true;

        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("GPS not enabled");
            IsGPSEnabled = false;
        }

        Input.location.Start(1f, 1f);

        float wait = GPSStartUpDelay;
        while (Input.location.status == LocationServiceStatus.Initializing && wait > 0)
        {
            yield return new WaitForSeconds(1);
            wait--;
        }

        if (Input.location.status != LocationServiceStatus.Running)
        {
            Debug.LogError("GPS failed");
            IsGPSEnabled = false;
        }

        if(IsGPSEnabled)
        {
            UpdateGPSLocation(true);
        }
        else
        {
            UpdateOverrideLocation(OverrideLocation);
        }
        
    }

    void UpdateGPSLocation(bool forceMapUpdate)
    {
        LocationInfo lastLocationUpdate = Input.location.lastData;

        LastGPSLocation = OverrideGPSLocation ? OverrideLocation : new Vector2d(lastLocationUpdate.latitude, lastLocationUpdate.longitude);

        if (forceMapUpdate)
        {
            Main.Instance.UpdateMapLocation(LastGPSLocation);
        }

        Vector3 targetWorldPos = Main.Instance.GPSToWorldPosition(LastGPSLocation);
        transform.position = Vector3.Lerp(transform.position, targetWorldPos, LocationSmoothingFactor);
    }

    void UpdateOverrideLocation(Vector2d GPSLocation)
    {
        OverrideLocation = GPSLocation;

        UpdateGPSLocation(true);
    }
}
