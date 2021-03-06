﻿/* === Hacks On Baby xD ===
 * Used during demo only.
 * 
 * Right Ctrl + H to show the hacks menu.
 * 
 * Allow us to change player settings so we can speed through the level as 
 * well as give/get resources without actually going to specific locations 
 * where the resources reside in the map.
 * */

/* Note to Self:
 *     RigidbodyFirstPersonController:
 *         ForwardSpeed, BackwardSpeed, StrafeSpeed
 *         RunMultiplier
 *     PlayerHealth:
 *         CurrentHealth
 *     PlayerStamina:
 *         CurrentStamina
 *     PlayerInventory:
 *         Has
 *             Fist, Flashlight, Crowbar, LaserPistol, Keycard
 *         Equipped
 *             Fist, Flashlight, Crowbar, LaserPistol
 *          NumMedkits
 *          NumBatteries
 *      Under PlayerPrefab:
 *          Player->PlayerView->Equipment:
 *              HandLight
 *              Crowbar
 *          HUD (medium priority, show/display models if equipped/not equipped)
 * */
using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[Serializable]
public class HacksOnBabyxD : MonoBehaviour
{
    public class PlayerValues
    {
        public float forwardSpeed, backwardSpeed, strafeSpeed;
        public float runMultiplier;

        public int currentHealth;

        public float currentStamina;

        public bool hasFist, hasFlashlight, hasCrowbar, hasLaserPistol, hasKeycard;
        public bool equippedFist, equippedFlashlight, equippedCrowbar, equippedLaserPistol;
        public int numMedkits, numBatteries;

        //public bool showHandLightModel, showCrowbarModel;
    }

    //↓↓↓ Warning about UnityStandardAssets is because there are 2 "Standard Assets" folder. Fix that and this will be fixed
    public GameObject Player;
    //public GameObject HandLightModel, CrowbarModel;


    private RigidbodyFirstPersonController RbFPCScript;
    private PlayerHealth HealthScript;
    private PlayerStamina StaminaScript;
    private PlayerInventory InventoryScript;

    private PlayerValues defaultValues = new PlayerValues();
    private PlayerValues customValues = new PlayerValues();

    private Rect HacksOnRect = new Rect(100, 50, (Screen.width - 200), (Screen.height - 100));
    private bool ShowHacksDialog = false;


    private string movementSpeedString = new string('\0', 5);
    private string runMultiplierString = new string('\0', 5);
    void Start()
    {
        RbFPCScript = Player.GetComponent<RigidbodyFirstPersonController>();
        HealthScript = Player.GetComponent<PlayerHealth>();
        StaminaScript = Player.GetComponent<PlayerStamina>();
        InventoryScript = Player.GetComponent<PlayerInventory>();
        SaveDefaultValues(defaultValues);   //save to use if needed
        SaveDefaultValues(customValues);    //save to show default values at start
    }

    private void OnGUI()
    {
        if (ShowHacksDialog)
        {
            HacksOnRect = GUILayout.Window(0, HacksOnRect, HacksOnContent, "Hacks On Baby (You Noob.)");
        }
    }

    void HacksOnContent(int WindowID)
    {
        GUILayout.Label("Todo still, will make it look better");

        GUILayout.Label("Move Player to:");
        if (GUILayout.Button("Level 1 Start"))  { Player.transform.position = new Vector3(-35.95f, 0.5f, -12.5f); }
        if (GUILayout.Button("Level 1 Exit"))   { Player.transform.position = new Vector3(6f, 0.5f, 30f);}
        if (GUILayout.Button("Level 2 Start (Not implemented yet)")) {
            Debug.Log("Not implemented yet");
            //Player.transform.position = new Vector3(-35.95f, 0.5f, -12.5f);
        }
        if (GUILayout.Button("Engine room"))    {
            Debug.Log("temporary location for engine room");
            Player.transform.position = new Vector3(20.4f, 0.5f, 28.4f);
        }
        if (GUILayout.Button("Locker room"))    {
            Debug.Log("temporary location for locker room");
            Player.transform.position = new Vector3(19.7f, 0.5f, 37.6f);
        }

        GUILayout.Label("Movement Speed");
        movementSpeedString = GUILayout.TextField(movementSpeedString, 100);
        movementSpeedString = Regex.Replace(movementSpeedString, @"[^0-9 ]", "");
        int movementSpeed;
        int.TryParse(movementSpeedString, out movementSpeed);

        GUILayout.Label("Run Multiplier");
        runMultiplierString = GUILayout.TextField(runMultiplierString, 100);
        runMultiplierString = Regex.Replace(runMultiplierString, @"[^0-9 ]", "");
        int runMultiplier;
        int.TryParse(runMultiplierString, out runMultiplier);

        if (GUILayout.Button("Change Settings"))
        {
            RbFPCScript.movementSettings.ForwardSpeed = movementSpeed;
            RbFPCScript.movementSettings.BackwardSpeed = movementSpeed;
            RbFPCScript.movementSettings.StrafeSpeed = movementSpeed;
            RbFPCScript.movementSettings.RunMultiplier = runMultiplier;
        }


    }
    void Update()
    {
        //right ctrl + h
        if( Input.GetKey(KeyCode.RightControl))
        {
            if( Input.GetKeyDown(KeyCode.H))
            {
                ShowHacksDialog = !ShowHacksDialog;
                CursorIOSwitch(ShowHacksDialog);
            }
        }
    }

    private void CursorIOSwitch(bool ShowHacksDialog)
    {
        if (ShowHacksDialog)
        {
            RbFPCScript.mouseLook.lockCursor = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            RbFPCScript.mouseLook.lockCursor = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void SaveDefaultValues(PlayerValues pv)
    {
        pv.forwardSpeed = RbFPCScript.movementSettings.ForwardSpeed;
        pv.backwardSpeed = RbFPCScript.movementSettings.BackwardSpeed;
        pv.strafeSpeed = RbFPCScript.movementSettings.StrafeSpeed;
        pv.runMultiplier = RbFPCScript.movementSettings.RunMultiplier;
        pv.currentHealth = HealthScript.currentHealth;
        pv.currentStamina = StaminaScript.currentStamina;
        pv.hasFist = InventoryScript.hasFist;
        pv.hasFlashlight = InventoryScript.hasFlashlight;
        pv.hasCrowbar = InventoryScript.hasCrowbar;
        pv.hasLaserPistol = InventoryScript.hasLaserPistol;
        pv.hasKeycard = InventoryScript.hasKeycard;
        pv.equippedFist = InventoryScript.equippedFist;
        pv.equippedFlashlight = InventoryScript.equippedFlashlight;
        pv.equippedCrowbar = InventoryScript.equippedCrowbar;
        pv.equippedLaserPistol = InventoryScript.equippedLaserPistol;
        pv.numMedkits = InventoryScript.numMedkits;
        pv.numBatteries = InventoryScript.numBatteries;
        //add default values for showing flashlight and crowbar models
    }

    //function: revert values back to default values

    //function: change values according to modified values (customValues)
}
