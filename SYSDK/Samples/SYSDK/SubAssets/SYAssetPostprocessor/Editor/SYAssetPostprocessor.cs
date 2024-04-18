using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SYAssetPostprocessor : AssetPostprocessor
{
    private void OnPostprocessTexture(Texture2D texture)
    {
        TextureImporter impor = this.assetImporter as TextureImporter;

        if (!impor || !TexturePreprocessorConfig.Instance)
        {
            return;
        }
        Debug.Log ("OnPreProcessTexture="+this.assetPath);
        
        impor.maxTextureSize = TexturePreprocessorConfig.MaxTextureSize;
        impor.isReadable = TexturePreprocessorConfig.EnableReadWrite;
        
        //Sprite和UI不需要Mipmap，自动屏蔽
        if (impor.textureType == TextureImporterType.Sprite || impor.textureType == TextureImporterType.GUI)
        {
            impor.mipmapEnabled = false;
        }
        else
        {
            impor.mipmapEnabled = TexturePreprocessorConfig.EnableMipmap;
        }
        
        impor.streamingMipmaps = TexturePreprocessorConfig.EnableMipMapStreaming;
        impor.alphaSource = TexturePreprocessorConfig.AlphaSource;
        impor.filterMode = TexturePreprocessorConfig.FilterMode;
        impor.anisoLevel = TexturePreprocessorConfig.AnisoLevel;
        impor.textureCompression = TexturePreprocessorConfig.Compression;
    }

    private void OnPostprocessModel(GameObject g)
    {
        ModelImporter impor = this.assetImporter as ModelImporter;

        if (!impor || !TexturePreprocessorConfig.Instance)
        {
            return;
        }
        Debug.Log ("OnPreProcessModel="+this.assetPath);

        impor.importBlendShapes = MeshPreprocessorConfig.ImportBlendShapes;
        impor.importVisibility = MeshPreprocessorConfig.ImportVisibility;
        impor.importCameras = MeshPreprocessorConfig.ImportCameras;
        impor.importLights = MeshPreprocessorConfig.ImportLights;
        impor.preserveHierarchy = MeshPreprocessorConfig.PreserveHierarchy;
        impor.meshCompression = MeshPreprocessorConfig.MeshCompression;
        impor.importNormals = MeshPreprocessorConfig.Normals;
        impor.isReadable = MeshPreprocessorConfig.ReadWriteEnable;
        impor.addCollider = MeshPreprocessorConfig.GenerateColliders;
        impor.importBlendShapeNormals = MeshPreprocessorConfig.Normals;
        impor.materialImportMode = MeshPreprocessorConfig.MaterialImportMode;
    }
}
