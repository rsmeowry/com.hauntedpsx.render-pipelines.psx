using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using UnityEditor.Rendering;
using HauntedPSX.RenderPipelines.PSX.Runtime;

namespace HauntedPSX.RenderPipelines.PSX.Editor
{
    [InitializeOnLoad]
    static class PSXRenderPipelineGlobalSettingsCreator
    {
        static PSXRenderPipelineGlobalSettingsCreator()
        {
            EditorApplication.delayCall += EnsureGlobalSettings;
        }

        static void EnsureGlobalSettings()
        {
            // Only ensure global settings if the PSX render pipeline is active
            if (GraphicsSettings.currentRenderPipeline is PSXRenderPipelineAsset)
            {
                // Ensure global settings asset exists
                var currentSettings = EditorGraphicsSettings.GetRenderPipelineGlobalSettingsAsset<PSXRenderPipeline>();
                if (currentSettings == null)
                {
                    PSXRenderPipelineGlobalSettings.Ensure(canCreateNewAsset: true);
                }
                
                // Initialize VolumeManager if not already initialized (needed for Editor volume component list)
                if (!VolumeManager.instance.isInitialized)
                {
                    VolumeManager.instance.Initialize();
                }
            }
        }
    }
}

