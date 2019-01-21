using UnityEngine;
using UnityEditor;

public class Selector : Sequence
{
    protected override Status BUpdate()
    {
        foreach (Behavior child in NextChild())
        {
            Status s = child.Tick();
            if (s != Status.FAILURE) return s;
        }
        return Status.FAILURE;
    }

}