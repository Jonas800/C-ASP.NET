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
    public static DataTable SelectAllFromOrderBy(string Table, string Column, string Direction)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM " + Table + " ORDER BY " + Column + " " + Direction, conn);
            da.Fill(dt);
        }
        catch
        {

        }
        return dt;

    }
    public static DataTable SelectAllFromOrderByComplex(SqlCommand cmd, string Column, string Direction)
    {
        cmd.CommandText += " ORDER BY " + Column + " " + Direction;
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
    //Opgave 3-4
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
    //Opgave 4½
    public static DataTable SelectSomethingFromByParameter(string Subject, string Table, string Param, int Id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT " + Subject + " FROM " + Table + " WHERE " + Param + " = @parameter", conn);
            cmd.Parameters.AddWithValue("@parameter", Id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch
        {

        }
        return dt;

    }
    //Opgave 5
    public static string SelectSubjectFromByRequest(string Subject, string Table, string Param, int Id)
    {
        string result = "output";
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM " + Table + " WHERE " + Param + " = @parameter", conn);
            cmd.Parameters.AddWithValue("@parameter", Id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = reader[Subject].ToString();
                conn.Close();
            }

        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);
        }

        return result;

    }
    //Opgave 6.1
    public static int CountAllFrom(string Table)
    {
        DataTable dt = new DataTable();
        int numberOfRecords = 0;

        try
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM " + Table, conn);
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);
        }
        numberOfRecords = dt.Select().Length;
        return numberOfRecords;
    }
    //Opgave 6.2
    public static int CountAllFromSQL(string Table, string Condition, string Operator, int Id)
    {
        object numberOfRecords = 0;

        try
        {
            SqlCommand cmd = new SqlCommand("SELECT count(*) FROM " + Table + " WHERE " + Condition + " " + Operator + " @Parameter", conn);
            cmd.Parameters.AddWithValue("@Parameter", Id);
            conn.Open();
            numberOfRecords = cmd.ExecuteScalar();
            conn.Close();
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);
        }
        return Convert.ToInt32(numberOfRecords);
    }
    /// <summary>
    /// Inner joins two tables with one and filters out from one parameter.
    /// </summary>
    /// <param name="Table"></param>
    /// <param name="InnerJoinTable1"></param>
    /// <param name="InnerJoinOn1"></param>
    /// <param name="InnerJoinTable2"></param>
    /// <param name="InnerJoinOn2"></param>
    /// <param name="param"></param>
    /// <param name="id"></param>
    /// <returns>DataTable</returns>
    public static DataTable DoubleInnerJoinWithParameter(string Table, string InnerJoinTable1, string InnerJoinOn1, string InnerJoinTable2, string InnerJoinOn2, string param, object id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM " + Table + " INNER JOIN " + InnerJoinTable1 + " ON fk_" + InnerJoinOn1 + " = " + InnerJoinOn1 + " INNER JOIN " + InnerJoinTable2 + " ON fk_" + InnerJoinOn2 + " = " + InnerJoinOn2 + " WHERE fk_" + param + " = @" + param, conn);
            da.SelectCommand.Parameters.AddWithValue(param, id);

            da.Fill(dt);
        }
        catch
        {

        }
        return dt;
    }
    //public static void Login(object LoginTextBox, object PasswordTextBox, string Login, string Password, string ReaderId, object LabelError, string ErrorMessage)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = conn;

    //    cmd.CommandText = "SELECT * FROM users WHERE " + " login " + " = @user_login AND " + Password + " = @user_password";

    //    cmd.Parameters.AddWithValue("@user_login", LoginTextBox);
    //    cmd.Parameters.AddWithValue("@user_password", PasswordTextBox);

    //    conn.Open();
    //    SqlDataReader reader = cmd.ExecuteReader();

    //    if (reader.Read())
    //    {
    //        HttpContext.Current.Session["user_id"] = reader["id"];
    //        HttpContext.Current.Response.Redirect("Admin.aspx");
    //    }
    //    else
    //    {
    //        LabelError = ErrorMessage;
    //    }
    //    conn.Close();
    //}
    //Opgave 7
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
    public static bool DeleteFromTableBoolean(string Table, string Field, int Id)
    {
        bool is_rows_affected = false;
        int affected_rows = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM " + Table + " WHERE " + Field + " = @parameter", conn);
            cmd.Parameters.AddWithValue("@parameter", Id);
            conn.Open();
            affected_rows = cmd.ExecuteNonQuery();
            if (affected_rows == 0)
            {
                is_rows_affected = false;
            }
            else
            {
                is_rows_affected = true;
            }
            conn.Close();

        }
        catch
        {

        }
        return is_rows_affected;
    }
    public static DataRow SelectSingleRowFrom(string Table, string Field, int Id)
    {
        SqlCommand cmd = new SqlCommand("SELECT * FROM " + Table + " WHERE " + Field + " = @param", conn);
        cmd.Parameters.AddWithValue("@param", Id);

        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch
        {

        }

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }
        else
        {
            return dt.NewRow();
        }
    }
    public static DataRow SelectSingleRow(string Table)
    {
        SqlCommand cmd = new SqlCommand("SELECT * FROM " + Table, conn);

        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch
        {

        }

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0];
        }
        else
        {
            return dt.NewRow();
        }
    }
    //public static DataRow Login(string Table, string login, string pass)
    //{
    //    SqlCommand cmd = new SqlCommand("SELECT * FROM " + Table + " WHERE " + login + " = @login AND " + pass + "= @pass", conn);
    //    cmd.Parameters.AddWithValue("@pass", pass);
    //    cmd.Parameters.AddWithValue("@login", login);

    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(dt);
    //    }
    //    catch (Exception ex)
    //    {
    //        HttpContext.Current.Response.Write(ex.Message);

    //    }

    //    if (dt.Rows.Count > 0)
    //    {
    //        return dt.Rows[0];
    //    }
    //    else
    //    {
    //        return dt.NewRow();
    //    }
    //}

    //public static SqlDataReader Login(string Table, string login, string pass, string GetLogin, string GetPassword)
    //{
    //    SqlCommand cmd = new SqlCommand("SELECT * FROM " + Table + " WHERE " + login + " = @login AND " + pass + "= @pass", conn);
    //    cmd.Parameters.AddWithValue("@pass", GetPassword);
    //    cmd.Parameters.AddWithValue("@login", GetLogin);

    //    conn.Open();
    //    SqlDataReader reader = cmd.ExecuteReader();
    //    return reader;
    //}
    public static int UpdateTable(SqlCommand cmd)
    {
        int affected_rows = 0;
        try
        {
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            affected_rows = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return affected_rows;
    }
    public static int InsertIntoTable(SqlCommand cmd)
    {
        int new_id = 0;
        try
        {
            cmd.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd.CommandText += ";SELECT SCOPE_IDENTITY()";
            new_id = Convert.ToInt32(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);

        }
        finally
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        return new_id;
    }
}