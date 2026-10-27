using UnityEngine;
using UnityEngine.XR;
public class HMDInfoManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!XRSettings.isDeviceActive)
            Debug.Log("No headswt plugged");
        else if (XRSettings.isDeviceActive && (XRSettings.loadedDeviceName == "Mock HMD" || XRSettings.loadedDeviceName == "MockHMDDisplay"))
            Debug.Log("Mock HMD plugged");
        else
            Debug.Log("We have a headset: " + XRSettings.loadedDeviceName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
