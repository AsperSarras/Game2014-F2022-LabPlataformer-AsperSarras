using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject onScreenControls;

    // Start is called before the first frame update
    void Awake()
    {
        onScreenControls = GameObject.Find("OnScreenControls");
        onScreenControls.SetActive(Application.isMobilePlatform);

        //onScreenControls.SetActive((Application.platform != RuntimePlatform.WindowsPlayer &&
        //                            Application.platform != RuntimePlatform.WindowsEditor));

    }
}