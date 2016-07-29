using System.Data.SqlClient;
using System.Data;

namespace DataServicesLib
{
    [System.ServiceModel.ServiceContract]
    public interface IAdminDataService
    {
        [System.ServiceModel.OperationContract]
        DataSet GetAdminList();
        [System.ServiceModel.OperationContract]
        DataSet GetAdminDisplayed();
        [System.ServiceModel.OperationContract]
        int Add_Admin(DataModelLib.Admin _adminUser);
        [System.ServiceModel.OperationContract]
        int Update_Admin(DataModelLib.Admin _adminUser);
        [System.ServiceModel.OperationContract]
        void Delete_Admin(string adminId);
        [System.ServiceModel.OperationContract]
        int Get_Priviledge(string adminId);
        [System.ServiceModel.OperationContract]
        string Get_Id(DataModelLib.Admin user);
    }
}