using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    private int skulls;

    public static Collection instance;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        skulls = States.instance.getSkulls();
    }

    public void updateSkulls(int gain)
    {
        skulls += gain;
        NpcInteraction.instance.UpdateUI();
    }

    public int getSkulls()
    {
        return skulls;
    }
}
