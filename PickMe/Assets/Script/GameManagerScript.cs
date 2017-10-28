using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class GameManagerScript : MonoBehaviour
    {
        private static GameManagerScript self = null;
        public static GameManagerScript Instance
        {
            get { return self; }
        }

        public Object_Manager_Script objManagerScript;
        public GameObject floor;

        void Awake()
        {
            if (null == self)
            {
                self = this;
            }
            else
            {
                Debug.LogWarning("Must be single monobehaviour.");
                return;
            }
        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
