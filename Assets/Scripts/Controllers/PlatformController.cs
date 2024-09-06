using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController
{
    private PlatformView _platformView;
    private Platform _platform;

    public PlatformController(PlatformView platformView, Platform platform)
    {
        _platformView = platformView;
        _platform = platform;
    }

    public void Initialize()
    {
        _platformView.Input.InputEventHandler.AddListener((newPosition) => { _platform.UpdatePosition(newPosition); });
        _platform.OnUpdatePositionEventHandler.AddListener(_platformView.UpdatePosition);
    }
}
