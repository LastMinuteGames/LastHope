using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://forum.unity3d.com/threads/drawline-with-gl.235793/
//https://pastebin.com/69QP1s45
//https://docs.unity3d.com/ScriptReference/LineRenderer.html
//https://www.assetstore.unity3d.com/en/#!/content/10962
//http://answers.unity3d.com/questions/712123/draw-borders-cube.html

public class ShowMeshBounds : MonoBehaviour
{
    public Color color = Color.green;

    private Vector3 v3FrontTopLeft;
    private Vector3 v3FrontTopRight;
    private Vector3 v3FrontBottomLeft;
    private Vector3 v3FrontBottomRight;
    private Vector3 v3BackTopLeft;
    private Vector3 v3BackTopRight;
    private Vector3 v3BackBottomLeft;
    private Vector3 v3BackBottomRight;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        CalcPositons();
        DrawBox();
    }

    void CalcPositons()
    {
        Bounds bounds = GetComponent<MeshFilter>().mesh.bounds;

        //Bounds bounds;
        //BoxCollider bc = GetComponent<BoxCollider>();
        //if (bc != null)
        //    bounds = bc.bounds;
        //else
        //return;

        Vector3 v3Center = bounds.center;
        Vector3 v3Extents = bounds.extents;

        v3FrontTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top left corner
        v3FrontTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z - v3Extents.z);  // Front top right corner
        v3FrontBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom left corner
        v3FrontBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z - v3Extents.z);  // Front bottom right corner
        v3BackTopLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top left corner
        v3BackTopRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y + v3Extents.y, v3Center.z + v3Extents.z);  // Back top right corner
        v3BackBottomLeft = new Vector3(v3Center.x - v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom left corner
        v3BackBottomRight = new Vector3(v3Center.x + v3Extents.x, v3Center.y - v3Extents.y, v3Center.z + v3Extents.z);  // Back bottom right corner

        v3FrontTopLeft = transform.TransformPoint(v3FrontTopLeft);
        v3FrontTopRight = transform.TransformPoint(v3FrontTopRight);
        v3FrontBottomLeft = transform.TransformPoint(v3FrontBottomLeft);
        v3FrontBottomRight = transform.TransformPoint(v3FrontBottomRight);
        v3BackTopLeft = transform.TransformPoint(v3BackTopLeft);
        v3BackTopRight = transform.TransformPoint(v3BackTopRight);
        v3BackBottomLeft = transform.TransformPoint(v3BackBottomLeft);
        v3BackBottomRight = transform.TransformPoint(v3BackBottomRight);
    }

    void DrawBox()
    {
        //if (Input.GetKey (KeyCode.S)) {

        /*GLDebug debugLine = new GLDebug();
        debugLine.DrawLine(v3FrontTopLeft, v3FrontTopRight, color,1,false);
        debugLine.DrawLine(v3FrontTopRight, v3FrontBottomRight, color, 1, false);
        debugLine.DrawLine(v3FrontBottomRight, v3FrontBottomLeft, color, 1, false);
        debugLine.DrawLine(v3FrontBottomLeft, v3FrontTopLeft, color, 1, false);

        debugLine.DrawLine(v3BackTopLeft, v3BackTopRight, color, 1, false);
        debugLine.DrawLine(v3BackTopRight, v3BackBottomRight, color, 1, false);
        debugLine.DrawLine(v3BackBottomRight, v3BackBottomLeft, color, 1, false);
        debugLine.DrawLine(v3BackBottomLeft, v3BackTopLeft, color, 1, false);

        debugLine.DrawLine(v3FrontTopLeft, v3BackTopLeft, color, 1, false);
        debugLine.DrawLine(v3FrontTopRight, v3BackTopRight, color, 1, false);
        debugLine.DrawLine(v3FrontBottomRight, v3BackBottomRight, color, 1, false);
        debugLine.DrawLine(v3FrontBottomLeft, v3BackBottomLeft, color, 1, false);

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = 1;
        lineRenderer.endWidth = 1;
        lineRenderer.numPositions = 8;

        Vector3[] points = new Vector3[8];
        points[0] = v3FrontTopLeft;
        points[1] = v3FrontTopRight;
        points[2] = v3FrontBottomLeft;
        points[3] = v3FrontBottomRight;
        points[4] = v3BackTopLeft;
        points[5] = v3BackTopRight;
        points[6] = v3BackBottomLeft;
        points[7] = v3BackBottomRight;

        lineRenderer.SetPositions(points);*/
        
        Debug.DrawLine(v3FrontTopLeft, v3FrontTopRight, color);
        Debug.DrawLine(v3FrontTopRight, v3FrontBottomRight, color);
        Debug.DrawLine(v3FrontBottomRight, v3FrontBottomLeft, color);
        Debug.DrawLine(v3FrontBottomLeft, v3FrontTopLeft, color);

        Debug.DrawLine(v3BackTopLeft, v3BackTopRight, color);
        Debug.DrawLine(v3BackTopRight, v3BackBottomRight, color);
        Debug.DrawLine(v3BackBottomRight, v3BackBottomLeft, color);
        Debug.DrawLine(v3BackBottomLeft, v3BackTopLeft, color);

        Debug.DrawLine(v3FrontTopLeft, v3BackTopLeft, color);
        Debug.DrawLine(v3FrontTopRight, v3BackTopRight, color);
        Debug.DrawLine(v3FrontBottomRight, v3BackBottomRight, color);
        Debug.DrawLine(v3FrontBottomLeft, v3BackBottomLeft, color);
        //}
    }

}
