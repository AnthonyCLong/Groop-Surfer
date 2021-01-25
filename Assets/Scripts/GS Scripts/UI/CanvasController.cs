﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    /*public RadioReference canvasRadio;
    public GameObject voxelContainer;
    public GameObject voxelPrefab;
    public GameObject tabContainer;
    public GameObject tabPrefab;

    public CameraController cameraController;
    public GameObject grid;
    public GameObject gridCenter;
    public GameObject focalPoint;

    public CustomButton undo;
    public CustomButton redo;
    public CustomButton _default;
    public CustomButton clear;
    public CustomButton layerSelect;

    public CustomButton selectMainButton;
    public Image selectMainIcon;
    public Sprite selectLayerSprite;

    public ScreenSettings screenSettings;

    public ClickController clickController;
    public ColorController colorController;
    public PointerController pointerController;
    public ToolController toolController;
    public CursorController cursorController;
    public List<GameObject> voxels;
    public List<GameObject> tabs;

    public void Start() {
        //Create(true);
        colorController.ResetColors();
    }

    public void Create(bool makeTex = false, int x = 16, int y = 40, int z = 14) {
        GameObject newVoxel = Instantiate(voxelPrefab, Vector3.zero, Quaternion.identity, voxelContainer.transform);
        MeshController meshController = newVoxel.GetComponent<MeshController>();
        TextureController textureController = newVoxel.GetComponent<TextureController>();

        if (makeTex) {
            textureController.MakeTexture();
        }

        GameObject newTab = Instantiate(tabPrefab, tabContainer.transform.position - new Vector3(0, 2, 0), Quaternion.identity, tabContainer.transform);
        CustomButton button = newTab.GetComponent<CustomButton>();

        tabContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(67, 0);
        newTab.transform.position += new Vector3(134 * (tabContainer.transform.childCount - 2), 0, 0);
        button.buttonOptions.radioReference = canvasRadio;
        if (!canvasRadio.defaultRadio) {
            canvasRadio.defaultRadio = button;
        }
        canvasRadio.Add(button);
        voxels.Add(newVoxel);
        tabs.Add(newTab);
        button.onLeftClick.AddListener(delegate () { Change(newVoxel); });

        CustomButton[] close = button.GetComponentsInChildren<CustomButton>();
        close[1].onLeftClick.AddListener(delegate () { Remove(newVoxel, newTab); });

        button.Select();
    }

    public void Change(GameObject voxel) {
        MeshController meshController = voxel.GetComponent<MeshController>();
        TextureController textureController = voxel.GetComponent<TextureController>();
        Variables variables = voxel.GetComponent<Variables>();

        clickController.meshScript = meshController;

        foreach (GameObject item in voxels) {
            item.SetActive(false);
        }

        voxel.SetActive(true);

        undo.onLeftClick.RemoveAllListeners();
        //undo.onLeftClick.AddListener(delegate () { historyController.undo(); });

        redo.onLeftClick.RemoveAllListeners();
        //redo.onLeftClick.AddListener(delegate () { historyController.redo(); });

        _default.onLeftClick.RemoveAllListeners();
        _default.onLeftClick.AddListener(delegate () { toolController.SetCurrentTool(""); });
        _default.onLeftClick.AddListener(delegate () { cursorController.SetDefault(); });

        clear.onLeftClick.RemoveAllListeners();
        clear.onLeftClick.AddListener(delegate () { meshController.Clear(); });
        clear.onLeftClick.AddListener(delegate () { toolController.SetCurrentTool("clearer"); });
        clear.onLeftClick.AddListener(delegate () { clear.Deselect(); });

        layerSelect.onLeftClick.RemoveAllListeners();
        layerSelect.onLeftClick.AddListener(delegate () { selectMainButton.Swap(layerSelect); });
        layerSelect.onLeftClick.AddListener(delegate () { selectMainIcon.sprite = selectLayerSprite; });
        layerSelect.onLeftClick.AddListener(delegate () { selectMainButton.Select(); });
        layerSelect.onLeftClick.AddListener(delegate () { cursorController.SetLayerSelect(); });

        //Update Screen settings
        screenSettings.curentVoxel = voxel;
    }

    public void Remove(GameObject voxel, GameObject tab) {
        int index = voxels.IndexOf(voxel);
        foreach (GameObject t in tabs) {
            if (t.transform.position.x > tab.transform.position.x) {
                t.transform.position -= new Vector3(134, 0, 0);
            }
        }

        tabContainer.GetComponent<RectTransform>().sizeDelta -= new Vector2(67, 0);

        if (index > 0) {
            tabs[index - 1].GetComponent<CustomButton>().Select();
        } else {
            if (tabs.Count > 1) {
                tabs[index + 1].GetComponent<CustomButton>().Select();
            }
        }
        
        voxels.Remove(voxel);
        tabs.Remove(tab);
        Destroy(voxel);
        Destroy(tab);
    }*/
}
