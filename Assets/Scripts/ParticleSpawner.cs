using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour {

    public GameObject prefab;
	void Awake () {
        EventsManager.SubscribeToEvent(EventType.GP_Destroy, InstanceParticle);
    }
	
    void InstanceParticle(params object[] parameters)
    {
        Instantiate(prefab, new Vector2((float)parameters[0], (float)parameters[1]), Quaternion.identity);

    }

    public void UnsubscribeEvent()
    {
        EventsManager.UnsubscribeToEvent(EventType.GP_Destroy, InstanceParticle);
    }
}
