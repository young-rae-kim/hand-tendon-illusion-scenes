using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoManager : MonoBehaviour
{
    [SerializeField]
    private GrabManager grabManager;

    [SerializeField]
    private LiftManager liftManager;

    [SerializeField]
    private HoldManager holdManager;

    [SerializeField]
    private TrainManager trainManager;

    [SerializeField]
    private StaplerManager staplerManager;

    public enum SceneType 
    {
        PushButton = 0, 
        HoverButton = 1, 
        GrabObject = 2, 
        LiftObject = 3, 
        DragObject = 4, 
        StaplerGadget = 5, 
        Gadget = 6
    }

    public SceneType CurrentScene {
        get { return currentScene; }
        set 
        {
            LoadScene(value);
            currentScene = value;
        }
    }

    private SceneType currentScene = SceneType.PushButton;

    [SerializeField]
    private GameObject[] pushButtonObjects;

    [SerializeField]
    private GameObject[] hoverButtonObjects;

    [SerializeField]
    private GameObject[] grabObjectObjects;

    [SerializeField]
    private GameObject[] liftObjectObjects;

    [SerializeField]
    private GameObject[] dragObjectObjects;

    [SerializeField]
    private GameObject[] staplerGadgetObjects;

    [SerializeField]
    private GameObject[] gadgetObjects;

    public void SetScene(int input)
    {
        CurrentScene = (SceneType) input;
    }

    public void LoadScene(SceneType type)
    {
        DisableScene();
        EnableScene(type);
    }

    public void ReloadScene()
    {
        DisableScene();
        EnableScene(CurrentScene);
    }

    private void DisableScene()
    {
        GameObject[] targetObjects = {};

        switch (currentScene)
        {
            case SceneType.PushButton:
                targetObjects = pushButtonObjects;
                break;

            case SceneType.HoverButton:
                targetObjects = hoverButtonObjects;
                break;

            case SceneType.GrabObject:
                targetObjects = grabObjectObjects;
                grabManager.Revert();
                break;

            case SceneType.LiftObject:
                targetObjects = liftObjectObjects;
                if (liftManager.gameObject.activeSelf)
                    liftManager.Revert();
                else
                    holdManager.Revert();
                break;

            case SceneType.DragObject:
                targetObjects = dragObjectObjects;
                trainManager.Revert();
                break;

            case SceneType.StaplerGadget:
                targetObjects = staplerGadgetObjects;
                staplerManager.Revert();
                break;

            case SceneType.Gadget:
                targetObjects = gadgetObjects;
                break;

            default:
                break;
        }

        for (int i = 0; i < targetObjects.Length; i++)
        {
            targetObjects[i].SetActive(false);
        }
    }

    private void EnableScene(SceneType targetScene)
    {
        GameObject[] targetObjects = {};

        switch (targetScene)
        {
            case SceneType.PushButton:
                targetObjects = pushButtonObjects;
                break;

            case SceneType.HoverButton:
                targetObjects = hoverButtonObjects;
                break;

            case SceneType.GrabObject:
                targetObjects = grabObjectObjects;
                break;

            case SceneType.LiftObject:
                targetObjects = liftObjectObjects;
                break;

            case SceneType.DragObject:
                targetObjects = dragObjectObjects;
                break;

            case SceneType.StaplerGadget:
                targetObjects = staplerGadgetObjects;
                break;

            case SceneType.Gadget:
                targetObjects = gadgetObjects;
                break;

            default:
                break;
        }

        for (int i = 0; i < targetObjects.Length; i++)
        {
            targetObjects[i].SetActive(true);
        }
    }
}
