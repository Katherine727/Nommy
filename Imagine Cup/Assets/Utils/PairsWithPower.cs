using System;
using UnityEngine;
namespace Assets.Utils.PairsWithPower {
    [Serializable]
    public class PowerSpritePair : IPair<PowerEnum, Sprite> {

        [SerializeField]
        private PowerEnum _power;
        [SerializeField]
        private Sprite _sprite;


        public PowerEnum Item1 {
            get { return _power; }
            set { _power = value; }
        }

        public Sprite Item2 {
            get { return _sprite; }
            set { _sprite = value; }
        }
    }

    [Serializable]
    public class PowerSlotModelPair : IPair<PowerEnum, SlotModel> {
        private PowerEnum _power;
        private SlotModel _model;
        public PowerEnum Item1 {
            get { return _power; }
            set { _power = value; }
        }

        public SlotModel Item2 {
            get { return _model; }
            set { _model = value; }
        }
    }
}
