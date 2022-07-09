using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererTest : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawLine(Vector3 start, Vector3 end)
    {
        _lineRenderer = GetComponent<LineRenderer>();
                
        _lineRenderer.SetColors(Color.red, Color.yellow);
        _lineRenderer.SetWidth(0.03f, 0.03f);

        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);
        
        Invoke("Delete", 0.03f);
    }

    void Delete()
    {
        Destroy(gameObject);
    }
}
