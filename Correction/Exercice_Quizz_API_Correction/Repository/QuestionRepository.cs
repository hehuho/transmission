using Exercice_Quizz_API.Model;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Exercice_Quizz_API.Repository
{
    public static class QuestionRepository
    {
        public static List<Question> GetAllQuestions(string path)
        {
            List<Question> items = new List<Question>();

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Question>>(json);
            }

            return items;
        }

        public static Question GetQuestion(string path, int questionId)
        {
            return GetAllQuestions(path).Where(x => x.QuestionId == questionId).SingleOrDefault();
        }

        public static List<Question> AddOrUpdateQuestion(string path, Question question)
        {
            List<Question> questions = GetAllQuestions(path);

            if (question.QuestionId == 0)
            {
                question.QuestionId = questions.Last().QuestionId + 1;
                questions.Add(question);
            }
            else
            {
                int index = questions.FindIndex(q => q.QuestionId == question.QuestionId);
                questions[index] = question;
            }

            return WriteInJsonAndReturn(path, questions);
        }

        public static List<Question> DeleteQuestion(string path, int questionId)
        {
            List<Question> questions = GetAllQuestions(path);

            Question questionToRemove = questions.Where(q => q.QuestionId == questionId).SingleOrDefault();

            questions.Remove(questionToRemove);

            return WriteInJsonAndReturn(path, questions);
        }

        #region Helpers

        private static List<Question> WriteInJsonAndReturn(string path, List<Question> questions)
        {
            string json = JsonConvert.SerializeObject(questions);

            List<Question> items = new List<Question>();

            //export data to json file. 
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.Write(json);
                items = JsonConvert.DeserializeObject<List<Question>>(json);
            };

            return items;
        }

        #endregion

    }


}
