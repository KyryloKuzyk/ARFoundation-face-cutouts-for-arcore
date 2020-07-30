using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;


public class MousePoints : MonoBehaviour {
    [SerializeField] ARFace face = null;
    [FormerlySerializedAs("indices")] [SerializeField] int[] mousePointsIndices = new[] {1, 14, 78, 292};
    [SerializeField] float pointerScale = 0.01f;
    [CanBeNull] [SerializeField] GameObject optionalPointerPrefab = null;

    readonly Dictionary<int, Transform> pointers = new Dictionary<int, Transform>(); 


    void Awake() {
        face.updated += delegate {
            for (var i = 0; i < mousePointsIndices.Length; i++) {
                var vertexIndex = mousePointsIndices[i];
                var pointer = getPointer(i);
                pointer.position = face.transform.TransformPoint(face.vertices[vertexIndex]);
            }
        };
    }
    
    Transform getPointer(int id) {
        if (pointers.TryGetValue(id, out var existing)) {
            return existing;
        } else {
            var newPointer = createNewPointer();
            pointers[id] = newPointer;
            return newPointer;
        }
    }
        
    Transform createNewPointer() {
        var result = instantiatePointer();
        return result;
    }

    Transform instantiatePointer() {
        if (optionalPointerPrefab != null) {
            return Instantiate(optionalPointerPrefab).transform;
        } else {
            var result = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            result.localScale = Vector3.one * pointerScale;
            return result;
        }
    }
}
