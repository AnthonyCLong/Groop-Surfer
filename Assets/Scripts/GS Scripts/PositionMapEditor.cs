// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;

// [CustomEditor(typeof(PositionMap))]
// public class PositionMapEditor : Editor {

//     public override void OnInspectorGUI() {

//         base.OnInspectorGUI();
//         PositionMap marker = (PositionMap)target;

//         if (GUILayout.Button("Map Position")) {
//             using (System.IO.StreamWriter file =
//             new System.IO.StreamWriter(@"C:\Users\Michael\Groop Surfer\Assets\Scripts\GS Scripts\Positions.txt", true)) {
//                 Vector3Int pos = marker.GetPosition();
//                 int i = marker.GetI();
//                 string line = "{new Vector3Int" + pos + ", new Vector2Int(" + i + ", 0)},";
//                 Debug.Log(line);
//                 file.WriteLine(line);
//             }
//         }

//         GUILayout.BeginHorizontal();

//         if (GUILayout.Button("-x")) {
//             marker.transform.position -= Vector3.right;
//         }

//         if (GUILayout.Button("+x")) {
//             marker.transform.position += Vector3.right;
//         }

//         GUILayout.EndHorizontal();

//         GUILayout.BeginHorizontal();

//         if (GUILayout.Button("-y")) {
//             marker.transform.position -= Vector3.up;
//         }

//         if (GUILayout.Button("+y")) {
//             marker.transform.position += Vector3.up;
//         }

//         GUILayout.EndHorizontal();

//         GUILayout.BeginHorizontal();

//         if (GUILayout.Button("-z")) {
//             marker.transform.position -= Vector3.forward;
//         }

//         if (GUILayout.Button("+z")) {
//             marker.transform.position += Vector3.forward;
//         }

//         GUILayout.EndHorizontal();
//     }
// }
