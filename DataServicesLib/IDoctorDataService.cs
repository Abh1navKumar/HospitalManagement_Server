using System.Collections.Generic;
using System.Data;
namespace DataServicesLib
{
    [System.ServiceModel.ServiceContract]
    public interface IDoctorDataService
    {
        [System.ServiceModel.OperationContract]
        DataSet GetDoctorList();
        [System.ServiceModel.OperationContract]
        int Add_Doctor(DataModelLib.Doctor _docUser);
        [System.ServiceModel.OperationContract]
        int Update_Doctor(DataModelLib.Doctor _docUser);
        [System.ServiceModel.OperationContract]
        void Delete_Doctor(string docId);
        [System.ServiceModel.OperationContract]
        List<string> GetRegions();
        [System.ServiceModel.OperationContract]
        string AuthenticateDoctorByRegion(DataModelLib.Doctor _docUser);
        [System.ServiceModel.OperationContract]
        string GetDoctorIdByRegion(string region);
        [System.ServiceModel.OperationContract]
        int VerifyDoctor(string docId);
    }
}