using ContractLib.TestComponents;
using ContractLib.TestComponents.QuestionComponents;
using ContractLib.UserComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ContractLib
{
    [ServiceContract]
    public interface IContract
    {
        [OperationContract]
        bool SignIn(string name, string password);
        [OperationContract]
        void SignOut();

        [OperationContract]
        void Register(UserInfo user);
        [OperationContract]
        bool UserNameIsAvailable(string name);
        [OperationContract]
        bool UserEmailIsAvailable(string email);

        [OperationContract]
        bool ChangeUserPassword(string oldPassword, string newPassword);
        [OperationContract]
        void ChangeUserEmailVisiblity();

        [OperationContract]
        UserInfo GetUser();
        [OperationContract]
        UserInfo GetUserById(int id);
        [OperationContract]
        UserInfo GetUserByName(string name);

        [OperationContract]
        Dictionary<int, string> GetCategories();
        [OperationContract]
        Dictionary<int, string> GetRatingSystems();

        [OperationContract]
        void AddTest(TestInfo test, List<QuestionInfo> questions);
        [OperationContract]
        bool EditTest(TestInfo test, List<QuestionInfo> questions);
        [OperationContract]
        bool RemoveTest(int id);
        [OperationContract]
        bool TestTitleIsAvailable(string title, int? testId = null);

        [OperationContract]
        List<QuestionInfo> GetQuestions(int testId);
        [OperationContract]
        TestInfo GetTest(int id);
        [OperationContract]
        List<int> GetTestsId(TestFilter filter = null, int? userId = null);

        [OperationContract]
        List<QuestionInfo> StartTest(int testId);
        [OperationContract]
        void FinishTest(Dictionary<int, List<int>> questionsResults);
        
        [OperationContract]
        void SendInvitation(InvitationInfo invitation);
        [OperationContract]
        List<InvitationInfo> GetInvitations();
        [OperationContract]
        void RemoveInvitation(int id);
    }
}
