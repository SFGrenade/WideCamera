using System.Reflection;
using JetBrains.Annotations;
using SFCore.Generics;
using SFCore.Utils;
using UnityEngine;

namespace WideCamera;

public class WideCameraSettings
{
    public bool ScaleHUD = false;
}

[UsedImplicitly]
public class WideCamera : GlobalSettingsMod<WideCameraSettings>
{
    public WideCamera() : base("Wide Camera")
    {
        On.ForceCameraAspect.Awake += (orig, self) => { Object.DestroyImmediate(self); };
        On.ForceCameraAspectLite.Start += (orig, self) => { Object.DestroyImmediate(self); };
        On.tk2dCamera.OnEnable += (orig, self) =>
        {
            orig(self);
            ChangeCam(self);
        };
        foreach (var camera in Resources.FindObjectsOfTypeAll<tk2dCamera>())
        {
            ChangeCam(camera);
        }
    }

    /// <summary>
    /// Displays the version.
    /// </summary>
    public override string GetVersion() => Util.GetVersion(Assembly.GetExecutingAssembly());

    private Vector3 gameCamerasOrigScale;

    public override void Initialize()
    {
        Log("Initializing");

        if (GlobalSettings.ScaleHUD)
        {
            gameCamerasOrigScale = GameCameras.instance.hudCamera.gameObject.transform.localScale;

            On.GameManager.Update += (orig, self) =>
            {
                orig(self);
                Vector3 newScale = gameCamerasOrigScale;
                newScale.x = newScale.x * ((((float)Screen.width) / ((float)Screen.height)) / (16.0f / 9.0f));
                GameCameras.instance.hudCamera.gameObject.transform.localScale = newScale;
            };
        }

        Log("Initialized");
    }

    private static void ChangeCam(tk2dCamera camera)
    {
        //camera.nativeResolutionWidth = (int) (1080.0f / ((float) GlobalSettings.AspectRatioDenominator) * ((float) GlobalSettings.AspectRatioNumerator));
        //camera.nativeResolutionHeight = 1080;
        //camera.forceResolution = new Vector2(720.0f / ((float) GlobalSettings.AspectRatioDenominator) * ((float) GlobalSettings.AspectRatioNumerator), 720);
        ////camera.CameraSettings.fieldOfView = 60;
        //camera.CameraSettings.rect = new Rect(0, 0, 1, 1);
        camera.resolutionOverride = new tk2dCameraResolutionOverride[]
        {
            new()
            {
                name = "1:1",
                matchBy = tk2dCameraResolutionOverride.MatchByType.AspectRatio, width = 0, height = 0,
                aspectRatioNumerator = 1,
                aspectRatioDenominator = 1,
                scale = 1, autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible, fitMode = tk2dCameraResolutionOverride.FitMode.Center
            },
            new()
            {
                name = "3:2",
                matchBy = tk2dCameraResolutionOverride.MatchByType.AspectRatio, width = 0, height = 0,
                aspectRatioNumerator = 3,
                aspectRatioDenominator = 2,
                scale = 1, autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible, fitMode = tk2dCameraResolutionOverride.FitMode.Center
            },
            new()
            {
                name = "16:10",
                matchBy = tk2dCameraResolutionOverride.MatchByType.AspectRatio, width = 0, height = 0,
                aspectRatioNumerator = 16,
                aspectRatioDenominator = 10,
                scale = 1, autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible, fitMode = tk2dCameraResolutionOverride.FitMode.Center
            },
            new()
            {
                name = "16:9",
                matchBy = tk2dCameraResolutionOverride.MatchByType.AspectRatio, width = 0, height = 0,
                aspectRatioNumerator = 16,
                aspectRatioDenominator = 9,
                scale = 1, autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible, fitMode = tk2dCameraResolutionOverride.FitMode.Center
            },
            new()
            {
                name = "21:9",
                matchBy = tk2dCameraResolutionOverride.MatchByType.AspectRatio, width = 0, height = 0,
                aspectRatioNumerator = 21,
                aspectRatioDenominator = 9,
                scale = 1, autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible, fitMode = tk2dCameraResolutionOverride.FitMode.Center
            },
            new()
            {
                name = "24:10",
                matchBy = tk2dCameraResolutionOverride.MatchByType.AspectRatio, width = 0, height = 0,
                aspectRatioNumerator = 24,
                aspectRatioDenominator = 10,
                scale = 1, autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible, fitMode = tk2dCameraResolutionOverride.FitMode.Center
            },
            new()
            {
                name = "32:9",
                matchBy = tk2dCameraResolutionOverride.MatchByType.AspectRatio, width = 0, height = 0,
                aspectRatioNumerator = 32,
                aspectRatioDenominator = 9,
                scale = 1, autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible, fitMode = tk2dCameraResolutionOverride.FitMode.Center
            },
            new()
            {
                name = "48:9",
                matchBy = tk2dCameraResolutionOverride.MatchByType.AspectRatio, width = 0, height = 0,
                aspectRatioNumerator = 48,
                aspectRatioDenominator = 9,
                scale = 1, autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible, fitMode = tk2dCameraResolutionOverride.FitMode.Center
            },
            new()
            {
                name = "Override",
                matchBy = tk2dCameraResolutionOverride.MatchByType.Wildcard, width = 0, height = 0,
                aspectRatioNumerator = 4,
                aspectRatioDenominator = 3,
                scale = 1, autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible, fitMode = tk2dCameraResolutionOverride.FitMode.Center
            },
        };
    }
}