using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropeSegmentPrefab;
    public Rigidbody2D connectedObject;
    List<GameObject> ropeSegments = new List<GameObject>();

    public bool isIncreasing { get; set; }
    public bool isDecreasing { get; set; }
    public float maxRopeSegmentLength = 1.0f;
    public float ropeSpeed = 4f;

    LineRenderer lineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ResetLength();
    }

    public void ResetLength()
    {
        foreach (GameObject segment in ropeSegments)
        {
            Destroy(segment);
        }

        ropeSegments = new List<GameObject>();

        isDecreasing = false;
        isIncreasing = false;
        CreateRopeSegment();
    }

    private void CreateRopeSegment()
    {
        GameObject segment = (GameObject)Instantiate(ropeSegmentPrefab, this.transform.position, Quaternion.identity);
        segment.transform.SetParent(this.transform, true);// Сделать звено потомком объекта this и сохранить его мировые координаты
        Rigidbody2D segmentBody = segment.GetComponent<Rigidbody2D>();
        SpringJoint2D segmentJoint = segment.GetComponent<SpringJoint2D>();

        if (segmentBody == null || segmentJoint == null)
        {
            Debug.LogError("Rope segment body prefab has no " +
                            "Rigidbody2D and/or SpringJoint2D!");
            return;
        }

        ropeSegments.Insert(0, segment);    // Теперь, после всех проверок, можно добавить // новое звено в начало списка звеньев

        if (ropeSegments.Count == 1)
        {
            SpringJoint2D connectedObjectJoint = connectedObject.GetComponent<SpringJoint2D>();
            connectedObjectJoint.connectedBody = segmentBody;

            connectedObjectJoint.distance = 0.1f;
            segmentJoint.distance = maxRopeSegmentLength;
        }
        else
        {
            GameObject nextSegment = ropeSegments[1];
            SpringJoint2D nextSegmentJoint = nextSegment.GetComponent<SpringJoint2D>();
            nextSegmentJoint.connectedBody = segmentBody;
            segmentJoint.distance = 0.0f;//установить начальную длину сочленения нового звена равной 0 - она увеличится автоматически
        }

        segmentJoint.connectedBody = this.GetComponent<Rigidbody2D>();
    }

    void RemoveRopeSegment()
    {
        if (ropeSegments.Count < 2)
            return;
        GameObject topSegment = ropeSegments[0];
        GameObject nextSegment = ropeSegments[1];

        SpringJoint2D nextSegmentJoint = nextSegment.GetComponent<SpringJoint2D>();
        nextSegmentJoint.connectedBody = this.GetComponent<Rigidbody2D>();

        ropeSegments.RemoveAt(0);
        Destroy(topSegment);
    }

    private void Update()
    {
        GameObject topSegment = ropeSegments[0];
        SpringJoint2D topSegmentJoint = topSegment.GetComponent<SpringJoint2D>();

        if (isIncreasing)
        {
            // Веревку нужно удлинить. Если длина сочленения больше или равна максимальной, добавляется новое звено;
            // иначе увеличивается длина сочленения звена.
            if (topSegmentJoint.distance >= maxRopeSegmentLength)
                CreateRopeSegment();
            else
                topSegmentJoint.distance += ropeSpeed * Time.deltaTime;
        }

        if (isDecreasing)
        {
            if (topSegmentJoint.distance <= 0.005f)
                RemoveRopeSegment();
            else
                topSegmentJoint.distance -= ropeSpeed * Time.deltaTime;
        }

        if (lineRenderer != null)
        {
            // Визуализатор LineRenderer рисует линию по коллекции точек. Эти точки должны соответствовать
            // позициям звеньев веревки. Число вершин, отображаемых визуализатором, равно числу звеньев плюс одна точка
            // на верхней опоре плюс одна точка на ноге гномика.
            lineRenderer.positionCount = ropeSegments.Count + 2;

            lineRenderer.SetPosition(0, this.transform.position);

            for (int i = 0; i < ropeSegments.Count; i++)
                lineRenderer.SetPosition(i + 1, ropeSegments[i].transform.position);

            SpringJoint2D connectedObjectJoint = connectedObject.GetComponent<SpringJoint2D>();
            lineRenderer.SetPosition(ropeSegments.Count + 1, connectedObject.transform.TransformPoint(connectedObjectJoint.anchor));
        }
    }
}