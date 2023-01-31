using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class MovingObstaclePreview 
{
    static public MovingObstaclePreview s_Preview = null;
    static public GameObject preview;

    static protected MovingObstacles movingPlatform;

    static MovingObstaclePreview()
    {
        Selection.selectionChanged += SelectionChanged;
    }

    static void SelectionChanged()
    {
        if (movingPlatform != null && Selection.activeGameObject != movingPlatform.gameObject)
        {
            DestroyPreview();
        }
    }

    static public void DestroyPreview()
    {
        if (preview == null)
            return;

        Object.DestroyImmediate(preview);
        preview = null;
        movingPlatform = null;
    }

    static public void CreateNewPreview(MovingObstacles origin)
    {
        if(preview != null)
        {
            Object.DestroyImmediate(preview);
        }

        movingPlatform = origin; 

        preview = Object.Instantiate(origin.gameObject);
        preview.hideFlags = HideFlags.DontSave;
        MovingObstacles plt = preview.GetComponentInChildren<MovingObstacles>();
        Object.DestroyImmediate(plt);


        Color c = new Color(0.2f, 0.2f, 0.2f, 0.4f);
        SpriteRenderer[] rends = preview.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < rends.Length; ++i)
            rends[i].color = c;
    }
}