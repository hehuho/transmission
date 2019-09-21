using Exercice_Quizz_API_Vierge.Model;
using Exercice_Quizz_API_Vierge.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exercice_Quizz_API_Test.RepositoryFake
{
    public class QuestionRepositoryFake : IQuestionRepository
    {
        private readonly List<Question> _questions;

        public QuestionRepositoryFake()
        {
            _questions = new List<Question>()
                {
                    new Question() { QuestionId = 1, QuestionIntitule = "En quelle annee a eu lieu la bataille de Waterloo ?", Answer = "1815" },
                    new Question() { QuestionId = 2, QuestionIntitule = "De quoi serait mort Napoleon ?", Answer = "D'un cancer de l'estomac" },
                    new Question() { QuestionId = 3, QuestionIntitule = "Quelle roi a decide la construction du chateau de Chambord ?", Answer = "Francois 1er" },
                    new Question() { QuestionId = 4, QuestionIntitule = "Qui fut elu president de la IIIeme Republique en 1873 ?", Answer = "Mac Mahon" },
                    new Question() { QuestionId = 5, QuestionIntitule = "Quelle est la mere du petit Cesarion ?", Answer = "Cleopatre" },
                    new Question() { QuestionId = 6, QuestionIntitule = "Quelle reine de France appelle-t-on la Grosse Banquiere ?", Answer = "Marie de Medicis" }
                };
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
    }
}
