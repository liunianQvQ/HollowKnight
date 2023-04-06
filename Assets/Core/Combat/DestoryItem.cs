using System.Collections;
using UnityEngine;

namespace Core.Combat
{
    public class DestoryItem : MonoBehaviour
    {
        public int CurrentHealth { get; set; }
        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
            CurrentHealth = GetComponent<Combat.Destructable>().health;
            if(CurrentHealth <= 0)
            {
                
                Destroy(this.gameObject);
            }
        }
    }
}