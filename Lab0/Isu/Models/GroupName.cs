using Isu.Exceptions;

namespace Isu.Models
{
    public class GroupName
    {
        public GroupName(string name)
        {
            if (Validation(name))
            {
                Name = name;
            }
            else
            {
                throw new GroupNameFormatException();
            }
        }

        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }

        private bool Validation(string name)
        {
            if (name.Length != 6)
            {
                return false;
            }
            else
            {
                if (!name.StartsWith("M"))
                {
                    return false;
                }
                else
                {
                    if (!name.Substring(1).StartsWith("3"))
                    {
                        return false;
                    }
                    else
                    {
                        if (!int.TryParse(name.Substring(2), out _))
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
    }
}