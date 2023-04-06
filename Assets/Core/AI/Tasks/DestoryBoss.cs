using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Core.Character;
using Core.Util;
using DG.Tweening;

namespace Core.AI
{
    public class DestoryBoss : EnemyAction
    {
        public float bleedTime = 2.0f;
        public ParticleSystem bleedEffect;
        public ParticleSystem explodeEffect;

        private bool isDestoryed;

        public override void OnStart()
        {
            EffectManager.Instance.PlayOneShot(bleedEffect, transform.position);
            DOVirtual.DelayedCall(bleedTime, () =>
            {
                EffectManager.Instance.PlayOneShot(explodeEffect, transform.position);
                CameraController.Instance.ShakeCamera(0.7f);
                isDestoryed = true;
                Object.Destroy(gameObject);
            }, false);
        }

        public override TaskStatus OnUpdate()
        {
            return isDestoryed ? TaskStatus.Success : TaskStatus.Running;
        }

    }
}