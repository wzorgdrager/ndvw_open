using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class BehaviorTreeBuilder
{
    private Behavior lastNode;
    private Stack<Composite> nodeStack;

    public BehaviorTreeBuilder()
    {
        lastNode = null;
        nodeStack = new Stack<Composite>();
    }

    public BehaviorTreeBuilder Action(Action action)
    {
        if (nodeStack.Count > 0)
        {
            Composite parentNode = nodeStack.Peek();
            parentNode.AddChild(action);
        }
        lastNode = action;
        return this;
    }

    public BehaviorTreeBuilder Condition(Condition condition)
    {
        if (nodeStack.Count > 0)
        {
            Composite parentNode = nodeStack.Peek();
            parentNode.AddChild(condition);
        }
        lastNode = condition;
        return this;
    }

    public BehaviorTreeBuilder Sequence()
    {
        Sequence sequence = new Sequence();
        return Composite(sequence);
    }

    public BehaviorTreeBuilder Selector()
    {
        Selector selector = new Selector();
        return Composite(selector);
    }

    private BehaviorTreeBuilder Composite(Composite composite)
    {
        if (nodeStack.Count > 0)
        {
            Composite parentNode = nodeStack.Peek();
            parentNode.AddChild(composite);
        }
        lastNode = composite;
        nodeStack.Push(composite);
        return this;
    }

    public BehaviorTreeBuilder Break()
    {
        if (nodeStack.Count > 0)
        {
            lastNode = nodeStack.Pop();
        }
        return this;
    }

    public BehaviorTree End()
    {
        return new BehaviorTree(lastNode);
    }

}