using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for db
/// </summary>
public static class db
{
    private static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    //Opgave 1-2
    /// <summary>
    /// Laver et select statement fra en tabel via tabelnavnet
    /// </summary>
    /// <param name="Table"></param>
    /// <returns></returns>
    public static DataTable SelectAllFrom(string Table)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM " + Table, conn);
            da.Fill(dt);
        }
        catch
        {

        }
        return dt;

    }

    /// <summary>
    /// Laver et select statement fra en tabel via en SqlCommand
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public static DataTable SelectTable(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        try
        {
            cmd.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch
        {

        }
        return dt;

    }
    /// <summary>
    /// Method to fill a DataTable with a dynamic WHERE sentence
    /// </summary>
    /// <param name="Table"></param>
    /// <param name="Param"></param>
    /// <param name="Id"></param>
    /// <returns>DataTable</returns>
    public static DataTable SelectAllFromByParameter(string Table, string Param, int Id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM " + Table + " WHERE " + Param + " = @Param", conn);
            cmd.Parameters.AddWithValue("@Param", Id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch
        {

        }
        return dt;
    }
    /// <summary>
    /// Sletter en række i en tabel
    /// </summary>
    /// <param name="Table"></param>
    /// <param name="Field"></param>
    /// <param name="Id"></param>
    /// <returns></returns>
    public static int DeleteFromTable(string Table, string Field, int Id)
    {
        int affected_rows = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM " + Table + " WHERE " + Field + " = @parameter", conn);
            cmd.Parameters.AddWithValue("@parameter", Id);
            conn.Open();
            affected_rows = cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch
        {

        }
        return affected_rows;
    }
}