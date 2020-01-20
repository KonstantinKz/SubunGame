using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRes : MonoBehaviour
{
    public static int ScreenWidth = Screen.width;
    public static int ScreenHeight = Screen.height;
    // Start is called before the first frame update
    void Start()
    {
        if (ScreenWidth == Screen.width || ScreenHeight == Screen.height)
        {
            Screen.orientation = ScreenOrientation.Portrait;
            //ScreenHeight = Screen.width - (640 * 2);
            //ScreenWidth = Screen.height - (360 * 2);
            Screen.SetResolution(1080/2, 1920/2, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
