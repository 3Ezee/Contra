using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadSprite : MonoBehaviour {

    private static OnLoadSprite _instance;
    public static OnLoadSprite Instance
    {
        get
        {
            return _instance;
        }
    }
   
    void Awake()
    {
        _instance = this;
	}


}
