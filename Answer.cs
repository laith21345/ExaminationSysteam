using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExaminationSysteam
{
    internal class Answer : IEquatable<Answer>
    {
        #region Properties
        public Guid AnswerId { get; set; }
        public string AnswerText { get; set; }
        #endregion

        #region Ctors
        public Answer(string answerText)
        {
            AnswerId = Guid.NewGuid();
            AnswerText = answerText;
        }
        #endregion

        #region Methods
        public bool Equals(Answer? other)
        {
            if (other is null) return false;
            return this.AnswerText == other.AnswerText;
        }
        #endregion
    }
}
