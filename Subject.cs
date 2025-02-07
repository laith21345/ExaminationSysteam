using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExaminationSysteam
{
    internal class Subject
    {
        private Exam _exam;
        private string _subjectName;

        public uint SubjectId { get; set; }
        public string SubjectName
        {
            get { return _subjectName; }
            set
            {
                if (value == null || value.Length == 0) throw new ArgumentNullException("_subjectName can't be null or empty");

                _subjectName = value;
            }
        }

        public Subject(uint subjectId, string subjectName)
        {
            this.SubjectId = subjectId;
            this.SubjectName = subjectName;
        }

        //private void AddMCQ();

        public void CreateExam()
        {
            int examType;
            do
            {
                Console.Write("please Enter The Type Of Exam You Want To Create( 1 for Practical and 2 for Final): ");
                examType = int.Parse(Console.ReadLine());
            } while (examType != 1 && examType != 2);

            ushort min;
            do
            {
                Console.Write("Please Enter The Time Of Exam in Minutes: ");
                min = ushort.Parse(Console.ReadLine());
            } while (min == 0);

            ushort NOQ;
            do
            {
                Console.Write("please Enter The Number of Questions You Wanted To Create : ");
                NOQ = ushort.Parse(Console.ReadLine());
            } while (NOQ == 0);

            string header;
            string body;
            float mark;
            Answer[] questionChoicesMCQ = new Answer[3];
            HashSet<Guid> rightAnswersMCQ = new HashSet<Guid>();

            if (examType == 1)
            {
                PracticalExam exam = new PracticalExam(NOQ,min);
                Console.WriteLine("Choose One Answer Question");

                Console.WriteLine("Please Enter The Header of Question:");
                header = Console.ReadLine();

                Console.Write("Please Enter The Marks of Question:");
                mark = float.Parse(Console.ReadLine());

                Console.WriteLine("The Choices of Question:");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write($"Please Enter The Choice Number {i + 1}:");
                    questionChoicesMCQ[i] = new Answer(Console.ReadLine());
                }
                Console.Write("Please Specify The Nummber of the Right Choice of Question:");
                int index;
                do
                {
                    index = int.Parse(Console.ReadLine());
                } while (index < 1 || index > 3);
                rightAnswersMCQ.Add(questionChoicesMCQ[index-1].AnswerId);

                Dictionary<Guid, Answer> dic = new Dictionary<Guid, Answer>()
                        {
                            {questionChoicesMCQ[0].AnswerId,questionChoicesMCQ[0]},
                            {questionChoicesMCQ[1].AnswerId,questionChoicesMCQ[1]},
                            {questionChoicesMCQ[2].AnswerId,questionChoicesMCQ[2]}
                        };

                exam.AddMCQ(header, dic, rightAnswersMCQ, mark);



                _exam = exam;
            }
            else
            {
                bool rightAnswersTrueFalse;

                FinalExam exam = new FinalExam(NOQ, min);


                for (int m = 0; m < NOQ; m++)
                {
                    Console.Write($"Please Choose The Type Of Question Number({m + 1}) (1 for True OR False | | 2 for MCQ) :");
                    int choice = int.Parse(Console.ReadLine());

                    Console.Clear();

                    if (choice == 1)
                    {
                        Console.WriteLine("True | False Question\nPlease Enter The Header of Question:");
                        header = Console.ReadLine();

                        Console.Write("Please Enter The Mark of Question:");
                        mark = float.Parse(Console.ReadLine());

                        Console.WriteLine("Please Enter The Right Answer of Question (1 for True and 2 for False):");
                        do
                        {
                            choice = int.Parse(Console.ReadLine());
                        } while (choice != 1 && choice != 2);
                        if (choice == 1) rightAnswersTrueFalse = true;
                        else rightAnswersTrueFalse = false;

                        exam.AddTrueFalse(header, rightAnswersTrueFalse, mark);
                    }
                    else
                    {
                        Console.WriteLine("Choose One Answer Question");

                        Console.WriteLine("Please Enter The Header of Question:");
                        header = Console.ReadLine();

                        Console.Write("Please Enter The Marks of Question:");
                        mark = float.Parse(Console.ReadLine());

                        Console.WriteLine("The Choices of Question:");
                        for (int i = 0; i < 3; i++)
                        {
                            Console.Write($"Please Enter The Choice Number {i + 1}:");
                            questionChoicesMCQ[i] = new Answer(Console.ReadLine());
                        }
                        Console.Write("Please Specify The Nummber of the Right Choice of Question:");
                        int index;
                        do
                        {
                            index = int.Parse(Console.ReadLine());
                        } while (index < 1 || index > 3);
                        rightAnswersMCQ.Add(questionChoicesMCQ[index-1].AnswerId);

                        Dictionary<Guid, Answer> dic = new Dictionary<Guid, Answer>()
                        {
                            {questionChoicesMCQ[0].AnswerId,questionChoicesMCQ[0]},
                            {questionChoicesMCQ[1].AnswerId,questionChoicesMCQ[1]},
                            {questionChoicesMCQ[2].AnswerId,questionChoicesMCQ[2]}
                        };

                        exam.AddMCQ(header, dic, rightAnswersMCQ, mark);
                    }


                }

                _exam = exam;
            }

        }

        public void Start() 
        {
            Answer[] answers;
            int indexOfRightAnswer;
            for (int i = 0; i < _exam.Questions.Length; i++)
            {
                answers = _exam.Questions[i].QuestionChoices.Values.ToArray();

                for (int j = 0; j < answers.Length; j++)
                {
                    Console.WriteLine($"{j + 1}) {answers[j].AnswerText} \t"); ;
                }
                Console.WriteLine();
                Console.WriteLine("enter you're choice");
                indexOfRightAnswer = int.Parse( Console.ReadLine() );
                _exam.Questions[i].ChoseAnswers(answers[indexOfRightAnswer-1].AnswerId);
            }
        }

        public void ShowExam()
        {
            _exam.Show();
        }
    }
}
