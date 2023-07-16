using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities
{
    public class Student
    {
        public Student(string firstName, string lastName, GroupName group)
        {
            if (Validation(firstName, lastName))
            {
                FirstName = firstName;
                LastName = lastName;
            }
            else
            {
                throw new StudentNameException();
            }

            ID = IDList.GenerateID();

            Group = group;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public int ID { get; }

        public GroupName Group { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + ID + " " + Group + "\n";
        }

        private bool Validation(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(lastName))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}