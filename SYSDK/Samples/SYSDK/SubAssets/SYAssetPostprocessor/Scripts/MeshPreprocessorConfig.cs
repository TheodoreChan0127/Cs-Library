using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName="ScriptableObject/AssetPreprocessor/MeshPreprocessorConfig")]
public class MeshPreprocessorConfig : BasePreprocessorConfig<MeshPreprocessorConfig>
{
    [Header("Default Mesh Settings")]
    [SerializeField] private bool importBlendShapes = false;
    [SerializeField] private bool importVisibility = false;
    [SerializeField] private bool importCameras = false;
    [SerializeField] private bool importLights = false;
    [SerializeField] private bool preserveHierarchy = false;
    [SerializeField] private ModelImporterMeshCompression meshCompression = ModelImporterMeshCompression.Low;
    [SerializeField] private bool readWriteEnable = false;
    [SerializeField] private bool generateColliders = false;
    [SerializeField] private ModelImporterNormals normals = ModelImporterNormals.None;
    [SerializeField] private ModelImporterMaterialImportMode materialImportMode = ModelImporterMaterialImportMode.None;

    public static bool ImportBlendShapes => Instance.importBlendShapes;

    public static bool ImportVisibility => Instance.importVisibility;

    public static bool ImportCameras => Instance.importCameras;

    public static bool ImportLights => Instance.importLights;

    public static bool PreserveHierarchy => Instance.preserveHierarchy;

    public static ModelImporterMeshCompression MeshCompression => Instance.meshCompression;

    public static bool ReadWriteEnable => Instance.readWriteEnable;

    public static bool GenerateColliders => Instance.generateColliders;
    

    public static ModelImporterNormals Normals => Instance.normals;

    public static ModelImporterMaterialImportMode MaterialImportMode => Instance.materialImportMode;
}