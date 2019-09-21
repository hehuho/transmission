using Exercice_Quizz_API_Vierge.Model;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Exercice_Quizz_API_Vierge.Repository
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


        public List<Question> AddOrUpdateQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public List<Question> DeleteQuestion(int questionId)
        {
            throw new NotImplementedException();
        }

        public List<Question> GetAllQuestions()
        {
            throw new NotImplementedException();
        }

        public Question GetQuestion(int questionId)
        {
            throw new NotImplementedException();
        }

        #region Helpers

        private static List<Question> WriteInJsonAndReturn(string path, List<Question> questions)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
