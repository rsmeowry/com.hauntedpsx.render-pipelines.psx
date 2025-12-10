using UnityEngine;
using UnityEditor;

namespace HauntedPSX.RenderPipelines.PSX.Editor
{
    /// <summary>
    /// Custom Camera Editor for PSX Render Pipeline that prevents the 
    /// "Camera.GetCommandBuffers only works with built-in renderer" error
    /// by providing a custom inspector that doesn't call BIRP-only APIs.
    /// </summary>
    [CustomEditor(typeof(Camera))]
    [CanEditMultipleObjects]
    public class PSXCameraEditor : UnityEditor.Editor
    {
        // Serialized properties
        private SerializedProperty clearFlags;
        private SerializedProperty backgroundColor;
        private SerializedProperty cullingMask;
        private SerializedProperty projectionMatrixMode;
        private SerializedProperty fieldOfView;
        private SerializedProperty orthographic;
        private SerializedProperty orthographicSize;
        private SerializedProperty depth;
        private SerializedProperty renderingPath;
        private SerializedProperty targetTexture;
        private SerializedProperty occlusionCulling;
        private SerializedProperty allowHDR;
        private SerializedProperty allowMSAA;
        private SerializedProperty allowDynamicResolution;
        private SerializedProperty targetDisplay;
        private SerializedProperty nearClipPlane;
        private SerializedProperty farClipPlane;
        private SerializedProperty rect;
        private SerializedProperty usePhysicalProperties;
        private SerializedProperty sensorSize;
        private SerializedProperty lensShift;
        private SerializedProperty focalLength;
        private SerializedProperty gateFit;

        private void OnEnable()
        {
            clearFlags = serializedObject.FindProperty("m_ClearFlags");
            backgroundColor = serializedObject.FindProperty("m_BackGroundColor");
            cullingMask = serializedObject.FindProperty("m_CullingMask");
            projectionMatrixMode = serializedObject.FindProperty("m_projectionMatrixMode");
            fieldOfView = serializedObject.FindProperty("field of view");
            orthographic = serializedObject.FindProperty("orthographic");
            orthographicSize = serializedObject.FindProperty("orthographic size");
            depth = serializedObject.FindProperty("m_Depth");
            renderingPath = serializedObject.FindProperty("m_RenderingPath");
            targetTexture = serializedObject.FindProperty("m_TargetTexture");
            occlusionCulling = serializedObject.FindProperty("m_OcclusionCulling");
            allowHDR = serializedObject.FindProperty("m_HDR");
            allowMSAA = serializedObject.FindProperty("m_AllowMSAA");
            allowDynamicResolution = serializedObject.FindProperty("m_AllowDynamicResolution");
            targetDisplay = serializedObject.FindProperty("m_TargetDisplay");
            nearClipPlane = serializedObject.FindProperty("near clip plane");
            farClipPlane = serializedObject.FindProperty("far clip plane");
            rect = serializedObject.FindProperty("m_NormalizedViewPortRect");
            usePhysicalProperties = serializedObject.FindProperty("m_UsePhysicalProperties");
            sensorSize = serializedObject.FindProperty("m_SensorSize");
            lensShift = serializedObject.FindProperty("m_LensShift");
            focalLength = serializedObject.FindProperty("m_FocalLength");
            gateFit = serializedObject.FindProperty("m_GateFitMode");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Clear Flags
            EditorGUILayout.PropertyField(clearFlags);

            // Background Color (only if clear flags is set to solid color)
            if (clearFlags.intValue == (int)CameraClearFlags.SolidColor)
            {
                EditorGUILayout.PropertyField(backgroundColor, new GUIContent("Background"));
            }

            // Culling Mask
            EditorGUILayout.PropertyField(cullingMask);

            EditorGUILayout.Space();

            // Projection
            EditorGUILayout.PropertyField(projectionMatrixMode, new GUIContent("Projection"));
            
            if (projectionMatrixMode.intValue == 0) // Explicit mode
            {
                // Show projection type
                EditorGUILayout.PropertyField(orthographic);
                
                if (orthographic.boolValue)
                {
                    EditorGUILayout.PropertyField(orthographicSize, new GUIContent("Size"));
                }
                else
                {
                    // Physical Camera Properties
                    EditorGUILayout.PropertyField(usePhysicalProperties, new GUIContent("Physical Camera"));
                    
                    if (usePhysicalProperties.boolValue)
                    {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(focalLength, new GUIContent("Focal Length"));
                        EditorGUILayout.PropertyField(sensorSize, new GUIContent("Sensor Size"));
                        EditorGUILayout.PropertyField(lensShift, new GUIContent("Lens Shift"));
                        EditorGUILayout.PropertyField(gateFit, new GUIContent("Gate Fit"));
                        EditorGUI.indentLevel--;
                        
                        // Calculate FOV from physical properties
                        Camera cam = target as Camera;
                        if (cam != null)
                        {
                            EditorGUI.BeginDisabledGroup(true);
                            EditorGUILayout.FloatField("Field of View", cam.fieldOfView);
                            EditorGUI.EndDisabledGroup();
                        }
                    }
                    else
                    {
                        EditorGUILayout.PropertyField(fieldOfView, new GUIContent("Field of View"));
                    }
                }
            }

            // Clipping Planes
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Clipping Planes", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(nearClipPlane, new GUIContent("Near"));
            EditorGUILayout.PropertyField(farClipPlane, new GUIContent("Far"));
            EditorGUI.indentLevel--;

            // Viewport Rect
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(rect, new GUIContent("Viewport Rect"));

            // Depth
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(depth);

            // Rendering Path
            EditorGUILayout.PropertyField(renderingPath, new GUIContent("Rendering Path"));

            // Target Texture
            EditorGUILayout.PropertyField(targetTexture, new GUIContent("Target Texture"));

            // Occlusion Culling
            EditorGUILayout.PropertyField(occlusionCulling, new GUIContent("Occlusion Culling"));

            // HDR
            EditorGUILayout.PropertyField(allowHDR, new GUIContent("Allow HDR"));

            // MSAA
            EditorGUILayout.PropertyField(allowMSAA, new GUIContent("Allow MSAA"));

            // Dynamic Resolution
            EditorGUILayout.PropertyField(allowDynamicResolution, new GUIContent("Allow Dynamic Resolution"));

            // Target Display
            EditorGUILayout.PropertyField(targetDisplay, new GUIContent("Target Display"));

            serializedObject.ApplyModifiedProperties();
        }
    }
}

