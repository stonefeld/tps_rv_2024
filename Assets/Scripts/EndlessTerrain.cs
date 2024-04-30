using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    public const float maxViewDistance = 450;
    public Transform viewer;
    public static Vector2 viewerPosition;
    int chunckSize;
    int chuncksVisibleInDist;
    Dictionary<Vector2, TerrainChunck> terrainChunckDictionary = new Dictionary<Vector2, TerrainChunck>();
    List<TerrainChunck> terrainChunckVisibleLastUpdate = new List<TerrainChunck>();

    void Start()
    {
        chunckSize = MapGenerator.mapChunkSize - 1;
        chuncksVisibleInDist = Mathf.RoundToInt(maxViewDistance / chunckSize);
    }

    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChuncks();
    }

    void UpdateVisibleChuncks()
    {
        for (int i = 0; i < terrainChunckVisibleLastUpdate.Count; i++)
        {
            terrainChunckVisibleLastUpdate[i].SetVisible(false);
        }

        terrainChunckVisibleLastUpdate.Clear();

        int currentChunckCoordX = Mathf.RoundToInt(viewerPosition.x / chunckSize);
        int currentChunckCoordY = Mathf.RoundToInt(viewerPosition.y / chunckSize);

        for (int yOffset = -chuncksVisibleInDist; yOffset < chuncksVisibleInDist; yOffset++)
        {
            for (int xOffset = -chuncksVisibleInDist; xOffset < chuncksVisibleInDist; xOffset++)
            {
                Vector2 viewedChunckCoord = new Vector2(currentChunckCoordX + xOffset, currentChunckCoordY + yOffset);
                if (terrainChunckDictionary.ContainsKey(viewedChunckCoord))
                {
                    terrainChunckDictionary[viewedChunckCoord].UpdateTerrainChunck();

                    if (terrainChunckDictionary[viewedChunckCoord].IsVisible())
                    {
                        terrainChunckVisibleLastUpdate.Add(terrainChunckDictionary[viewedChunckCoord]);
                    }
                }
                else
                {
                    terrainChunckDictionary.Add(viewedChunckCoord, new TerrainChunck(viewedChunckCoord, chunckSize, transform));
                }
            }
        }
    }

    public class TerrainChunck
    {
        Vector2 position;
        GameObject meshObject;
        Bounds bounds;

        public TerrainChunck(Vector2 coord, int size, Transform parent)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionv3 = new Vector3(position.x, 0, position.y);

            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.transform.position = positionv3;
            meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;

            SetVisible(false);
        }

        public void UpdateTerrainChunck()
        {
            float viewerDistFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visible = viewerDistFromNearestEdge <= maxViewDistance;
            SetVisible(visible);
        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }
    }
}
