using UnityEngine;

public enum AudioMomentType
{
    onOff,
    change
}

public class AudioMomentController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string eventRef;
    [SerializeField]
    private AudioMomentType type;
    [SerializeField]
    private string parameterName;
    [SerializeField]
    private float parameterChangeValue;
    private float oldParameterValue;

    private FMOD.Studio.EventInstance eventsInstance = null;
    private FMOD.Studio.ParameterInstance parameterInstance = null;
    private bool isChanged = false;

    void OnEnable ()
    {
        if (eventsInstance == null ) { eventsInstance = FMODUnity.RuntimeManager.CreateInstance(eventRef); }
        eventsInstance.start();
        if (type == AudioMomentType.change)
        {
            eventsInstance.getParameter(parameterName,out parameterInstance);
            parameterInstance.getValue(out oldParameterValue);
        }
    }

	void OnDisable ()
    {
        if (type == AudioMomentType.onOff)
        {
            eventsInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    public void ChangeValue(bool enable)
    {
        if (eventsInstance == null) { OnEnable(); }
        if (enable)
        {
            parameterInstance.setValue(parameterChangeValue);
            isChanged = true;
        }
        else
        {
            parameterInstance.setValue(oldParameterValue);
        }
    }

}
