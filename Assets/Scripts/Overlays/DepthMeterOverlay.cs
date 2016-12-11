using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthMeterOverlay : MonoBehaviour
{
    private RectTransform needle;
    private float depth = -26;
	void Awake ()
    {
        needle = transform.GetChild(1).GetComponent<RectTransform>();
	}

    void OnEnable()
    {
        StartCoroutine(DrillCutscene(10));
    }

    IEnumerator DrillCutscene(float duration)
    {
        float oldDepth = depth;
        depth -= 51;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            needle.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(oldDepth, depth, elapsedTime/duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (GameManager.instance.globalState == GlobalState.Cutscene) { GameManager.instance.globalState = GlobalState.Gameplay; }
        GameManager.instance.closeOverlay();
    }

}
