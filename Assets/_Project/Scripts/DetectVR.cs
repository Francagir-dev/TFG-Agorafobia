using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;

public class DetectVR : MonoBehaviour
{
    public bool startVR; //Bool for test in editor
    public GameObject xrOrigin; //XR Character
    public GameObject desktopCharacter; //Character when non-vr


    // Start is called before the first frame update
    void Start()
    {
        if (startVR)
        {
            var xrSettings = XRGeneralSettings.Instance; //Get Instance of XRGeneralSettings
            if (xrSettings == null)//if null
            {
                Debug.Log("XR Settings null"); //Display error
                return; //Stop
            }

            var xrManager = xrSettings.Manager; //Assign XRManager
            if (xrManager == null)
            {
                Debug.Log("Manager is null"); //Display error
                return; //Stop
            }

            var xrLoader = xrManager.activeLoader; //XR Loader (Load VR)
            if (xrLoader == null) //If null
            {
                Debug.Log("XRLoader is null");
                ActivateDesktopCharacter(); //Activate Desktop character (NO VR)
            }

            Debug.Log("XRLoader not null");
            desktopCharacter.SetActive(false);//Disable NO VR
            xrOrigin.SetActive(true); //Enable VR
           
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
