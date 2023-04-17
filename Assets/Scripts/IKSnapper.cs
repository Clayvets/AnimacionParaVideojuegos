using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class IKSnapper : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float proceduralInfluence;
    [SerializeField] private MultiParentConstraint[] animatedBones;
    [SerializeField] private MultiParentConstraint[] proceduralBones;
    

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

    private void LateUpdate()
    {
        if (overrideTrigger)
        {
            proceduralInfluence = Mathf.Lerp(proceduralInfluence, proceduralInfluence + 3, Time.deltaTime);
        }
        else
        {
            proceduralInfluence = Mathf.Lerp(proceduralInfluence, proceduralInfluence - 3, Time.deltaTime);
        }

        proceduralInfluence = Mathf.Clamp01(proceduralInfluence);
        UpdateInfluence(proceduralInfluence);
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
