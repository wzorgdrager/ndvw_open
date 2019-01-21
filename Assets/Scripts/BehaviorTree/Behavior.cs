using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior : MonoBehaviour
{

    public enum Status
    {
        SUCCESS,
        FAILURE,
        RUNNING,
        SUSPENDED,
        INVALID
    }

    private Status status;

    public Behavior()
    {
        status = Status.INVALID;
    }

	// Use this for initialization
	void Start () {
        status = Status.INVALID;
	}

    protected void OnInitialize() { }

    protected abstract Status BUpdate();

    protected void OnTerminate() { }

    public Status Tick ()
    {
        if (status != Status.RUNNING) OnInitialize();
        status = BUpdate();
        if (status != Status.RUNNING) OnTerminate();
        return status;
    }


}
