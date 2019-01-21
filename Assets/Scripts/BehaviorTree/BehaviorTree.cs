using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree
{

    protected Behavior root;

    public BehaviorTree()
    {
    }

    public BehaviorTree(Behavior root)
    {
        this.root = root;
    }

    public void Tick()
    {
        root.Tick();
    }

}
