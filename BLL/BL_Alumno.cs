using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DAL;
using Entity;

namespace BLL
{
    public class BL_Alumno
    {
        public static List<string> Validar(string PCadena, DtoAlumno alumno)
        {
            List<string> lstValidaciones = new List<string>();

            if (ValidacionTexto(alumno.Nombre))
            {
                lstValidaciones.Add("Revise el Nombre del alumno");
            }

            if (ValidaNombreAlumno(PCadena, alumno.Nombre + alumno.Apaterno + alumno.Amaterno))
            {
                lstValidaciones.Add("El alumno ya fue registrado ");
            }

            if (ValidacionTexto(alumno.Apaterno))
            {
                lstValidaciones.Add("Revise el Apellido paterno");
            }

            if (ValidacionTexto(alumno.Amaterno))
            {
                lstValidaciones.Add("Revise el Apellido materno");
            }

            //if (ValidacionTexto(alumno.IdMatricula))
            //{
            //    lstValidaciones.Add("Revise la Matrícula");
            //}
            //Aqui marca error porque no seria ValidacionTexto sino que hay que hacer uno de Numero//

            return lstValidaciones;
        }

        private static Boolean ValidaNombreAlumno(string PCadena, String alumno)
        {
            bool Validacion = false;

            var dpParametros = new
            {
                P_Accion = 3,
                P_Texto = alumno
            };

            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsultarAlumnos", dpParametros);
            if (Dt.Rows.Count > 0)
            {
                Validacion = true;

            }
            return Validacion;
        }
        //Buscar es Consulta?

        private static bool ValidacionTexto(string PTexto)
        {
            bool Validacion = false;

            foreach (char Letra in PTexto.Replace(" ", ""))
            {
                if (!char.IsLetter(Letra))
                {
                    Validacion = true;
                    break;
                }
            }
            return Validacion;
        }

        public static List<string> GuardarInfo(string Cadena, DtoAlumno alumno)
        {
            List<string> lstDatos = new List<string>();
            try
            {
                var dpParametros = new
                {
                    P_Nombre = alumno.Nombre,
                    P_APaterno = alumno.Apaterno,
                    P_AMaterno = alumno.Amaterno,
                    p_Correo_Uni = alumno.Correo_Uni
                };

                Contexto.Procedimiento_StoreDB(Cadena, "spAddAlumno", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El alumno fue registrado con éxito");
            }
            catch (SqlException ex)
            {
                lstDatos.Add("14");
                lstDatos.Add(ex.Message);
            }
            return lstDatos;
        }

        public static List<string> Editar(string Cadena, DtoAlumno alumno)
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

                Contexto.Procedimiento_StoreDB(Cadena, "modificar", dpParametros);

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

        public static List<string> Borrar(string Cadena, int pIdMatricula)
        {
            List<string> lstDatos = new List<string>();
            try
            {
                var dpParametros = new
                {
                    IdMatricula = pIdMatricula
                };

                Contexto.Procedimiento_StoreDB(Cadena, "Eliminar", dpParametros);

                lstDatos.Add("00");
                lstDatos.Add("El alumno fue borrado con éxito");
            }
            catch (SqlException ex)
            {
                lstDatos.Add("14");
                lstDatos.Add(ex.Message);
            }
            return lstDatos;
        }

        public static List<DtoBuscar_Eliminar> MostrarAlumnos(string PCadena)
        {
            List<DtoBuscar_Eliminar> lstalumnos = new List<DtoBuscar_Eliminar>();
            var dpParametros = new
            {
                P_Accion = 1
            };
            DataTable Dt = Contexto.Funcion_StoreDB(PCadena, "spConsultarAlumnos",dpParametros);

            if (Dt.Rows.Count > 0)
            {
                lstalumnos = (from item in Dt.AsEnumerable()
                              select new DtoBuscar_Eliminar
                              {
                                  IdMatricula = item.Field<int>("IdMatricula"),
                                  Nombre_Completo = item.Field<string>("Nombre_Completo"),
                                  Correo_Uni = item.Field<string>("Correo_Uni"),
                                  Registro = item.Field<string>("Registro")
                              }).ToList();
            }

            return lstalumnos;
        }

    }
}