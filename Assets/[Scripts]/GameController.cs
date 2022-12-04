using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject onScreenControls;
    public GameObject miniMap;

    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        onScreenControls = GameObject.Find("OnScreenControls");

        onScreenControls.SetActive((Application.platform != RuntimePlatform.WindowsPlayer &&
                                    Application.platform != RuntimePlatform.WindowsEditor));

        soundManager = FindObjectOfType<SoundManager>();


        soundManager.PlayMusic();

        miniMap = GameObject.Find("MiniMap");

        if (miniMap)
        {
            miniMap.SetActive(false);
        }
    }

    void Update()
    {
        if ((miniMap) && (Input.GetKeyDown(KeyCode.M)))
        {
            miniMap.SetActive(!miniMap.activeInHierarchy);
        }
    }
}