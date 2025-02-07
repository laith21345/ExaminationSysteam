using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSysteam
{
    internal class TrueFalse : Question
    {
        public TrueFalse(string header, bool rightAnswers, float mark)
        {
            this.Header = header;
            this.Body = "1) T\t2) F";

            Answer answer01 = new Answer("T");
            Answer answer02 = new Answer("F");
            this.QuestionChoices = new Dictionary<Guid, Answer>() 
            {
                {answer01.AnswerId,answer01}, {answer02.AnswerId,answer02}
            };

            if (rightAnswers) 
            {
                this.RightAnswers = new HashSet<Guid>([answer01.AnswerId]);
            }
            else
                this.RightAnswers = new HashSet<Guid>([answer02.AnswerId]);
            this.Mark = mark;
        }
    }
}
