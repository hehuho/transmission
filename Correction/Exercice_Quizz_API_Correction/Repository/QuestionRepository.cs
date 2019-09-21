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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _path;

        public QuestionRepository(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _path = hostingEnvironment.ContentRootPath + "/Json/Questions.json";
        }

        public List<Question> GetAllQuestions()
        {
            List<Question> items = new List<Question>();

            using (StreamReader r = new StreamReader(_path))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Question>>(json);
            }

            return items;
        }

        public Question GetQuestion(int questionId)
        {
            return GetAllQuestions().Where(x => x.QuestionId == questionId).SingleOrDefault();
        }

        public List<Question> AddOrUpdateQuestion(Question question)
        {
            List<Question> questions = GetAllQuestions();

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

            return WriteInJsonAndReturn(_path, questions);
        }

        public List<Question> DeleteQuestion(int questionId)
        {
            List<Question> questions = GetAllQuestions();

            Question questionToRemove = questions.Where(q => q.QuestionId == questionId).SingleOrDefault();

            questions.Remove(questionToRemove);

            return WriteInJsonAndReturn(_path, questions);
        }

        #region Helpers

        private List<Question> WriteInJsonAndReturn(string path, List<Question> questions)
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
