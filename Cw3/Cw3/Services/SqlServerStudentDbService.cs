using Cw3.DIOs.Requests;
using Cw3.DTOs.Requests;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {
        public string EnrollStudent(EnrollStudentRequest request)
        {
            var st = new Student();
            var semester = new Enrollment();
            var conBuilder = new SqlConnectionStringBuilder();
            conBuilder.DataSource = "db-mssql";
            conBuilder.InitialCatalog = "s17025";
            conBuilder.IntegratedSecurity = true;
            //conBuilder.MultipleActiveResultSets = true;
            string ConString = conBuilder.ConnectionString;

            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;


                try
                {
                    //1. Czy studia istnieją?
                    com.CommandText = "SELECT IdStudy FROM STUDIES WHERE NAME=@name";
                    com.Parameters.AddWithValue("name", request.Studies);
                    
                    var dr = com.ExecuteReader();
                    
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return "Brak studiow";
                    }
                    int idstudies = (int)dr["IdStudy"];
                    dr.Close();
                    com.CommandText = "SELECT * FROM ENROLLMENT WHERE SEMESTER=1 AND IDSTUDY=@idstudies";
                    com.Parameters.AddWithValue("idstudies", idstudies);
                    dr = com.ExecuteReader();
                
                    if (!dr.Read())
                    {
                        //2.Szukam Max idEnrollment
                        com.CommandText = "SELECT MAX(IdEnrollment)'idEnrollment' from Enrollment;";
                        dr = com.ExecuteReader();
                        int maxEnrollment = (int)dr["idEnrollment"] + 1;
                        var date = DateTime.Today;
                        com.CommandText = "INSERT INTO ENROLLMENT VALUES(" + maxEnrollment + "1, " + idstudies + ", " + date.ToShortDateString() + ")";
                    }
                    dr.Close();
                    com.CommandText = "SELECT * FROM ENROLLMENT WHERE IDSTUDY = @idstudy AND SEMESTER = 1";
                    com.Parameters.AddWithValue("idstudy", idstudies);
                    dr = com.ExecuteReader();
                    int enroll = (int)dr["idEnrollment"];
                    semester.Semester = (int)dr["Semester"];
                    //dr.Close();
                    

                    com.CommandText = "INSERT INTO STUDENT(INDEXNUMBER, FIRSTNAME, LASTNAME, BIRTHDATE, IDENROLLMENT) VALUES(@request.IndexNumber, @request.FirstName, @request.LastName, @request.BirthDate, @maxEnrollment)";
                    com.Parameters.AddWithValue("request.IndexNumber", request.IndexNumber);
                    com.Parameters.AddWithValue("request.FirstName", request.FirstName);
                    com.Parameters.AddWithValue("request.LastName", request.LastName);
                    com.Parameters.AddWithValue("request.BirthDate", DateTime.Parse(request.BirthDate));
                    com.Parameters.AddWithValue("maxEnrollment", enroll);

                    com.ExecuteNonQuery();
                    dr.Close();


                    
                    tran.Commit();
                    con.Close();

                    //wrocic bo trzeba zwrocic semester klasy enrollment 
                    return Convert.ToString(semester.Semester);
                }
               catch (SqlException e)
                {
                    tran.Rollback();
                    return "Niespodziewany blad";
                }
            }
            
           
        }

        public string PromoteStudents(PropomoteStudentsRequest request)
        {
            var conBuilder = new SqlConnectionStringBuilder();
            conBuilder.DataSource = "db-mssql";
            conBuilder.InitialCatalog = "s17025";
            conBuilder.IntegratedSecurity = true;
            string ConString = conBuilder.ConnectionString;

            

            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;

                try
                {
                    com.CommandText = "SELECT S.IdStudy FROM STUDIES S, ENROLLMENT WHERE NAME=@name AND SEMESTER=@semester";
                    com.Parameters.AddWithValue("name", request.Studies);
                    com.Parameters.AddWithValue("semester", request.Semester);

                    var dr = com.ExecuteReader();

                    if (!dr.Read())
                    {
                        tran.Rollback();
                        return "Brak studiow";
                    }
                    int idstudy = (int)dr["IdStudy"];
                    com.CommandText = "EXEC PromoteStudents @name, @semester";
                    com.ExecuteNonQuery();
                    dr.Close();

                    com.CommandText = "SELECT * FROM ENROLLMENT WHERE IDSTUDY = @idstudy AND SEMESTER=@semester+1";
                    com.Parameters.AddWithValue("idstudy", idstudy);
                    com.Parameters.AddWithValue("semester", request.Semester);
                    var enrollment = new Enrollment();
                    enrollment.IdEnrollment = (int)dr["IdEnrollment"];
                    enrollment.IdStudy = idstudy;
                    enrollment.Semester = (int)dr["Semester"];
                    enrollment.StartDate = Convert.ToString(dr["StartDate"]);
                    com.ExecuteNonQuery();
                    dr.Close();
                    tran.Commit();

                    return "Ok(201), idWpisu: " +
                        Convert.ToString(enrollment.IdEnrollment) + ", idStudy: " +
                        Convert.ToString(enrollment.IdStudy) + ", semester: " +
                        Convert.ToString(enrollment.Semester) + ", data rozpoczecia: " +
                        Convert.ToString(enrollment.StartDate);



                }
                catch(SqlException e)
                {
                    return "Niespodziewany blad!";
                }
            }

            
        }
    }
}
