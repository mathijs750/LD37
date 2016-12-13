using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthMeterOverlay : MonoBehaviour
{
    [SerializeField]
    private AudioEventManager audioManager;
    private RectTransform needle;

    private float depth = -26;
	void Awake ()
    {
        needle = transform.GetChild(1).GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        StartCoroutine(DrillCutscene(4.5f));
    }

    IEnumerator DrillCutscene(float duration)
    {
        audioManager.setParameter(new eventParameters(0,"engineMovement"), 1);
        float oldDepth = depth;
        depth -= 80;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            needle.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(oldDepth, depth, elapsedTime/duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (GameManager.instance.globalState == GlobalState.Cutscene) { GameManager.instance.globalState = GlobalState.Gameplay; }
        GameManager.instance.closeOverlay();
        audioManager.setParameter(new eventParameters(0, "engineMovement"), 0);

    }

}
