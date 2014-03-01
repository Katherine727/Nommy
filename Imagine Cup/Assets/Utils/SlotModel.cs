using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utils{

    /// <summary>
    /// This class modeling slots.
    /// </summary>
    [Serializable]
    public class SlotModel {
        public PowerEnum power;
        public Sprite spriteProgressBar;
        public Sprite background;
        public Sprite foreground;
        public Sprite icon;
        public string name;
        public float timeToEndInSec;
        public float usingMultiPlayer;

    }
}
