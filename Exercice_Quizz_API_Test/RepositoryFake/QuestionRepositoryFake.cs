using Exercice_Quizz_API.Model;
using Exercice_Quizz_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

            return questions;
        }

        public List<Question> DeleteQuestion(int questionId)
        {
            List<Question> questions = GetAllQuestions();

            Question questionToRemove = questions.Where(q => q.QuestionId == questionId).SingleOrDefault();

            questions.Remove(questionToRemove);

            return questions;
        }

        public List<Question> GetAllQuestions()
        {
            return _questions;
        }

        public Question GetQuestion(int questionId)
        {
            return GetAllQuestions().Where(x => x.QuestionId == questionId).SingleOrDefault();
        }
    }
}
