using System.Collections.Generic;

namespace ClassAssessment.Model
{
    public class NameShort
    {
        public int Id;
        public string Name, Surname;
    }

    public class QuestionShort
    {
        public int id;
        public string Text;
    }

    public class AnsweredQuestion
    {
        public int Id;
        public string Text;
        public List<AnswerShort> Answers;
        public double median, mode, average;
    }

    public class AnswerShort
    {
        public string User;
        public int? Value;
    }
}