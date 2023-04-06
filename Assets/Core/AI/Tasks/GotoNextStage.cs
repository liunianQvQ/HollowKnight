using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Core.AI
{
    public class GotoNextStage : EnemyAction
    {
        public SharedInt CurrentStage;

        public override TaskStatus OnUpdate()
        {
            CurrentStage.Value += 1;
            return TaskStatus.Success;
        }
    }
}