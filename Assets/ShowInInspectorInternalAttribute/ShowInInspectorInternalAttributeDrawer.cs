/*
#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;


namespace ARFoundationRemote.Editor {
    [CustomEditor(typeof(Component), true), CanEditMultipleObjects]
    public class ShowInInspectorAttributeDrawer : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            showMethodsInInspector(targets);
        }

        static Action callback;
        
        public static void showMethodsInInspector(params Object[] targets) {
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.Static;
            var methods = targets.First().GetType().GetMethods(flags).Where(m => m.GetCustomAttributes(typeof(ShowInInspectorInternalAttribute)).Any() && m.GetParameters().Length == 0);
            foreach (var methodInfo in methods) {
                if (GUILayout.Button(methodInfo.Name, new GUIStyle(GUI.skin.button))) {
                    callback = () => {
                        foreach (var target in targets) {
                            methodInfo.Invoke(target, null);
                        }
                    };
                    EditorApplication.update += update;
                    return;
                }
            }
        }

        static void update() {
            EditorApplication.update -= update;            
            Assert.IsNotNull(callback);
            callback();
        }
    }
}
#endif // UNITY_EDITOR
*/
