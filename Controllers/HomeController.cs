using join_loginSignup.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace join_loginSignup.Controllers
{
    public class HomeController : Controller
    {
        public string Con = ConfigurationManager.ConnectionStrings["ConnDB"].ToString();

        public string status;


        //--------------------------------------customer login------------------------------------------------
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult userpage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Model e)
        {
            if (e.name != null && e.email != null && e.phoneNumber != null && e.password != null)
            {
                if (Request.HttpMethod == "POST")
                {
                    Model er = new Model();
                    using (SqlConnection con = new SqlConnection("Data Source=EDITH;Initial Catalog=loginSignup;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("Sp_signup", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@name", e.name);
                            cmd.Parameters.AddWithValue("@email", e.email);
                            cmd.Parameters.AddWithValue("@phoneNumber", e.phoneNumber);
                            cmd.Parameters.AddWithValue("@password", e.password);
                            cmd.Parameters.AddWithValue("@Action", 1);
                            con.Open();
                            ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                ModelState.Clear();
                return View();
            }
            else
            {

                String SqlCon = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
                SqlConnection con = new SqlConnection(SqlCon);
                string SqlQuery = "select email,password from signup where email=@email and password=@password";
                con.Open();
                SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
                cmd.Parameters.AddWithValue("@email", e.email);
                cmd.Parameters.AddWithValue("@password", e.password);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    Session["email"] = e.email.ToString();
                    return RedirectToAction("userpage");
                }
                else
                {
                    ViewData["Message"] = "User Login Details Failed!!";
                }
                if (e.email.ToString() != null)
                {
                    Session["email"] = e.email.ToString();
                    status = "1";
                }
                else
                {
                    status = "3";
                }

                con.Close();
                ModelState.Clear();
                return View();
            }
        }

        public int UpdateCustomer(Model DAM)
        {
            List<Model> lst = new List<Model>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_signup", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", DAM.Id);
            cmd.Parameters.AddWithValue("@name", DAM.name);
            cmd.Parameters.AddWithValue("@email", DAM.email);
            cmd.Parameters.AddWithValue("@phoneNumber", DAM.phoneNumber);
            cmd.Parameters.AddWithValue("@password", DAM.password);
            cmd.Parameters.AddWithValue("@Action", 2);
            con.Open();
            int J = cmd.ExecuteNonQuery();
            con.Close();
            return J;
        }

        
        //-------------------------------------seller login---------------------------------------
        public ActionResult admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult admin(Model1 e)
        {
            if (e.name != null && e.email != null && e.phoneNumber != null && e.password != null)
            {
                if (Request.HttpMethod == "POST")
                {
                    Model1 er = new Model1();
                    using (SqlConnection con = new SqlConnection("Data Source=EDITH;Initial Catalog=loginSignup;Integrated Security=True"))
                    {
                        using (SqlCommand cmd = new SqlCommand("Sp_adminLogin", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@name", e.name);
                            cmd.Parameters.AddWithValue("@email", e.email);
                            cmd.Parameters.AddWithValue("@phoneNumber", e.phoneNumber);
                            cmd.Parameters.AddWithValue("@password", e.password);
                            cmd.Parameters.AddWithValue("@status", "INSERT");
                            con.Open();
                            ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                ModelState.Clear();
                return View();
            }
            else
            {

                String SqlCon = ConfigurationManager.ConnectionStrings["ConnDB"].ConnectionString;
                SqlConnection con = new SqlConnection(SqlCon);
                string SqlQuery = "select name,password from adminLogin where name=@name and password=@password";
                con.Open();
                SqlCommand cmd = new SqlCommand(SqlQuery, con);
                cmd.Parameters.AddWithValue("@name", e.name);
                cmd.Parameters.AddWithValue("@password", e.password);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    Session["name"] = e.name.ToString();
                    return RedirectToAction("AddItem");
                }
                else
                {
                    ViewData["Message"] = "User Login Details Failed!!";
                }
                if (e.email.ToString() != null)
                {
                    Session["name"] = e.name.ToString();
                    status = "1";
                }
                else
                {
                    status = "3";
                }

                con.Close();
                ModelState.Clear();
                return View();
            }
        }


        //-----------------------------------------seller add items---------------------------------------------
        public ActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddItem(Model2 e)
        {
            Model er = new Model();


            using (SqlConnection con = new SqlConnection("Data Source=EDITH;Initial Catalog=loginSignup;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_addcar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", Session["name"]);
                    cmd.Parameters.AddWithValue("@CarName", e.CarName);
                    cmd.Parameters.AddWithValue("@description", e.description);
                    cmd.Parameters.AddWithValue("@images", e.CarName + ".jpg");
                    cmd.Parameters.AddWithValue("@OrderCar", e.OrderCar);
                    cmd.Parameters.AddWithValue("@Action", 1);
                    con.Open();
                    ViewData["result"] = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            if (e.File != null && e.File.ContentLength > 0)
            {
                string fileName = Path.GetFileName(e.File.FileName);
                string filePath = Path.Combine(Server.MapPath("~/upload/"), e.CarName+".jpg");
                e.File.SaveAs(filePath);

                ViewBag.Message = "File uploaded successfully.";
            }

            return View("AddItem", e);
        }

    //-----------------------------------------seller get list-----------------------------------------------------
    [HttpGet]
        public ActionResult List()
        {
            return View(GetItem());
        }
        public List<Model2> GetItem()
        {
            List<Model2> lst = new List<Model2>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from adminLogin join addcar on adminLogin.name = addcar.name where adminLogin.name = '"+ Session["name"]+"'";
            con.Open();
            SqlDataReader Sdr = cmd.ExecuteReader();

            while (Sdr.Read())
                if (Sdr.FieldCount > 0)
                {
                    Model2 DM = new Model2();
                    DM.Id = Convert.ToInt32(Sdr["Id"]);
                    DM.name = Convert.ToString(Sdr["name"]);
                    DM.CarName = Convert.ToString(Sdr["CarName"]);
                    DM.description = Convert.ToString(Sdr["description"]);
                    DM.images = Convert.ToString(Sdr["images"]);
                    DM.OrderCar = Convert.ToBoolean(Sdr["OrderCar"]);

                    lst.Add(DM);
                }
            return lst;
        }

        
        //------------------------------------costomer view items list---------------------------------------------------------
        public List<Model2> GetAll_List()
        {
            List<Model2> lst = new List<Model2>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_addcar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", 3);
            con.Open();
            SqlDataReader Sdr = cmd.ExecuteReader();

            while (Sdr.Read())
                if (Sdr.FieldCount > 0)
                {
                    Model2 DM = new Model2();
                    DM.Id = Convert.ToInt32(Sdr["Id"]);
                    DM.name = Convert.ToString(Sdr["name"]);
                    //DM.DOB = Convert.ToDateTime(Sdr["DOB"]);
                    DM.CarName = Convert.ToString(Sdr["CarName"]);
                    DM.description = Convert.ToString(Sdr["description"]);
                    DM.images = Convert.ToString(Sdr["images"]);
                    DM.OrderCar = Convert.ToBoolean(Sdr["OrderCar"]);
                    lst.Add(DM);
                }
            return lst;
        }
        [HttpGet]
        public ActionResult List2()
        {
            return View(GetAll_List());
        }


        //---------------------------------------delete by seller--------------------------------------------------------
        public int Delete(int Id)
        {
            List<Model2> lst = new List<Model2>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_addcar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@Action", 2);
            con.Open();
            int L = cmd.ExecuteNonQuery();
            con.Close();
            return L;
        }


        //------------------------------------------update by seller-------------------------------------------------------
        public int Update(Model2 DAM)
        {
            List<Model2> lst = new List<Model2>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_addcar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", DAM.Id);
            cmd.Parameters.AddWithValue("@name", DAM.name);
            cmd.Parameters.AddWithValue("@CarName", DAM.CarName);
            cmd.Parameters.AddWithValue("@description", DAM.description);
            cmd.Parameters.AddWithValue("@likemessage", DAM.OrderCar);
            cmd.Parameters.AddWithValue("@Action", 4);
            con.Open();
            int J = cmd.ExecuteNonQuery();
            con.Close();
            return J;

        }
        [HttpGet]
        public ActionResult Update(int ID)
        {
            return View(GetItem().Find(SM => SM.Id == ID));
        }
       


        //------------------------------------------update by customer for items----------------------------------
        public int Updateuser(Model2 DAM)
        {
            List<Model2> lst = new List<Model2>();
            SqlConnection con = new SqlConnection(Con);
            SqlCommand cmd = new SqlCommand("Sp_addcar", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", DAM.Id);
            cmd.Parameters.AddWithValue("@name", DAM.name);
            cmd.Parameters.AddWithValue("@CarName", DAM.CarName);
            cmd.Parameters.AddWithValue("@description", DAM.description);
            cmd.Parameters.AddWithValue("@OrderCar", DAM.OrderCar);
            cmd.Parameters.AddWithValue("@Action", 4);
            con.Open();
            int J = cmd.ExecuteNonQuery();
            con.Close();
            return J;

        }

        [HttpGet]
        public ActionResult Updateuser(int ID)
        {
            return View(GetAll_List().Find(SM => SM.Id == ID));
        }
      
        
        //-----------------------------------welcome page for customer---------------------------------------------------------
        [HttpGet]
        public ActionResult Welcome(Model e)
        {
            Model user = new Model();
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection("Data Source=EDITH;Initial Catalog=loginSignup;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_getsignup", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = Session["email"].ToString();
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<Model> userlist = new List<Model>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Model uobj = new Model();

                        uobj.name = ds.Tables[0].Rows[i]["name"].ToString();
                        uobj.email = ds.Tables[0].Rows[i]["email"].ToString();
                        uobj.phoneNumber = Convert.ToInt64(ds.Tables[0].Rows[i]["phoneNumber"].ToString());
                        uobj.password = ds.Tables[0].Rows[i]["password"].ToString();

                        userlist.Add(uobj);

                    }
                    user.Enrollsinfo = userlist;
                }
                con.Close();

            }
            return View(user);
        }
    }
}