using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utils{

    [Serializable]
    public class SlotModel {
        public PowerEnum power;
        public Sprite spriteRing;
        public Sprite background;
        public Sprite spriteFaceActivated;
        public Sprite spriteFaceDeactivated;
        public string name;
        public float timeToEndInSec;
        public float usingMultiPlayer;

    }
}
