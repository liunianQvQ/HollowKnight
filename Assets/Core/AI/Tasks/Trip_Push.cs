using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;

namespace Core.AI
{
    public class Trip_Push : EnemyAction
    {
        public override void OnStart()
        {
            animator.SetBool("isPush", true);
            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

    }
}