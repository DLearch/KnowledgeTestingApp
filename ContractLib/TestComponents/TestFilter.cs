using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractLib.TestComponents
{
    public class TestFilter
    {
        public string Title { get; set; }

        public int? MinQuestionsCount { get; set; }

        public int? MaxQuestionsCount { get; set; }

        public List<int> Categories { get; set; }

        public List<int> RatingSystems { get; set; }
    }
}
