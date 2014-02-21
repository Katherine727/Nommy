using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Scripts.Slots;

namespace Assets.Scripts.Slots {

    [Serializable]
    public class PowerSpritePair {
        public PowerEnum power;
        public Sprite sprite;
    }
}
public class PowerSlotSpriteProvider : MonoBehaviour {
    public List<PowerSpritePair> powerSpritePairs;

    void Start() {
        powerSpritePairs = new List<PowerSpritePair>();
    }
}

