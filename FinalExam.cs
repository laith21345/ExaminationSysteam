using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSysteam
{
    internal class FinalExam : Exam
    {
        float _totalMark;
        public float Grade 
        {
            get 
            {
                float finalGrade = 0;
                for (int i = 0; i < Questions.Count; i++)
                {
                    finalGrade += Questions[i].Grade;
                }
                return finalGrade;
            } 
        }
        public float TotalMark
        {
            get 
            {
                _totalMark = 0;
                for (int i = 0; i < Questions.Count; i++)
                {
                    _totalMark += Questions[i].Mark;
                }
                return _totalMark;
            }
        }
        public FinalExam(ushort numberOfQuestions, ushort minutes) : base(numberOfQuestions, minutes)
        {

        }

        public override void Show()
        {
            for (int i = 0; i < Questions.Count; i++)
            {
                Console.WriteLine(Questions[i]);
                Console.WriteLine("===============================================================");
            }
            Console.WriteLine($"You're Grade :{Grade} from {TotalMark}");
        }

        public bool AddTrueFalse(string header, bool answer, float mark)
        {
            return AddQuestion(new TrueFalse(header, answer, mark));
        }
    }
}
