﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackBehavior {

    void Attack(int layer, Transform attackSpawner);
}
