using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using BLL;
using DAL;
using Entity;

namespace BLL
{
    public class BL_Alumno
    {
        public static List<string> EditarInfo(string Cadena, DtoAlumno alumno)
        {
            List<string> lstDatos = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_Nombre = alumno.Nombre,
                    P_APaterno = alumno.Apaterno,
                    P_AMaterno = alumno.Amaterno,
                    p_Correo_Uni = alumno.Correo_Uni,
                    P_IdMatricula= alumno.IdMatricula
                };

                Contexto.Procedimiento_StoreDB(Cadena, "Modificar", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El alumno fue modificado con éxito");
            }
            catch (SqlException ex)
            {
                lstDatos.Add("14");
                lstDatos.Add(ex.Message);
            }
            return lstDatos;
        }

        public static List<DtoAlumno> MostrarAlumnos(string PCadena)
        {
            List<DtoAlumno> lstalumno = new List<DtoAlumno>();

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "MostrarTodo");

            if (Dt.Rows.Count > 0)
            {
                lstalumno = (from item in Dt.AsEnumerable()
                              select new DtoAlumno
                              {
                                  IdMatricula = item.Field<int>("IdMatricula"),
                                  Nombre = item.Field<string>("Nombre"),
                                  Apaterno = item.Field<string>("Apaterno"),
                                  Amaterno = item.Field<string>("Amaterno"),
                                  Correo_Uni = item.Field<string>("Correo_Uni"),
                                  FechaRegistro = item.Field<string>("Registro")
                              }).ToList();
            }

            return lstalumno;
        }

    }
}