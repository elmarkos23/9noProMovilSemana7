using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class ConsultarRegistro : ContentPage
  {
    private SQLiteAsyncConnection con;
    private ObservableCollection<Models.Estudiante> tablaEstudiantes;
    public ConsultarRegistro()
    {
      InitializeComponent();
      con = DependencyService.Get<DataBase>().GetConnection();
     
    }
    protected override void OnAppearing()
    {
      base.OnAppearing();
      consultar();
    }
    private async void consultar()
    {
      var registros = await con.Table<Models.Estudiante>().ToListAsync();
      tablaEstudiantes = new ObservableCollection<Models.Estudiante>(registros);
      lvEstudiantes.ItemsSource = tablaEstudiantes;
    }

    private void lvEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
      var obj = (Models.Estudiante)e.SelectedItem;
      var item = obj.Id.ToString();
      int id = Convert.ToInt32(item);
      try
      {
        Navigation.PushAsync(new Elemento(id));
      }
      catch (Exception ex)
      {

        throw;
      }
    }
  }
}