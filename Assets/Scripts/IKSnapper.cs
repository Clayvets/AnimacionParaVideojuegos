using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKSnapper : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float proceduralInfluence;
    [SerializeField] private MultiParentConstraint[] animatedBones;
    [SerializeField] private MultiParentConstraint[] proceduralBones;
    //[SerializeField] private AnimationCurve activationAnimation;
    //[SerializeField] private AnimationCurve deactivationAnimation;
    [SerializeField] private float intepolationSpeed = 1.0f;

    private float weightt;
   
    private bool overrideTrigger;
    private void UpdateInfluence(float weight)
    {
        if (animatedBones == null) return;
        
        foreach (MultiParentConstraint multiParentConstraint in animatedBones)
        {
            if (multiParentConstraint == null) continue;
            multiParentConstraint.weight = weight;
        }

        if (proceduralBones == null) return;
        
        foreach (MultiParentConstraint proceduralConstraint in proceduralBones)
        {
            if (proceduralConstraint == null) continue;
            proceduralConstraint.weight = 1 - weight;
        }
    }

    public void SetWeight(bool state)
    {
        Debug.Log(state);
        weightt += Time.deltaTime * intepolationSpeed * (state ? 1 : -1);
        weightt = Mathf.Clamp01(weightt);
        //UpdateInfluence();
    }

    private void LateUpdate()
    {
        /*if (overrideTrigger)
        {
            proceduralInfluence = Mathf.Lerp(proceduralInfluence, proceduralInfluence + 3, Time.deltaTime);
        }
        else
        {
            proceduralInfluence = Mathf.Lerp(proceduralInfluence, proceduralInfluence - 3, Time.deltaTime);
        }*/

        proceduralInfluence = Mathf.Clamp01(proceduralInfluence);
        UpdateInfluence(weightt);
    }

    private void OnValidate()
    {
        //UpdateInfluence(proceduralInfluence);
    }

    public void OverrideIK(bool message)
    {
        overrideTrigger = message;
    }
}
