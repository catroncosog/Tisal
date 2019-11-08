using System.Web.Services;
using System.Web.Services.Protocols;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;




namespace WebService1
{
    /// <summary>
    /// Servicio  de Ejemplo 
    /// </summary>
    /// 

    [WebService(Namespace = "http://tisal.cl/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service1 : WebService
    {
        //Se declara Token. En tiempo de ejecución se llena con el valor que envía el cliente
        public AuthenticationHeader Token;
        
        string con1 = ConfigurationManager.ConnectionStrings["DataSource_1"].ConnectionString;

       
         //METODO ObtenerEntidadUsuario retorna DataTable
         [WebMethod]
         [SoapHeader("Token")]

        public DataTable ObtenerEntidadUsuario(string Filial, int Cod_Entidad, string Codigo_Valor)
        {
            DataTable tt = new DataTable();
            try
            {
                using (var conn = new OracleConnection(con1))
                {
                    OracleConnection conn1 = new OracleConnection(con1);
                    conn1.Open();
                    OracleCommand cmd = new OracleCommand("DM_ENTIDADES_CORP.ObtenerEntidadUsuario", conn1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("in_Filial", OracleDbType.Varchar2).Value = Filial;
                    cmd.Parameters.Add("in_Cod_Entidad", OracleDbType.Int32).Value = Cod_Entidad;
                    cmd.Parameters.Add("in_CodigoValor", OracleDbType.Varchar2).Value = Codigo_Valor;
                    cmd.Parameters.Add("OUTCURSORENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataAdapter myDa = new OracleDataAdapter(cmd);
                    DataSet myDs = new DataSet();
                    myDa.Fill(myDs);
                    tt = myDs.Tables[0];
                    conn1.Close();
                }
                
            }
            catch (OracleException ex)
            {
                
            }
            return tt;
        }


         [WebMethod]
         [SoapHeader("Token")]
         public DataTable ConsultarSolicitudUsuario(string Filial, string ResponsableSol, string fec_ini, string fec_fin, int cod_entidad)
         {
             DataTable tt = new DataTable();
             try
             {
                 using (var conn = new OracleConnection(con1))
                 {
                     OracleConnection conn1 = new OracleConnection(con1);
                     conn1.Open();
                     OracleCommand cmd = new OracleCommand("DM_ENTIDADES_CORP.ConsultarSolicitudUsuario", conn1);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.Add("in_Filial", OracleDbType.Varchar2).Value = Filial;
                     cmd.Parameters.Add("in_ResponsableFilial", OracleDbType.Varchar2).Value = ResponsableSol;
                     cmd.Parameters.Add("in_fec_ini", OracleDbType.Varchar2).Value = fec_ini;
                     cmd.Parameters.Add("in_fec_fin", OracleDbType.Varchar2).Value = fec_fin;
                     cmd.Parameters.Add("in_cod_entidad", OracleDbType.Int32).Value = cod_entidad;
                     cmd.Parameters.Add("OUTCURSORENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                     OracleDataAdapter myDa = new OracleDataAdapter(cmd);
                     DataSet myDs = new DataSet();
                     myDa.Fill(myDs);
                     tt = myDs.Tables[0];
                     conn1.Close();
                 }
             }

             catch (Exception ex)
             {

             }

             return tt;
         }


         [WebMethod]
         [SoapHeader("Token")]
         public DataTable SolicitarEntidadUsuario(int cod_entidad, string ResponsableSol, string cod_valor_entidad, string descripcion_entidad, string filial, string motivo_solicitud, string TipoSolicitud)
         {

          DataTable tt = new DataTable();
          try
          {
              using (var conn = new OracleConnection(con1))
              {
                  OracleConnection conn1 = new OracleConnection(con1);
                  conn1.Open();
                  OracleCommand cmd = new OracleCommand("DM_ENTIDADES_CORP.SolicitarEntidadUsuario", conn1);
                  cmd.CommandType = CommandType.StoredProcedure;
                  cmd.Parameters.Add("in_cod_entidad", OracleDbType.Int32).Value = cod_entidad;
                  cmd.Parameters.Add("in_ResponsableSol", OracleDbType.Varchar2).Value = ResponsableSol;
                  cmd.Parameters.Add("in_cod_valor_entidad", OracleDbType.Varchar2).Value = cod_valor_entidad;
                  cmd.Parameters.Add("in_descripcion_entidad", OracleDbType.Varchar2).Value = descripcion_entidad;
                  cmd.Parameters.Add("in_Filial", OracleDbType.Varchar2).Value = filial;
                  cmd.Parameters.Add("in_motivo_solicitud", OracleDbType.Varchar2).Value = motivo_solicitud;
                  cmd.Parameters.Add("in_tipo_solicitud", OracleDbType.Varchar2).Value = TipoSolicitud;
                  cmd.Parameters.Add("OUTCURSORENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                  OracleDataAdapter myDa = new OracleDataAdapter(cmd);
                  DataSet myDs = new DataSet();
                  myDa.Fill(myDs);
                  tt = myDs.Tables[0]; ;
                  conn1.Close();
              }
             
          }
          catch (Exception ex)
          {

          }
          return tt;

         }


         [WebMethod]
         [SoapHeader("Token")]
         public DataTable AnularSolicitudUsuario(int id_solicitud)
         {
             DataTable tt = new DataTable();
              try
            {
                 using (var conn = new OracleConnection(con1))
                  {
                 OracleConnection conn1 = new OracleConnection(con1);
                 conn1.Open();
                 OracleCommand cmd = new OracleCommand("DM_ENTIDADES_CORP.AnularSolicitudUsuario", conn1);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.Add("in_id_solicitud", OracleDbType.Int32).Value = id_solicitud;
                 cmd.Parameters.Add("OUTCURSORENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                 OracleDataAdapter myDa = new OracleDataAdapter(cmd);
                 DataSet myDs = new DataSet();
                 myDa.Fill(myDs);
                 tt = myDs.Tables[0];
                 conn1.Close();
                }  
            }
              catch (Exception ex)
              {

              }
             return tt;
         }

                   
        [WebMethod]
        [SoapHeader("Token")]
        public DataTable ObtenerFilialEntidad(int Cod_Empresa, int Homologacion, string Cod_Mantenedor)
        {
            DataTable tt = new DataTable();
            try
            {
                using (var conn = new OracleConnection(con1))
                {
                    OracleConnection conn1 = new OracleConnection(con1);
                    conn1.Open();
                    OracleCommand cmd = new OracleCommand("DM_ENTIDADES_CORP.ObtenerFilialEntidad", conn1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("in_cod_empresa", OracleDbType.Int32).Value = Cod_Empresa;
                    cmd.Parameters.Add("in_homologacion", OracleDbType.Int32).Value = Homologacion;
                    cmd.Parameters.Add("in_codigo_mantenedor", OracleDbType.Varchar2).Value = Cod_Mantenedor;
                    cmd.Parameters.Add("OUTCURSORENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataAdapter myDa = new OracleDataAdapter(cmd);
                    DataSet myDs = new DataSet();
                    myDa.Fill(myDs);
                    tt = myDs.Tables[0];
                    conn1.Close();
                }
            }

            catch (Exception ex)
            {
               
            }
            return tt;
        }


       
    [WebMethod]
        [SoapHeader("Token")]
        public DataTable ObtenerDetalleEntidad(int Cod_Entidad,string Codigo_Valor)
        {
            DataTable tt = new DataTable();
           
            try
            {
                using (var conn = new OracleConnection(con1))
                {
                  
                   
                    OracleConnection conn1 = new OracleConnection(con1);
                    conn1.Open();
                    OracleCommand cmd = new OracleCommand("DM_ENTIDADES_CORP.ObtenerDetalleEntidad", conn1);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("in_cod_entidad", OracleDbType.Int32).Value = Cod_Entidad;
                    cmd.Parameters.Add("in_codigo_valor", OracleDbType.Varchar2).Value = Codigo_Valor;
                    cmd.Parameters.Add("OUTCURSORENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataAdapter myDa = new OracleDataAdapter(cmd);
                    DataSet myDs = new DataSet();
                    myDa.Fill(myDs);
                    tt = myDs.Tables[0];
                    conn1.Close();
                       
                }
            }

            catch (ArgumentNullException ex)
            {
                
            }
            return tt;
        }


    [WebMethod]
    [SoapHeader("Token")]
    public DataTable VerificarUsuarioAdministrador(string usuarioAdmin, string passwordAdmin)
    {
        DataTable tt = new DataTable();
        try
        {
            using (var conn = new OracleConnection(con1))
            {
                OracleConnection conn1 = new OracleConnection(con1);
                conn1.Open();
                OracleCommand cmd = new OracleCommand("DM_ENTIDADES_CORP.VerificarUsuarioAdministrador", conn1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("in_usuario", OracleDbType.Varchar2).Value = usuarioAdmin;
                cmd.Parameters.Add("in_password", OracleDbType.Varchar2).Value = passwordAdmin;
                cmd.Parameters.Add("OUTCURSORENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataAdapter myDa = new OracleDataAdapter(cmd);
                DataSet myDs = new DataSet();
                myDa.Fill(myDs);
                tt = myDs.Tables[0];
                conn1.Close();
            }
        }

        catch (Exception ex)
        {

        }
        return tt;
    }


    [WebMethod]
    [SoapHeader("Token")]
    public DataTable ListaSolicitudAdministrador(int CodEntidad, string FechaInicio, string FechaFin, string UsuarioSolicitante)
    {
        DataTable tt = new DataTable();
        try
        {
            using (var conn = new OracleConnection(con1))
            {
                OracleConnection conn1 = new OracleConnection(con1);
                conn1.Open();
                OracleCommand cmd = new OracleCommand("DM_ENTIDADES_CORP.ListaSolicitudAdministrador", conn1);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("in_cod_entidad", OracleDbType.Int32).Value = CodEntidad;
                cmd.Parameters.Add("in_fecha_inicio", OracleDbType.Varchar2).Value = FechaInicio;
                cmd.Parameters.Add("in_fecha_fin", OracleDbType.Varchar2).Value = FechaFin;
                cmd.Parameters.Add("in_Usuario_Solicitante", OracleDbType.Varchar2).Value = UsuarioSolicitante;
                cmd.Parameters.Add("OUTCURSORENT", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataAdapter myDa = new OracleDataAdapter(cmd);
                DataSet myDs = new DataSet();
                myDa.Fill(myDs);
                tt = myDs.Tables[0];
                conn1.Close();
            }
        }

        catch (Exception ex)
        {

        }
        return tt;
    }


    [WebMethod]
    [SoapHeader("Token")]
    public DataTable RespuestaSolicitudAdmin(string usuario, string password, string filial, string solicitud)
    {
        string con = ConfigurationManager.ConnectionStrings["DataSource_1"].ConnectionString;
        string sql;

        sql = " SELECT TE.NOMBRE_ENTIDAD,VE.CODIGO_VALOR,VE.DESCRIPCION from DM_TIPO_ENTIDAD TE inner join DM_VALORES_ENTIDAD VE on TE.CODIGO_ENTIDAD = VE.CODIGO_ENTIDAD INNER JOIN DM_RESP_POR_FILIAL RF ON TE.CODIGO_ENTIDAD = RF.CODIGO_ENTIDAD";

        OracleConnection conn = new OracleConnection(con);
        OracleDataAdapter dr = new OracleDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        ds.Tables.Add("dodo");
        dr.Fill(ds, "dodo");
        DataTable tt = ds.Tables[0];
        return tt;

    }


    [WebMethod]
    [SoapHeader("Token")]
    public DataTable AceptarSolicitudAdmin(int CodigoEntidad, string CodigoValor, string Descripcion, string FechaCreacion, string Responsable )
    {

        string con = ConfigurationManager.ConnectionStrings["DataSource_1"].ConnectionString;
        string sql;

        sql = " SELECT TE.NOMBRE_ENTIDAD,VE.CODIGO_VALOR,VE.DESCRIPCION from DM_TIPO_ENTIDAD TE inner join DM_VALORES_ENTIDAD VE on TE.CODIGO_ENTIDAD = VE.CODIGO_ENTIDAD INNER JOIN DM_RESP_POR_FILIAL RF ON TE.CODIGO_ENTIDAD = RF.CODIGO_ENTIDAD";

        OracleConnection conn = new OracleConnection(con);
        OracleDataAdapter dr = new OracleDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        ds.Tables.Add("dodo");
        dr.Fill(ds, "dodo");
        DataTable tt = ds.Tables[0];
        return tt;

    }




     [WebMethod]
    [SoapHeader("Token")]
    public DataTable SolicitarModificarValorEntidad(int CodigoEntidad, string ResponableSol, string CodValorEntidad, string DescripcionEntidad, string MotivoSol )
    {

        string con = ConfigurationManager.ConnectionStrings["DataSource_1"].ConnectionString;
        string sql;

        sql = " SELECT TE.NOMBRE_ENTIDAD,VE.CODIGO_VALOR,VE.DESCRIPCION from DM_TIPO_ENTIDAD TE inner join DM_VALORES_ENTIDAD VE on TE.CODIGO_ENTIDAD = VE.CODIGO_ENTIDAD INNER JOIN DM_RESP_POR_FILIAL RF ON TE.CODIGO_ENTIDAD = RF.CODIGO_ENTIDAD";

        OracleConnection conn = new OracleConnection(con);
        OracleDataAdapter dr = new OracleDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        ds.Tables.Add("dodo");
        dr.Fill(ds, "dodo");
        DataTable tt = ds.Tables[0];
        return tt;

    }


        



        [WebMethod]
        [SoapHeader("Token")]
        public DataTable VerificarUsuarioResponsable(string usuario, string password)
        {
            string con = ConfigurationManager.ConnectionStrings["DataSource_1"].ConnectionString;
            string sql;

            sql = " SELECT TE.NOMBRE_ENTIDAD,VE.CODIGO_VALOR,VE.DESCRIPCION from DM_TIPO_ENTIDAD TE";

            OracleConnection conn = new OracleConnection(con);
            OracleDataAdapter dr = new OracleDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            ds.Tables.Add("dodo");
            dr.Fill(ds, "dodo");
            DataTable tt = ds.Tables[0];
            return tt;

        }

  




     

         






                

    }
}