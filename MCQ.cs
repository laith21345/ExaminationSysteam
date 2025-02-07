using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSysteam
{
    internal class MCQ : Question
    {
        private void FormatBody()
        {
            string[] body = new string[QuestionChoices.Count];
            int t = 0;
            foreach (var item in QuestionChoices)
            {
                body[t] = $"{t + 1}) {item.Value.AnswerText}";
            }

            List<string> bodyInFormating = new List<string>();
            for (int i = 0; i < body.Length - 1; i += 2)
            {
                bodyInFormating.Add($"{body[i]}\t{body[i + 1]}");
            }
            if (bodyInFormating.Count < body.Length)
            {
                bodyInFormating.Add(body[body.Length - 1]);
            }
            
            Body = string.Join('\n', bodyInFormating);
        }
        public MCQ(string header, Dictionary<Guid, Answer> questionChoices, HashSet<Guid> rightAnswers, float mark)
        {
            this.Header = header;
            this.QuestionChoices = questionChoices;
            this.RightAnswers = rightAnswers;
            this.Mark = mark;
            this.FormatBody();
        }
    }
}
