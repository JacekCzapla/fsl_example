using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLS;
using FLS.Rules;

namespace Fuzzy_FLS_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var water = new LinguisticVariable("Water");
            var cold = water.MembershipFunctions.AddTrapezoid("Cold", 0, 0, 20, 40);
            var warm = water.MembershipFunctions.AddTriangle("Warm", 30, 50, 70);
            var hot = water.MembershipFunctions.AddTrapezoid("Hot", 50, 80, 100, 100);

            var sugar = new LinguisticVariable("Sugar");
            var big = water.MembershipFunctions.AddTrapezoid("big", 0, 0, 20, 40);
            var small = water.MembershipFunctions.AddTrapezoid("small", 50, 80, 100, 100);


            var power = new LinguisticVariable("Power");
            var low = power.MembershipFunctions.AddTriangle("Low", 0, 25, 50);
            var medium = power.MembershipFunctions.AddTriangle("Medium", 25, 50, 75);
            var high = power.MembershipFunctions.AddTriangle("High", 50, 75, 100);

            IFuzzyEngine fuzzyEngine = new FuzzyEngineFactory().Default();


            var rule1 = Rule.If(water.Is(cold).And(sugar.Is(big))).Then(power.Is(high));
            var rule2 = Rule.If(water.Is(cold).And(sugar.Is(small))).Then(power.Is(medium));
            var rule3 = Rule.If(water.Is(hot).And(sugar.Is(big))).Then(power.Is(medium));
            var rule4 = Rule.If(water.Is(hot).And(sugar.Is(small))).Then(power.Is(low));

            
            fuzzyEngine.Rules.Add(rule1, rule2, rule3, rule4);

            var result = fuzzyEngine.Defuzzify(new { water = 90, sugar=30 });
        }
    }
}
