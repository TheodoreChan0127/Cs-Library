using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObject/AssetPreprocessor/TexturePreprocessorConfig")]
public class TexturePreprocessorConfig : BasePreprocessorConfig<TexturePreprocessorConfig>
{
    [Header("Default Texture Settings")]
    [SerializeField] private int maxTextureSize = 512;
    [SerializeField] private bool enableReadWrite = false;
    [SerializeField] private bool enableMipmap = false;
    [SerializeField] private bool enableMipMapStreaming = false;
    [SerializeField] private TextureImporterAlphaSource alphaSource = TextureImporterAlphaSource.None;
    [SerializeField] private TextureImporterCompression compression = TextureImporterCompression.CompressedLQ;
        
    [Header("Filtering Settings")]
    [SerializeField] private FilterMode filterMode = FilterMode.Trilinear;
    [SerializeField] private int anisoLevel = 1;

    public static int MaxTextureSize => Instance.maxTextureSize;
    public static bool EnableReadWrite => Instance.enableReadWrite;
    public static bool EnableMipmap => Instance.enableMipmap;

    public static bool EnableMipMapStreaming => Instance.enableMipMapStreaming;

    public static TextureImporterAlphaSource AlphaSource => Instance.alphaSource;

    public static FilterMode FilterMode => Instance.filterMode;

    public static int AnisoLevel => Instance.anisoLevel;

    public static TextureImporterCompression Compression => Instance.compression;
}
