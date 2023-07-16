using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities
{
    public class Subject
    {
        public Subject(SubjectName subjectName, Faculty faculty, int limit)
        {
            if (!Validation(limit))
            {
                throw new IncorrectInfoException();
            }

            SubjectName = subjectName;
            Faculty = faculty;
            Limit = limit;
            Amount = 0;
            FlowCounter = 0;
            FlowList = new List<Flow>();
        }

        public SubjectName SubjectName { get; }

        public Faculty Faculty { get; }

        public int Limit { get; }

        public int Amount { get; private set; }

        public int FlowCounter { get; private set; }

        public List<Flow> FlowList { get; }

        public Flow AddFlow(int limit)
        {
            if (limit <= 0 || limit > Limit)
            {
                throw new Exception();
            }

            FlowCounter++;
            var flow = new Flow(FlowCounter, limit, SubjectName);
            Amount += limit;
            FlowList.Add(flow);

            return flow;
        }

        public bool Validation(int limit)
        {
            if (limit <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
