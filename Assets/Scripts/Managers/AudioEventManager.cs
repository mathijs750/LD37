using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct eventParameters
{
    public int indexInEventRefs;
    //public FMOD.Studio.ParameterInstance parameter;
    public string parameterName;

    public eventParameters(int indexInEventRefs, string parameterName)
    {
        this.indexInEventRefs = indexInEventRefs;
        this.parameterName = parameterName;
    }
}

public class AudioEventManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string[] eventRefs;
    FMOD.Studio.EventInstance[] eventsInstances;

    void Awake ()
    {
        eventsInstances = new FMOD.Studio.EventInstance[eventRefs.Length];
        for (int i = 0; i < eventRefs.Length; i++)
        {
            eventsInstances[i] = FMODUnity.RuntimeManager.CreateInstance(eventRefs[i]);
            eventsInstances[i].start();
        }
	}
	
    public void setParameter(eventParameters evParams, float value)
    {
        eventsInstances[evParams.indexInEventRefs].setParameterValue(evParams.parameterName, value);
    }

}
