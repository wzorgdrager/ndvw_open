using System;

public class Sequence : Composite
{

    public System.Collections.Generic.IEnumerable<Behavior>
    NextChild()
    {
        foreach (var child in children)
        {
            yield return child;
        }
    }

    protected override Status BUpdate()
    {
        foreach (Behavior child in NextChild())
        {
            Status s = child.Tick();
            if (s != Status.SUCCESS) return s;
        }
        return Status.SUCCESS;
    }

}
