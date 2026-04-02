using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace HauntedPSX.RenderPipelines.PSX.Runtime
{
    [SupportedOnRenderPipeline(typeof(PSXRenderPipelineAsset))]
    [Serializable]
    public class PSXRenderPipelineGlobalSettings : RenderPipelineGlobalSettings<PSXRenderPipelineGlobalSettings, PSXRenderPipeline>
    {
        internal static string defaultPath => "Assets/PSXRenderPipelineGlobalSettings.asset";

        [SerializeField] private List<IRenderPipelineGraphicsSettings> m_Settings = new();
        protected override List<IRenderPipelineGraphicsSettings> settingsList => m_Settings;

        public static PSXRenderPipelineGlobalSettings Ensure(bool canCreateNewAsset = true)
        {
            PSXRenderPipelineGlobalSettings currentInstance = instance;
                
            return instance;
        }
    }
}

