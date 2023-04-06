using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Core.AI
{
    public class IsHealthUnder : EnemyConditional
    {
        public SharedInt HealthTreshold;
        public override TaskStatus OnUpdate()
        {
            return destructable.CurrentHealth < HealthTreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}