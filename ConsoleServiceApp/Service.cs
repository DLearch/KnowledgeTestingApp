using ConsoleServiceApp.Models;
using ContractLib;
using ContractLib.TestComponents;
using ContractLib.TestComponents.QuestionComponents;
using ContractLib.UserComponents;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServiceApp
{
    public class Service : IContract
    {
        User user;
        ModelDB db;

        static Exception wrongDataException = new FaultException("Неправильные данные.");
        static Exception userAccessException = new FaultException("У вас нет доступа.");
        static Exception userInitializedException = new FaultException("Вы уже вошли.");

        public Service()
        {
            db = new ModelDB();
        }

        #region Sign

        public bool SignIn(string name, string password)
        {
            if (user != null)
                throw userInitializedException;

            if (!UserValidator.IsValidName(name)
                || !UserValidator.IsValidPassword(password))
                throw wrongDataException;

            user = db.Users.FirstOrDefault(u => u.Name == name);

            if (user == null || user.Password != password)
            {
                user = null;
                return false;
            }

            return true;
        }

        public void SignOut()
        {
            if (user == null)
                throw userAccessException;

            user = null;
        }
        #endregion
        #region Register

        public void Register(UserInfo user)
        {
            if (this.user != null)
                throw userInitializedException;

            if (!UserValidator.IsValid(user)
                || !UserNameIsAvailable(user.Name)
                || !UserEmailIsAvailable(user.Email))
                throw wrongDataException;

            user.RegDate = DateTime.Now;

            db.Users.Add(new User(user));
            db.SaveChanges();

            this.user = db.Users.FirstOrDefault(u => u.Name == user.Name);
        }

        public bool UserNameIsAvailable(string name)
        {
            if (!UserValidator.IsValidName(name))
                throw wrongDataException;

            return db.Users.All(u => u.Name != name);
        }

        public bool UserEmailIsAvailable(string email)
        {
            if (!UserValidator.IsValidEmail(email))
                throw wrongDataException;

            return db.Users.All(u => u.Email != email);
        }
        #endregion
        #region Get User

        public UserInfo GetUser()
        {
            if (user == null)
                return null;

            return GetUserInfo(u => u.Id == user.Id);
        }

        public UserInfo GetUserById(int id)
        {
            return GetUserInfo(u => u.Id == id);
        }

        public UserInfo GetUserByName(string name)
        {
            return GetUserInfo(u => u.Name == name);
        }

        UserInfo GetUserInfo(Func<User, bool> predicate)
        {
            User user = db.Users.FirstOrDefault(predicate);

            if (user == null)
                return null;

            return user.GetEncryptedInfo();
        }
        #endregion
        #region User Edit

        public bool ChangeUserPassword(string oldPassword, string newPassword)
        {
            if (user == null)
                throw userAccessException;

            if (!UserValidator.IsValidPassword(oldPassword)
                || !UserValidator.IsValidPassword(newPassword))
                throw wrongDataException;

            if (oldPassword != user.Password)
                return false;

            user.Password = newPassword;
            db.SaveChanges();

            return true;
        }

        public void ChangeUserEmailVisiblity()
        {
            if (user == null)
                throw userAccessException;

            user.EmailIsVisible = !user.EmailIsVisible;
            db.SaveChanges();
        }
        #endregion
        #region Test Add, Edit, Remove

        public void AddTest(TestInfo info, List<QuestionInfo> questions)
        {
            if (user == null)
                throw userAccessException;

            if (!TestValidator.IsValid(info)
                || !TestTitleIsAvailable(info.Title)
                || !TestValidator.IsValidQuestions(questions))
                throw wrongDataException;

            Test test = new Test(info)
            {
                AddDate = DateTime.Now,
                UserId = user.Id
            };
            
            test = db.Tests.Add(test);
            db.SaveChanges();

            AddQuestions(test, questions);

            return;
        }

        public bool EditTest(TestInfo info, List<QuestionInfo> questions)
        {
            if (user == null)
                throw userAccessException;

            if (!TestValidator.IsValid(info)
                || !TestTitleIsAvailable(info.Title, info.Id)
                || !TestValidator.IsValidQuestions(questions))
                throw wrongDataException;

            Test test = db.Tests.FirstOrDefault(t => t.Id == info.Id);

            if (test == null)
                return false;

            if (test.UserId != user.Id)
                throw userAccessException;

            RemoveQuestions(test);

            test.UserId = user.Id;
            test.Title = info.Title;
            test.Description = info.Description;
            test.Duration = info.Duration;
            test.Attempts = info.Attempts;
            test.Interval = info.Interval;
            test.AddDate = DateTime.Now;
            test.CategoryId = info.Category.Key;
            test.RatingSystemId = info.RatingSystem.Key;
            test.IsPrivate = info.IsPrivate;
            test.IsQuestionsMix = info.IsQuestionsMix;
            
            db.SaveChanges();
            
            AddQuestions(test, questions);

            return true;
        }

        public bool RemoveTest(int id)
        {
            if (user == null)
                throw userAccessException;

            Test test = db.Tests.FirstOrDefault(t => t.Id == id);

            if (test == null)
                return false;

            if (test.UserId != user.Id)
                throw userAccessException;

            RemoveQuestions(test);
            db.TestResults.RemoveRange(test.TestResults);
            db.Tests.Remove(test);

            db.SaveChanges();

            return true;
        }

        public bool TestTitleIsAvailable(string title, int? testId = null)
        {
            return db.Tests.All(t => (testId != null && t.Id == testId) || t.Title != title);
        }
        #endregion
        #region Questions Add, Remove

        void AddQuestions(Test test, ICollection<QuestionInfo> questions)
        {
            Question question;
            Answer answer;
            foreach (var questionInfo in questions)
            {
                question = new Question(questionInfo);
                question.TestId = test.Id;
                question = db.Questions.Add(question);
                db.SaveChanges();

                foreach (var answerInfo in questionInfo.Answers)
                {
                    answer = new Answer(answerInfo);
                    answer.QuestionId = question.Id;
                    db.Answers.Add(answer);
                }
            }
            db.SaveChanges();
        }

        void RemoveQuestions(Test test)
        {
            foreach (var item in test.Questions)
                db.Answers.RemoveRange(item.Answers);
            db.Questions.RemoveRange(test.Questions);
            db.SaveChanges();
        }
        #endregion
        #region Get Categories & RatingSystems

        public Dictionary<int, string> GetCategories()
        {
            return db.Categories.ToDictionary(c => c.Id, c => c.Name);
        }

        public Dictionary<int, string> GetRatingSystems()
        {
            return db.RatingSystems.ToDictionary(c => c.Id, c => c.Name);
        }
        #endregion
        #region Get Questions, Test, TestsId

        public List<QuestionInfo> GetQuestions(int testId)
        {
            if (user == null)
                throw userAccessException;

            Test test = db.Tests.FirstOrDefault(t => t.Id == testId);

            if (test == null)
                return null;

            if (test.UserId != user.Id)
                throw userAccessException;

            return test.Questions.Select(q => q.GetInfo()).ToList();
        }
        public TestInfo GetTest(int id)
        {
            if (user == null)
                throw userAccessException;

            Test test = db.Tests.FirstOrDefault(t => t.Id == id);

            if (test == null)
                return null;

            return test.GetInfo(user.Id);
        }

        public List<int> GetTestsId(TestFilter filter = null, int? userId = null)
        {
            List<Test> tests = db.Tests.ToList();

            if (userId.HasValue)
                tests = tests.Where(t => t.UserId == userId.Value).ToList();

            if (!userId.HasValue || userId.HasValue && userId != user.Id)
                tests = tests.Where(t => !t.IsPrivate).ToList();

            if (filter != null)
                tests = tests.Where(t =>
                        (string.IsNullOrEmpty(filter.Title) || t.Title.Contains(filter.Title))
                    && (filter.MinQuestionsCount == null || filter.MinQuestionsCount <= t.Questions.Count())
                    && (filter.MaxQuestionsCount == null || filter.MaxQuestionsCount > t.Questions.Count())
                    && (filter.Categories == null || filter.Categories.Count == 0 || filter.Categories.Any(c => c == t.CategoryId))
                    && (filter.RatingSystems == null || filter.RatingSystems.Count == 0 || filter.RatingSystems.Any(rs => rs == t.RatingSystemId))
                ).ToList();

            return tests.Select(t => t.Id).ToList();
        }
        #endregion
        #region Start & Finish Test

        TestResult testResult;

        public List<QuestionInfo> StartTest(int id)
        {
            if (user == null)
                throw userAccessException;

            if (testResult != null)
                throw new Exception("Тест уже начат.");

            Test test = db.Tests.FirstOrDefault(t => t.Id == id);

            if (test == null)
                throw wrongDataException;

            if (test.IsPrivate && user.Id != test.UserId)
                throw userAccessException;

            if(test.TestResults != null)
                testResult = test.TestResults.FirstOrDefault(tr => tr.UserId == user.Id);

            if (testResult == null)
            {
                db.TestResults.Add(new TestResult()
                {
                    Attempts = 0,
                    StartTime = DateTime.Now,
                    Duration = new TimeSpan(0),
                    TestId = test.Id,
                    UserId = user.Id,
                    Score = 0
                });

                db.SaveChanges();

                testResult = test.TestResults.FirstOrDefault(tr => tr.UserId == user.Id);

                if (testResult == null)
                    throw wrongDataException;
            }

            if (testResult.Attempts == test.Attempts)
                throw wrongDataException;

            testResult.StartTime = DateTime.Now;
            testResult.Duration = new TimeSpan(0);
            if (test.UserId != user.Id)
                testResult.Attempts++;
            testResult.Score = 0;
            db.SaveChanges();
            return test.Questions.Select(q => q.GetEncryptedInfo()).ToList();
        }
        public void FinishTest(Dictionary<int, List<int>> questionsResults)
        {
            if (user == null)
                throw userAccessException;

            if (testResult == null)
                throw new Exception("Тест не начат.");

            if (questionsResults == null)
                throw wrongDataException;

            testResult.Duration = DateTime.Now - testResult.StartTime;
            if (testResult.Test.Duration == null || testResult.Test.Duration.Value >= testResult.Duration)
                foreach (var question in testResult.Test.Questions)
                    if (questionsResults.ContainsKey(question.Id)
                        && question.Answers.Count(a => a.IsCorrect) == questionsResults[question.Id].Count
                        && question.Answers.Where(a => a.IsCorrect).All(a => questionsResults[question.Id].Any(id => id == a.Id)))
                        testResult.Score += question.Weight;

            db.SaveChanges();

            testResult = null;
        }
        #endregion
        #region Invitations Get, Send, Remove 

        public List<InvitationInfo> GetInvitations()
        {
            if (user == null)
                throw userAccessException;

            return db.Invitations
                .Where(i => i.Addressee.Id == user.Id && db.Tests.Any(t => t.Id == i.TestId))
                .ToList()
                .Select(i => i.GetEncryptedInfo())
                .ToList();
        }

        public void SendInvitation(InvitationInfo info)
        {
            if (user == null)
                throw userAccessException;

            if (info == null)
                throw wrongDataException;

            Test test = db.Tests.FirstOrDefault(t => t.Id == info.TestId);

            if (test == null)
                throw wrongDataException;

            if(test.IsPrivate && test.User.Id != user.Id && test.Invitations.All(i => i.AddresseeId != user.Id && !i.IsTransferable))
                throw userAccessException;

            Invitation invitation = test.Invitations.FirstOrDefault(i => i.SenderId == user.Id && i.AddresseeId == info.Addressee.Id);

            if (invitation != null)
            {
                invitation.IsTransferable = invitation.IsTransferable;
                invitation.Date = DateTime.Now;
                db.SaveChanges();
                return;
            }

            invitation = new Invitation(info);
            invitation.Date = DateTime.Now;
            invitation.SenderId = user.Id;

            db.Invitations.Add(invitation);
            db.SaveChanges();
        }

        public void RemoveInvitation(int id)
        {
            if (user == null)
                throw userAccessException;

            Invitation invitation = db.Invitations.FirstOrDefault(i => i.Id == id);

            if (invitation == null)
                throw wrongDataException;

            if(invitation.AddresseeId != user.Id && invitation.SenderId != user.Id)
                throw userAccessException;

            db.Invitations.Remove(invitation);
            db.SaveChanges();
        }
        #endregion
    }
}