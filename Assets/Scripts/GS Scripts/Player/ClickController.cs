using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ClickController : MonoBehaviour {
    public CameraController cameraScript;
    public ToolController toolController;
    public PointerController pointerScript;
    public ColorController colorController;

    public MeshController hatMeshController;
    public TextureController hatTextureController;

    public MeshController backMeshController;
    public TextureController backTextureController;

    public PositionMaps positionMaps;

    public void Update() 
    {
        
        //WHEN NOT OVER UI
        if (!EventSystem.current.IsPointerOverGameObject()) 
        {
            //CAMERA ROTATE
            if (Input.GetMouseButton(1)) 
            {
                cameraScript.Orbit();
            }

            //CAMERA PAN
            if (Input.GetMouseButton(2)) {
                cameraScript.Pan();
            }

            //LEFT CLICKS
            if (Input.GetMouseButtonDown(0)) 
            {
                if (toolController.currentTool == "pen") 
                {
                    meshComponent component = positionMaps.GetComponent(pointerScript.normalPosition);
                    if (component == meshComponent.hat) {
                        hatMeshController.AddVoxel(pointerScript.normalPosition);
                    } else if (component == meshComponent.back) {
                        backMeshController.AddVoxel(pointerScript.normalPosition);
                    }
                }
                        
                else if (toolController.currentTool == "eraser") 
                {
                    meshComponent component = positionMaps.GetComponent(pointerScript.position);
                    if (component == meshComponent.hat) {
                        hatMeshController.RemoveVoxel(pointerScript.position);
                    } else if (component == meshComponent.back) {
                        backMeshController.RemoveVoxel(pointerScript.position);
                    }
                }
                        
                else if (toolController.currentTool == "picker") 
                {
                    meshComponent component = positionMaps.GetComponent(pointerScript.position);
                    if (component == meshComponent.hat) {
                        colorController.SetColor(hatMeshController.maps[pointerScript.position].color);
                    } else if (component == meshComponent.back) {
                        colorController.SetColor(backMeshController.maps[pointerScript.position].color);
                    }
                }
                        
                else if (toolController.currentTool == "brush") 
                {
                    meshComponent component = positionMaps.GetComponent(pointerScript.position);
                    if (component == meshComponent.hat) {
                        hatTextureController.SetPixel(pointerScript.position, ColorController.primaryColor);
                    } else if (component == meshComponent.back) {
                        backTextureController.SetPixel(pointerScript.position, ColorController.primaryColor);
                    }
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0) 
            {
                cameraScript.Zoom();
            }
        
        }
    }
}