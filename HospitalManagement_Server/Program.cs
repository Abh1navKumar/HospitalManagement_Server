using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace HospitalManagement_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            System.ServiceModel.ServiceHost _wcfUserHost = new System.ServiceModel.ServiceHost(typeof(DataServicesLib.UserDataService));
            _wcfUserHost.Open();
            System.ServiceModel.ServiceHost _wcfDoctorHost = new System.ServiceModel.ServiceHost(typeof(DataServicesLib.DoctorDataService));
            _wcfDoctorHost.Open();
            System.ServiceModel.ServiceHost _wcfPatientHost = new System.ServiceModel.ServiceHost(typeof(DataServicesLib.PatientDataService));
            _wcfPatientHost.Open();
            System.ServiceModel.ServiceHost _wcfAdmintHost = new System.ServiceModel.ServiceHost(typeof(DataServicesLib.AdminDataService));
            _wcfAdmintHost.Open();
            Console.WriteLine("Server Started.... and Listening.");
            Console.ReadLine();
            //string folderName = "C:\\Users\\310246678\\Documents\\Visual Studio 2015\\Projects\\HospitalManagement_Server\\ReportImages\\" + "p002";
            //if (System.IO.Directory.Exists(folderName))
            //{
            //    System.IO.Directory.Delete(folderName, true);
            //}
            //DataServicesLib.UserDataService s = new DataServicesLib.UserDataService();
            //DataModelLib.User u = new DataModelLib.User();
            //u.Name = "abhinav";
            //u.Password = "12345";
            //string a = s.MD5Hash("12345");
            //Console.WriteLine(a);

            //DataModelLib.User u = new DataModelLib.User();
            //DataServicesLib.PatientDataService p = new DataServicesLib.PatientDataService();
            //DataModelLib.Patient pt = new DataModelLib.Patient();
            //pt.Age = 20;
            //pt.DocId = "user003";
            //pt.Id = "p010";
            //pt.Name = "ROn";
            //p.Add_Patient(pt);
            ////DataServicesLib.DoctorDataService s = new DataServicesLib.DoctorDataService();
            //DataServicesLib.AdminDataService a = new DataServicesLib.AdminDataService();
            //DataModelLib.Admin admin = new DataModelLib.Admin();
            //admin.Id = "user001";
            //admin.Name = "abhinav";
            //admin.Password = "1234";
            //string io = a.Get_Id(admin);
            //int a = s.CheckId_replication("admin002", "proc_select_userTable");
            //u.Name = "abhinav";
            //u.Id = "user001";
            //u.Password = "123";
            //Console.WriteLine( s.AuthenticateDoctorByRegion(u));
            //Console.WriteLine(p.getXrayImgLocation("p001"));
            //DataSet a =  s.GetPatientList();
            //Console.WriteLine(a.GetXmlSchema());
            ////u.Name = "abhinav";
            //u.Id = "user001";
            //u.Password = "123";
            //u.Role = "doctor";
            //string role = s.AuthenticateUserById(u);
            //Console.WriteLine(role);
            //s.GetRegions
        }
    }
}
