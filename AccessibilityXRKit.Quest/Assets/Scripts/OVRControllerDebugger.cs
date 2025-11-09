using UnityEngine;

public class OVRControllerDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"xxxxx OVRPlugin.initialized={OVRPlugin.initialized}");
        Debug.Log($"xxxxx OVRManager.instance={(OVRManager.instance != null ? "present" : "null")}");
        Debug.Log($"xxxxx OVRManager.initialized={OVRManager.OVRManagerinitialized}");
        Debug.Log($"xxxxx XR API: {(OVRManager.instance != null ? OVRManager.instance.xrApi.ToString() : "unknown")}");
    }

    void Update()
    {
        // prints connected controller flags reported by OVRInput
        try
        {
            var connected = OVRInput.GetConnectedControllers();
            Debug.Log($"xxxxx Frame {Time.frameCount}: OVRInput.GetConnectedControllers() = {connected}");
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("xxxxx OVRInput.GetConnectedControllers() threw: " + e);
        }
    }
}