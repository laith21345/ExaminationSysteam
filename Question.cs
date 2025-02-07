using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSysteam
{
    internal abstract class Question
    {
        #region attributes
        private string _header;
        private string _body;
        private float _mark;
        private Dictionary<Guid,Answer> _questionChoices;
        private HashSet<Guid> _rightAnswers;
        private List<Guid> _chosenAnswers;
        #endregion

        #region Properties
        public Guid QuestionId { get; set; }
        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                if (value is null) throw new ArgumentNullException(nameof(value), "_header not accept null");

                _header = value;
            }
        }
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                if (value is null) throw new ArgumentNullException(nameof(value), "_body not accept null");

                _body = value;
            }
        }
        public float Mark
        {
            get
            {
                return _mark;
            }
            set
            {
                if (value < 0) value = 0;
                _mark = value;
            }
        }
        public Dictionary<Guid, Answer> QuestionChoices
        {
            get
            {
                if (_questionChoices is null) throw new InvalidOperationException("_questionChoices can't be null");
                return _questionChoices;
            }
            set
            {
                if (value is null) throw new InvalidOperationException("_questionChoices not accept null");
                if (value.Count == 0) throw new InvalidOperationException("_questionChoices can't be empty");

                /*source[i].AnswerText == source[j].AnswerText*/
                //_questionChoices = Helper.RemoveDublicationFromArray<Answer>(value, (iValue, jValue) => iValue.AnswerText == jValue.AnswerText);
                _questionChoices = value;
            }
        }
        public HashSet<Guid> RightAnswers
        {
            set
            {
                if (value is null || value.Count == 0) throw new ArgumentNullException("_rightAnswers not accept null or be empty");
                if (value.Count > QuestionChoices.Count) throw new InvalidOperationException("_rightAnswers numbers must be less than Question Choices number");
                if (value.Count == QuestionChoices.Count) throw new InvalidOperationException("_rightAnswers must not be all choices");

                foreach (Guid rightAnswerId in value)
                {
                    if (IsValidQuestionChoice(rightAnswerId) == false) throw new InvalidOperationException("there is a rightAnswer is not in the QuestionChoices");
                }
                //_rightAnswers = Helper.RemoveDublicationFromArray<Guid>(value, (iValue, jValue) => iValue == jValue);

                _rightAnswers = value;
            }
            get
            {
                if (_rightAnswers is null) throw new InvalidOperationException("_rightAnswer can't be null");

                return _rightAnswers;
            }
        }
        public List<Guid> ChosenAnswers
        {
            get
            {
                if (_chosenAnswers is null) throw new ArgumentNullException("_chosenAnswers must not be null");
                return _chosenAnswers;
            }
        }
        public float Grade
        {
            get
            {
                if (Mark == 0) return 0;
                if (ChosenAnswers.Count == 0) return 0;


                float grade = Mark;
                float markForEveryQuestion = grade / RightAnswers.Count;
                for (int i = 0;
                    grade > 0 && i < ChosenAnswers.Count && i < ChosenAnswers.Count;
                    i++)
                {
                    if (!RightAnswers.Contains(ChosenAnswers[i]))
                    {
                        grade -= markForEveryQuestion;
                    }
                }

                if (grade == Mark && ChosenAnswers.Count < RightAnswers.Count)
                {
                    int numberOfRightAnswersNotChosen = RightAnswers.Count - ChosenAnswers.Count;
                    while (numberOfRightAnswersNotChosen > 0)
                    {
                        grade -= markForEveryQuestion;
                    }
                }
                return grade;
            }
        }
        #endregion

        #region Constractors
        public Question(string header, string body, Dictionary<Guid,Answer> questionChoices, HashSet<Guid> rightAnswers, float mark)
        {
            this.Header = header;
            this.Body = body;
            this.QuestionChoices = questionChoices;
            this.RightAnswers = rightAnswers;
            this.Mark = mark;
        }
        protected Question()
        {
            
        }
        #endregion

        #region Methods
        private bool IsValidQuestionChoice(Guid answerld)
        {
            return QuestionChoices.ContainsKey(answerld);
        }
        public bool ChoseAnswers(Guid answerld)
        {
            if (IsValidQuestionChoice(answerld) == false) return false;

            if (_chosenAnswers is null) _chosenAnswers = new List<Guid>();
            _chosenAnswers.Add(answerld);
            return true;
        }
        public override string ToString()
        {
            List<string> chosenAnswersTextList = new List<string>();
            Guid key;
            for (int i = 0; i < ChosenAnswers.Count; i++)
            {
                key = ChosenAnswers[i];
                chosenAnswersTextList.Add($"- {QuestionChoices[key].AnswerText}");
            }

            List<string> rightAnswersTextList = new List<string>();
            for (int i = 0; i < RightAnswers.Count; i++)
            {
                key = ChosenAnswers[i];
                rightAnswersTextList.Add($"- {QuestionChoices[key].AnswerText}");
            }

            string chosenAnswersText = string.Join('\t', chosenAnswersTextList);
            string rightAnswersText = string.Join('\t', rightAnswersTextList);

            return $"{Header} ({Mark})\n{Body}\nYou're Answer/s :\n{chosenAnswersText}\nThe right Answer/s :\n{rightAnswersText}";
        }

        #endregion
    }
}
