using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureController : MonoBehaviour {

    public void Start() {
        MakeTexture();
    }

    public void MakeTexture() {
        MeshController meshController = transform.GetComponent<MeshController>();

        Texture2D texture = new Texture2D(meshController.maps.Count, 1, TextureFormat.RGBA32, false);

        Color[] colors = new Color[meshController.maps.Count];
        for (int i = 0; i < colors.Length; i++) {
            colors[i] = Color.clear;
        }

        texture.SetPixels(colors);
        texture.Apply();

        transform.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", texture);
        Debug.Log(transform.GetComponent<MeshRenderer>().material.name);
    }

    public void SetPixel(Vector3Int position, Color color) {
        Texture2D texture = transform.GetComponent<MeshRenderer>().material.GetTexture("_BaseMap") as Texture2D;
        MeshController meshController = transform.GetComponent<MeshController>();

        Vector2Int pixel = meshController.maps[position].pixel;

        texture.SetPixel(pixel.x, pixel.y, color);
        texture.Apply();
    }

    public void Clear() {
        MeshController meshController = transform.GetComponent<MeshController>();
        Texture2D texture = transform.GetComponent<MeshRenderer>().material.GetTexture("_BaseMap") as Texture2D;

        Color[] colors = new Color[meshController.maps.Count];
        for (int i = 0; i < colors.Length; i++) {
            colors[i] = Color.clear;
        }

        texture.SetPixels(colors);
        texture.Apply();
    }

    public void SaveTexture(string path) {
        Texture2D texture = transform.GetComponent<MeshRenderer>().material.GetTexture("_BaseMap") as Texture2D;
        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, bytes);
    }

    public void loadTexture(string path) {
        //Clear();
        byte[] bytes;
        bytes = System.IO.File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(bytes);
        texture.Apply();

        transform.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", texture);
    }

}
