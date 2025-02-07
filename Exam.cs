using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSysteam
{
    internal abstract class Exam
    {

        private ushort _minutes;
        private QuestionList _questions;

        public DateTime UntilTime { get; set; }
        public ushort Minutes
        {
            get { return _minutes; }
            set
            {
                if (_minutes == 0) throw new InvalidOperationException("the time of the exam can't be 0 minutes");
                _minutes = value;
            }
        }
        public ushort NumberOfQuestions { get { return _questions.Length; } }
        public QuestionList Questions { get { return _questions; } }

        public Exam(ushort numberOfQuestions, ushort minutes)
        {
            this._minutes = minutes;
            this._questions = new QuestionList(numberOfQuestions);
            this.UntilTime = DateTime.Now.AddMinutes(minutes);
        }

        protected bool AddQuestion(Question question)
        {
            return Questions.Add(question);
        }

        public bool AddMCQ(string header, Dictionary<Guid, Answer> questionChoices, HashSet<Guid> rightAnswers, float mark)
        {
            return AddQuestion(new MCQ(header, questionChoices, rightAnswers, mark));
        }

        public abstract void Show();


    }
}
