using System.Data.SqlClient;
using System.Data;
namespace DataServicesLib
{
    [System.ServiceModel.ServiceContract]
    public interface IPatientDataService
    {
        [System.ServiceModel.OperationContract]
        DataSet GetPatientList();
        [System.ServiceModel.OperationContract]
        DataSet GetPatientListByDocId(string DocId);
        [System.ServiceModel.OperationContract]
        int CheckPatientForDoctor(string DocId);
        [System.ServiceModel.OperationContract]
        int Add_Patient(DataModelLib.Patient _patient);
        [System.ServiceModel.OperationContract]
        int Update_Patient(DataModelLib.Patient _patient);
        [System.ServiceModel.OperationContract]
        int Delete_Patient(string patientId);
        [System.ServiceModel.OperationContract]
        int Add_Report(DataModelLib.Report _report, string _commandText);
        [System.ServiceModel.OperationContract]
        DataSet GetXrayData(string patient_id);
        [System.ServiceModel.OperationContract]
        DataSet GetEcgData(string patient_id);
        [System.ServiceModel.OperationContract]
        DataSet GetMriData(string patient_id);
        [System.ServiceModel.OperationContract]
        string getXrayImgLocation(string patient_id);
        [System.ServiceModel.OperationContract]
        string getEcgImgLocation(string patient_id);
        [System.ServiceModel.OperationContract]
        string getMriImgLocation(string patient_id);
    }
}