﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour {

    public float time;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 1);
	}
	
}