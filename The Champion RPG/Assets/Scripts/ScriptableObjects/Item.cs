﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject {

    public string itemName;
    public Sprite itemSprite;
    public string itemDescription;
    public int Strength;
    public int Agility;
}
