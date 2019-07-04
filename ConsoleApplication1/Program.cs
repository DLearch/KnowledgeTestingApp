using ContractLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Connect("net.tcp://localhost:4222/IContract");
            Channel.SignIn("Lector", "565656");
            Console.WriteLine(5);
            var rs = Channel.GetRatingSystems();
            var cats = Channel.GetCategories();
            int i = 1;
            Channel.AddTest(new ContractLib.TestComponents.TestInfo()
            {
               Title = "Test" + i,
               IsPrivate = false,
               IsQuestionsMix  =  false,
               Category  = cats.First(),
               RatingSystem = rs.First(),
               AddDate = DateTime.Now,
               Attempts = null,
               Description = "asd",
               Duration = null,
               Interval = null,
               Mark = null,
               QuestionsCount = 1,
               UsedAttempts = 0,
               User = Channel.GetUser()
            }, new List<ContractLib.TestComponents.QuestionComponents.QuestionInfo>()
            {
                new ContractLib.TestComponents.QuestionComponents.QuestionInfo()
                {
                    IsAnswersMix = true,
                    IsRadio = true,
                    Text = "question",
                    Weight = 1,
                    Image = null,
                    Answers = new List<ContractLib.TestComponents.QuestionComponents.AnswerInfo>()
                    {
                        new ContractLib.TestComponents.QuestionComponents.AnswerInfo()
                        {
                            IsCorrect = false,
                            Text = "false answer",
                            Image = null
                        },
                        new ContractLib.TestComponents.QuestionComponents.AnswerInfo()
                        {
                            IsCorrect = true,
                            Text = "true answer",
                            Image = null
                        }
                    }
                }
            });
        }
        #region Channel, Connect(-)

        public static IContract Channel { get; set; }

        static void Connect(string uri)
        {
            Uri address = new Uri(uri);
            NetTcpBinding binding = new NetTcpBinding();
            binding.MaxReceivedMessageSize = int.MaxValue;
            EndpointAddress endpoint = new EndpointAddress(address);
            ChannelFactory<IContract> factory = new ChannelFactory<IContract>(binding, endpoint);
            Channel = factory.CreateChannel();
        }

        #endregion

    }
}
