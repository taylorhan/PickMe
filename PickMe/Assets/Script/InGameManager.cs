using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doll
{
    public class InGameManager : MonoBehaviour
    {
        private static InGameManager self = null;
        public static InGameManager Instance
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

        public Vector3 Set_Random_Destiny()
        {
            float floorWidth = floor.GetComponent<BoxCollider2D>().size.x;
            float floorHeight = floor.GetComponent<BoxCollider2D>().size.y;
            float destPosX = Random.Range(-floorWidth / 2, floorWidth / 2);
            float destPosY = Random.Range(-floorHeight / 2, floorHeight / 2);

            return new Vector3(destPosX, destPosY, 0);
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