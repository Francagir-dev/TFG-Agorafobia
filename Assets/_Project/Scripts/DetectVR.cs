using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class DetectVR : MonoBehaviour
{
    public bool startVR;
    public GameObject xrOrigin;
    public GameObject desktopCharacter;


    // Start is called before the first frame update
    void Start()
    {
        if (startVR)
        {
            var xrSettings = XRGeneralSettings.Instance;
            if (xrSettings == null)
            {
                Debug.Log("XR Settings null");
                return;
            }

            var xrManager = xrSettings.Manager;
            if (xrManager == null)
            {
                Debug.Log("Manager is null");
                return;
            }

            var xrLoader = xrManager.activeLoader;
            if (xrLoader == null)
            {
                Debug.Log("XRLoader is null");
                ActivateDesktopCharacter();
            }

            Debug.Log("XRLoader not null");
            desktopCharacter.SetActive(false);
            xrOrigin.SetActive(true);
           
            //Activate Headset
            XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
        else
        {
            ActivateDesktopCharacter();
        }
    }

    private void ActivateDesktopCharacter()
    {
        xrOrigin.SetActive(false);
        desktopCharacter.SetActive(true);
        
        //Deactivate Headset
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    }
}