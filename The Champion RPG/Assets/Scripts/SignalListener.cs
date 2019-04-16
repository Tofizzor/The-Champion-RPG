using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Signal Listener that allows to use Scriptable Objects

public class SignalListener : MonoBehaviour {

    public Signal signal;
    public UnityEvent signalEvent;

	public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.RegisterListener(this);
    }
    private void OnDisable()
    {
        signal.DeRegisterListener(this);
    }

}
