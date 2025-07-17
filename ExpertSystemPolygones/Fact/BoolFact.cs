namespace ExpertSystemPCL
{
    internal class BoolFact : IFact
    {
        protected string name;
        public string Name()
        {
            return name;
        }

        protected bool value;
        public object Value()
        {
            return value;
        }

        protected int level;
        public int Level()
        {
            return level;
        }
        public void SetLevel(int l)
        {
            level = l;
        }

        protected string question = null;
        public string Question()
        {
            return question;
        }

        public BoolFact(string _name, bool _value, string _question = null, int _level = 0)
        {
            name = _name;
            value = _value;
            question = _question;
            level = _level;
        }

        public override string ToString()
        {
            string res = "";
            if (!value)
            {
                res += "!";
            }
            res += name + " (" + level + ")";
            return res;
        }
    }
}
