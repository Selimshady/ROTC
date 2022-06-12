using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collection : MonoBehaviour
{
    private int skulls;

    public static Collection instance;

    public TMP_Text skullText;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        skulls = States.instance.getSkulls();
        skullText.SetText(skulls.ToString());
    }

    public void updateSkulls(int gain)
    {
        skulls += gain;
        NpcInteraction.instance.UpdateUI();
        States.instance.setSkulls(skulls);
        skullText.SetText(skulls.ToString());
    }
    public void UpdateSkullsCombat(int gain)
    {
        skulls += gain;
        States.instance.setSkulls(skulls);
        skullText.SetText(skulls.ToString());
    }

    public int getSkulls()
    {
        return skulls;
    }
}
