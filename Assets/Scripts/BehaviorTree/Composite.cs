using System;
using System.Collections.Generic;

public abstract class Composite : Behavior
{
    protected List<Behavior> children;

    public Composite()
    {
        children = new List<Behavior>();
    }

    public void AddChild(Behavior child)
    {
        children.Add(child);
    }

    public void ClearChildren()
    {
        children = new List<Behavior>();
    }
}
