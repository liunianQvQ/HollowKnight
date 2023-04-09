using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;

namespace Core.AI
{
    public class Trip_Back : EnemyAction
    {
        public override void OnStart()
        {
            animator.SetBool("isPush", false);
            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

    }
}