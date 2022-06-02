using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretInfo defaultTurret;

    public TurretInfo missileLauncher;

    public TurretInfo laser;
    
    BuildMananger buildMananger;

    private void Start()
    {
        buildMananger = BuildMananger.instance;
    }
    public void selectFirstTurret() // purchase
    {
        //Debug.Log("turret placed");
        buildMananger.selectTurretToBuild(defaultTurret);
    }

    public void selectMissileLauncher()
    {
        //Debug.Log("launcher placed");
        buildMananger.selectTurretToBuild(missileLauncher);
    }  
    
    public void selectLaser()
    {
        //Debug.Log("laser placed");
        buildMananger.selectTurretToBuild(laser);
    }
}
