using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    private Node target;

    public GameObject UI;

    public void setTarget(Node _target)
    {
        target = _target;        //this.target = target;

        transform.position = target.getBuildPosition();

        UI.SetActive(true);
    }

    public void hideUI()
    {
        UI.SetActive(false);
    }
}
