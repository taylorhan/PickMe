using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class Doll : MonoBehaviour
    {

        public int index = 0;
        public int Type = 2;

        public enum eDollState
        {
            Idle = 0,
            Move, //이동 중
            Battle, //전투 중
            Invincible, //무적(집게 충돌 중)
            Pickup, //집게 올라가는 중
        }
        eDollState dollState;

        public void Set_Information(int index, int Type)
        {
            this.index = index;
            this.Type = Type;
        }

        // Use this for initialization
        void Start()
        {

        }

        public void SetDollState(eDollState state)
        {
            dollState = state;
        }

        void CheckState()
        {
            switch (dollState)
            {
                case eDollState.Idle:
                    {

                    }
                    break;
                case eDollState.Move:
                    {

                    }
                    break;
                case eDollState.Battle:
                    {

                    }
                    break;
                case eDollState.Invincible:
                    {

                    }
                    break;
                case eDollState.Pickup:
                    {

                    }
                    break;
                default:
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            CheckState();
        }
    }
}

