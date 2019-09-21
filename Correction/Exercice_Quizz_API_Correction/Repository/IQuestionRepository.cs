using Exercice_Quizz_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercice_Quizz_API.Repository
{
    public interface IQuestionRepository
    {
        List<Question> GetAllQuestions();

        Question GetQuestion(int questionId);

        List<Question> AddOrUpdateQuestion(Question question);

        List<Question> DeleteQuestion(int questionId);
    }
}
