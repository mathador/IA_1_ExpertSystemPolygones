using ExpertSystemPCL.Interfaces;

namespace ExpertSystemPCL;

internal static class FactFactory
{
    internal static IFact Fact(IFact f, Motor m)
    {
        IFact newFact;
        if (f.GetType().Equals(typeof(IntFact)))
        {
            int value = m.AskIntValue(f.Question());
            newFact = new IntFact(f.Name(), value, null, 0);
        }
        else
        {
            bool value = m.AskBoolValue(f.Question());
            newFact = new BoolFact(f.Name(), value, null, 0);
        }
        return newFact;
    }

    internal static IFact Fact(string factStr)
    {
        factStr = factStr.Trim();
        if (factStr.Contains("="))
        {
            string[] nameValue = factStr.Split(new string[] { "=", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            if (nameValue.Length >= 2)
            {
                string question = null;
                if (nameValue.Length == 3)
                {
                    question = nameValue[2].Trim();
                }
                return new IntFact(nameValue[0].Trim(), int.Parse(nameValue[1].Trim()), question);
            }
            else
            {
                return null;
            }
        }
        else
        {
            bool value = true;
            if (factStr.StartsWith("!"))
            {
                value = false;
                factStr = factStr.Substring(1).Trim(); // On enlève le ! du nom
            }
            string[] nameQuestion = factStr.Split(new string[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            string question = null;
            if (nameQuestion.Length == 2)
            {
                question = nameQuestion[1].Trim();
            }
            return new BoolFact(nameQuestion[0].Trim(), value, question);
        }
    }
}
