using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercice_Quizz_API.Model
{
    public class Question
    {
        // Créer le model objet Question et son constructeur
        public int QuestionId { get; set; }
        public string QuestionIntitule { get; set; }
        public string Answer { get; set; }

        public Question()
        {

        }

    }
}
