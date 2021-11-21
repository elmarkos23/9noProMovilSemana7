using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class Elemento : ContentPage
  {
    private SQLiteAsyncConnection con;
    private int IdSeleccionado = 0;
    private IEnumerable<Models.Estudiante> ResultadoDelete;
    private IEnumerable<Models.Estudiante> ResultadoUpdate;
    public Elemento(int Id)
    {
      InitializeComponent();
      this.IdSeleccionado = Id;
      con = DependencyService.Get<DataBase>().GetConnection();
      var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
      var db = new SQLiteConnection(documentPath);
      var resultado = SELECT_WHERE(db, IdSeleccionado);
      txtNombre.Text = resultado.Nombre;
      txtUsuario.Text = resultado.Usuario;
      txtContrasenia.Text = resultado.Contrasenia;
    }
    public static IEnumerable<Models.Estudiante> DELETE(SQLiteConnection db, int Id)
    {
      return db.Query<Models.Estudiante>("DELETE FROM Estudiante WHERE Id = ? ", Id);
    }
    public static IEnumerable<Models.Estudiante> UPDATE(SQLiteConnection db, int Id, string Nombre, string Usuario, string Contrasenia)
    {
      return db.Query<Models.Estudiante>("UPDATE Estudiante SET Nombre= ? ,Usuario = ?, Contrasenia = ? WHERE Id= ?", Nombre, Usuario, Contrasenia, Id);
    }
    public static Models.Estudiante SELECT_WHERE(SQLiteConnection db, int Id)
    {
      return db.Query<Models.Estudiante>("SELECT * FROM Estudiante WHERE Id = ?", Id).FirstOrDefault();
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
      try
      {
        var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
        var db = new SQLiteConnection(documentPath);
        ResultadoUpdate = UPDATE(db, IdSeleccionado, txtNombre.Text, txtUsuario.Text, txtContrasenia.Text);
        DisplayAlert("Mensaje", "Se actualizo correctamente", "Aceptar");
      }
      catch (Exception ex)
      {
        DisplayAlert("Alerta", "Existe algun error " + ex.Message, "Aceptar");
      }

    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
      try
      {
        var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
        var db = new SQLiteConnection(documentPath);
        ResultadoUpdate = DELETE(db, IdSeleccionado);
        DisplayAlert("Mensaje", "Se elimino correctamente", "Aceptar");
      }
      catch (Exception ex)
      {
        DisplayAlert("Alerta", "Existe algun error " + ex.Message, "Aceptar");
      }
    }
  }
}