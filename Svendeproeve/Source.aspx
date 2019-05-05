<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Source.aspx.cs" Inherits="Source" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>C# samples</h2>

            <h3>Aktiviteter.aspx</h3>
            <pre>public partial class _Default : System.Web.UI.Page
{
    //Connection string
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //Population of data from database to repeaters via SQL and C#
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT oplysning_forside FROM oplysninger";
        conn.Open();
        Repeater_Forside.DataSource = cmd.ExecuteReader();
        Repeater_Forside.DataBind();
        conn.Close();

        cmd.CommandText = "SELECT TOP 1 * FROM aktiviteter ORDER BY aktivitet_id DESC";
        conn.Open();
        Repeater_Nyheder.DataSource = cmd.ExecuteReader();
        Repeater_Nyheder.DataBind();
        conn.Close();
    }

    //Text truncation
    public static string Truncate(string text)
    {
        //To truncate text and not break HTML, we need to strip the text of HTML tags
        string stripped_text = StripHTML(text);

        //See if text is longer than the desired length, i.e. if we actually need to shorten the text
        if (stripped_text.Length > 180)
        {
            //Simple shorten of string by only returning the needed substring
            return stripped_text.Substring(0, 180) + "...";
        }
        return stripped_text + "...";
    }

    //Replaces all HTML tags with nothing
    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }
}</pre>
            <h3>Admin side for aktiviteter</h3>
            <pre>public partial class Admin_Aktiviteter : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //Checks if user is logged in, gets the user info from the class object and see whether it's an admin or not
        if (Session["brugere"] != null)
        {
            Bruger bruger = Session["brugere"] as Bruger;
            if (bruger.Rolle == 1)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                SelectAktiviteter(cmd);

                //If user clicks on a link, in this case it's to delete a image from the database and server
                if (Request.QueryString["id"] != null)
                {   
                    //Sets a variabel we'll use later on to determine whether the database query was succesful
                    int slettede_rækker = 0;

                    //Gets image filename
                    cmd.CommandText = "SELECT aktivitet_billede FROM aktiviteter";
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    //If the image exists in the database, we delete it (method for deleting further down)
                    if (reader.Read())
                    {
                        DeletePictures(reader["aktivitet_billede"]);
                    }
                    conn.Close();

                    //After we've deleted it from the server, we remove it from the database
                    cmd.CommandText = "DELETE FROM aktiviteter WHERE aktivitet_id = @id";
                    conn.Open();
                    slettede_rækker = cmd.ExecuteNonQuery(); //gives us an integer on how many rows were affected by the query
                    conn.Close();
                    if (slettede_rækker > 0)
                    {
                        Session["besked"] = "Sletning lykkedes.";
                        Response.Redirect("Aktiviteter.aspx");
                    }
                    else
                    {
                        Session["besked"] = "Sletning fejlede. Prøv igen eller kontakt webmaster.";
                        Response.Redirect("Aktiviteter.aspx");

                    }
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    private void SelectAktiviteter(SqlCommand cmd)
    {
        cmd.CommandText = "SELECT * FROM aktiviteter";
        conn.Open();
        Repeater_Aktiviteter.DataSource = cmd.ExecuteReader();
        Repeater_Aktiviteter.DataBind();
        conn.Close();
    }

    public static void DeletePictures(object file_name)
    {
        if (file_name != DBNull.Value)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("../billeder/produkter/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("../billeder/produkter/") + file_name.ToString());
            }
            if (File.Exists(HttpContext.Current.Server.MapPath("../billeder/backup/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("../billeder/backup/") + file_name.ToString());
            }
            if (File.Exists(HttpContext.Current.Server.MapPath("../billeder/thumbs/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("../billeder/thumbs/") + file_name.ToString());
            }
        }
    }
}</pre>

            <h3>Min Side (gemt bag login)</h3>
            <pre>
    //Creation of a button which changes function depending on different factors
    //Clicking the button will:
    //First click: Apply for the course/team
    //Second click: Unapply
    //The button is only clickable if:
    //The date of the course hasn't passed
    //It also becomes unclickable if the user has applied and there is less than four hours until the course starts
    //Or lastly, if the max capacity of the course is reached

    //Method to determine what button should show
    //In reality, in it just returns a string based on above factors. But we can return that string into an OnItemCommand within the control and then create the functions later. For more fun, you can also return it to the CssClass for easy styling.
    public string LavKnap(int id)
    {
        SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn2;

        Bruger bruger = Session["brugere"] as Bruger;

        cmd.Parameters.AddWithValue("@hold_id", id);
        cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);

        //Gets the starting time of the course and whether the user has already applied by checking if a record exists in the database 
        cmd.CommandText = "SELECT hold_brugere_id, hold_tidspunkt FROM hold_brugere INNER JOIN hold ON fk_hold_id = hold_id WHERE hold_brugere.fk_bruger_id = @bruger_id AND fk_hold_id = @hold_id";
        conn2.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            DateTime tidspunkt = Convert.ToDateTime(reader["hold_tidspunkt"]);
            conn2.Close();
            //Gets the hours left until the start of the course
            double tilbage = tidspunkt.Subtract(DateTime.Now).TotalHours;

            //More than four, it returns the possibility of unapplying, if less it cannot cancelled
            if (tilbage > 4)
            {
                return "Frameld";
            }
            else
            {
                return "Kan ikke frameldes";
            }
        }
        else
        {
            //If the user has yet to apply for the course, we need to make a button for applying
            conn2.Close();

            //Gets max capacity, current participators, and starting time
            cmd.CommandText = "SELECT hold_max_antal, hold_tidspunkt, (SELECT COUNT (fk_hold_id) FROM hold_brugere WHERE fk_hold_id = hold_id) as tilmeldte FROM hold WHERE hold_id = @hold_id";

            conn2.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int tilmeldte = Convert.ToInt32(reader["tilmeldte"]);
                int max = Convert.ToInt32(reader["hold_max_antal"]);
                DateTime tidspunkt = Convert.ToDateTime(reader["hold_tidspunkt"]);

                double tilbage = tidspunkt.Subtract(DateTime.UtcNow).TotalHours;

                if (tilbage > 0)
                {
                    //Sees if current amount of participators is less than max
                    if (tilmeldte < max)
                    {
                        return "Tilmeld";
                    }
                    else
                    {
                        conn2.Close();
                        return "Optaget";
                    }
                }
                else
                {
                    return "Overstået";
                }
            }
            else
            {
                conn2.Close();
                return "";
            }
        }
    }</pre>

            <h4>Fun with buttons continued</h4>

            <pre>
    //As the buttons we have been working with are inside a repeater, we use the OnItemCommand function            
    protected void Repeater_Hold_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //There only to commands which has a functionality: applying (tilmeld)/unapplying (frameld). The rest are duds and nothing should happen.

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        Bruger bruger = Session["brugere"] as Bruger;
        int hold_id = 0;
        
        //Gets the ID for the course from a HiddenField control
        int.TryParse((e.Item.FindControl("HiddenField_ID") as HiddenField).Value, out hold_id);

        //If the command sent from the control is Tilmeld, we insert the user and team into our many-to-many table where we can see who has applied for what. If it is Frameld, we just delete those values.
        if (e.CommandName == "Tilmeld")
        {
            cmd.CommandText = "INSERT INTO hold_brugere (fk_hold_id, fk_bruger_id) VALUES (@hold_id, @bruger_id)";
            cmd.Parameters.AddWithValue("@hold_id", hold_id);
            cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Response.Redirect(Request.RawUrl);
        }
        else if (e.CommandName == "Frameld")
        {
            cmd.CommandText = "DELETE FROM hold_brugere WHERE fk_hold_id = @hold_id AND fk_bruger_id = @bruger_id";
            cmd.Parameters.AddWithValue("@hold_id", hold_id);
            cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Response.Redirect(Request.RawUrl);
        }
    }</pre>
        </div>
    </form>
</body>
</html>
