using DataModelLib;
using System;
using System.Security.Cryptography;

namespace DataServicesLib
{
    [System.ServiceModel.ServiceContract]
    public interface IUserDataService
    {
        [System.ServiceModel.OperationContract]
        string AuthenticateUser(DataModelLib.User user);

        [System.ServiceModel.OperationContract]
        string AuthenticateUserById(DataModelLib.User user);
        [System.ServiceModel.OperationContract]
        string MD5Hash(string input);
        [System.ServiceModel.OperationContract]
        int CheckId_replication(String Id, String _commandText);
        [System.ServiceModel.OperationContract]
        string Get_Id(DataModelLib.User user);
    }
}