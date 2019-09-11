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
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "api/question/getAll"));
            return await GetAsync<List<Question>>(requestUrl);
        }

        public async Task<Question> GetQuestionById(int questionId)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "api/question/get/" + questionId));
            return await GetAsync<Question>(requestUrl);
        }

        public async Task<List<Question>> AddQuestion(Question model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "api/question/addQuestion"));
            var answer = await PostAsync(requestUrl, model);
            string answerString = JsonConvert.SerializeObject(answer);
            return JsonConvert.DeserializeObject<List<Question>>(answerString);
        }

        public async Task<List<Question>> UpdateQuestion(int questionId, Question model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "api/question/updateQuestion/" + questionId));
            var answer = await PutAsync(requestUrl, model);
            string answerString = JsonConvert.SerializeObject(answer);
            return JsonConvert.DeserializeObject<List<Question>>(answerString);
        }

        public async Task<List<Question>> DeleteQuestion(int questionId, Question model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "api/question/deleteQuestion/" + questionId));
            var answer = await DeleteAsync(requestUrl, model);
            string answerString = JsonConvert.SerializeObject(answer);
            return JsonConvert.DeserializeObject<List<Question>>(answerString);
        }
    }
}
