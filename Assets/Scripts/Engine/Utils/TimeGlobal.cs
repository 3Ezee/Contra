using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGlobal : MonoBehaviour {
    public float timeWin;
    private float _time;
	// Use this for initialization
	void Start () {
        _time = timeWin;
	}
	// Update is called once per frame
	void Update () {
        timeWin -= Time.deltaTime;

        if(timeWin<=0)
            EventsManager.TriggerEvent(EventType.GP_Win, new object[] {});
    }
}
