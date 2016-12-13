using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct brokenMachine
{
    public GameObject gameObject;
    public bool isFixed;
}

public class DrillManager : MonoBehaviour
{
    [SerializeField]
    private brokenMachine[] brokenMachines;

    void Awake()
    {
    }

    public bool allMachinesFixed()
    {
        for (int i = 0; i < brokenMachines.Length; i++)
        {
            if (!brokenMachines[i].isFixed) { return false; }
        }
        return true;
    }

    public void SetFixed(GameObject invoker)
    {
        for (int i = 0; i < brokenMachines.Length; i++)
        {
            if (brokenMachines[i].gameObject == invoker)
            {
                Debug.Log("New fixed machine");
                brokenMachines[i].isFixed = true;
            }
        }
    }
}
