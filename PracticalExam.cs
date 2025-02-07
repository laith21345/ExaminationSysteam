using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSysteam
{
    internal class PracticalExam : Exam
    {
        public PracticalExam(ushort numberOfQuestions, ushort minutes) : base(numberOfQuestions, minutes)
        {
        }

        public override void Show()
        {
            for (int i = 0; i < Questions.Count; i++)
            {
                Console.WriteLine(Questions[i]);
                Console.WriteLine("===============================================================");
            }
        }
    }
}
