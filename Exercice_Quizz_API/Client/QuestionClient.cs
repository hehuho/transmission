using Exercice_Quizz_API.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercice_Quizz_API.Client
{
    public partial class ApiClient
    {
        public async Task<List<Question>> GetAllQuestions()
        {
            throw new NotImplementedException();
        }

        public async Task<Question> GetQuestionById(int questionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Question>> AddQuestion(Question model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Question>> UpdateQuestion(int questionId, Question model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Question>> DeleteQuestion(int questionId, Question model)
        {
            throw new NotImplementedException();
        }
    }
}
