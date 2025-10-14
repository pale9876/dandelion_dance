using Godot;
using SmartFormat;
using System;
using SmartFormat.Extensions;

[Tool]
public partial class CSTransceiver : Node
{

    // public Timer gc_alert_timer = new Timer();

    public CSTransceiver()
    {
        Smart.Default.AddExtensions(new KoreanFormatter(Smart.Default));
        GD.Print("Smart.Default => Init KoreanFormatter");
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        // if (what == Notification)

    }

    // public override void _EnterTree()
    // {
    //     base._EnterTree();
    //     AddChild(gc_alert_timer);
    //     gc_alert_timer.Timeout += timeout;
    //     gc_alert_timer.WaitTime = 240.0;
    //     gc_alert_timer.Autostart = true;
    //     gc_alert_timer.OneShot = false;
    //     gc_alert_timer.Start();
    // }

    // public override void _ExitTree()
    // {
    //     base._ExitTree();
    //     gc_alert_timer.Timeout -= timeout;
    //     gc_alert_timer.QueueFree();
    // }


    // private void timeout()
    // {
    //     if (Engine.IsEditorHint())
    //     {
    //         GD.Print($" GC => {System.GC.GetTotalMemory(false)}");
    //         GC.Collect(0, GCCollectionMode.Optimized);
    //         GD.Print("! => GC Collected");
    //     }
    // }

}
