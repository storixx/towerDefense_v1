using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMananger : MonoBehaviour
{
    public static BuildMananger instance;

    public GameObject buildEffectPref;    

    public NodeUI nodeUI;    

    private TurretInfo turretToBuild;

    private Node selectedNode;
    
    private void Awake()
    {
        instance = this;
    }

    // @Property
    public bool allowBuilding
    {
        get        
        {            
            return turretToBuild != null;       
        }
    }
    // @Property
    public bool enoughMoneyForBuilding
    {
        get
        {
            return PlayerStats.playerMoney >= turretToBuild.turretCost;
        }
    }

    public void buildTurretAt(Node node)
    {
        //GameObject turretToBuild = buildMananger.getTurretToBuild();
        //currentTurret = (GameObject)Instantiate(turretToBuild, transform.position + turretPositionOffset, transform.rotation);

        if (PlayerStats.playerMoney < turretToBuild.turretCost)
        {
            //Debug.Log("Not enough money to build");
            return;
        }

        PlayerStats.playerMoney -= turretToBuild.turretCost;
        
        GameObject turret = (GameObject)Instantiate(turretToBuild.turretPrefab, node.getBuildPosition(), Quaternion.identity);
        node.currentTurret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffectPref, node.getBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void selectNode(Node node)
    {
        if(selectedNode == node)
        {
            deselectNode();
            return;
        }
        
        selectedNode = node;
        turretToBuild = null;

        nodeUI.setTarget(node);
    }

    public void deselectNode()
    {
        selectedNode = null;
        nodeUI.hideUI();
    }
    
    public void selectTurretToBuild(TurretInfo turret)
    {
        turretToBuild = turret;

        deselectNode();
    }
}
