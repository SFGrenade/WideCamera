using System.Reflection;
using Modding;
using JetBrains.Annotations;
using MonoMod.RuntimeDetour;
using SFCore.Generics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WideCamera;

public class WideCameraSettings
{
    public int AspectRatioNumerator = 16;
    public int AspectRatioDenominator = 9;
}

[UsedImplicitly]
public class WideCamera : GlobalSettingsMod<WideCameraSettings>
{
    public WideCamera() : base("Wide Camera")
    {
        On.ForceCameraAspect.Awake += (orig, self) =>
        {
            Object.DestroyImmediate(self);
        };
        On.ForceCameraAspectLite.Start += (orig, self) =>
        {
            Object.DestroyImmediate(self);
        };
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

    public override void Initialize()
    {
        Log("Initializing");

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
                name = "Override",
                matchBy = tk2dCameraResolutionOverride.MatchByType.Wildcard, width = 0, height = 0,
                aspectRatioNumerator = 4,
                aspectRatioDenominator = 3,
                scale = 1, autoScaleMode = tk2dCameraResolutionOverride.AutoScaleMode.FitVisible, fitMode = tk2dCameraResolutionOverride.FitMode.Center
            },
        };
    }
}