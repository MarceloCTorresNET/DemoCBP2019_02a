using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Xamarin.Forms;
using Prism.Behaviors;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace DemoCBP2019_02a.Behaviors
{
  public class SetMapViewViewpointBehavior : BehaviorBase<MapView>
  {
    /// <summary>
    /// 
    /// </summary>
    public static readonly BindableProperty ViewpointProperty =
      BindableProperty
        .Create(nameof(Viewpoint),
                typeof(Viewpoint),
                typeof(SetMapViewViewpointBehavior));

    /// <summary>
    /// 
    /// </summary>
    public Viewpoint Viewpoint
    {
      get => (Viewpoint)GetValue(ViewpointProperty);
      set => SetValue(ViewpointProperty, value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      base.OnPropertyChanged(propertyName);
      if (propertyName == nameof(Viewpoint))
      {
        SetViewpoint(Viewpoint);
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="viewPoint"></param>
    private async void SetViewpoint(Viewpoint viewPoint)
    {
      if (viewPoint != null)
      {
        var actualViewpoint = AssociatedObject.GetCurrentViewpoint(ViewpointType.BoundingGeometry);
        if (actualViewpoint == null || (actualViewpoint != null && !actualViewpoint.Equals(viewPoint)))
        {
          await AssociatedObject.SetViewpointAsync(viewPoint);
        }
      }
    }

  }
}
