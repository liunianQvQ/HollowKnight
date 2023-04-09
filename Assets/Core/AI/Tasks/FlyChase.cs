using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Core.AI
{
    public class FlyChase : EnemyAction
    {

        [Header("追击速度")]
        public float Speed;
        private float Dire;
        private int a = 1; 
        private Vector2 dis;

        public override void OnStart()
        {
            Dire = transform.position.x - player.transform.position.x < 0 ? 1 : -1;
            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            CaucuDistance();
            ChaseStatus();
            return TaskStatus.Running;
        }

        void ChaseStatus()
        {
            if (a == 1)
            {
                animator.SetTrigger("isAttack");
                a = a - 1;
            }
            transform.Translate(dis*Time.deltaTime);
        }

        void CaucuDistance()
        {
            dis =  player.transform.position - transform.position;
        }

    }
}