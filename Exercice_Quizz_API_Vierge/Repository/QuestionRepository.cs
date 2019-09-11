using Exercice_Quizz_API_Vierge.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Exercice_Quizz_API_Vierge.Repository
{
    public static class QuestionRepository
    {
        public static List<Question> GetAllQuestions(string path)
        {
            throw new NotImplementedException();
        }

        public static Question GetQuestion(string path, int questionId)
        {
            throw new NotImplementedException();
        }

        public static List<Question> AddOrUpdateQuestion(string path, Question question)
        {
            throw new NotImplementedException();
        }

        public static List<Question> DeleteQuestion(string path, int questionId)
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
