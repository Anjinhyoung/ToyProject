using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : StateMachineBehaviour
{
    // IAnimationInterface inter;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        // PlayerFire pf = animator.GetComponentInParent<PlayerFire>();

        IAnimationInterface inter = animator.GetComponent<IAnimationInterface>();
        inter.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        IAnimationInterface inter = animator.GetComponent<IAnimationInterface>();
        inter.OnStateExit(animator, stateInfo, layerIndex);
    }
}
