using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(FaceCutoutsData))]
public class CollectTrisEditor : Editor
{
    /*public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        ShowInInspectorAttributeDrawer.showMethodsInInspector(targets);
    }*/

    // needs Gizmos enabled in Scene view
    void OnSceneGUI() {
        var e = Event.current;
        if (e.type == EventType.MouseDown) {
            // Debug.Log("CollectTrisEditor EventType.TouchDown");

            Ray ray = HandleUtility.GUIPointToWorldRay (Event.current.mousePosition);
            if (Physics.Raycast(ray, out var hit)) {
                var i = hit.triangleIndex;
                // Debug.Log(i);

                var mesh = (hit.collider as MeshCollider).sharedMesh;
                Vector3[] vertices = mesh.vertices;
                int[] triangles = mesh.triangles;
                Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
                Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
                Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];
                Transform hitTransform = hit.collider.transform;
                p0 = hitTransform.TransformPoint(p0);
                p1 = hitTransform.TransformPoint(p1);
                p2 = hitTransform.TransformPoint(p2);
                Debug.DrawLine(p0, p1, Color.white, 10);
                Debug.DrawLine(p1, p2, Color.white, 10);
                Debug.DrawLine(p2, p0, Color.white, 10);
                (target as FaceCutoutsData).tryAddTri(i);
            }
        }
    }
}
