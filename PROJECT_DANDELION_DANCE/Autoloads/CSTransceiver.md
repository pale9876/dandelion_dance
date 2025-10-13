---

---
---
### [TOOL]
##### 외부 CS 라이브러리를 생성시키기 위한 클래스입니다. 루트씬트리에서 가장 먼저 위치하게 됩니다.

런타임 또는 에디터 실행 시에 먼저 생성하여 메모리에 위치해야 하는 객체들에 한정합니다.
##### 사용 중인 외부 라이브러리는 다음과 같습니다.
- SmartFormat.KoreanFormatter




``` CS
using Godot;
using SmartFormat;
using System;
using SmartFormat.Extensions;

[Tool]

public partial class CSTransceiver : Node
{
    public Timer gc_alert_timer = new Timer();
    
    public CSTransceiver()
    {
        Smart.Default.AddExtensions(new KoreanFormatter(Smart.Default));
        GD.Print("Smart.Default => Init KoreanFormatter");
    }
    
    public override void _EnterTree()
    {

        base._EnterTree();

        AddChild(gc_alert_timer);

        gc_alert_timer.Timeout += timeout;

        gc_alert_timer.WaitTime = 240.0;

        gc_alert_timer.Autostart = true;

        gc_alert_timer.OneShot = false;

        gc_alert_timer.Start();
    }

    public override void _ExitTree()

    {

        base._ExitTree();

        gc_alert_timer.Timeout -= timeout;

        gc_alert_timer.QueueFree();

    }

    private void timeout()
    {
        if (Engine.IsEditorHint())
        {
            GD.Print($" GC => {System.GC.GetTotalMemory(false)}");

            GC.Collect(0, GCCollectionMode.Optimized);

            GD.Print("! => GC Collected");
        }
    }
}

```