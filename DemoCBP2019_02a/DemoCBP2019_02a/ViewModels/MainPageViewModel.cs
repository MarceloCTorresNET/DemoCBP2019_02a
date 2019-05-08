using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

#if WINDOWS_UWP
using Colors = Windows.UI.Colors;
#else
using Colors = System.Drawing.Color;
#endif

namespace DemoCBP2019_02a.ViewModels
{
  public class MainPageViewModel : ViewModelBase
  {
    private Map _map;
    private GraphicsOverlayCollection _graphicOverlyaCollection;
    private Viewpoint _newViewpoint;


    public Map Map
    {
      get => _map;
      set => SetProperty(ref _map, value);
    }

    public GraphicsOverlayCollection GraphicsOverlayCollection
    {
      get => _graphicOverlyaCollection;
      set => SetProperty(ref _graphicOverlyaCollection, value);
    }

    public Viewpoint NewViewpoint
    {
      get => _newViewpoint;
      set => SetProperty(ref _newViewpoint, value);
    }

    public ICommand AddGraphicsCommand { get; private set; }

    public ICommand CenterMapCommand { get; private set; }

    public ICommand ClearEventsCommand { get; private set; }

    public MainPageViewModel(INavigationService navigationService)
        : base(navigationService)
    {
      Title = "Demo Nugets";
      Map = new Map(Basemap.CreateStreets());

      GraphicsOverlayCollection = new GraphicsOverlayCollection()
      {
        new GraphicsOverlay() { Id = "Esri Colombia"},
        new GraphicsOverlay() { Id = "Eventos"}
      };

      var mapPoint = new MapPoint(-74.051331, 4.673646, SpatialReferences.Wgs84);

      var graphic = new Graphic()
      {
        Geometry = mapPoint,
        Symbol = new SimpleMarkerSymbol
        {
          Color = Colors.Green,
          Style = SimpleMarkerSymbolStyle.Circle,
          Size = 10
        }
      };
      GraphicsOverlayCollection.First().Graphics.Add(graphic);
      Map.InitialViewpoint = new Viewpoint(mapPoint, 5000);

      AddGraphicsCommand = new DelegateCommand<MapPoint>(AddGraphicsAction);
      ClearEventsCommand = new DelegateCommand(ClearEventsAction);
      CenterMapCommand = new DelegateCommand(CenterMapAction);
    }

    private void AddGraphicsAction(MapPoint location)
    {
      var graphic = new Graphic()
      {
        Geometry = location,
        Symbol = new SimpleMarkerSymbol
        {
          Color = Colors.Red,
          Style = SimpleMarkerSymbolStyle.Diamond,
          Size = 20
        }
      };
      var graphicOverlay = GraphicsOverlayCollection.Where(go => go.Id == "Eventos").FirstOrDefault();
      if (graphicOverlay != null)
      {
        graphicOverlay.Graphics.Add(graphic);
      }
    }

    private void ClearEventsAction()
    {
      var graphicOverlay = GraphicsOverlayCollection.Where(go => go.Id == "Eventos").FirstOrDefault();
      if (graphicOverlay != null)
      {
        graphicOverlay.Graphics.Clear();
      }
    }

    private void CenterMapAction()
    {
      var mapPoint = new MapPoint(-74.051331, 4.673646, SpatialReferences.Wgs84);
      NewViewpoint = new Viewpoint(mapPoint, 5000);
    }

  }
}
