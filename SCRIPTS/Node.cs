using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color mouseEnterColor;

    public Color errorBuildColor;

    public Vector3 turretPositionOffset;

    public GameObject currentTurret;

    private Renderer renderer_;

    private Color nodeStartColor;

    BuildMananger buildMananger;

    private void Start()
    {
        renderer_ = GetComponent<Renderer>();
        nodeStartColor = renderer_.material.color;

        buildMananger = BuildMananger.instance;
    }

    public Vector3 getBuildPosition()
    {
        return transform.position + turretPositionOffset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (currentTurret != null)
        {
            //Debug.Log("Cant build");
            buildMananger.selectNode(this);
            return;
        }

        if (!buildMananger.allowBuilding)
        {
            return;
        }

        buildMananger.buildTurretAt(this);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildMananger.allowBuilding)
        {
            return;
        }

        if (currentTurret != null)
        {
            renderer_.material.color = errorBuildColor;
            return;
        }

        if (buildMananger.enoughMoneyForBuilding)
        {
            renderer_.material.color = mouseEnterColor;
        }
        else
        {
            renderer_.material.color = errorBuildColor; // not enough money
        }
    }

    private void OnMouseExit()
    {
        renderer_.material.color = nodeStartColor;
    }
}
