using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public enum meshDirection {
    posX = 0,
    posY = 1,
    posZ = 2,
    negX = 3,
    negY = 4,
    negZ = 5
}

public enum meshComponent {
    none = 0,
    hat = 1,
    back = 2
}

public struct MapValue {
    public Vector2Int pixel;
    public Color color;

    public MapValue(Vector2Int pix, Color col) {
        this.pixel = pix;
        this.color = col;
    }
}


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshController : MonoBehaviour {
    public meshComponent component;
    public PositionMaps positionMaps;
    public Dictionary<Vector3Int, MapValue> maps;

    public void Awake() {
        GetComponent<MeshFilter>().mesh = new Mesh();
        
        if (component == meshComponent.hat) {
            maps = positionMaps.hatMaps;
        } else if (component == meshComponent.back) {
            maps = positionMaps.backMaps;
        }
    }

    public void Clear() {
        foreach (KeyValuePair<Vector3Int, MapValue> entry in maps) {
            maps[entry.Key] = new MapValue(maps[entry.Key].pixel, Color.clear);
        }
    }

   public void AddVoxel(Vector3Int position) {
        if (maps.ContainsKey(position)) {
            List<meshDirection> facesToAdd = new List<meshDirection>() { meshDirection.posX, meshDirection.posY, meshDirection.posZ, meshDirection.negX, meshDirection.negY, meshDirection.negZ };
            List<int> facesToRemove = new List<int>();
            Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;

            for (int i = 0; i < vertices.Length; i += 4) {
                if ((vertices[i] == (position)) && (normals[i] == Vector3.right)) {
                    if ((vertices[i + 2] == (position + new Vector3(0, 1, 1))) && (normals[i + 2] == Vector3.right)) {
                        facesToAdd.Remove(meshDirection.negX);
                        facesToRemove.Add(i);
                    }

                } else if ((vertices[i] == (position + new Vector3(1, 0, 1))) && (normals[i] == Vector3.left)) {
                    if ((vertices[i + 2] == (position + new Vector3(1, 1, 0))) && (normals[i + 2] == Vector3.left)) {
                        facesToAdd.Remove(meshDirection.posX);
                        facesToRemove.Add(i);
                    }

                } else if ((vertices[i] == (position + new Vector3(1, 0, 0))) && (normals[i] == Vector3.forward)) {
                    if ((vertices[i + 2] == (position + new Vector3(0, 1, 0))) && (normals[i + 2] == Vector3.forward)) {
                        facesToAdd.Remove(meshDirection.negZ);
                        facesToRemove.Add(i);
                    }

                } else if ((vertices[i] == (position + new Vector3(0, 0, 1))) && (normals[i] == Vector3.back)) {
                    if ((vertices[i + 2] == (position + new Vector3(1, 1, 1))) && (normals[i + 2] == Vector3.back)) {
                        facesToAdd.Remove(meshDirection.posZ);
                        facesToRemove.Add(i);
                    }

                } else if ((vertices[i] == (position)) && (normals[i] == Vector3.up)) {
                    if ((vertices[i + 2] == (position + new Vector3(1, 0, 1))) && (normals[i + 2] == Vector3.up)) {
                        facesToAdd.Remove(meshDirection.negY);
                        facesToRemove.Add(i);
                    }

                } else if ((vertices[i] == (position + new Vector3(0, 1, 1))) && (normals[i] == Vector3.down)) {
                    if ((vertices[i + 2] == (position + new Vector3(1, 1, 0))) && (normals[i + 2] == Vector3.down)) {
                        facesToAdd.Remove(meshDirection.posY);
                        facesToRemove.Add(i);
                    }

                }

            }

            maps[position] = new MapValue(maps[position].pixel, ColorController.primaryColor);
            transform.GetComponent<TextureController>().SetPixel(position, ColorController.primaryColor);

            RemoveFaces(facesToRemove);
            AddFaces(position, facesToAdd);
        } else {
            Debug.Log("Not a valid position...");
        }
   }
   
    public void RemoveVoxel(Vector3Int position) 
    {
        if (maps.ContainsKey(position)) {   
            List<int> facesToRemove = new List<int>();
            Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;
            Vector3[] normals = mesh.normals;

            for (int i = 0; i < vertices.Length; i += 4) {
                if ((vertices[i] == (position + new Vector3(1, 0, 0))) && (normals[i] == Vector3.right)) {
                    if ((vertices[i + 2] == (position + new Vector3(1, 1, 1))) && (normals[i + 2] == Vector3.right)) {
                        facesToRemove.Add(i);
                    }

                } else if ((vertices[i] == (position + new Vector3(0, 0, 1))) && (normals[i] == Vector3.left)) {
                    if ((vertices[i + 2] == (position + new Vector3(0, 1, 0))) && (normals[i + 2] == Vector3.left)) {
                        facesToRemove.Add(i);
                    }

                } else if ((vertices[i] == (position + new Vector3(1, 0, 1))) && (normals[i] == Vector3.forward)) {
                    if ((vertices[i + 2] == (position + new Vector3(0, 1, 1))) && (normals[i + 2] == Vector3.forward)) {
                        facesToRemove.Add(i);
                    }

                } else if ((vertices[i] == (position)) && (normals[i] == Vector3.back)) {
                    if ((vertices[i + 2] == (position + new Vector3(1, 1, 0))) && (normals[i + 2] == Vector3.back)) {
                        facesToRemove.Add(i);
                    }

                } else if ((vertices[i] == (position + new Vector3(0, 1, 0))) && (normals[i] == Vector3.up)) {
                    if ((vertices[i + 2] == (position + new Vector3(1, 1, 1))) && (normals[i + 2] == Vector3.up)) {
                        facesToRemove.Add(i);
                    }

                } else if ((vertices[i] == (position + new Vector3(0, 0, 1))) && (normals[i] == Vector3.down)) {
                    if ((vertices[i + 2] == (position + new Vector3(1, 0, 0))) && (normals[i + 2] == Vector3.down)) {
                        facesToRemove.Add(i);
                    }

                }
            }

            RemoveFaces(facesToRemove);
            maps[position] = new MapValue(maps[position].pixel, Color.clear);
            transform.GetComponent<TextureController>().SetPixel(position, Color.clear);

            Vector3Int tmp = new Vector3Int(position.x + 1, position.y, position.z);
            if (maps.ContainsKey(tmp)) {
                if (maps[tmp].color != Color.clear) {
                    AddFaces(position + Vector3Int.right, new List<meshDirection>() { meshDirection.negX });
                }
            }

            tmp = new Vector3Int(position.x, position.y + 1, position.z);
            if (maps.ContainsKey(tmp)) {
                if (maps[tmp].color != Color.clear) {
                    AddFaces(position + Vector3Int.up, new List<meshDirection>() { meshDirection.negY });
                }
            }

            tmp = new Vector3Int(position.x, position.y, position.z + 1);
            if (maps.ContainsKey(tmp)) {
                if (maps[tmp].color != Color.clear) {
                    AddFaces(position + new Vector3Int(0, 0, 1), new List<meshDirection>() { meshDirection.negZ });
                }
            }

            tmp = new Vector3Int(position.x - 1, position.y, position.z);
            if (maps.ContainsKey(tmp)) {
                if (maps[tmp].color != Color.clear) {
                    AddFaces(position + Vector3Int.left, new List<meshDirection>() { meshDirection.posX });
                }
            }

            tmp = new Vector3Int(position.x, position.y - 1, position.z);
            if (maps.ContainsKey(tmp)) {
                if (maps[tmp].color != Color.clear) {
                    AddFaces(position + Vector3Int.down, new List<meshDirection>() { meshDirection.posY });
                }
            }

            tmp = new Vector3Int(position.x, position.y, position.z - 1);
            if (maps.ContainsKey(tmp)) {
                if (maps[tmp].color != Color.clear) {
                    AddFaces(position + new Vector3Int(0, 0, -1), new List<meshDirection>() { meshDirection.posZ });
                }
            }
            
        } else {
            Debug.Log("Not a valid position...");
        }
    }

    public void RemoveFaces(List<int> index) {
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
        List<Vector3> vertices = new List<Vector3>(mesh.vertices);
        List<int> triangles = new List<int>(mesh.triangles);
        List<Vector2> uvs = new List<Vector2>(mesh.uv);

        for (int i = 0; i < index.Count; i++) {
            vertices.RemoveAt(index[i]);
            vertices.RemoveAt(index[i]);
            vertices.RemoveAt(index[i]);
            vertices.RemoveAt(index[i]);

            uvs.RemoveAt(index[i]);
            uvs.RemoveAt(index[i]);
            uvs.RemoveAt(index[i]);
            uvs.RemoveAt(index[i]);

            for (int j = 0; j < index.Count; j++) {
                if (index[j] > index[i]) {
                    index[j] -= 4;
                }
            }

            for (int j = 0; j < triangles.Count; j += 6) {
                if (index[i] == triangles[j]) {
                    triangles.RemoveAt(j);
                    triangles.RemoveAt(j);
                    triangles.RemoveAt(j);
                    triangles.RemoveAt(j);
                    triangles.RemoveAt(j);
                    triangles.RemoveAt(j);

                    for (int k = 0; k < triangles.Count; k++) {
                        if (triangles[k] >= index[i]) {
                            triangles[k] -= 4;
                        }
                    }
                }
            }
        }

        mesh.triangles = triangles.ToArray();
        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
        transform.GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void AddFaces(Vector3Int position, List<meshDirection> directions) {
        Vector2Int pixel = maps[position].pixel;
        float offset = 1f / maps.Count / 2;
        float u = (float)pixel.x / maps.Count + offset;
        float v = 0 + .5f;

        foreach (meshDirection direction in directions) {
            Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
            List<Vector3> vertices = new List<Vector3>(mesh.vertices);
            List<int> triangles = new List<int>(mesh.triangles);
            List<Vector2> uvs = new List<Vector2>(mesh.uv);
            int numVerts = vertices.Count;

            switch (direction) {
                case meshDirection.posX:
                    vertices.Add(new Vector3(position.x + 1, position.y + 0, position.z + 0));
                    vertices.Add(new Vector3(position.x + 1, position.y + 0, position.z + 1));
                    vertices.Add(new Vector3(position.x + 1, position.y + 1, position.z + 1));
                    vertices.Add(new Vector3(position.x + 1, position.y + 1, position.z + 0));
                    break;

                case meshDirection.negX:
                    vertices.Add(new Vector3(position.x + 0, position.y + 0, position.z + 1));
                    vertices.Add(new Vector3(position.x + 0, position.y + 0, position.z + 0));
                    vertices.Add(new Vector3(position.x + 0, position.y + 1, position.z + 0));
                    vertices.Add(new Vector3(position.x + 0, position.y + 1, position.z + 1));
                    break;

                case meshDirection.posY:
                    vertices.Add(new Vector3(position.x + 0, position.y + 1, position.z + 0));
                    vertices.Add(new Vector3(position.x + 1, position.y + 1, position.z + 0));
                    vertices.Add(new Vector3(position.x + 1, position.y + 1, position.z + 1));
                    vertices.Add(new Vector3(position.x + 0, position.y + 1, position.z + 1));
                    break;

                case meshDirection.negY:
                    vertices.Add(new Vector3(position.x + 0, position.y + 0, position.z + 1));
                    vertices.Add(new Vector3(position.x + 1, position.y + 0, position.z + 1));
                    vertices.Add(new Vector3(position.x + 1, position.y + 0, position.z + 0));
                    vertices.Add(new Vector3(position.x + 0, position.y + 0, position.z + 0));
                    break;

                case meshDirection.posZ:
                    vertices.Add(new Vector3(position.x + 1, position.y + 0, position.z + 1));
                    vertices.Add(new Vector3(position.x + 0, position.y + 0, position.z + 1));
                    vertices.Add(new Vector3(position.x + 0, position.y + 1, position.z + 1));
                    vertices.Add(new Vector3(position.x + 1, position.y + 1, position.z + 1));
                    break;

                case meshDirection.negZ:
                    vertices.Add(new Vector3(position.x + 0, position.y + 0, position.z + 0));
                    vertices.Add(new Vector3(position.x + 1, position.y + 0, position.z + 0));
                    vertices.Add(new Vector3(position.x + 1, position.y + 1, position.z + 0));
                    vertices.Add(new Vector3(position.x + 0, position.y + 1, position.z + 0));
                    break;
            }

            uvs.Add(new Vector2(u, v));
            uvs.Add(new Vector2(u, v));
            uvs.Add(new Vector2(u, v));
            uvs.Add(new Vector2(u, v));

            triangles.Add(numVerts + 0);
            triangles.Add(numVerts + 2);
            triangles.Add(numVerts + 1);
            triangles.Add(numVerts + 0);
            triangles.Add(numVerts + 3);
            triangles.Add(numVerts + 2);

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uvs.ToArray();

            mesh.RecalculateNormals();
            transform.GetComponent<MeshCollider>().sharedMesh = mesh;
        }
    }
}
