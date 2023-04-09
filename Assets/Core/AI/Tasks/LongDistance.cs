using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Core.AI
{
    public class LongDistance : EnemyConditional
    {
        public float partrolDis;
        private float dis;
        public override TaskStatus OnUpdate()
        {
            dis = Mathf.Abs(Mathf.Abs(transform.position.y) - Mathf.Abs(player.transform.position.y));
            return dis <= partrolDis ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}