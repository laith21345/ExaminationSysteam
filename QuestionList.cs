using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSysteam
{
    internal class QuestionList
    {
        #region attrs
        private Question[] _questions;
        #endregion

        #region props
        public ushort Count { get; set; }
        public ushort Length { get { return (ushort)_questions.Length; } }
        #endregion

        #region Ctors
        public QuestionList(ushort numberOfQuestion)
        {
            if (numberOfQuestion == 0) throw new InvalidOperationException("numberOfQuestion can't be 0");

            _questions = new Question[numberOfQuestion];
            this.Count = 0;
        }
        #endregion


        #region Methods
        public Question this[int i]
        {
            get { return _questions[i]; }
            set { _questions[i] = value; }
        }
        public bool Add(Question question)
        {
            if (Count < _questions?.Length)
            {
                _questions[Count++] = question;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < _questions.Length; i++)
            {
                list.Add(_questions[i].ToString());
            }

            return string.Join("\n=================================================================\n", list);
        }
        #endregion
    }
}
