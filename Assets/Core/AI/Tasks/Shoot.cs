using System.Collections.Generic;
using UnityEngine;
using Core.Combat;
using BehaviorDesigner.Runtime.Tasks;
using Core.Character;

namespace Core.AI
{
    public class Shoot : EnemyAction
    {

        public Weapon weapons;
        public bool shakeCamera;

        public override TaskStatus OnUpdate()
        {
            
                var projectile = Object.Instantiate(weapons.projectilePrefab, weapons.weaponTransform.position, Quaternion.identity);
                projectile.Shooter = gameObject;

                var force = new Vector2(weapons.horizontalForce * transform.localScale.x, weapons.verticalForce);
                projectile.SetForce(force);

                if (shakeCamera)
                    CameraController.Instance.ShakeCamera(0.5f);
            

            return TaskStatus.Success;
        }

    }
}