using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Core.AI
{
    public class IsDistanceUnder : EnemyConditional
    {
        public float partrolDis;
        private float dis;
        public override TaskStatus OnUpdate()
        {
            dis = (transform.position - player.transform.position).magnitude;
            return dis <= partrolDis ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}