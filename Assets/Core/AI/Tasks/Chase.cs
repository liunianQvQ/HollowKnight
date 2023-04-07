using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Core.AI
{
    public class Chase : EnemyAction
    {
        [Header("追击速度")]
        public float Speed;
        private float Dire;
        private int a = 1;

        public override void OnStart()
        {
            Dire = transform.position.x - player.transform.position.x < 0 ? 1 : -1;
            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            ChaseStatus();
            return TaskStatus.Running;
        }

        void ChaseStatus()
        {
            if(a == 1)
            {
                animator.SetTrigger("isAttack");
                a = a - 1;
            }
            transform.Translate(new Vector2(Speed * Dire * Time.deltaTime,0));
        }

    }
}