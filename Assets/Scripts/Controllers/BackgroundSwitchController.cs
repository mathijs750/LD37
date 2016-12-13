using UnityEngine;

public class BackgroundSwitchController : MonoBehaviour
{
    private int layerIndex;

	void Awake ()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
	}
	
	public void NextLayer()
    {
        transform.GetChild(layerIndex).gameObject.SetActive(false);
        layerIndex++;
        transform.GetChild(layerIndex).gameObject.SetActive(true);
    }
}
