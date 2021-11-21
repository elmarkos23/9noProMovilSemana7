using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Registro : ContentPage
  {
    private SQLiteAsyncConnection con;
    public Registro()
    {
      InitializeComponent();
      con = DependencyService.Get<DataBase>().GetConnection();
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
      try
      {
        var nuevoEstudiante = new Models.Estudiante { Nombre = txtNombre.Text, Usuario = txtUsuario.Text, Contrasenia = txtContrasenia.Text };
        con.InsertAsync(nuevoEstudiante);
        DisplayAlert("Mensaje", "Dato registrado", "Aceptar");
        limpiarRegistros();
      }
      catch (Exception ex)
      {

        throw;
      }
    }

    private void limpiarRegistros()
    {
      txtNombre.Text = string.Empty;
      txtUsuario.Text = string.Empty;
      txtContrasenia.Text = string.Empty;
    }
  }
}