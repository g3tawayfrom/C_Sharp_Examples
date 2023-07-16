using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Services
{
    public class IsuExtraService : Isu.Services.IsuService
    {
        private List<Subject> subjectList = new List<Subject>();
        private List<Flow> flowList = new List<Flow>();
        private List<GroupExtra> groupExtraList = new List<GroupExtra>();

        private List<StudentNew> studentNewList = new List<StudentNew>();
        private List<GroupNew> groupNewList = new List<GroupNew>();

        public StudentNew AddStudentNew(GroupName groupName, string firstName, string lastName)
        {
            Student student = AddStudent(FindGroup(groupName), firstName, lastName);

            var studentNew = new StudentNew(student);
            studentNewList.Add(studentNew);

            return studentNew;
        }

        public GroupNew AddGroupNew(GroupName groupName)
        {
            Group group = AddGroup(groupName);

            var groupNew = new GroupNew(group);
            groupNewList.Add(groupNew);

            return groupNew;
        }

        public Subject AddExtra(SubjectName subjectName, Faculty faculty, int limit)
        {
            if (limit <= 0)
            {
                throw new IncorrectInfoException();
            }

            Subject? subject_test = subjectList.FirstOrDefault(x => x.SubjectName == subjectName);

            if (subject_test != null)
            {
                throw new AlreadyExistException();
            }

            var subject = new Subject(subjectName, faculty, limit);
            subjectList.Add(subject);

            return subject;
        }

        public Subject GetSubject(SubjectName subjectName)
        {
            Subject? subject = subjectList.FirstOrDefault(x => x.SubjectName == subjectName);

            if (subject == null)
            {
                throw new NotExistException();
            }

            return subject;
        }

        public Flow AddFlowToExtra(int limit, Subject subject)
        {
            if (limit > subject.Limit - subject.Amount)
            {
                throw new IsFullException();
            }

            Flow flow = subject.AddFlow(limit);
            flowList.Add(flow);

            return flow;
        }

        public Flow GetSpecFlow(int flowNumber, Subject subject)
        {
            Flow? flow = subject.FlowList.FirstOrDefault(x => x.FlowNumber == flowNumber);

            if (flow == null)
            {
                throw new NotExistException();
            }

            return flow;
        }

        public List<Flow> GetAllFlows(Subject subject)
        {
            List<Flow> flows = subject.FlowList;

            if (flows.Count == 0)
            {
                throw new NotExistException();
            }

            return flows;
        }

        public GroupExtra AddGroupToFlow(Teacher teacher, int auditorium, int limit, Flow flow)
        {
            if (limit > flow.Limit - flow.Amount)
            {
                throw new IsFullException();
            }

            GroupExtra groupExtra = flow.AddGroupExtra(teacher, auditorium, limit);
            groupExtraList.Add(groupExtra);

            return groupExtra;
        }

        public void AddLessonToFlow(Flow flow, Lesson lesson, Day day, Week week)
        {
            Timetable timetable = flow.Timetable;

            timetable.AddLesson(lesson, day, week);
        }

        public void AddLessonToGroup(GroupNew group, Lesson lesson, Day day, Week week)
        {
            Timetable timetable = group.Timetable;

            timetable.AddLesson(lesson, day, week);
        }

        public bool TTIntersection(Timetable timetable_1, Timetable timetable_2)
        {
            for (int i = 0; i < 12; i++)
            {
                Lesson? lesson = timetable_1.Days[i].FirstOrDefault(x => timetable_2.Days[i].Contains(x));
                Console.WriteLine(lesson);

                if (lesson != 0)
                {
                    return true;
                }
            }

            return false;
        }

        public Teacher AddTeacher(string firstName, string lastName)
        {
            var teacher = new Teacher(firstName, lastName);

            return teacher;
        }

        public void SubStudentToExtra(StudentNew student, Subject subject)
        {
            if (student.Faculty.Equals(subject.Faculty))
            {
                throw new UnexpectedIntersectionException();
            }

            if (student.ExtrasList.Count == 2)
            {
                throw new NoMoreOptionsException();
            }

            foreach (Guid extra in student.ExtrasList)
            {
                GroupExtra? groupExtra_test = groupExtraList.FirstOrDefault(g => g.GroupId == extra);

                if (groupExtra_test == null)
                {
                    throw new NotExistException();
                }

                if (groupExtra_test.SubjectName == subject.SubjectName)
                {
                    throw new UnexpectedIntersectionException();
                }
            }

            Group group = FindGroup(student.Student.Group);

            GroupNew? groupNew = groupNewList.FirstOrDefault(x => x.Group == group);

            if (groupNew == null)
            {
                throw new NotExistException();
            }

            List<Flow> flows = GetAllFlows(subject);

            Flow? flow = null;
            int min = subject.Limit;

            foreach (Flow flow_ in flows)
            {
                int amount = flow_.Limit;
                if (!TTIntersection(groupNew.Timetable, flow_.Timetable))
                {
                    bool checker = true;

                    foreach (Guid extra in student.ExtrasList)
                    {
                        GroupExtra? groupExtra1 = groupExtraList.FirstOrDefault(x => x.GroupId == extra);

                        if (groupExtra1 == null)
                        {
                            throw new NotExistException();
                        }

                        Flow? flow1 = flowList.FirstOrDefault(x => x.GroupExtraList.Contains(groupExtra1));

                        if (flow1 == null)
                        {
                            throw new NotExistException();
                        }

                        if (TTIntersection(flow1.Timetable, flow_.Timetable))
                        {
                            checker = false;
                        }
                    }

                    if (amount < min && checker)
                    {
                        flow = flow_;
                        min = amount;
                    }
                }
            }

            if (flow == null)
            {
                throw new NotExistException();
            }

            List<GroupExtra> groups = flow.GroupExtraList;

            GroupExtra? groupExtra = null;

            min = flow.Limit;

            foreach (GroupExtra groupExtra_ in groups)
            {
                int amount = groupExtra_.GetAmount();
                if (amount < min)
                {
                    groupExtra = groupExtra_;
                    min = amount;
                }
            }

            if (groupExtra == null)
            {
                throw new NotExistException();
            }

            groupExtra.AddStudent(student);
            student.ExtrasList.Add(groupExtra.GroupId);
        }

        public void UnsubStudentFromExtra(StudentNew student, Subject subject)
        {
            List<Flow> flows = GetAllFlows(subject);

            GroupExtra? groupExtra = null;

            foreach (Flow flow in flows)
            {
                groupExtra = flow.GroupExtraList.FirstOrDefault(g => student.ExtrasList.Contains(g.GroupId));

                if (groupExtra != null)
                {
                    break;
                }
            }

            if (groupExtra == null)
            {
                throw new NotExistException();
            }

            groupExtra.RemoveStudent(student.Student.ID);
            student.ExtrasList.Remove(groupExtra.GroupId);
        }

        public List<StudentNew> FindBusyStudents(Guid groupId)
        {
            GroupExtra? groupExtra = groupExtraList.FirstOrDefault(g => g.GroupId == groupId);

            if (groupExtra == null)
            {
                throw new NotExistException();
            }

            return groupExtra.StudentsList;
        }

        public List<StudentNew> FindFreeStudent(GroupNew group)
        {
            var students = studentNewList.Where(x => x.Student.Group == group.Group.GroupName).ToList();

            var freeStudents = students.Where(s => s.ExtrasList.Count == 0).ToList();

            if (!freeStudents.Any())
            {
                throw new IsEmptyException();
            }

            return freeStudents;
        }
    }
}
