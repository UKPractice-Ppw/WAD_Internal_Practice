using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace InternalPracticeApp.Models
{
    public class dbHandler
    {
        private SqlConnection con;
        private void Connection()
        {
            string connstr = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            con = new SqlConnection(connstr);
        }
        public bool add_state(State st)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_State VALUES(@state_name)",con);
            cmd.Parameters.AddWithValue("@state_name",st.state_name);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i>=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool add_city(City ct)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_City VALUES(@state_id,@city_name)",con);
            cmd.Parameters.AddWithValue("@state_id",ct.state_id);
            cmd.Parameters.AddWithValue("@city_name", ct.city_name);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i <= 1)
                return true;
            else
                return false;
        }
        public DataTable ddlQuery(string str)
        {
            Connection();
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(str,con);
            sda.Fill(dt);
            con.Close();
            return dt;
        }
        public List<State> GetAllState()
        {
            Connection();
            List<State> states = new List<State>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_State",con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            sda.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {
                states.Add(
                    new State
                    {
                        state_id = Convert.ToInt32(dr["state_id"]),
                        state_name = dr["state_name"].ToString()
                    });
            }
            return states;
        }
        public List<City> GetAllCity()
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_City",con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            List<City> cities = new List<City>();
            con.Open();
            sda.Fill(dt);
            con.Close();
            foreach(DataRow dr in dt.Rows)
            {
                cities.Add(
                    new City
                    {
                        city_id = Convert.ToInt32(dr["city_id"]),
                        state_id = Convert.ToInt32(dr["state_id"]),
                        city_name = dr["city_name"].ToString()
                    });
            }
            return cities;
        }
        public bool addUser(User ur)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("INSERT INTO tbl_User (user_name,user_pass,user_email,state_id,city_id) VALUES(@user_name,@user_pass,@user_email,@state_id,@city_id)",con);
            cmd.Parameters.AddWithValue("@user_name",ur.user_name);
            cmd.Parameters.AddWithValue("@user_pass",ur.user_pass);
            cmd.Parameters.AddWithValue("@user_email",ur.user_email);
            cmd.Parameters.AddWithValue("@state_id",ur.state_id);
            cmd.Parameters.AddWithValue("@city_id",ur.city_id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteCity(int c_id)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("DELETE FROM tbl_City WHERE city_id=@city_id",con);
            cmd.Parameters.AddWithValue("@city_id",c_id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i>=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int Login_User(User ur)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_User WHERE user_name=@us_name AND user_pass=@user_pass",con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@us_name", ur.user_name);
            cmd.Parameters.AddWithValue("@user_pass", ur.user_pass);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            if(sdr.HasRows)
            {
                sdr.Read();
                int id = (int)sdr["user_id"];
                return id;
            }
            else
            {
                throw new Exception("Please Enter Correct User_name and Password");
                return 0;
            }
        }
        public bool Update_city(int c_id,City ct)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("UPDATE tbl_City SET city_name=@city_name,state_id=@state_id WHERE city_id=@city_id", con);
            cmd.Parameters.AddWithValue("@city_name",ct.city_name);
            cmd.Parameters.AddWithValue("@state_id",ct.state_id);
            cmd.Parameters.AddWithValue("@city_id",c_id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if(i>=1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<City> GetCitiesByState(int? state_id)
        {
            Connection();
            List<City> citiList = new List<City>();
            string qry = "SELECT * FROM tbl_City";
            if(state_id != null && state_id>0)
            {
                qry += " WHERE state_id="+state_id;
            }

            SqlCommand cmd = new SqlCommand(qry,con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                City ct = new City
                {
                    city_id = (int)sdr["city_id"],
                    state_id = (int)sdr["state_id"],
                    city_name = sdr["city_name"].ToString()
                };
                citiList.Add(ct);
            }
            con.Close();
            return citiList;
        }
    }
}