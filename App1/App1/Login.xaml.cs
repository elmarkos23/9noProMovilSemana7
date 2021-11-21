using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;

namespace App1
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Login : ContentPage
  {
    private SQLiteAsyncConnection con;
    public Login()
    {
      InitializeComponent();
      con = DependencyService.Get<DataBase>().GetConnection();
    }

    public static IEnumerable<Models.Estudiante> SELECT_WHERE(SQLiteConnection db, string usuario, string contrasenia)
    {
      return db.Query<Models.Estudiante>("SELECT * FROM Estudiante WHERE Usuario = ? AND Contrasenia = ?", usuario, contrasenia);
    }
    private void btnIniciar_Clicked(object sender, EventArgs e)
    {
      try
      {
        var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
        var db = new SQLiteConnection(documentPath);
        db.CreateTable<Models.Estudiante>();
        IEnumerable<Models.Estudiante> resultado = SELECT_WHERE(db, txtUsuario.Text, txtContrasenia.Text);
        if (resultado.Count() > 0)
        {
          Navigation.PushAsync(new ConsultarRegistro());
        }
        else
        {
          DisplayAlert("Aletar", "Usuario no existe, por favor registrase", "Aceptar");
        }
      }
      catch (Exception ex)
      {
        DisplayAlert("", "", "Aceptar");
      }
    }

    private void btnRegistro_Clicked(object sender, EventArgs e)
    {

    }
  }
}